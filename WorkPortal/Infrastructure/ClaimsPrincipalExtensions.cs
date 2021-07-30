using System.Security.Claims;
using GlobalConstants;
using WorkPortal.Areas.Admin;

namespace WorkPortal.Infrastructure
{
    using static AdminConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
