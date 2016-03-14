//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptsServer.MVC
//  Name..........: UnityDependencyResolver.cs
//  Aufgabe/Fkt...: Implementierung des IDependencyResolver- Interfaces 
//                  für den Unity IoC für WebAPI
//                  Quelle: http://www.asp.net/web-api/overview/advanced/dependency-injection
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;


namespace CanvasScriptServer.MVC {

    public class UnityDependencyResolverWebApi : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityDependencyResolverWebApi(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityDependencyResolverWebApi(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}