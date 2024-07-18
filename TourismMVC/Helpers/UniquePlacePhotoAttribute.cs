using System.ComponentModel.DataAnnotations;
using Tourism.Repository.Data;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
    public class UniquePlacePhotoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var placePh = (PlacePhotoViewModel)validationContext.ObjectInstance;
            if (placePh.PhotoFile != null)
            {
                placePh.Photo = DocumentSetting.UploadFile(placePh.PhotoFile, "places");

            }



            TourismContext context = new TourismContext();

            if (value != null && placePh.Photo != null)
            {


                int placephvalue = (int)value;

                var placePhoto = context.CityPhotos.Where(x => x.CityId == placephvalue && x.Photo == placePh.Photo).FirstOrDefault();
                if (placePhoto == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Choose Unique Place & Photo");
                }



            }

            else
            {
                return new ValidationResult("The Place & Photo is required");
            }

        }

    }
}
