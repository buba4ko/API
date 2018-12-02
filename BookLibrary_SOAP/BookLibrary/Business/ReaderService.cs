using BookLibrary.Entities;
using BookLibrary.Helpers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookLibrary.Business
{
    public class ReaderService : BaseService
    {
        #region Constructors
        public ReaderService()
            : base()
        {
        }
        #endregion

        #region Public Methods
        public List<Reader> GetAll()
        {
            List<Reader> result = Context.Readers.ToList();
            return result;
        }

        public Reader GetReaderByID(int ID)
        {
            Reader result = Context.Readers.FirstOrDefault(reader => reader.ID == ID);
            return result;
        }

        public void AddReader(Reader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");

            Context.Readers.Add(reader);
            Context.SaveChanges();
        }

        public void EditReader(Reader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");

            Reader dbReader = Context.Readers.FirstOrDefault(r => r.ID == reader.ID);
            if (dbReader == null)
            {
                // throw custom error or ignore the problem
                // you can also log it
                throw new BookLibraryException($"Reader with ID: {reader.ID} not found. Can not edit it.");
            }
            dbReader.FirstName = reader.FirstName;
            dbReader.LastName = reader.LastName;
            dbReader.PhoneNumber = reader.PhoneNumber;
            Context.SaveChanges();
            Context.Readers.Add(reader);
        }

        public void DeleteReader(int readerID)
        {
            Reader dbReader = this.GetReaderByID(readerID);
            if (dbReader == null)
            {
                // throw custom error or ignore the problem
                // you can also log it
                throw new BookLibraryException($"Reader with ID: {readerID} not found. Can not edit it.");
            }

            Context.Readers.Remove(dbReader);
            Context.SaveChanges();
        }

        public void BorrowBook(int readerID, int bookID)
        {
            Reader dbReader = this.GetReaderByID(readerID);
            if (dbReader == null)
            {
                throw new BookLibraryException($"Reader with ID: {readerID} not found. Can not return book for him.");
            }
            Book dbBook = Context.Books.FirstOrDefault(b => b.ID == bookID);
            if (dbBook == null)
            {
                throw new BookLibraryException($"Book with ID: {bookID} not found. Can not return it.");
            }

            BorrowBook bb = new BorrowBook
            {
                // no need to set readerID if we add this object as a child object of Reader
                // ReaderID = readerID,
                BookID = bookID,
                TakenDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(14)
            };
            dbReader.BorrowBooks.Add(bb);
            Context.SaveChanges();
        }

        public void ReturnBook(int readerID, int bookID)
        {
            Reader dbReader = this.GetReaderByID(readerID);
            if (dbReader == null)
            {
                throw new BookLibraryException($"Reader with ID: {readerID} not found. Can not return book for him.");
            }
            Book dbBook = Context.Books.FirstOrDefault(b => b.ID == bookID);
            if (dbBook == null)
            {
                throw new BookLibraryException($"Book with ID: {bookID} not found. Can not return it.");
            }

            BorrowBook borrowBook = Context.BorrowBooks
                .SingleOrDefault(bb => bb.BookID == bookID && bb.ReaderID == readerID && bb.ReturnedDate == null);
            if (borrowBook == null)
            {
                // throw custom error or ignore the problem
                // you can also log it
                throw new BookLibraryException($"Book with ID: {bookID} not found. Can not return it.");
            }

            borrowBook.ReturnedDate = DateTime.Now;
            Context.SaveChanges();
        }
        #endregion
    }
}
