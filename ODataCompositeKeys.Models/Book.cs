using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataCompositeKeys.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public short PublicationYear { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; } 
    }
}
