using System.ComponentModel.DataAnnotations;
using Tourism.Repository.Data;
using TourismMVC.ViewModels;

namespace TourismMVC.Helpers
{
    public class UniquePlaceTripAttribute : ValidationAttribute
    {
        TourismContext TourismContext = new TourismContext();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Trip = (Place_TripModel)validationContext.ObjectInstance;


            if (value != null && Trip.TripId != null)
            {
                int idofPlace = (int)value;
                var pt = TourismContext.Place_Trips.Where(x => x.PlaceId == idofPlace && x.TripId == Trip.TripId).FirstOrDefault();
                if (pt == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Choose another place");
                }

            }
            else
                return new ValidationResult("The Place Name is required");
        }
    }
}
