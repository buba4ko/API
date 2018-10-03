using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCalculatorAPI.Common.Exceptions
{
    public class BaseMarsCalulatorException : Exception
    {
        public BaseMarsCalulatorException()
        {
        }

        public BaseMarsCalulatorException(string message)
            : base(message)
        {
        }

        public BaseMarsCalulatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
