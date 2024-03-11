using KnowWeatherApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowWeatherApp.Domain.Entities
{
    public class Trigger
    {
        [Key]
        public int TriggerId { get; set; }
        [ForeignKey(nameof(City))]
        public required string CityId { get; set; }
        public required virtual City City { get; set; }
        [ForeignKey(nameof(AppUser))]
        public required string UserId { get; set; }
        public required virtual AppUser User { get; set; } 
        public List<NotificationType> NotificationTypes { get; set; } = new List<NotificationType>();
        public EqualityType EqualityType { get; set; }
        public required string Threshold { get; set; }
        public TimeOnly TimeToNotify { get; set; }
        public TimeOnly TimeToNotifyUtc { get; set; }
        public TimeOnly TimeOfDayToCheck { get; set; }
        public WeatherFieldType Field { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset Modified { get; set; }
    }
}
