using System;

namespace WindowsFormsAppTest.errors
{
    internal class InvalidFileException : Exception
    {
        public InvalidFileException(string file_path) : base($"The file '{file_path}' is invalid or doesn't exist!") { }
    }
}
