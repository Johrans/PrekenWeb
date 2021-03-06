﻿using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        { 
            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());

            config.Routes.MapHttpRoute("PreekNieuwDutchApi", "api/Preek/Nieuw", new { controller = "Sermons", action = "New"});
            config.Routes.MapHttpRoute("PreekDutchApi", "api/Preek", new { controller = "Sermons", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithActionAndId",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: null,
                //defaults: new { id = RouteParameter.Optional },
                constraints: new { action= @"\w+", id = @"\d*" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithId",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }//,
                //constraints: new { id = @"\d*" }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            //var apiDescriptions = config.Services.GetApiExplorer().ApiDescriptions;

            var trustedHostNames = new[]
            {
                "https://prekenweb.nl",
                "https://sermonweb.org",
                "https://www.sermonweb.org",
                "https://www.prekenweb.nl",
                "https://test.prekenweb.nl",
                "https://dev.prekenweb.nl"
            };

            config.EnableCors(new EnableCorsAttribute(string.Join(",", trustedHostNames), "*", "GET, POST, OPTIONS, PUT, DELETE"));
        }
    }
}