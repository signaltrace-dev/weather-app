using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WeatherApi.Repositories;
using NLog;

namespace WeatherApi.Filters
{
    public class ApiKeyRequired : AuthorizationFilterAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            var authHeaders = actionContext.Request.Headers.Authorization;
            var apiKey = authHeaders != null && authHeaders.Scheme.ToLower() == "apikey" ? authHeaders.Parameter : "";

            string message = "";
            if (!string.IsNullOrEmpty(apiKey))
            {
                if (ApiKeyIsValid(apiKey))
                {
                    return;
                }
                else
                {
                    message = "API key is not valid.";
                }
            }
            else
            {
                message = "API key is required.";
            }

            actionContext.Response =
                actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    message);
        }


        private bool ApiKeyIsValid(string apiKey)
        {
            bool isValid = false;

            try
            {
                var repo = new ApiKeyRepository();
                return !string.IsNullOrEmpty(repo.Get(apiKey));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            return isValid;
        }

    }
}