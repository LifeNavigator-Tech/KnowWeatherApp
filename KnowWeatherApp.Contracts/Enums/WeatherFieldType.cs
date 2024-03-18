using System.ComponentModel;

namespace KnowWeatherApp.Contracts.Enums
{
    public enum WeatherFieldType
    {
        Pressure,
        [Description("Dew point")]
        DewPoint,
        [Description("Wind speed")]
        WindSpeed,
        Uvi,
        Humidity,
        [Description("Wind gust")]
        WindGust,
        [Description("Temperature in the morning")]
        TempMorning,
        [Description("Temperature in the evening")]
        TempEvening,
        [Description("Temperature at night")]
        TempNight,
        [Description("Daytime temperature")]
        TempDay,
        [Description("Minimum temperature")]
        TempMin,
        [Description("Maximum temperature")]
        TempMax,
        [Description("Feels like in the morning temperature")]
        FeelsLikeMorning,
        [Description("Feels like in the morning evening")]
        FeelsLikeEvening,
        [Description("Feels like at night temperature")]
        FeelsLikeNight,
        [Description("Feels like daytime temperature")]
        FeelsLikeDay,
        Clouds,
        Precipitation,
        Rain,
        [Description("Sunrise")]
        SunRise,
        [Description("Sunset")]
        SunSet
    }
}
