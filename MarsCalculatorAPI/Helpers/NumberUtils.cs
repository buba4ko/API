using MarsCalculatorAPI.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCalculatorAPI.Helpers
{
    class NumberUtils
    {
        public static int ParseNumber(string number)
        {
            try
            {
                return Convert.ToInt32(number, 8);
            }
            catch
            {
                throw new InvalidNumberException("You provided a value which is not a valid Marsian number!");
            }

        }
    }
}
