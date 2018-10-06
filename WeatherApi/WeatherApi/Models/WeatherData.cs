using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Models
{
    public class WeatherData
    {
        public string CityName { get; set; }
        public string Description { get; set; }
        public int Humidity { get; set; }
        public decimal Temperature { get; set; }
        public string Zip { get; set; }
    }
}
