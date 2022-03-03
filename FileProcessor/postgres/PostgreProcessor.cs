using DatabaseProcessor;
using SystemLocalStore.models;

namespace FileProcessor.postgres
{
    public class PostgreProcessor : AbsDBMSProcessor
    {
        public PostgreProcessor(SourceFile[] files, DBSchema source, DBSchema vocab) : base(files, source, vocab) { }

    }
}
