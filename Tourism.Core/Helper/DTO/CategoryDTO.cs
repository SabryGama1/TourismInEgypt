namespace Tourism.Core.Helper.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public virtual ICollection<PlaceDTO> Places { get; set; }
    }
}
