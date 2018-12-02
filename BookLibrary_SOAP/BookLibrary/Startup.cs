using BookLibrary.Business;
using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary
{
    class Startup
    {
        class Car
        {
            public int ID;
            public String Manufacturer;
            public String Model;
            public int Quantity;
        };

        static void Main(string[] args)
        {
            List<Car> cars = new List<Car> {
                new Car{ID=1, Manufacturer="BMW", Model="X5", Quantity=5},
                new Car{ID=2, Manufacturer="BMW", Model="3", Quantity=10},
                new Car{ID=3, Manufacturer="Peugeot", Model="207", Quantity=1},
                new Car{ID=4, Manufacturer="Peugeot", Model="306", Quantity=2},
                new Car{ID=5, Manufacturer="Peugeot", Model="307", Quantity=4},
                new Car{ID=6, Manufacturer="Peugeot", Model="407", Quantity=3},
                new Car{ID=7, Manufacturer="Audi", Model="100", Quantity=4},
                new Car{ID=8, Manufacturer="Audi", Model="A6", Quantity=5},
                new Car{ID=10, Manufacturer="Audi", Model="Quattro", Quantity=0},
                new Car{ID=11, Manufacturer="Opel", Model="Corsa", Quantity=7},
                new Car{ID=12, Manufacturer="Opel", Model="Vectra", Quantity=8}
              };

            int total2 = cars.Select(c => c.Quantity)
                .Sum();

            int total = cars.Sum(c => c.Quantity);

            List<Car> missingCars = cars.Where(c => c.Quantity == 0).ToList();


            int bmwCarsCount = cars
                .Where(c => c.Manufacturer == "BMW")
                .Select(c => c.Quantity)
                .Sum();

            List<Car> tempList = cars
                .Where(c => c.Quantity >= 1 && c.Quantity <= 5)
                .OrderBy(c => c.Manufacturer)
                .ThenBy(c => c.Model)
                .ToList();

            List<string> brands = cars
                .Select(c => c.Manufacturer) // ve4e imame List<string>
                .Distinct()
                .OrderBy(c => c)
                .ToList();

            var maxCar = cars.Where(c => c.Manufacturer == "Opel")
                .OrderByDescending(c => c.Quantity)
                .FirstOrDefault();

        }

        static void Main2(string[] args)
        {
            List<int> numbers = new List<int> { 1, 8, 5, 3, 7, 4, 2, 6 };

            List<int> evenNumbers = numbers
                .Where(c => c % 2 == 0)
                .OrderBy(c => c)
                .ToList();

            bool s = numbers.Count(c => c % 2 == 0) >= 3;

            var list2 = numbers.OrderByDescending(c => c).ToList();

            List<int> oddNumbers = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] % 2 == 0)
                {
                    oddNumbers.Add(numbers[i]);
                }
            }

            var s23 = numbers.Count(c => c % 2 == 0) >= 3;

            using (BookService service = new BookService())
            {
                List<Book> books = service.GetAll();

                Book book = books.FirstOrDefault();

                book.Quantity++;
                service.EditBook(book);

                Book book2 = service.GetBookByID(book.ID);

                Book newBook = new Book {
                    Author = "Mecho Puh",
                    Title = "прасчо и аз",
                    Description = "дълго е за разказване",
                    Genre = "приказки",
                    Quantity = 1,
                    CreatedDate = DateTime.Now
                };

                service.AddBook(newBook);


                var booksByAuthor = service.GetByAuthor("Puh");
                if (booksByAuthor.Count > 0)
                {
                    int bookID = booksByAuthor.First().ID;
                    service.DeleteBook(bookID);
                }


            }

            LibraryContext context = new LibraryContext();
            var x = context.Books.ToList();
        }
    }
}
