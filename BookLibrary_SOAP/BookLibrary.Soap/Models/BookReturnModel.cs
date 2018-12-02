using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Soap.Models
{
    public class BookReturnModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        //public DateTime CreatedDate { get; set; }

        public string ErrorMessage { get; set; }

        public BookReturnModel()
        { }
        public BookReturnModel(Book book)
        {
            this.ID = book.ID;
            this.Author = book.Author;
            this.Description = book.Description;
            this.Genre = book.Genre;
            this.Quantity = book.Quantity;
            this.Title = book.Title;
        }
    }
}