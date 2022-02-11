using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest.errors
{
    internal class InvalidFileException : Exception
    {
        public InvalidFileException(string file_path) : base($"The file '{file_path}' is invalid or doesn't exist!") { }
    }
}
