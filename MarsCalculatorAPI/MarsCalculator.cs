using MarsCalculatorAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsCalculatorAPI
{
    /// <summary>
    /// A class which performs the basic arithmetic operations in Mars
    /// </summary>
    public class MarsCalculator
    {
        /// <summary>
        /// Finds the sum of two numbers
        /// </summary>
        /// <param name="number1">The value of the 1st number</param>
        /// <param name="number2">The value of the 2nd number</param>
        /// <returns>The sum of the two numbers</returns>
        public string Add(string number1, string number2)
        {
            int decNumber1 = NumberUtils.ParseNumber(number1);
            int decNumber2 = NumberUtils.ParseNumber(number2);
            int decResult = decNumber1 + decNumber2;

            return Convert.ToString(decResult, 8);
        }

        /// <summary>
        /// Substracts two numbers
        /// </summary>
        /// <param name="number1">The value of the 1st number</param>
        /// <param name="number2">The value of the 2nd number</param>
        /// <returns>The substraction of the two numbers</returns>
        /// <exception cref="InvalidNumberException">Ocures when an invalid octal number is provided</exception>
        public string Subtract(string number1, string number2)
        {
            int decNumber1 = NumberUtils.ParseNumber(number1);
            int decNumber2 = NumberUtils.ParseNumber(number2);
            int decResult = decNumber1 - decNumber2;
            return Convert.ToString(decResult, 8);
        }

        /// <summary>
        /// Mulpiply two numbers in octal numeric system
        /// </summary>
        /// <param name="number1">The first number</param>
        /// <param name="number2"></param>
        /// <returns></returns>
        public string Multiply(string number1, string number2)
        {
            int decNumber1 = NumberUtils.ParseNumber(number1);
            int decNumber2 = NumberUtils.ParseNumber(number2);
            int decResult = decNumber1 * decNumber2;
            return Convert.ToString(decResult, 8);
        }

        // todo: write the summary here
        public string Divide(string number1, string number2)
        {
            int decNumber1 = NumberUtils.ParseNumber(number1);
            int decNumber2 = NumberUtils.ParseNumber(number2);
            int decResult = (int) (decNumber1 / decNumber2);
            return Convert.ToString(decResult, 8);
        }
    }
}
