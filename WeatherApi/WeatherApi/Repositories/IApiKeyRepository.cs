using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Repositories
{
    public interface IApiKeyRepository
    {
        IEnumerable<string> GetAll();
        string Get(string key);
    }
}
