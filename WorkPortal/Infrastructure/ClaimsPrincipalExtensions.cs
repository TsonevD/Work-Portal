using System.Security.Claims;
using WorkPortal.Areas.Admin;

namespace WorkPortal.Infrastructure
{
    using static AdminConstants;
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier);

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);

    }
}
