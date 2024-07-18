//using Microsoft.AspNetCore.Authentication;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tourism.Core.Helper.DTO
{
    public class ExternalLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
        public string ReturnURL { get; set; }
        //public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
