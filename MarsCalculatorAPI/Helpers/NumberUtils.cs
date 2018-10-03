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
            catch (Exception ex)
            {
                throw new InvalidNumberException();
            }

        }
    }
}
