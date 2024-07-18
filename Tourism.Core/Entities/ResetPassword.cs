using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Core.Entities
{
    public class ResetPassword : BaseEntity
    {

        public string Email { get; set; }

        public string Token { get; set; }
        public int OTP { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
