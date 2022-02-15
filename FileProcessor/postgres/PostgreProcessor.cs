using DatabaseProcessor;
using SystemLocalStore.models;

namespace FileProcessor.postgres
{
    public class PostgreProcessor : AbsDBMSProcessor
    {
        protected new string DBMSName { get { return DBMSIdentifier.GetName(DBMSType.POSTGRESQL); } }

        public PostgreProcessor(SourceFile[] files, DBSchema source, DBSchema vocab) : base(files, source, vocab) { }

    }
}
