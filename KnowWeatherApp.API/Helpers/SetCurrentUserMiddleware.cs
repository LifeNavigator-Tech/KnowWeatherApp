using KnowWeatherApp.API.Interfaces;
using System.Security.Claims;

namespace KnowWeatherApp.API.Helpers
{
    public class SetCurrentUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICurrentUserService currentUserService;

        public SetCurrentUserMiddleware(RequestDelegate next, ICurrentUserService currentUserService)
        {
            _next = next;
            this.currentUserService = currentUserService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var email = context.User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
                this.currentUserService.SetCurrentUserDetails(email, userId, true);
            }
            else
            {
                this.currentUserService.Reset();
            }
            await _next(context);
        }
    }
}
