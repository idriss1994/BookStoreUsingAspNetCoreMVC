using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Enter your email"), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
