using System.ComponentModel.DataAnnotations;

namespace TourismMVC.ViewModels
{
    public class RoleEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}
