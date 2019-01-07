using BookLibrary.Business;
using BookLibrary.Entities;
using BookLibrary.Rest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookLibrary.Rest.Controllers
{
    /// <summary>
    /// Controller for borrowed books by a user
    /// </summary>
    [RoutePrefix("api/reader/{readerID}/borrowed-books")]
    public class BorrowedBooksController : ApiController
    {
        /// <summary>
        /// Get books borrowed by a reader
        /// </summary>
        /// <param name="readerID">The reader ID</param>
        /// <returns>A List of Books</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpGet]
        [Route]
        public IHttpActionResult Get(int readerID)
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

        /// <summary>
        /// Borrows a book by a reader
        /// </summary>
        /// <param name="readerID">the reader ID</param>
        /// <param name="bookID">the book ID</param>
        /// <returns>OK status or error status</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpPost]
        [Route("{bookID}")]
        public IHttpActionResult Post(int readerID, int bookID)
        {
            BookService bookService = new BookService();
            ReaderService readerService = new ReaderService();

            // check if the reader really exists
            Reader reader = readerService.GetReaderByID(readerID);
            if (reader == null)
                return BadRequest($"invalid readerID: {readerID}");

            // check if the book really exists
            Book book = bookService.GetBookByID(bookID);
            if (reader == null)
                return BadRequest($"invalid bookID: {bookID}");

            readerService.BorrowBook(readerID, bookID);

            return Ok();
        }

        /// <summary>
        /// Returns a book by a reader
        /// </summary>
        /// <param name="readerID">the reader ID</param>
        /// <param name="bookID">the book ID</param>
        /// <returns>OK status or error status</returns>
        /// <response code="200">OK</response>
        /// <response code="400">BadRequest</response>
        [HttpDelete]
        [Route("{bookID}")]
        public IHttpActionResult Delete(int readerID, int bookID)
        {
            BookService bookService = new BookService();
            ReaderService readerService = new ReaderService();

            // check if the reader really exists
            Reader reader = readerService.GetReaderByID(readerID);
            if (reader == null)
                return BadRequest($"invalid readerID: {readerID}");

            // check if the book really exists
            Book book = bookService.GetBookByID(bookID);
            if (reader == null)
                return BadRequest($"invalid bookID: {bookID}");

            readerService.ReturnBook(readerID, bookID);

            return Ok();
        }
    }
}
