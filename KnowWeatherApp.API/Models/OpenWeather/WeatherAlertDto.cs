using KnowWeatherApp.API.Helpers;
using System.ComponentModel;
using System.Text.Json.Serialization;
using WeatherPass.FunctionApp.Helpers;

namespace KnowWeatherApp.API.Models.OpenWeather;

public class WeatherAlertDto
{
    [JsonPropertyName("sender_name")]
    public string SenderName { get; set; }
    public string Event { get; set; }
    [JsonPropertyName("start")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime End { get; set; }
    public string Description { get; set; }
}
