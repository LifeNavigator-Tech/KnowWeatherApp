using KnowWeatherApp.Contracts.Enums;
using System;

namespace KnowWeatherApp.Contracts.Models.Triggers
{
    public class TriggerSummaryDto
    {
        public int TriggerId { get; set; }
        public string City { get; set; }
        public string NotificationTypes { get; set; }
        public string EqualityType { get; set; }
        public string Threshold { get; set; }
        public DateTime TimeToNotify { get; set; }
        public DateTime TimeToNotifyUtc { get; set; }
        public DateTime TimeOfDayToCheck { get; set; }
        public string Field { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
