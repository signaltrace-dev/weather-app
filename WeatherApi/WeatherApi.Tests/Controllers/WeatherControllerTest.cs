using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApi.Controllers;
using WeatherApi.Filters;
using WeatherApi.Models;

namespace WeatherApi.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        [TestMethod]
        public void Get_Method_Returns_Forbidden()
        {
            WeatherController controller = new WeatherController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Get();

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Forbidden, "GET method should return a Forbidden response message.");
        }

        [TestMethod]
        public void Post_Method_Has_Authorize_Attribute() {
            WeatherController controller = new WeatherController();
            var type = controller.GetType();
            var methodInfo = type.GetMethod("Post", new Type[] { typeof(WeatherDataRequest) });
            var attributes = methodInfo.GetCustomAttributes(typeof(ApiKeyRequired), true);
            Assert.IsTrue(attributes.Any(), "No ApiKeyRequired attribute found on WeatherController Post method.");
        }

        [TestMethod]
        public async Task Null_Request_Returns_Bad_Request_Response() {
            WeatherController controller = new WeatherController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Post(null);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest, "Null parameter should return a Bad Request response message.");
        }

        [TestMethod]
        public async Task Invalid_Model_Returns_Bad_Request_Response()
        {
            WeatherController controller = new WeatherController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var wdRequest = new WeatherDataRequest();
            controller.Validate(wdRequest);
            var response = await controller.Post(wdRequest);

            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.BadRequest, "An invalid model should return a Bad Request response message.");
        }
    }
}
