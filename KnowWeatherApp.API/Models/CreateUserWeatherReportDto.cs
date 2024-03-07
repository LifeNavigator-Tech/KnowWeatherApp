using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.API.Models;

public class CreateUserWeatherReportDto
{
    [Required]
    public List<CityDto> Cities { get; set; } = new List<CityDto>();
}
