using System.Security.Claims;
using System.Security.Principal;
using SellerWebService.DataLayer.DTOs;
using SellerWebService.DataLayer.DTOs.Identity;

namespace SellerWebService.WebApi.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserUniqueCode(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                if (data != null) return Guid.Parse(data.Value);
            }

            return default(Guid);
        }
        public static Guid GetUserUniqueCode(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.GetUserUniqueCode();
        } 
        public static Guid GetStoreCode(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.SerialNumber);
                if (data != null) return Guid.Parse(data.Value);
            }

            return default(Guid);
        }
        public static Guid GetStoreCode(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.GetStoreCode();
        }
        public static UserClaimsStore GetCurrentUser(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var userClaims = claimsPrincipal.Claims;

                return new UserClaimsStore
                {
                    UniqueCode = Guid.Parse(claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value),
                    Mobile = claimsPrincipal.Claims.SingleOrDefault(x=> x.Type == ClaimTypes.MobilePhone)?.Value,
                    FirstName = claimsPrincipal.Claims.SingleOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = claimsPrincipal.Claims.SingleOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = claimsPrincipal.Claims.SingleOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                    SerialNumber = claimsPrincipal.Claims.SingleOrDefault(o => o.Type == ClaimTypes.SerialNumber)?.Value
                };
            }
            return null;
        }
    }
}

