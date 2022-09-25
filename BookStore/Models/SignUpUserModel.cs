using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your fast name")]
        [Display(Name = "Last name")]
        [MinLength(3)]
        public string LastName { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a strong password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
