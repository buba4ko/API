using BookLibrary.Business;
using BookLibrary.Entities;
using BookLibrary.Soap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace BookLibrary.Soap
{
    /// <summary>
    /// Summary description for Library
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Library : System.Web.Services.WebService
    {

        [WebMethod]
        public List<BookReturnModel> GetAllBooks()
        {
			// BookService should be inside a using block, but for simplicity we shall skip it
            BookService bookService = new BookService();

            // variant 1 - direktno konvertirane
            //List<BookReturn> result = bookService.GetAll()
            //    .Select(b => new BookReturn
            //    {
            //        ID = b.ID,
            //        Author = b.Author,
            //        Description = b.Description,
            //        Genre = b.Genre,
            //        Quantity = b.Quantity,
            //        Titile = b.Title
            //    })
            //    .ToList();            
            
            List<BookReturnModel> result = bookService.GetAll()
                .Select(b => new BookReturnModel(b))
                .ToList();

            return result;
        }

        [WebMethod]
        public BookReturnModel GetBookByID(int bookID)
        {
            try
            {
                BookService bookService = new BookService();

                Book dbBook = bookService.GetBookByID(bookID);
                if (dbBook == null)
                    throw new Exception($"Could not find a book with the given ID: {bookID}"); // it is better if you throw your custom Exception!

                BookReturnModel result = new BookReturnModel(dbBook);
                return result;
            }
            catch (Exception ex)
            {
                BookReturnModel result = new BookReturnModel();
                result.ErrorMessage = ex.GetBaseException().Message;
                return result;
            }
        }

        [WebMethod]
        public BooksListModel GetBooksByAuthor(string authorName)
        {
            try
            {
                BookService bookService = new BookService();

                List<Book> dbBooks = bookService.GetByAuthor(authorName);
                BooksListModel result = new BooksListModel(dbBooks);

                return result;
            }
            catch (Exception ex)
            {
                BooksListModel result = new BooksListModel();
                result.ErrorMessage = ex.GetBaseException().Message;
                return result;
            }
        }

        [WebMethod]
        public string AddBook(string title, string author, string genre, string description, int quantity)
        {
            try
            {
                BookService bookService = new BookService();

                Book newBook = new Book
                {
                    Author = author,
                    Description = description,
                    Genre = genre,
                    Quantity = quantity,
                    Title = title,
                    CreatedDate = DateTime.Now
                };
                bookService.AddBook(newBook);

                return "Book is added successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to save the book. Error: {ex.GetBaseException().Message}";
            }
        }

        [WebMethod]
        public string AddBook2(BookAddModel book)
        {
            try
            {
                BookService bookService = new BookService();

                Book newBook = new Book();
                newBook.Author = book.Author;
                newBook.Description = book.Description;
                newBook.Genre = book.Genre;
                newBook.Quantity = book.Quantity;
                newBook.Title = book.Title;
                newBook.CreatedDate = DateTime.Now;
                bookService.AddBook(newBook);

                return "Book is added successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to add the book. Error: {ex.GetBaseException().Message}";
            }
        }

        [WebMethod]
        public string EditBook(int bookID, string title, string author, string genre, string description, int quantity)
        {
            try
            {
                BookService bookService = new BookService();

                Book dbBook = bookService.GetBookByID(bookID);
                if (dbBook == null)
                    return $"Could not find a book with the given ID: {bookID}";

                dbBook.Author = author;
                dbBook.Description = description;
                dbBook.Genre = genre;
                dbBook.Quantity = quantity;
                dbBook.Title = title;
                dbBook.CreatedDate = DateTime.Now;
                bookService.EditBook(dbBook);

                return "Book is saved successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to save the book. Error: {ex.GetBaseException().Message}";
            }
        }

        [WebMethod]
        public string EditBook2(BookEditModel book)
        {
            try
            {
                BookService bookService = new BookService();

                Book dbBook = bookService.GetBookByID(book.ID);
                if (dbBook == null)
                    return $"Could not find a book with the given ID: {book.ID}";

                dbBook.Author = book.Author;
                dbBook.Description = book.Description;
                dbBook.Genre = book.Genre;
                dbBook.Quantity = book.Quantity;
                dbBook.Title = book.Title;
                dbBook.CreatedDate = DateTime.Now;
                bookService.EditBook(dbBook);

                return "Book is saved successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to save the book. Error: {ex.GetBaseException().Message}";
            }
        }

        [WebMethod]
        public string DeleteBook(int bookID)
        {
            try
            {
                BookService bookService = new BookService();
                bookService.DeleteBook(bookID);
                return "Book is deleted successfully";
            }
            catch (Exception ex)
            {
                return $"Failed to delete book with id={bookID}. Error: {ex.GetBaseException().Message}";
            }
        }

        [WebMethod]
        public BooksListModel GetNotReturnedBooks()
        {
            try
            {
                BookService bookService = new BookService();

                List<Book> dbBooks = bookService.GetNotReturnedBooks();
                BooksListModel result = new BooksListModel(dbBooks);

                return result;
            }
            catch (Exception ex)
            {
                BooksListModel result = new BooksListModel();
                result.ErrorMessage = $"Could not retrieve not returned books. Error message is: {ex.GetBaseException().Message}";
                return result;
            }
        }

        [WebMethod]
        public BooksListModel GetBorrowedBooksByReader(int readerID)
        {
            try
            {
                BookService bookService = new BookService();

                List<Book> dbBooks = bookService.GetBorrowedBooksByReader(readerID);
                BooksListModel result = new BooksListModel(dbBooks);

                return result;
            }
            catch (Exception ex)
            {
                BooksListModel result = new BooksListModel();
                result.ErrorMessage = $"Could not get borrowed books by reader {readerID}. Error message is: {ex.GetBaseException().Message}";
                return result;
            }
        }
    }
}
