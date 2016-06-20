using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using Microsoft.Practices.Unity;

namespace CanvasScriptServerDI.Test
{
    [TestClass]
    public class DIIoCUnityTest
    {

        interface IUserFactrory
        {
            CanvasScriptServer.IUser Create(string name);
        }


        class MockUserFactory : IUserFactrory
        {

            public CanvasScriptServer.IUser Create(string name)
            {
                return new CanvasScriptServer.Mocks.User(name);
            }
        }


        class UserRepository
        {
            List<CanvasScriptServer.IUser> users = new List<CanvasScriptServer.IUser>();

            IUserFactrory _factory;

            public UserRepository(IUserFactrory factory)
            {
                _factory = factory;
            }


            public void Create(string id)
            {
                var user = _factory.Create(id);
                users.Add(user);
                
            }

            IEnumerable<CanvasScriptServer.IUser> GetAll()
            {
                return users;
            }
        }



        [TestMethod]
        public void PoorMansDITest()
        {
            // Abhängigkeit
            var Anton = new CanvasScriptServer.Mocks.User("Anton");

            // Abhängige
            var mgr = new UserRepository(new MockUserFactory());

            mgr.Create("Anton");
            mgr.Create("Berta");

            

        }


        [TestMethod]
        public void IoCDITest()
        {

            var IoC = new UnityContainer();

            // Registrieren der Typen, die für die Auflösung bestimmter Schnittstellen
            // benötigt werden

            IoC.RegisterType<CanvasScriptServer.IUser, CanvasScriptServer.Mocks.User>();
            IoC.RegisterType<IUserFactrory, MockUserFactory>();

            var mgr = IoC.Resolve<UserRepository>();

            mgr.Create("Anton");
            mgr.Create("Berta");

        }
    }
}
