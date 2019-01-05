﻿using BookLibrary.Business;
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
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        /// <summary>
        /// Gets all books in the library
        /// </summary>
        /// <returns>A List<BookModel> with all books</returns>
        [HttpGet]
        [Route]
        public List<BookModel> Get()
        {
            BookService bookService = new BookService();
            var allBooks = bookService.GetAll()
                .Select(c => new BookModel(c))
                .ToList();
            return allBooks;
        }

        /// <summary>
        /// Get info for a single book by bookID
        /// </summary>
        /// <param name="bookID">The bookID in the database</param>
        /// <returns>The found book</returns>
        [HttpGet]
        [Route("{bookID:int}")]
        public IHttpActionResult GetByID(int? bookID)
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

        /// <summary>
        /// Finds all books by a given author
        /// </summary>
        /// <param name="name">the author name</param>
        /// <returns>A list with all found books. If no books are found - return empty string</returns>
        [HttpGet]
        [Route("author-name/{name}")]
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

        /// <summary>
        /// Finds all books by a given author or title
        /// </summary>
        /// <param name="author">the author name. optional parameter.</param>
        /// <param name="title">text in the book title. optional parameter.</param>
        /// <returns>A list with all found books. If no books are found - return empty string</returns>
        [HttpGet]
        [Route("search")]
        public IHttpActionResult GetBooksByAuthorOrTitle(string author=null, string title=null)
        {
            // now author and title are optional that's why they are GET query parameters, not a route parameters

            BookService bookService = new BookService();
            List<Book> books = bookService.GetByAuthorOrTitle(author, title);

            List<BookModel> apiBooks = books
                .Select(b => new BookModel(b))
                .ToList();

            return Ok(apiBooks);
        }

        [HttpPut]
        [Route]
        public IHttpActionResult Put(BookModel book)
        {
            try
            {
                BookService bookService = new BookService();
                Book dbBook = bookService.GetBookByID(book.ID);
                if (dbBook == null)
                    return NotFound();

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
        [Route]
        public IHttpActionResult Post(BookModel book)
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
                BookModel newBook = new BookModel(dbBook);
                return Ok(newBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{bookID:int}")]
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

        [HttpGet]
        [Route("not-returned")]
        public IHttpActionResult GetNotReturnedBooks()
        {
            // we write a new method, but it might me better if we use the GetAllBooks() method with one parameter - bool getNotReturnedBooks = false

            BookService bookService = new BookService();
            var allBooks = bookService.GetNotReturnedBooks()
                .Select(c => new BookModel(c))
                .ToList();
            return Ok(allBooks);
        }

    }
}

