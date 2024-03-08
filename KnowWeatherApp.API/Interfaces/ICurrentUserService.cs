namespace KnowWeatherApp.API.Interfaces
{
    public interface ICurrentUserService
    {
        public string Email { get; }
        public string UserId { get; }

        void SetCurrentUserDetails(string email, string userId, bool isAuthenticated);
        void Reset();
    }
}
