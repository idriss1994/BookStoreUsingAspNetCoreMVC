using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }

        public string Language { get; set; }

        public int? TotalPages { get; set; }

        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
    }
}
