using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Util
{
    public static class StringExtension
    {
        private const UInt64 gbBytes = 1073741824;

        public static string Truncate(this string value, int maxLength=200, string truncationSuffix = "…")
        {
            return String.IsNullOrEmpty(value) || value.Length <= maxLength
                ? value
                : value.Substring(0, maxLength) + truncationSuffix;
        }

        public static string ToCamelCase(this string str)
        {
            return str.ToPascalCase().FirstCharToLower();
        }

        public static string ToSnakeCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str.FirstCharToLower(), "(.*)([A-Z])", "$1_$2").ToLower(), "[^0-9a-zA-Z]+", "_");
        }

        public static string FirstCharWord(this string str)
        {
            return Regex.Replace(str, "^[^0-9a-zA-Z]+", "");
        }

        public static string ToKebabCase(this string str)
        {
            return Regex.Replace(str.ToSnakeCase(), "[^0-9a-zA-Z]+", "-");
        }

        public static string ToPascalCase(this string str)
        {
            return Regex.Replace(Regex.Replace(str, "[^0-9a-zA-Z]+", "_"), @"(^|_)([0-9]+)?([a-zA-Z])",
                                m => m.Groups[2].Value + m.Groups[3].Value.ToUpper());
        }

        public static string FirstCharToLower(this string str)
        {
            return string.IsNullOrEmpty(str) || char.IsLower(str[0]) ? str : char.ToLower(str[0]) + str.Substring(1);
        }

        public static string FirstCharToUpper(this string str)
        {
            return string.IsNullOrEmpty(str) || char.IsUpper(str[0]) ? str : char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string ToFileMD5Hash(this string str)
        {
            if (!Directory.Exists(str) && !File.Exists(str)) { throw new Exception($"'{str}' cannot be hashed. Only Files and directories can be hashed!"); }
            return Directory.Exists(str) ? dirHash(str) : fileHash(str);
        }

        private static string fileHash(string path)
        {
            using (var md5 = MD5.Create())
            {
                if (exceedsMD5((ulong)new FileInfo(path).Length))
                {
                    return hasckedHash(md5, path, 0, 0, null);
                }
                using (var stream = File.OpenRead(path))
                {
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        private static string hasckedHash(MD5 md5, string cnt, int i, int count, byte[] contentBytes)
        {
            if (0 == count) md5.TransformFinalBlock(string.IsNullOrEmpty(cnt) ? new byte[0] : Encoding.ASCII.GetBytes(cnt), 0, 0);
            else indexTransform(i, count, contentBytes, md5);
            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
        }

        private static string dirHash(string path)
        {
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                             .OrderBy(p => p).ToList();

            using (MD5 md5 = MD5.Create())
            {
                if (files.Count <= 0)
                {
                    md5.TransformFinalBlock(new byte[0], 0, 0);
                    return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
                }
                for (int i = 0; i < files.Count; i++)
                {
                    string file = files[i];
                    if (exceedsMD5((ulong)new FileInfo(file).Length))
                    {
                        hasckedHash(md5, file, i, files.Count, Encoding.ASCII.GetBytes(file));
                        continue;
                    }

                    // hash path
                    string relativePath = file.Substring(path.Length + 1);
                    byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                    md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                    // hash contents
                    byte[] contentBytes = File.ReadAllBytes(file);
                    indexTransform(i, files.Count, contentBytes, md5);
                }

                return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
            }
        }

        private static void indexTransform(int i, int count, byte[] contentBytes, MD5 md5)
        {
            if (i == count - 1)
                md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
            else
                md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
        }

        private static bool exceedsMD5(ulong bytes)
        {
            return bytes > (2 * gbBytes);
        }
    }
}
