namespace KnowWeatherApp.API.Helpers
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder SetCurrentUser(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetCurrentUserMiddleware>();
        }
    }
}
