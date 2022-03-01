using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CRM.Dynamics.Handlers;

namespace CRM.Dynamics
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //InicializadorParametros.Instance.ObtenerParametrosGenerales();
            //Autenticacion para el acceso por medio de Token JWT
            config.MessageHandlers.Add(new AuthHandler());
            
        }
    }
}
