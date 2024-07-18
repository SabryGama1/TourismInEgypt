using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class PasswordResetDto
    {
        [Required(ErrorMessage = "New Password Is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Doesn't match password")]
        public string ConfirmePassword { get; set; }
    }
}
