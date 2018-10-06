using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WeatherApi.Models
{
    public class WeatherDataRequest
    {

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters.")]
        public string Location { get; set; }

        public bool LocationIsZip
        {
            get
            {
                var regex = new Regex(@"\d{5}(-\d{4})?");
                var match = regex.Match(Location);
                return match.Success;
            }
        }
        public DateTime Timestamp { get; }

        public WeatherDataRequest()
        {
            Timestamp = DateTime.Now;
        }
    }
}
