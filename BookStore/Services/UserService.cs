using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public string GetLoggedInUserId()
        {
            return _httpContextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User?.Identity.IsAuthenticated ?? false;
        }
    }
}
