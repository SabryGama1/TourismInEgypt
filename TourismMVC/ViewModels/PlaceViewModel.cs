using Tourism.Core.Entities;

namespace TourismMVC.ViewModels
{
    public class PlaceViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Link { get; set; }
        public string Phone { get; set; }

        public IEnumerable<City> Cities_List { get; set; } = new List<City>();

        public IEnumerable<Category> Categories_List { get; set; } = new List<Category>();

        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public int CityId { get; set; }
        public virtual City? City { get; set; }

        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        //public  ICollection<PlacePhotos> placePhotos { get; set; }
    }
}
