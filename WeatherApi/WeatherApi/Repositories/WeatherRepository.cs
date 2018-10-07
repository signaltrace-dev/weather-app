using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Repositories
{
    /// <summary>
    /// Test implementation of an IWeatherRepository using static data.
    /// </summary>
    public class WeatherRepository : IWeatherRepository
    {
        public IEnumerable<WeatherData> Get(WeatherDataRequest request)
        {
            return new List<WeatherData>() {
                new WeatherData(){CityName = "Foo", Description = "Bar", Humidity = 90, Temperature = 73.4},
                new WeatherData(){CityName = "Bar", Description = "Bar", Humidity = 67, Temperature = 63.4},
                new WeatherData(){CityName = "Baz", Description = "Bar", Humidity = 10, Temperature = 90.4},
            }.Where(w => w.CityName.Equals(request.Location, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
