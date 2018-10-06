using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Repositories
{
    interface IWeatherRepository
    {
        IEnumerable<WeatherData> Get(WeatherDataRequest request);
    }
}
