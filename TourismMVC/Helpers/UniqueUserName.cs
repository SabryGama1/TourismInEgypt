using System.ComponentModel.DataAnnotations;
using Tourism.Repository.Data;

namespace TourismMVC.Helpers
{
    public class UniqueUserName : ValidationAttribute
    {
        TourismContext Context = new TourismContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var UName = value.ToString();

                var UNameIndb = Context.Users.FirstOrDefault(u => u.UserName == UName);
                if (UNameIndb == null)
                {
                    return ValidationResult.Success;
                }
                else
                    return new ValidationResult("User Name Already Tooken");


            }
            else
                return new ValidationResult(" User Name Required");

        }

    }
}
