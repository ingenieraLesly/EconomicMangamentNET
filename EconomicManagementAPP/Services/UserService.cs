using EconomicManagementAPP.Interfaces;
using System.Security.Claims;

namespace EconomicManagementAPP.Services
{
    public class UserService : IUserServices
    {
        private readonly HttpContext httpContext;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            httpContext = httpContextAccessor.HttpContext;
        }
        public int GetUserId()
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                throw new ApplicationException("User is not authenticated");
            }

            var idClaim = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var id = int.Parse(idClaim.Value);
            return id;
        }
    }
}
