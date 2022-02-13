using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLocalStore.models;

namespace FileProcessor
{
    public abstract class AbsDBMSProcessor
    {
        protected SourceFile[] sourceFiles;
        protected DBSchema sourceSchema;
        protected DBSchema vocabularySchema;
        protected int Index = 0;

        protected string DBMSName { get { return DBMSIdentifier.GetName(DBMSType.POSTGRESQL); } }

        protected string lookupTypeTable = "lookup_types";
        protected string[] vocabularyTables = { "concept", "concept_ancestor", "concept_class", "concept_relationship", "concept_synonym", "domain", "drug_strength", "relationship", "vocabulary", };

        public AbsDBMSProcessor(SourceFile[] files, DBSchema source, DBSchema vocab)
        {
            sourceFiles = files;
            sourceSchema = source;
            vocabularySchema = vocab;
        }

        public List<Queue> GetQueue()
        {
            List<Queue> queue = BeforeCopyQueue();
            queue.AddRange(BeforeCopyUpdatesQueue());
            queue.AddRange(CopyQueue());
            queue.AddRange(AfterCopyQueue());
            queue.AddRange(AfterCopyUpdatesQueue());
            return queue;
        }

        protected dynamic GetScriptContent(string file_name)
        {
            var filePath = File.Exists(file_name) || Directory.Exists(file_name) ? file_name : Path.Combine(Environment.CurrentDirectory, "filescripts", DBMSName, file_name);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            if (Directory.Exists(filePath))
            {
                return Directory.GetFiles(filePath, "*.sql", SearchOption.AllDirectories).OrderBy(f => Path.GetFileNameWithoutExtension(f))
                    .Select(p => (string)GetScriptContent(p)).ToArray();
            }
            return string.Empty;
        }

        private Queue createSourceSchema()
        {
            Queue queue = new Queue();
            queue.TaskIndex = Index++;
            queue.FilePath = "[Source Schema Creator]";
            queue.FileContent = ((string)GetScriptContent("create-schema.sql")).ReplaceSchema(sourceSchema).ClearHolders();
            return queue;
        }

        private Queue createSourceTables()
        {
            Queue queue = new Queue();
            queue.TaskIndex = Index++;
            queue.FilePath = "[Source Tables Creator]";
            queue.FileContent = ((string)GetScriptContent("create-tables.sql")).ReplaceSchema(sourceSchema).ClearHolders();
            return queue;
        }

        private Queue createSourceIndexes()
        {
            Queue queue = new Queue();
            queue.TaskIndex = Index++;
            queue.FilePath = "[Source Indexes Creator]";
            queue.FileContent = ((string)GetScriptContent("create-indexes.sql")).ReplaceSchema(sourceSchema).ClearHolders();
            return queue;
        }

        private Queue createVocabularySchema()
        {
            Queue queue = new Queue();
            queue.TaskIndex = Index++;
            queue.FilePath = "[Vocabulary Schema Creator]";
            queue.FileContent = ((string)GetScriptContent("create-schema.sql")).ReplaceSchema(vocabularySchema).ClearHolders();
            return queue;
        }

        private Queue createVocabularyTables()
        {
            Queue queue = new Queue();
            queue.TaskIndex = Index++;
            queue.FilePath = "[Vocabulary Tables Creator]";
            queue.FileContent = ((string)GetScriptContent("create-vocabulary-tables.sql")).ReplaceSchema(vocabularySchema).ClearHolders();
            return queue;
        }

        private Queue createVocabularyIndexes()
        {
            Queue queue = new Queue();
            queue.TaskIndex = Index++;
            queue.FilePath = "[Vocabulary Indexes Creator]";
            queue.FileContent = ((string)GetScriptContent("create-vocabulary-indexes.sql")).ReplaceSchema(vocabularySchema).ClearHolders();
            return queue;
        }

        private Queue[] createCopies()
        {
            var tIndex = Index++;
            var pIndex = 0;
            List<Queue> queues = new List<Queue>();
            foreach (var sourceFile in sourceFiles)
            {
                pIndex++;
                if (sourceFile.IsFile) queues.Add(createCopy(sourceFile, tIndex, pIndex));
                else
                {
                    var ord = 0;
                    var files = Directory.GetFiles(sourceFile.FilePath, "*.*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        ord++;
                        var nSourceFile = (SourceFile)sourceFile.ShallowCopy();
                        nSourceFile.FilePath = file;
                        queues.Add(createCopy(nSourceFile, tIndex,pIndex, ord));
                    }

                }
            }
            return queues.ToArray();
        }

        private Queue[] createUpdates(string file_name, DBSchema schema)
        {
            return ((string[])GetScriptContent(file_name)).Select(c =>
            {
                Queue queue = new Queue();
                queue.ParalellIndex = Index++;
                queue.FilePath = $"[{file_name}]";
                queue.FileContent = c.ReplaceSchema(schema).ClearHolders();
                return queue;
            }).ToArray();
        }

        private Queue[] createPostSourceUpdates() { return createUpdates("postcopy-updates", sourceSchema); }
        private Queue[] createPreSourceUpdates() { return createUpdates("precopy-updates", sourceSchema); }
        private Queue[] createPostVocabularyUpdates() { return createUpdates("postcopy-vocabulary-updates", vocabularySchema); }
        private Queue[] createPreVocabularyUpdates() { return createUpdates("precopy-vocabulary-updates", vocabularySchema); }

        private Queue createCopy(SourceFile sourceFile, int tIndex, int pIndex, int ord = 0)
        {
            Queue q = new Queue();
            q.TaskIndex = tIndex;
            q.ParalellIndex = pIndex;
            q.Ordinal = ord;
            q.FilePath = sourceFile.FilePath;
            var isVocab = vocabularyTables.Contains(sourceFile.TableName);
            var schema = isVocab ? vocabularySchema : sourceSchema;
            q.FileContent = ((string)GetScriptContent("copy-file.sql")).ReplaceAllHolders(schema, sourceFile, isVocab);
            return q;
        }

        protected List<Queue> AfterCopyQueue()
        {
            return new List<Queue>
            {
                createSourceIndexes(),
                createVocabularyIndexes(),
            };
        }

        protected List<Queue> AfterCopyUpdatesQueue()
        {
            var queues = new List<Queue>();
            queues.AddRange(createPostSourceUpdates());
            queues.AddRange(createPostVocabularyUpdates());
            return queues;
        }

        protected List<Queue> BeforeCopyQueue()
        {
            return new List<Queue>
            {
                createSourceSchema(),
                createVocabularySchema(),
                createSourceTables(),
                createVocabularyTables(),
            };
        }

        protected List<Queue> BeforeCopyUpdatesQueue()
        {
            var queues = new List<Queue>();
            queues.AddRange(createPreSourceUpdates());
            queues.AddRange(createPreVocabularyUpdates());
            return queues;
        }

        protected List<Queue> CopyQueue()
        {
            return createCopies().ToList();
        }
    }
}
