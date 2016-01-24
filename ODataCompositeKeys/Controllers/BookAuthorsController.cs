using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.Http.OData.Routing;
using ODataCompositeKeys.Models;

namespace ODataCompositeKeys.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ODataCompositeKeys.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<BookAuthor>("BookAuthors");
    builder.EntitySet<Author>("Authors"); 
    builder.EntitySet<Book>("Books"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class BookAuthorsController : ODataController
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: odata/BookAuthors
        [EnableQuery]
        public IQueryable<BookAuthor> GetBookAuthors()
        {
            return db.BookAuthors;
        }

        // GET: odata/BookAuthors(5)
        [EnableQuery]
        public SingleResult<BookAuthor> GetBookAuthor([FromODataUri] int key)
        {
            return SingleResult.Create(db.BookAuthors.Where(bookAuthor => bookAuthor.BookId == key));
        }

        // PUT: odata/BookAuthors(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<BookAuthor> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookAuthor bookAuthor = await db.BookAuthors.FindAsync(key);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            patch.Put(bookAuthor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookAuthorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bookAuthor);
        }

        // POST: odata/BookAuthors
        public async Task<IHttpActionResult> Post(BookAuthor bookAuthor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BookAuthors.Add(bookAuthor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BookAuthorExists(bookAuthor.BookId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(bookAuthor);
        }

        // PATCH: odata/BookAuthors(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<BookAuthor> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            BookAuthor bookAuthor = await db.BookAuthors.FindAsync(key);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            patch.Patch(bookAuthor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookAuthorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(bookAuthor);
        }

        // DELETE: odata/BookAuthors(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            BookAuthor bookAuthor = await db.BookAuthors.FindAsync(key);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            db.BookAuthors.Remove(bookAuthor);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/BookAuthors(5)/Author
        [EnableQuery]
        public SingleResult<Author> GetAuthor([FromODataUri] int key)
        {
            return SingleResult.Create(db.BookAuthors.Where(m => m.BookId == key).Select(m => m.Author));
        }

        // GET: odata/BookAuthors(5)/Book
        [EnableQuery]
        public SingleResult<Book> GetBook([FromODataUri] int key)
        {
            return SingleResult.Create(db.BookAuthors.Where(m => m.BookId == key).Select(m => m.Book));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookAuthorExists(int key)
        {
            return db.BookAuthors.Count(e => e.BookId == key) > 0;
        }
    }
}
