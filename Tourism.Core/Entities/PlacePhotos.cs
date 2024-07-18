namespace Tourism.Core.Entities
{
    public class PlacePhotos : BaseEntity
    {

        public int PlaceId { get; set; }
        public string Photo { get; set; }

        public virtual Place Place { get; set; }

    }
}
