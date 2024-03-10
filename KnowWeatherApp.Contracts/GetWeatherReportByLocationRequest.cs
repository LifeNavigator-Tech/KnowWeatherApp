using System.ComponentModel.DataAnnotations;

namespace KnowWeatherApp.Contracts
{
    public class GetWeatherReportByLocationRequest
    {
        public GetWeatherReportByLocationRequest()
        {
        }

        public GetWeatherReportByLocationRequest(double lat, double lon)
        {
            this.Lat = lat;
            this.Lon = lon;
        }

        [Required]
        public double Lat { get; set; }
        [Required]
        public double Lon { get; set; }
    }
}
