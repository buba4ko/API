using BookLibrary.Entities;
using BookLibrary.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Business
{
    public class BookService : BaseService
    {
        #region Constructors
        public BookService()
            : base()
        {
        }
        #endregion

        #region Public methods
        public List<Book> GetAll()
        {
            List<Book> result = new List<Book>();
            result = Context.Books.ToList();
            return result;
        }

        public List<Book> GetByAuthor(string name)
        {
            List<Book> result = new List<Book>();

            // variant 1
            foreach (Book book in Context.Books)
            {
                if (book.Author == name)
                {
                    result.Add(book);
                }
            }

            // variant 2
            result = Context.Books
                .Where(book => book.Author.Contains(name))
                .ToList();

            return result;
        }

        public Book GetBookByID(int ID)
        {
            Book result = Context.Books.FirstOrDefault(book => book.ID == ID);
            return result;
        }

        public void AddBook(Book book)
        {
            book.CreatedDate = DateTime.Now;
            Context.Books.Add(book);
            Context.SaveChanges();
        }

        public void EditBook(Book book)
        {
            Book dbBook = this.GetBookByID(book.ID);
            dbBook.Author = book.Author;
            dbBook.Description = book.Description;
            dbBook.Genre = book.Genre;
            dbBook.Quantity = book.Quantity;
            dbBook.Title = book.Title;
            Context.SaveChanges();
        }

        public void DeleteBook(int bookID)
        {
            Book dbBook = this.GetBookByID(bookID);
            if (dbBook != null)
            {
                Context.Books.Remove(dbBook);
                Context.SaveChanges();
            }
            else
            {
                // throw custom exception or ignore the problem
                throw new BookLibraryException("could not find a book with ID: " + bookID);
            }
        }

        public List<Book> GetNotReturnedBooks()
        {
            List<Book> result = Context.BorrowBooks
                .Where(bb => bb.ReturnedDate == null)
                .Select(bb => bb.Book)
                .Distinct()
                .ToList();
            return result;
        }

        public List<Book> GetBorrowedBooksByReader(int readerID)
        {
            List<Book> result = Context.BorrowBooks
            .Where(bb => bb.ReaderID == readerID)
            .Select(bb => bb.Book)
            .ToList();
            return result;
        }
        #endregion
    }
}
