using Microsoft.AspNetCore.Identity;
using Tourism.Core.Entities;

namespace Tourism.Core.Repositories.Contract
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
