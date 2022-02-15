using DatabaseProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        protected string lookupTypeTable = "lookuptype";
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

        protected T GetScriptContent<T>(string file_name)
        {
            var filePath = File.Exists(file_name) || Directory.Exists(file_name) ? file_name : Path.Combine(Environment.CurrentDirectory, "filescripts", DBMSName, file_name);
            if (File.Exists(filePath))
            {
                return (T)Convert.ChangeType(File.ReadAllText(filePath), typeof(T));
            }
            if (Directory.Exists(filePath))
            {
                var v = Directory.GetFiles(filePath, "*.sql", SearchOption.AllDirectories).OrderBy(f => Path.GetFileNameWithoutExtension(f))
                    .Select(p => GetScriptContent<string>(p)).ToArray();
                return (T)Convert.ChangeType(v, typeof(T));
            }
            return default(T);
        }

        private Queue createSourceSchema()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)sourceSchema.Id,
                FilePath = "[Source Schema Creator]",
                FileContent = (GetScriptContent<string>("create-schema.sql")).ReplaceSchema(sourceSchema).ClearHolders()
            };
        }

        private Queue dropSourceTables()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)sourceSchema.Id,
                FilePath = "[Source Schema Clean Tables]",
                FileContent = (GetScriptContent<string>("drop-tables.sql")).ReplaceSchema(sourceSchema).ClearHolders()
            };
        }

        private Queue createSourceTables()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)sourceSchema.Id,
                FilePath = "[Source Tables Creator]",
                FileContent = (GetScriptContent<string>("create-tables.sql")).ReplaceSchema(sourceSchema).ClearHolders()
            };
        }

        private Queue createSourceIndexes()
        {
            Queue queue = new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)sourceSchema.Id,
                FilePath = "[Source Indexes Creator]",
                FileContent = (GetScriptContent<string>("create-indexes.sql")).ReplaceSchema(sourceSchema).ClearHolders()
            };
            return queue;
        }

        private Queue createVocabularySchema()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)vocabularySchema.Id,
                FilePath = "[Vocabulary Schema Creator]",
                FileContent = (GetScriptContent<string>("create-schema.sql")).ReplaceSchema(vocabularySchema).ClearHolders()
            };
        }

        private Queue dropVocabularyTables()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)vocabularySchema.Id,
                FilePath = "[Vocabulary Schema Clean Tables]",
                FileContent = (GetScriptContent<string>("drop-vocabulary-tables.sql")).ReplaceSchema(vocabularySchema).ClearHolders()
            };
        }

        private Queue createVocabularyTables()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)vocabularySchema.Id,
                FilePath = "[Vocabulary Tables Creator]",
                FileContent = (GetScriptContent<string>("create-vocabulary-tables.sql")).ReplaceSchema(vocabularySchema).ClearHolders()
            };
        }

        private Queue createVocabularyIndexes()
        {
            return new Queue
            {
                TaskIndex = Index++,
                ParallelIndex = 0,
                Ordinal = 0,
                DBSchemaId = (long)vocabularySchema.Id,
                FilePath = "[Vocabulary Indexes Creator]",
                FileContent = (GetScriptContent<string>("create-vocabulary-indexes.sql")).ReplaceSchema(vocabularySchema).ClearHolders()
            };
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
                    var isLookup = sourceFile.TableName.Equals(lookupTypeTable);
                    if (isLookup) queues.Add(modifyLookupTable("add-table-column.sql", sourceFile, tIndex, pIndex, ord++, "[Lookup Append Column]"));

                    var added = directoryCopy(sourceFile, tIndex, pIndex, isLookup, ord);
                    ord += added.Length;
                    queues.AddRange(added);

                    if (isLookup)
                    {
                        queues.Add(mergeLookupTables(tIndex, pIndex, ord++));
                        queues.Add(modifyLookupTable("drop-table-column.sql", sourceFile, tIndex, pIndex, ord++, "[Lookup Clean Appended Column]"));
                    }
                }
            }
            return queues.ToArray();
        }

        private Queue[] directoryCopy(SourceFile sourceFile, int tIndex, int pIndex, bool isLookup, int ordStart = 0)
        {
            var queues = new List<Queue>();
            var files = Directory.GetFiles(sourceFile.FilePath, "*.*", SearchOption.AllDirectories)
                .Where(fn => (new List<string> { "txt", "csv" }).Contains(Path.GetExtension(fn).TrimStart('.').ToLowerInvariant()));
            var lookuptype_file = string.Empty;
            if (isLookup)
            {
                ordStart++;
                var lSourceFile = (SourceFile)sourceFile.ShallowCopy();
                lSourceFile.FilePath = lookuptype_file = Path.Combine(sourceFile.FilePath, "lookuptype.tsv");
                queues.Add(createCopy(lSourceFile, tIndex, pIndex, ordStart));
            }
            foreach (var file in files)
            {
                ordStart++;
                var nSourceFile = (SourceFile)sourceFile.ShallowCopy();
                nSourceFile.FilePath = file;
                if (isLookup)
                {
                    nSourceFile.TableName = "lookup";
                    modifyLookupFile(nSourceFile, lookuptype_file);
                }
                queues.Add(createCopy(nSourceFile, tIndex, pIndex, ordStart));
            }
            return queues.ToArray();
        }

        private void modifyLookupFile(SourceFile sourceFile, string type_path)
        {
            var fn = Path.GetFileNameWithoutExtension(sourceFile.FilePath);
            var desc = string.Empty;
            var txt = File.ReadLines(sourceFile.FilePath)
                .Select((line, index) =>
                {
                    if (2 <= (uint)line.Count(cr => cr == '\t')) return line;
                    if (index == 0)
                    {
                        desc = line.Split('\t').Last();
                        return $"code\ttext\ttemp_file_name";
                    }
                    return $"{line}\t{fn}";
                })
                .ToList();
            File.WriteAllLines(sourceFile.FilePath, txt);

            if (string.IsNullOrEmpty(desc)) return;

            if (!File.Exists(type_path))
            {
                using (StreamWriter sw = File.CreateText(type_path))
                {
                    sw.WriteLine("name\tdescription");
                }
            }
            using (StreamWriter sw = File.AppendText(type_path))
            {
                sw.WriteLine($"{fn}\t{desc}");
            }
        }

        private Queue mergeLookupTables(int tIndex, int pIndex, int ordinal)
        {
            return new Queue
            {
                TaskIndex = tIndex,
                ParallelIndex = pIndex,
                Ordinal = ordinal,
                FilePath = "[Merge Lookup Tables]",
                DBSchemaId = (long)sourceSchema.Id,
                FileContent = (GetScriptContent<string>("lookup-merge.sql")).ReplaceSchema(sourceSchema)
                .ClearHolders()
            };
        }

        private Queue modifyLookupTable(string file_name, SourceFile sourceFile, int tIndex, int pIndex, int ordinal, string name)
        {
            Queue queue = new Queue
            {
                TaskIndex = tIndex,
                ParallelIndex = pIndex,
                Ordinal = ordinal,
                FilePath = name,
                DBSchemaId = (long)sourceSchema.Id,
                FileContent = (GetScriptContent<string>(file_name)).ReplaceSchema(sourceSchema)
                .Replace("{tb}", "lookup")
                .Replace("{clm}", "temp_file_name")
                .Replace("{dtype}", "VARCHAR(240) NOT NULL")
                .ClearHolders()
            };
            return queue;
        }

        private Queue[] createUpdates(string file_name, DBSchema schema)
        {
            return (GetScriptContent<string[]>(file_name) ?? new string[] { }).Select(c =>
              {
                  Queue queue = new Queue
                  {
                      ParallelIndex = Index++,
                      FilePath = $"[{file_name}]",
                      DBSchemaId = (long)schema.Id,
                      FileContent = c.ReplaceSchema(schema).ClearHolders()
                  };
                  return queue;
              }).ToArray();
        }

        private Queue[] createPostSourceUpdates() { return createUpdates("postcopy-updates", sourceSchema); }
        private Queue[] createPreSourceUpdates() { return createUpdates("precopy-updates", sourceSchema); }
        private Queue[] createPostVocabularyUpdates() { return createUpdates("postcopy-vocabulary-updates", vocabularySchema); }
        private Queue[] createPreVocabularyUpdates() { return createUpdates("precopy-vocabulary-updates", vocabularySchema); }

        private Queue createCopy(SourceFile sourceFile, int tIndex, int pIndex, int ord = 0)
        {
            var isVocab = vocabularyTables.Contains(sourceFile.TableName);
            var schema = isVocab ? vocabularySchema : sourceSchema;
            return new Queue
            {
                TaskIndex = tIndex,
                ParallelIndex = pIndex,
                Ordinal = ord,
                DBSchemaId = (long)schema.Id,
                FilePath = sourceFile.FilePath,
                FileContent = GetScriptContent<string>("copy-file.sql").Replace("{cls}", fileColumns(sourceFile)).ReplaceAllHolders(schema, sourceFile)
            };
        }

        private string fileColumns(SourceFile source, bool isCSV = false)
        {
            if (!File.Exists(source.FilePath)) return null;
            using (StreamReader reader = new StreamReader(source.FilePath))
            {
                string line = reader.ReadLine();
                var cols = isCSV ? (new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))")).Split(line) : line.Split('\t');
                return cols.Length > 0 ? "(" + string.Join(", ", cols.Select(c=>$"\"{c}\"")) + ")" : string.Empty;
            }
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
                dropSourceTables(),
                dropVocabularyTables(),
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
