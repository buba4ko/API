using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCalculatorAPI.Common.Exceptions
{
    public class MarsCalulatorException : Exception
    {
        public MarsCalulatorException()
        {
        }

        public MarsCalulatorException(string message)
            : base(message)
        {
        }

        public MarsCalulatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
