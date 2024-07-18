namespace Tourism.Core.Helper.DTO
{
    public class PlaceSpecificDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
        public float Rating { get; set; }


        public string City { get; set; }


        public virtual IEnumerable<PhotoDTO> Photos { get; set; }

    }
}
