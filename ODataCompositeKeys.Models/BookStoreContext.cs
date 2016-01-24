using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataCompositeKeys.Models
{
    public class BookStoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        //static BookStoreContext()
        //{
        //    System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<BookStoreContext>());
        //}

        public BookStoreContext() : base("BookStoreConnection")
        {
            Configuration.LazyLoadingEnabled = false; //to get around circular reference problem
        }
    }
}
