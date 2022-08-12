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

        public int LanguageId { get; set; }

        public int? TotalPages { get; set; }
        public string CoverBookUrl { get; set; }

        public DateTime? CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string BookPdfUrl { get; set; }

        //Create relation between Book and Language (Book has one language (one to one))
        public Language Language { get; set; }

        //Create relation between Book and BookGallery (one to many)
        public ICollection<BookGallery> BookGallery { get; set; }
    }
}
