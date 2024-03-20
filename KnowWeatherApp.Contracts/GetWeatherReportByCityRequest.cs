using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.Contracts
{
    public class GetWeatherReportByCityRequest
    {
        [Required]
        public string CityId { get; set; }
    }
}
