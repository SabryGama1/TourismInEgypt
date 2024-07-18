namespace TourismMVC.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? PhotoFile { get; set; }
    }
}
