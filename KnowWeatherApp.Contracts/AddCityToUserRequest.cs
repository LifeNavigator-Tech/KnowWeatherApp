using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.Contracts;

public class AddCityToUserRequest
{
    [Required]
    public string CityId { get; set; }
}
