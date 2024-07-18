namespace Tourism.Core.Entities
{
    public class Place : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Link { get; set; }
        public string Phone { get; set; }
        public bool IsFav { get; set; } = false;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual IEnumerable<PlacePhotos> Photos { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }

        public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    }


}
