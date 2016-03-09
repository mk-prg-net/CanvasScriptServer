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

            container.RegisterType<UserCo.ICrud<IUser, string>, Mocks.UsersRepository>();
            container.RegisterType<UserCo.IFilterAndSort<IUser>, Mocks.UsersRepository>();
            container.RegisterType<UserCo.IGetBo<IUser, string>, Mocks.UsersRepository>();

            container.RegisterType<CanvasScriptServer.UserRepository, Mocks.UsersRepository>();
            container.RegisterType<CanvasScriptServer.CanvasScriptRepository, Mocks.CanvasScriptsRepository>();

            container.RegisterType<CanvasScriptServer.ICanvasScriptServerUnitOfWork, CanvasScriptServer.Mocks.CanvasScriptServerUnitOfWork>();

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
