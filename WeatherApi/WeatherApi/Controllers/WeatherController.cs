using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeatherApi.Models;
using WeatherApi.Repositories;
using NLog;
using WeatherApi.Filters;
using System.Threading.Tasks;

namespace WeatherApi.Controllers
{
    public class WeatherController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public string Get(string template)
        {
            return "This endpoint only allows POST requests.";
        }

        [HttpPost]
        [ApiKeyRequired]
        public async Task<HttpResponseMessage> Post(WeatherDataRequest request)
        {
            try
            {
                // Ensure that we have a valid request
                if(request == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You must provide a valid request body.");
                }
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                var repo = new WeatherApiRepository(Settings.WeatherApi.WeatherApiEndPoint, Settings.WeatherApi.WeatherApiKey);
                var weatherData = await repo.GetAsync(request);

                return Request.CreateResponse(HttpStatusCode.OK, weatherData);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There was an error processing your request.");
            }
        }
    }
}
