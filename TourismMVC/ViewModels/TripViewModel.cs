using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TourismMVC.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //[UniqueTripPhoto]
        public string? ImgUrl { get; set; }
        public IFormFile? PhotoFile { get; set; }
        [Required]
        [DisplayName("City Name")]
        public string City { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
