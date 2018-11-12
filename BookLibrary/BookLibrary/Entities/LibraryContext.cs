namespace BookLibrary.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("name=LibraryContext")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BorrowBook> BorrowBooks { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(e => e.BorrowBooks)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reader>()
                .HasMany(e => e.BorrowBooks)
                .WithRequired(e => e.Reader)
                .WillCascadeOnDelete(false);
        }
    }
}
