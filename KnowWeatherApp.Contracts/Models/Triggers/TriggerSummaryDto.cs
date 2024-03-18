using KnowWeatherApp.Contracts.Enums;

namespace KnowWeatherApp.Contracts.Models.Triggers
{
    public class TriggerSummaryDto
    {
        public int TriggerId { get; set; }
        public required string City { get; set; }
        public string? NotificationTypes { get; set; }
        public string EqualityType { get; set; }
        public required string Threshold { get; set; }
        public TimeOnly TimeToNotify { get; set; }
        public TimeOnly TimeToNotifyUtc { get; set; }
        public TimeOnly TimeOfDayToCheck { get; set; }
        public string Field { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
