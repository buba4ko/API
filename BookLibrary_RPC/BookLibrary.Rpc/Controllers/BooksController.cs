using BookLibrary.Business;
using BookLibrary.Entities;
using BookLibrary.Rpc.Models;
// using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace BookLibrary.Rpc.Controllers
{
    //[RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        [HttpGet]
        //[Route("api/books/GetBookByID")]
        public List<BookModel> GetAllBooks()
        {
            BookService bookService = new BookService();
            var allBooks = bookService.GetAll()
                .Select(c => new BookModel(c))
                .ToList();
            return allBooks;
        }

        [HttpGet]
        //[Route("api/books/GetBookByID/{bookID:int}")]
        public IHttpActionResult GetBookByID(int? bookID)
        {
            if (bookID == null)
                return BadRequest("the parameter bookID is empty");

            BookService bookService = new BookService();
            Book book = bookService.GetBookByID(bookID.Value);
            if (book == null)
                return BadRequest($"Could not find book with ID: {bookID}");

            BookModel apiBook = new BookModel(book);
            return Ok(apiBook);
        }

        [HttpGet]
        public IHttpActionResult GetBooksByAuthor(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("the parameter 'name' is empty");

            BookService bookService = new BookService();
            List<Book> books = bookService.GetByAuthor(name);

            List<BookModel> apiBooks = books
                .Select(b => new BookModel(b))
                .ToList();

            return Ok(apiBooks);
        }

        [HttpPost]
        public IHttpActionResult Edit(BookModel book)
        {
            try
            {
                BookService bookService = new BookService();
                Book dbBook = bookService.GetBookByID(book.ID);
                if (dbBook == null)
                    return NotFound();

                //dbBook.Description = book.Description;
                //dbBook.Author = book.Author;
                //dbBook.Genre = book.Genre;
                //dbBook.Quantity = book.Quantity;
                //dbBook.Title = book.Title;
                //dbBook.Description = book.Description;
                book.CopyValuesToEntity(dbBook);
                bookService.EditBook(dbBook);

                return StatusCode(HttpStatusCode.NoContent); // or use Ok()
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(BookModel book)
        {
            try
            {
                BookService bookService = new BookService();
                Book dbBook = new Book();

                //dbBook.Description = book.Description;
                //dbBook.Author = book.Author;
                //dbBook.Genre = book.Genre;
                //dbBook.Quantity = book.Quantity;
                //dbBook.Title = book.Title;
                book.CopyValuesToEntity(dbBook);
                bookService.AddBook(dbBook);

                // return the newly created Book
                return Ok(dbBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult Delete(int bookID)
        {
            try
            {
                BookService bookService = new BookService();
                Book dbBook = bookService.GetBookByID(bookID);
                if (dbBook == null)
                    return NotFound();

                bookService.DeleteBook(bookID);

                // we decide to return no message for information
                // so the response has empty body with status 204 - success
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // example with a possible return type - OkNegotiatedContentResult, which is not used often
        [HttpGet]
        public OkNegotiatedContentResult<List<BookModel>> GetNotReturnedBooks()
        {
            // we write a new method, but it might me better if we use the GetAllBooks() method with one parameter - bool getNotReturnedBooks = false

            BookService bookService = new BookService();
            var allBooks = bookService.GetNotReturnedBooks()
                .Select(c => new BookModel(c))
                .ToList();

            return Ok(allBooks);
        }

        [HttpGet]
        public IHttpActionResult GetBorrowedBooksByReader(int readerID)
        {
            BookService bookService = new BookService();
            ReaderService readerService = new ReaderService();

            // check if the reader really exists
            Reader reader = readerService.GetReaderByID(readerID);
            if (reader == null)
                return BadRequest($"invalid readerID: {readerID}");

            var allBooks = bookService.GetBorrowedBooksByReader(readerID)
                .Select(c => new BookModel(c))
                .ToList();

            return Ok(allBooks);
        }
    }
}
