namespace Tourism.Core.Helper.DTO
{
    public class ReturnFavoritesDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
        public string Link { get; set; }
        public string city { get; set; }
        public bool IsFav { get; set; }

        public IEnumerable<PhotoDTO> photos { get; set; }


    }
}
