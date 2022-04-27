using System.Security.Claims;
using System.Security.Principal;

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
    }
}

