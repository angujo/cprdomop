using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest.errors
{
    internal class GeneralException : Exception
    {
        public GeneralException(string msg) : base(msg) { }
    }
}
