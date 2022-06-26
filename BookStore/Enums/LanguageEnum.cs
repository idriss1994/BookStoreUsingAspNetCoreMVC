using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Enums
{
    public enum LanguageEnum
    {
        [Display(Name = "Arabic language")]
        Arabic = 1,

        [Display(Name = "Amazigh language")]
        Amazigh = 2,

        [Display(Name = "English language")]
        English = 3,

        [Display(Name = "French language")]
        French = 4,

        [Display(Name = "Hindi language")]
        Hindi = 5,

        [Display(Name = "Dutch language")]
        Dutch = 6
    }
}
