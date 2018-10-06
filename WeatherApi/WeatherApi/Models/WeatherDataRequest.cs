using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Models
{
    public class WeatherDataRequest
    {
        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters.")]
        public string Location { get; set; }
        public DateTime Timestamp { get; }

        public WeatherDataRequest()
        {
            Timestamp = DateTime.Now;
        }
    }
}
