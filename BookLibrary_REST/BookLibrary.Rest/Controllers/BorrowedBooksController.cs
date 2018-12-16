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
    [RoutePrefix("api/reader/{readerID}/borrowed-books")]
    public class BorrowedBooksController : ApiController
    {
        [HttpGet]
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
