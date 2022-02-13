using System;

namespace WindowsFormsAppTest.errors
{
    internal class InvalidDirectoryException : Exception
    {
        public InvalidDirectoryException(string dir_path)
            : base($"The directory path '{dir_path}' is invalid or doesn't exist!") { }
    }
}
