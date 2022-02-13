using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SystemLocalStore.models;

namespace FileProcessor
{
    public static class PlaceHolder
    {
        public const string Table = "{tb}";
        public const string Schema = "{sc}";
        public const string FilePath = "{fp}";
        public const string Delimiter = "{dl}";

        public static string ReplaceAllHolders(this string content, DBSchema schema, SourceFile source, bool IsCSV = false, bool clear = true)
        {
            content = IsCSV ? content.ReplaceCSVDelimiter() : content.ReplaceTSVDelimiter();
            content = content
                .ReplaceTable(source)
                .ReplaceSchema(schema)
                .ReplaceFilePath(source);
            return clear ? content.ClearHolders() : content;
        }

        public static string ReplaceTable(this string content, SourceFile source)
        {
            return content.Replace(Table, source.TableName);
        }
        public static string ReplaceSchema(this string content, DBSchema schema)
        {
            return content.Replace(Schema, schema.SchemaName);
        }

        public static string ReplaceFilePath(this string content, SourceFile source)
        {
            return content.Replace(FilePath, source.FilePath);
        }
        public static string ReplaceDelimiter(this string content, string delimiter)
        {
            var del = string.IsNullOrEmpty(delimiter) ? string.Empty : $"E'{delimiter}'";
            return content.Replace(Delimiter, $"CSV DELIMITER {del}");
        }
        public static string ReplaceCSVDelimiter(this string content)
        {
            return content.ReplaceDelimiter(string.Empty);
        }
        public static string ReplaceTSVDelimiter(this string content)
        {
            return content.ReplaceDelimiter("\\t");
        }
        public static string ClearHolders(this string content)
        {
            return Regex.Replace(content, @"\{(\w+)\}", "");
        }
    }
}
