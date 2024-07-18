namespace Tourism.Core.Entities
{
    public class Category : BaseEntity
    {

        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public virtual IEnumerable<Place> Places { get; set; }

    }
}
