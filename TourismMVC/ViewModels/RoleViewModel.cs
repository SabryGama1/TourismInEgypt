using System.ComponentModel.DataAnnotations;

namespace TourismMVC.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string RoleName { get; set; }
    }
}
