﻿using KnowWeatherApp.API.Entities;

namespace KnowWeatherApp.API.Models;

public class CityDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public double Lon { get; set; }
    public double Lat { get; set; }
}