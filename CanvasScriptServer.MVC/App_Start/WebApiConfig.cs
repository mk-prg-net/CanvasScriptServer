using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Microsoft.Practices.Unity;

using UserCo = mko.BI.Repositories.Interfaces;

namespace CanvasScriptServer.MVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var container = new UnityContainer();

            container.RegisterType<CanvasScriptServer.ICanvasScriptServerUnitOfWork, CanvasScriptServer.DB.CanvasScriptDBContainer>();

            config.DependencyResolver = new UnityDependencyResolverWebApi(container);            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

    }
}
