using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCalculatorAPI.Common.Exceptions
{
    class InvalidNumberException : MarsCalulatorException
    {
        public InvalidNumberException(string message)
            : base(message)
        {
        }
    }
}
