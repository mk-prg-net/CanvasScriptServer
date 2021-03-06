﻿using System;
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

            // Spezifischen Xml- Formatter für ICanvasScript registrieren
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.Add(new MyWebApi.CanvasScriptServerXmlFormatterBuffered());
            GlobalConfiguration.Configuration.Formatters.Add(new MyWebApi.CanvasScriptHtmlFormaterBuffered());


            // Dependency- Injection konfigurieren 
            // DI- Container ist Unit- Framework
            var container = new UnityContainer();

            container.RegisterType<CanvasScriptServer.ICanvasScriptServerUnitOfWork, CanvasScriptServer.DB.CanvasScriptDBContainer>();
            //container.RegisterType<CanvasScriptServer.ICanvasScriptServerUnitOfWork, CanvasScriptServer.Mocks.CanvasScriptServerUnitOfWork>();

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
