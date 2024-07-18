namespace Tourism.Core.Helper.DTO
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public bool IsFav { get; set; }
        public string Link { get; set; }
        public string Phone { get; set; }

        public string Category { get; set; }
        public string City { get; set; }


        public virtual IEnumerable<PhotoDTO> Photos { get; set; }
        public virtual IEnumerable<ReviewDTO> reviews { get; set; }

    }
}
