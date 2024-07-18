using System.ComponentModel.DataAnnotations;
using Tourism.Repository.Data;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
    public class UniqueTripPhotoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var TripPh = (TripViewModel)validationContext.ObjectInstance;
            if (TripPh.PhotoFile != null)
            {
                TripPh.ImgUrl = DocumentSetting.UploadFile(TripPh.PhotoFile, "Images");
            }

            TourismContext context = new TourismContext();

            if (value != null)
            {
                var TripPhUrl = value.ToString();
                var TP = context.Trips.Where(x => x.ImgUrl == TripPhUrl).FirstOrDefault();
                if (TP == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Choose Unique Photo");
                }



            }

            else
            {
                return new ValidationResult("The Photo is required");
            }

        }
    }
}
