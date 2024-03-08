using KnowWeatherApp.API.Interfaces;

namespace KnowWeatherApp.API.Helpers
{
    public class CurrentUserService : ICurrentUserService
    {
        public void SetCurrentUserDetails(string email,  string userId, bool isAuthenticated)
        {
            this.Email = email;
            this.UserId = userId;
            this.IsAuthenticated = isAuthenticated;
        }

        public void Reset()
        {
            this.Email = string.Empty;
            this.UserId = string.Empty;
            this.IsAuthenticated = false;
        }
        public string? Email { get; private set; }
        public string? UserId { get; private set; }
        public bool IsAuthenticated { get; set; } = false;
    }
}
