namespace Tourism.Core.Entities
{
    public class CityPhotos : BaseEntity
    {
        //[UniqueCityPhoto]
        public int CityId { get; set; }
        public string Photo { get; set; }
        public virtual City city { get; set; }
    }
}
