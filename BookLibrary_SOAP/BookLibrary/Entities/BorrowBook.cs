namespace BookLibrary.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BorrowBook
    {
        public int ID { get; set; }

        public int BookID { get; set; }

        public int ReaderID { get; set; }

        public DateTime TakenDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        public virtual Book Book { get; set; }

        public virtual Reader Reader { get; set; }
    }
}
