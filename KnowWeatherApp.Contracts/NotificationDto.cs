using System;

namespace KnowWeatherApp.Contracts
{
    public class NotificationDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Link { get; set; }
        public DateTime EventDate { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
