using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ResetPasswordModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Enter your new password")]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Enter your new password")]
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirm new password does not match")]
        public string ConfirmNewPassword { get; set; }
        public bool IsSuccess { get; set; }
    }
}
