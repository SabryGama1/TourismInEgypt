using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository.Data;

namespace Tourism.Repository.Repository
{
    public class ChangePassword : GenericRepository<ResetPassword>, IChangePassword
    {
        private readonly TourismContext _context;

        public ChangePassword(TourismContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ResetPassword> GetPasswordofOTP(int otp, string email)
        {
            var User = _context.Passwords.Where(x => x.Email == email && x.OTP == otp)
                 .OrderByDescending(x => x.Date).FirstOrDefault();
            return User;
        }
    }
}
