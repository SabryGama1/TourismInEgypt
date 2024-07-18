using System.ComponentModel.DataAnnotations;
using TourismMVC.Helpers;

namespace TourismMVC.ViewModels
{
    public class RegisterUserModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string FName { get; set; }
        [Required]
        [MaxLength(15)]
        public string LName { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        //[Required]
        //[Compare("Password")]
        //[DataType(DataType.Password)]
        //public string ConfirmedPassword { get; set; }

        [Required]
        [UniqueUserName]
        public string UserName { get; set; }

        public string Email { get; set; }



    }
}
