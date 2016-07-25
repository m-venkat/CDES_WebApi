using CDES_WebApi.DI.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CDES_WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.DependencyResolver = new UnityDependencyResolver(BootStrapDI.Container);
            // Web API routes
          

            //config.Routes.MapHttpRoute(
            //    name: "Index",
            //    routeTemplate: "",
            //    defaults: new { controller = "Values", action = "Index" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Values" }
            );
            
        }
    }
}
