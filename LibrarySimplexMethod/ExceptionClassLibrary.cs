using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySimplexMethod
{
    public class ExceptionClassLibrary : Exception
    {
        public ExceptionClassLibrary(string message) : base(message)
        { }
    }
}
