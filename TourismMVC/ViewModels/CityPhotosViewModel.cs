using System.ComponentModel.DataAnnotations.Schema;
using Tourism.Core.Entities;

using TourismMVC.Helpers;

namespace TourismMVC.ViewModels
{
    public class CityPhotosViewModel
    {
        public int? Id { get; set; }

        [UniqueCityPhoto]
        [ForeignKey("city")]
        public int? CityId { get; set; }
        public City? city { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string Photo { get; set; }
        public IEnumerable<City>? cities { get; set; }
    }
}
