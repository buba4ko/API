using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static void PrintTopInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Enter your operation:");
            Console.WriteLine("'1' - Get all books");
            Console.WriteLine("'2' - Get book by ID");
            Console.WriteLine("'3' - Create Book");
            Console.WriteLine("'4' - Delete Book");
            Console.WriteLine();
            Console.WriteLine("'6' - Get all reader");
            Console.WriteLine("'7' - Get reader by ID");
            Console.WriteLine("'8' - Create Reader");
            Console.WriteLine();
            Console.WriteLine("'9' - See borrowed books by reader");
            Console.WriteLine("10' - Borrow book");
            Console.WriteLine("11' - Return book");

            Console.WriteLine();
            Console.WriteLine("'h' - Help");
            Console.WriteLine("'x' = Exit");
        }

        static void Main(string[] args)
        {
            PrintTopInfo();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Select operation: ");
                string line = Console.ReadLine();
                if (line == "x")
                {
                    return;
                }

                try
                {
                    int bookID;
                    int readerID;
                    ClientWrapper client = new ClientWrapper();
                    switch (line)
                    {
                        case "1":
                            client.GetAllBooks();
                            break;
                        case "2":
                            Console.Write("Enter bookID: ");
                            bookID = ReadIntegerFromConsole();
                            client.GetBookByID(bookID);
                            break;
                        case "3":
                            Console.Write("Title: ");
                            string title = Console.ReadLine();
                            Console.Write("Author: ");
                            string author = Console.ReadLine();
                            Console.Write("Genre: ");
                            string genre = Console.ReadLine();
                            Console.Write("Description: ");
                            string description = Console.ReadLine();
                            Console.Write("Quantity: ");
                            int quantity = ReadIntegerFromConsole();
                            client.CreateBook(title, author, genre, description, quantity);
                            break;
                        case "4":
                            Console.Write("Enter bookID: ");
                            bookID = ReadIntegerFromConsole();
                            client.DeleteBook(bookID);
                            break;
                        case "6":
                            client.GetAllReaders();
                            break;
                        case "7":
                            Console.Write("Enter readerID: ");
                            bookID = ReadIntegerFromConsole();
                            client.GetReaderByID(bookID);
                            break;
                        case "8":
                            Console.Write("First Name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            string lastName = Console.ReadLine();
                            Console.Write("Phone: ");
                            string phoneNumber = Console.ReadLine();
                            client.Createreader(firstName, lastName, phoneNumber);
                            break;
                        case "9":
                            Console.Write("Enter readerID: ");
                            bookID = ReadIntegerFromConsole();
                            client.GetReaderBorrowedBook(bookID);
                            break;
                        case "10":
                            Console.Write("Enter readerID: ");
                            readerID = ReadIntegerFromConsole();
                            Console.Write("Enter bookID: ");
                            bookID = ReadIntegerFromConsole();
                            client.BorrowBook(readerID, bookID);
                            break;
                        case "11":
                            Console.Write("Enter readerID: ");
                            readerID = ReadIntegerFromConsole();
                            Console.Write("Enter bookID: ");
                            bookID = ReadIntegerFromConsole();
                            client.ReturnBook(readerID, bookID);
                            break;
                        case "h":
                            Console.Clear();
                            PrintTopInfo();
                            break;
                        default:
                            Console.WriteLine("invalid command. Type 'h' for help");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("There is an error: {0}", ex.Message);
                    continue;
                }
            }
        }

        private static int ReadIntegerFromConsole()
        {
            int result;
            string line = Console.ReadLine();
            while (int.TryParse(line, out result) == false)
            {
                Console.WriteLine("Please enter valid integer number!");
                line = Console.ReadLine();
            }
            return result;
        }
    }
}
