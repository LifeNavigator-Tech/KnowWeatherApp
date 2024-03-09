namespace KnowWeatherApp.Common.Interfaces
{
    public interface ICurrentUserHelper
    {
        public string Email { get; }
        public string UserId { get; }
        public bool IsAuthenticated { get; }
        void SetCurrentUserDetails(string email, string userId, bool isAuthenticated);
    }
}
