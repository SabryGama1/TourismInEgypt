namespace Tourism.Core.Helper.DTO
{
    public class CategorySpecificWithPlaceDTO
    {

        public int id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public virtual IEnumerable<PlaceSpecificDTO> Places { get; set; }

    }
}
