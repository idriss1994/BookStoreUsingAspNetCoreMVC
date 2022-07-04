using BookStore.Enums;
using BookStore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        //[StringLength(100, MinimumLength = 5)]
        //[Required(ErrorMessage = "Please enter the title of your book")]
        [MyCustomValidation(text: "text")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the author name")]
        public string Author { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
      
        [Required(ErrorMessage = "Please select the  language of your book")]
        public int LanguageId { get; set; }

        public string Language { get; set; }

        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }
    }
}
