using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class ContactDTO
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
