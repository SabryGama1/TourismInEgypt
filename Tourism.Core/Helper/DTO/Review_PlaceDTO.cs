namespace Tourism.Core.Helper.DTO
{
    public class Review_PlaceDTO
    {
        public string? Message { get; set; }

        public float Rating { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
