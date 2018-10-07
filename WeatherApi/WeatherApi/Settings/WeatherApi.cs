using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WeatherApi.Settings
{
    public static class WeatherApi
    {
        public static string WeatherApiEndPoint
        {
            get {
                return ConfigurationManager.AppSettings["weatherApiEndpoint"];
            }
        }

        public static string WeatherApiIconUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["weatherApiIconUrl"];
            }
        }

        public static string WeatherApiKey
        {
            get {
                return ConfigurationManager.AppSettings["weatherApiKey"];
            }
        }
    }
}