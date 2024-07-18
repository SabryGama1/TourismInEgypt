using Tourism.Core.Entities;

namespace Tourism.Core.Helper.DTO
{
    public class placeOfTripDto
    {
        public int Id { get; set; }
        public string placeName { get; set; }

        public float Rating { get; set; }
        public virtual IEnumerable<PlacePhotos> Photos { get; set; }


    }
}
