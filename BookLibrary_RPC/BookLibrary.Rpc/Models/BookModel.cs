using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Rpc.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        //public DateTime CreatedDate { get; set; }

        public BookModel()
        { }
        public BookModel(BookLibrary.Entities.Book book)
        {
            this.ID = book.ID;
            this.Author = book.Author;
            this.Description = book.Description;
            this.Genre = book.Genre;
            this.Quantity = book.Quantity;
            this.Title = book.Title;
        }

        public void CopyValuesToEntity(BookLibrary.Entities.Book dbBook)
        {
            dbBook.Description = this.Description;
            dbBook.Author = this.Author;
            dbBook.Genre = this.Genre;
            dbBook.Quantity = this.Quantity;
            dbBook.Title = this.Title;
            dbBook.Description = this.Description;
        }
    }
}