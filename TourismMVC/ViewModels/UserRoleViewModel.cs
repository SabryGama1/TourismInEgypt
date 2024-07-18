namespace TourismMVC.ViewModels
{
    public class UserRoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<RoleEditViewModel> Roles { get; set; }
    }
}
