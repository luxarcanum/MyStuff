using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBooks.Web.Models
{
    public class Book
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string Genre { get; set; }
        public virtual string Review { get; set; }
    }
}