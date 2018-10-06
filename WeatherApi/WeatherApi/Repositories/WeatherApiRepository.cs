using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherApi.Models;
using NLog;

namespace WeatherApi.Repositories
{
    public class WeatherApiRepository : IWeatherRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string ApiKey { get; private set; }
        public string ApiEndpoint { get; private set; }

        public IEnumerable<WeatherData> Get(WeatherDataRequest request)
        {
            var data = new List<WeatherData>();

            // API uses different query params depending on the type of search
            var queryType = request.LocationIsZip ? "zip" : "q";

            var apiRequest = (HttpWebRequest)WebRequest.Create(String.Format("{0}?{1}={2}&APPID={3}&units=imperial", ApiEndpoint, queryType, request.Location, ApiKey));

            using (var response = apiRequest.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();

                    if (!string.IsNullOrEmpty(content))
                    {
                        var weather = JsonConvert.DeserializeObject<WeatherApiData>(content);
                        if (weather != null && weather.id > 0)
                        {
                            var description = weather.weather.Count > 0 ? weather.weather.FirstOrDefault().description : "";
                            data.Add(new WeatherData() { CityName = weather.name, Description = description, Humidity = weather.main.humidity, Temperature = weather.main.temp });
                        }
                    }
                }
            }

            return data;
        }

        public async Task<IEnumerable<WeatherData>> GetAsync(WeatherDataRequest request)
        {
            var data = new List<WeatherData>();

            // API uses different query params depending on the type of search
            var queryType = request.LocationIsZip ? "zip" : "q";

            var apiRequest = (HttpWebRequest)WebRequest.Create(String.Format("{0}?{1}={2}&APPID={3}&units=imperial", ApiEndpoint, queryType, request.Location, ApiKey));

            using (var response = await apiRequest.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    var content = await reader.ReadToEndAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        var weather = JsonConvert.DeserializeObject<WeatherApiData>(content);
                        if (weather != null && weather.id > 0)
                        {
                            var description = weather.weather.Count > 0 ? weather.weather.FirstOrDefault().description : "";
                            data.Add(new WeatherData() { CityName = weather.name, Description = description, Humidity = weather.main.humidity, Temperature = weather.main.temp });
                        }
                    }
                }
            }
 
            return data;
        }

        public WeatherApiRepository(string apiEndpoint, string apiKey)
        {
            ApiEndpoint = apiEndpoint;
            ApiKey = apiKey;
        }
    }
}
