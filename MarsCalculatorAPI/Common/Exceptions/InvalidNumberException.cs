using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCalculatorAPI.Common.Exceptions
{
    class InvalidNumberException : BaseMarsCalulatorException
    {
        public InvalidNumberException()
            : base("You provided a value which is not a valid Marsian number!")
        {
        }
    }
}
