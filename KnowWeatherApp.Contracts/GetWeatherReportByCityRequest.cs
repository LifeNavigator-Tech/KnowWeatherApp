using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.Contracts
{
    public class GetWeatherReportByCityRequest
    {
        [Required]
        public required string CityId { get; set; }
    }
}
