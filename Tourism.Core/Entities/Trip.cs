namespace Tourism.Core.Entities
{
    public class Trip : BaseEntity
    {
        public string Name { get; set; }

        public string? ImgUrl { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public virtual ICollection<Place> Places { get; set; } = new List<Place>();
    }

}
