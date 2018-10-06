﻿using System;

namespace WeatherApi.Models
{
    public class WeatherData
    {
        public string CityName { get; set; }
        public string Description { get; set; }
        public int Humidity { get; set; }
        public double Temperature { get; set; }
        public int TemperatureRounded {
            get
            {
                return Convert.ToInt32(Temperature);
            }
        }
        public string Zip { get; set; }

    }
}
