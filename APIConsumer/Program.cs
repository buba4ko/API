using MarsCalculatorAPI;
using MarsCalculatorAPI.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIConsumer
{
    class Program
    {
        private static void PrintTopInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Enter your operation using + - * or /");
            Console.WriteLine("For exit enter 'x'");
        }

        static void Main(string[] args)
        {
            PrintTopInfo();

            while (true)
            {
                Console.Write("Enter operation: ");
                string line = Console.ReadLine();
                if (line == "x")
                {
                    return;
                }

                string result = string.Empty;
                MarsCalculator calculator = new MarsCalculator();

                try
                {
                    Tuple<char, string, string> x = ParseLine(line);
                    switch (x.Item1)
                    {
                        case '+':
                            result = calculator.Add(x.Item2, x.Item3);
                            break;
                        case '-':
                            result = calculator.Subtract(x.Item2, x.Item3);
                            break;
                        case '*':
                            result = calculator.Multiply(x.Item2, x.Item3);
                            break;
                        case '/':
                            result = calculator.Divide(x.Item2, x.Item3);
                            break;
                        default:
                            break;
                    }
                }
                catch (MarsCalulatorException ex)
                {
                    Console.WriteLine("Error in Mars Calculator API: {0}", ex.Message);
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is an error: {0}", ex.Message);
                    continue;
                }
                Console.WriteLine("{0} = {1}", line, result);
            }
        }

        private static Tuple<char, string, string> ParseLine(string line)
        {
            char operation;
            if (line.Contains('+'))
            {
                operation = '+';
            }
            else if (line.Contains('-'))
            {
                operation = '-';
            }
            else if (line.Contains('*'))
            {
                operation = '*';
            }
            else if (line.Contains('/'))
            {
                operation = '/';
            }
            else
            {
                throw new Exception("Unknown operation");
            }

            string[] array = line.Split(operation);
            if (array.Length < 2 || array.Length > 2)
            {
                throw new Exception("Invalid expression");
            }

            string operand1 = array[0];
            string operand2 = array[1];

            return new Tuple<char, string, string>(operation, operand1, operand2);
        }
    }
}

