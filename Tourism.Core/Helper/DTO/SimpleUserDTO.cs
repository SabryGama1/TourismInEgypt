using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class SimpleUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? ImgURL { get; set; }

        public string DisplayName { get; set; }
    }
}
