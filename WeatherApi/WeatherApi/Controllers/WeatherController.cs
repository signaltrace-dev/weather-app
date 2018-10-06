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
using System.Web.Http.Cors;

namespace WeatherApi.Controllers
{
    // This is not ideal! In an actual live environment, you would want to limit the origins/headers/methods 
    // based on the host name of the client application
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WeatherController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public string Get(string template)
        {
            return "This endpoint only allows POST requests.";
        }

        [HttpOptions]
        public HttpResponseMessage Post()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
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
            catch(WebException wex)
            {
                logger.Error(wex);
                if(wex.Status == WebExceptionStatus.ProtocolError && wex.Response != null)
                {
                    var response = (HttpWebResponse)wex.Response;
                    if(response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return Request.CreateErrorResponse(response.StatusCode, "Could not find anything with the provided parameters.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(response.StatusCode, "There was an error processing your request.");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There was an error processing your request.");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "There was an error processing your request.");
            }
        }
    }
}
