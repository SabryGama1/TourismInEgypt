using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace Tourism.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {

            //Private Claims

            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Name , user.FName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.MobilePhone , user.PhoneNumber),
            };

            var userroles = await userManager.GetRolesAsync(user);

            foreach (var role in userroles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            //Secret Key

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secretKey"]));

            //Register Claims

            var token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:Duration"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
