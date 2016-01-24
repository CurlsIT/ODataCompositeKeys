namespace ODataCompositeKeys.Models.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookStoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Authors.AddOrUpdate(
                a => a.Id,
                new Author {Id = 1, FirstName = "Frank", LastName = "Sinatra"},
                new Author {Id = 2, FirstName = "Euzebiusz", LastName = "Bodo"},
                new Author {Id = 3, FirstName = "Joseph", LastName = "Dickins"}
                );

            context.Books.AddOrUpdate(
                b => b.Id,
                new Book {Id = 1, Title = "Dark Moon", ISBN = "978-3-16-148410-0", PublicationYear = 1978},
                new Book {Id = 2, Title = "Pieœni wygnañców", ISBN = "978-83-7181-510-2", PublicationYear = 1942},
                new Book {Id = 3, Title = "Dirty desert", ISBN = "978-66-7384-711-4", PublicationYear = 1979}
                );

            context.BookAuthors.AddOrUpdate(
                ba => new { ba.BookId, ba.AuthorId},
                new BookAuthor {BookId = 1, AuthorId = 1 },
                new BookAuthor {BookId = 1, AuthorId = 3 },
                new BookAuthor {BookId = 2, AuthorId = 2 },
                new BookAuthor {BookId = 3, AuthorId = 1 },
                new BookAuthor {BookId = 3, AuthorId = 2 }
                );

            context.SaveChanges();
        }
    }
}
