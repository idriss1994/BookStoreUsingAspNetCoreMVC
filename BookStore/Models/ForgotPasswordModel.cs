using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Enter your registred email")]
        [EmailAddress(ErrorMessage = "Registred email invalide")]
        [Display(Name = "Registred email address")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
