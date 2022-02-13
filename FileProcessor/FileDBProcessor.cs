using DatabaseProcessor;
using FileProcessor.postgres;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
