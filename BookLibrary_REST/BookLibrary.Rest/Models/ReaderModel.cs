using BookLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookLibrary.Rest.Models
{
    public class ReaderModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        //public DateTime CreatedDate { get; set; }

        public ReaderModel()
        {
        }

        public ReaderModel(BookLibrary.Entities.Reader reader)
        {
            this.ID = reader.ID;
            this.FirstName = reader.FirstName;
            this.LastName = reader.LastName;
            this.PhoneNumber = reader.PhoneNumber;
        }

        public void CopyValuesToEntity(BookLibrary.Entities.Reader dbReader)
        {
            dbReader.FirstName = this.FirstName;
            dbReader.LastName = this.LastName;
            dbReader.PhoneNumber = this.PhoneNumber;
        }
    }
}