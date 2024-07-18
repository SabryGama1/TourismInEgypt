using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IChangePassword : IGenericRepository<ResetPassword>
    {
        Task<ResetPassword> GetPasswordofOTP(int otp, string email);
    }
}
