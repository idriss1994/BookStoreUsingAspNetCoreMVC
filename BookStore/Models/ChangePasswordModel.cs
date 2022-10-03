using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Enter your current password"), DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Enter your new password"), DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password"), DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "New password and confirm new password does not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
