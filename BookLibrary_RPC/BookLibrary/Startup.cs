using BookLibrary.Business;
using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BookLibrary
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (BookService service = new BookService())
            {
                List<Book> books = service.GetAll();

                // test if we get date from the DB
                Book book = books.FirstOrDefault();

                // edit a book
                book.Quantity++;
                service.EditBook(book);

                // get book by ID
                Book book2 = service.GetBookByID(book.ID);

                // add new book
                Book newBook = new Book
                {
                    Author = "Microsoft",
                    Title = "Creating API",
                    Description = "книга за програмиране",
                    Genre = "programming",
                    Quantity = 1,
                    CreatedDate = DateTime.Now
                };
                service.AddBook(newBook);

                // find book by author and delete if we have > 1
                var booksByAuthor = service.GetByAuthor("Microsoft");
                if (booksByAuthor.Count > 1)
                {
                    int bookID = booksByAuthor.Last().ID;
                    service.DeleteBook(bookID);
                }
            }

            //// another way to get all books - do not use this way, use BookService
            //LibraryContext context = new LibraryContext();
            //var x = context.Books.ToList();


            using (ReaderService readerService = new ReaderService())
            {
                // add new reader
                Reader newReader = new Reader
                {
                    FirstName = "reader",
                    LastName = DateTime.Now.ToShortTimeString(),
                    PhoneNumber = "08812345678",
                    CreatedDate = DateTime.Now
                };
                readerService.AddReader(newReader);

                int readerID = newReader.ID;

                // get reader by ID
                Reader reader2 = readerService.GetReaderByID(readerID);

                // edit reader
                reader2.FirstName = "reader " + readerID;
                readerService.EditReader(reader2);
            }

            using (BookService bookService = new BookService())
            using (ReaderService readerService = new ReaderService())
            {
                // get some valid IDs
                int bookID = bookService.GetByAuthor("Microsoft").First().ID;
                int readerID = readerService.GetAll().Last().ID;

                // borrow book
                readerService.BorrowBook(readerID, bookID);

                // return book
                Thread.Sleep(50);
                readerService.ReturnBook(readerID, bookID);
            }
        }
    }
}
