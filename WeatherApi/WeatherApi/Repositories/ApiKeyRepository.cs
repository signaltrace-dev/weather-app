using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Repositories
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        public string Get(string key)
        {
            return GetAll().Where(k => k.Equals(key)).FirstOrDefault();
        }

        public IEnumerable<string> GetAll()
        {
            return new List<string>() {
                "McfNGT6AvJ7AVDLtg2zL7NM42jt5rhmf",
                "9ZMevNnB5owC4NNiOwzyQFzgRt7AuRQ8",
                "IhMrEoAFaRxmpg1GPUciHPFKy3oqG4zN"
            };
        }
    }
}
