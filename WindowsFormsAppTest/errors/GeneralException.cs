using System;

namespace WindowsFormsAppTest.errors
{
    internal class GeneralException : Exception
    {
        public GeneralException(string msg) : base(msg) { }
    }
}
