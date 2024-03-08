using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.API.Models;

public class AddCityToUserRequest
{
    [Required]
    public string CityId { get; set; }
}
