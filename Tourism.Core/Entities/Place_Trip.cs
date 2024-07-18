namespace Tourism.Core.Entities
{
    public class Place_Trip : BaseEntity
    {
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public int TripId { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
