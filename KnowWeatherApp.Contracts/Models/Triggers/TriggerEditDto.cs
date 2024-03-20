using KnowWeatherApp.Contracts.Enums;
using System;
using System.Collections.Generic;

namespace KnowWeatherApp.Contracts.Models.Triggers
{
    public class TriggerEditDto
    {
        public string CityId { get; set; }
        public List<NotificationType> NotificationTypes { get; set; } = new List<NotificationType>();
        public EqualityType EqualityType { get; set; }
        public string Threshold { get; set; }
        public DateTime TimeToNotify { get; set; }
        protected DateTime TimeToNotifyUtc => TimeToNotify.ToUniversalTime();
        public DateTime TimeOfDayToCheck { get; set; }
        public WeatherFieldType Field { get; set; }
        public bool IsActive { get; set; }
    }
}
