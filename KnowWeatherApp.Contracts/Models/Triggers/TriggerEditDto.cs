using KnowWeatherApp.Contracts.Enums;

namespace KnowWeatherApp.Contracts.Models.Triggers
{
    public class TriggerEditDto
    {
        public required string CityId { get; set; }
        public List<NotificationType> NotificationTypes { get; set; } = new List<NotificationType>();
        public EqualityType EqualityType { get; set; }
        public required string Threshold { get; set; }
        public TimeOnly TimeToNotify { get; set; }
        protected TimeOnly TimeToNotifyUtc => TimeOnly.FromTimeSpan(TimeToNotify.ToTimeSpan());
        public TimeOnly TimeOfDayToCheck { get; set; }
        public WeatherFieldType Field { get; set; }
        public bool IsActive { get; set; }
    }
}
