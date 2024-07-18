using System.ComponentModel.DataAnnotations.Schema;
using Tourism.Core.Entities;
using TourismMVC.Helpers;

namespace TourismMVC.ViewModels
{
    public class PlacePhotoViewModel
    {

        public int? Id { get; set; }

        [UniquePlacePhoto]
        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place? Place { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string Photo { get; set; }
        public IEnumerable<Place>? places { get; set; }
    }
}
