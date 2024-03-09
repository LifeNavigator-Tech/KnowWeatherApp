using KnowWeatherApp.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KnowWeatherApp.API.Helpers
{
    public class CurrentUserHelper : ICurrentUserHelper
    {
        public CurrentUserHelper(IHttpContextAccessor httpContext)
        {
            var context = httpContext.HttpContext;
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var email = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email.ToString())?.Value ?? string.Empty;
                var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString())?.Value ?? string.Empty;
                this.SetCurrentUserDetails(email, userId, true);
            }
        }
        public void SetCurrentUserDetails(string email, string userId, bool isAuthenticated)
        {
            this.Email = email;
            this.UserId = userId;
            this.IsAuthenticated = isAuthenticated;
        }
        public string? Email { get; private set; }
        public string? UserId { get; private set; }
        public bool IsAuthenticated { get; set; } = false;
    }
}
