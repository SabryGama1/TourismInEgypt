namespace Tourism.Core.Helper.DTO
{
    public class TripDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public List<placeOfTripDto> places { get; set; } = new List<placeOfTripDto>();
    }
}
