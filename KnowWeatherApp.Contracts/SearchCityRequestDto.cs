using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.Contracts;

public class SearchCityRequestDto
{
    [Required]
    [MinLength(2)]
    public required string Name { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
