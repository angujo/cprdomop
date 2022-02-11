using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest.errors
{
    internal class InvalidDirectoryException : Exception
    {
        public InvalidDirectoryException(string dir_path)
            : base($"The directory path '{dir_path}' is invalid or doesn't exist!") { }
    }
}
