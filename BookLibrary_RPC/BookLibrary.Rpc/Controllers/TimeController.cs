using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BookLibrary.Rpc.Controllers
{
    public class CurrentTime
    {
        public CurrentTime()
        {
            this.Now = DateTime.Now.ToString();
        }
        public string Now { get; set; }
    }

    public class TimeController : ApiController
    {
        [HttpGet]
        [Route("api/time/CurrentTime")]
        public string CurrentTime()
        {
            // return a plain object - string
            return DateTime.Now.ToString();
        }

        [HttpGet]
        [Route("api/time/CurrentTime2")]
        public IHttpActionResult CurrentTime2()
        {
            // typical usage - return string
            return Ok(DateTime.Now.ToString());
        }

        [HttpGet]
        [Route("api/time/CurrentTime3")]
        public HttpResponseMessage CurrentTime3()
        {
            // used rarely - return some custom HttpResponseMessage
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.ServiceUnavailable,
                Content = new StringContent(DateTime.Now.ToString())
            };
        }

        [HttpGet]
        [Route("api/time/CurrentTime4")]
        public IHttpActionResult CurrentTime4()
        {
            // typical usage - return object
            CurrentTime currentTime = new CurrentTime();
            return Ok(currentTime);
        }

        [HttpGet]
        [Route("api/time/CurrentTime5")]
        public HttpResponseMessage CurrentTime5()
        {
            // very rarely - return custom HttpResponseMessage
            CurrentTime currentTime = new CurrentTime();
            var response = new HttpResponseMessage()
            {
                //Content = new StringContent($"{{\"currentTime\" :\"{DateTime.Now.ToString()}\"}}")
                Content = new StringContent(JsonConvert.SerializeObject(currentTime))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

    }
}
