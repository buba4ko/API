using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace BookLibrary.Rpc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            ////Force JSON responses on all requests
            //config.Formatters.Clear();
            //config.Formatters.Add(new JsonMediaTypeFormatter()
            //{
            //    SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            //    {
            //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            //    }
            //});


            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
