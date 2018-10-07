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
    /// <summary>
    /// An implementation of an IWeatherRepository that fetches weather data from the OpenWeatherMap API.
    /// </summary>
    public class WeatherApiRepository : IWeatherRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public string ApiKey { get; private set; }
        public string ApiEndpoint { get; private set; }

        public IEnumerable<WeatherData> Get(WeatherDataRequest dataRequest)
        {
            var data = new List<WeatherData>();

            var apiRequest = CreateApiRequest(dataRequest);

            using (var response = apiRequest.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();

                    data = TransformData(content);
                }
            }

            return data;
        }

        public async Task<IEnumerable<WeatherData>> GetAsync(WeatherDataRequest dataRequest)
        {
            var data = new List<WeatherData>();

            var apiRequest = CreateApiRequest(dataRequest);

            using (var response = await apiRequest.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    var content = await reader.ReadToEndAsync();

                    data = TransformData(content);
                }
            }
 
            return data;
        }

        private HttpWebRequest CreateApiRequest(WeatherDataRequest request)
        {
            // API uses different query params depending on the type of search
            var queryType = request.LocationIsZip ? "zip" : "q";

            // Create request from weather API URL - note that we're passing in Imperial for the units param, but 
            // this would likely be something that we could break out into a configuration param.
            var apiRequest = (HttpWebRequest)WebRequest.Create(String.Format("{0}?{1}={2}&APPID={3}&units=imperial", ApiEndpoint, queryType, request.Location, ApiKey));

            return apiRequest;
        }

        private List<WeatherData> TransformData(string content)
        {
            var data = new List<WeatherData>();

            if (!string.IsNullOrEmpty(content))
            {
                // Deserialize the reponse to C# objects
                var weather = JsonConvert.DeserializeObject<WeatherApiData>(content);

                // A valid response should at least have an ID
                if (weather != null && weather.id > 0)
                {
                    // A response can contain multiple "weather" objects. For now, let's just focus on the first one.
                    var weatherItem = weather.weather.FirstOrDefault();
                    var description = weatherItem != null ? weatherItem.description : "";
                    var icon = weatherItem != null ? weatherItem.icon : "";
                    var iconUrl = !string.IsNullOrEmpty(Settings.WeatherApi.WeatherApiIconUrl) && !string.IsNullOrEmpty(icon) ? String.Format("{0}/{1}.png", Settings.WeatherApi.WeatherApiIconUrl, icon) : "";

                    data.Add(new WeatherData() { CityName = weather.name, Description = description, Humidity = weather.main.humidity, IconUrl = iconUrl, Temperature = weather.main.temp });
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
