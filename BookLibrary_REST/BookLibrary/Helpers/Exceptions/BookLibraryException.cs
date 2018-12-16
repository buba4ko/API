using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Helpers.Exceptions
{
    class BookLibraryException : Exception
    {
        public BookLibraryException(string message)
            : base(message)
        {
        }
    }
}
