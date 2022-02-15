using DatabaseProcessor;
using FileProcessor.postgres;
using SystemLocalStore.models;

namespace FileProcessor
{
    public class FileDBProcessor
    {
        public static AbsDBMSProcessor GetProcessor(SourceFile[] files, DBSchema src, DBSchema voc, DBMSType type = DBMSType.POSTGRESQL)
        {
            switch (type)
            {
                case DBMSType.POSTGRESQL: return new PostgreProcessor(files, src, voc);
                default: return null;
            }
        }
    }
}
