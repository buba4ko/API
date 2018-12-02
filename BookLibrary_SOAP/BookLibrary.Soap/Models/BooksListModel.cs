using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Soap.Models
{
    /// <summary>
    /// Create a separate class when we have to return list of BookReturn objects, in order to set ErrorMesage
    /// </summary>
    public class BooksListModel
    {
        public List<BookModel> BooksList { get; set; }

        public string ErrorMessage { get; set; }

        public BooksListModel()
        {
            BooksList = new List<BookModel>();
        }

        public BooksListModel(List<Book> books)
            : this()
        {
            this.BooksList = books
                .Select(b => new BookModel(b))
                .ToList();
        }
    }
}