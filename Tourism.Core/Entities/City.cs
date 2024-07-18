namespace Tourism.Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public virtual IEnumerable<CityPhotos> CityPhotos { get; set; }

        public virtual IEnumerable<Place> Places { get; set; }



    }
}
