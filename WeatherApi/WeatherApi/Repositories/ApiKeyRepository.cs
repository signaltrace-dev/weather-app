using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Repositories
{
    /// <summary>
    /// A simple implementation of an API key repository. Ideally we would want
    /// something using a database to store valid keys along with a front end interface
    /// to request / manage keys.
    /// </summary>
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly List<string> _keys = new List<string>() {
            "McfNGT6AvJ7AVDLtg2zL7NM42jt5rhmf",
            "9ZMevNnB5owC4NNiOwzyQFzgRt7AuRQ8",
            "IhMrEoAFaRxmpg1GPUciHPFKy3oqG4zN"
        };

        public string Get(string key)
        {
            return _keys.Where(k => k.Equals(key)).FirstOrDefault();
        }

        public IEnumerable<string> GetAll()
        {
            return _keys;
        }
    }
}
