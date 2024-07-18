namespace TourismMVC.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public IList<string> Roles { get; set; }


    }
}
