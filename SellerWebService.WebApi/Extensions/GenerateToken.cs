using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SellerWebService.DataLayer.Entities.Account;

namespace SellerWebService.WebApi.Extensions
{
    public static class GenerateToken
    {
        public static string GenerateJwtToken(this User user, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.MobilePhone, user.Mobile),
                new Claim(ClaimTypes.GivenName, user.FirstName ),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.UniqueCode.ToString("N"))
            };

            var token = new JwtSecurityToken(
                configuration.GetSection("AppSettings:Issuer").Value,
                configuration.GetSection("AppSettings:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
