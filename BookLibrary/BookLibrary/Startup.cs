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
        static void Main(string[] args)
        {
            using (BookService service = new BookService())
            {
                List<Book> books = service.GetAll();

                Book book = books.FirstOrDefault();

                book.Quantity++;
                service.EditBook(book);

                Book book2 = service.GetBookByID(book.ID);

                Book newBook = new Book {
                    Author = "Mecho Puh",
                    Titile = "прасчо и аз",
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
