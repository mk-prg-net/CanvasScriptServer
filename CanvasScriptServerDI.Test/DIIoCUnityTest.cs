using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Practices.Unity;

namespace CanvasScriptServerDI.Test
{
    [TestClass]
    public class DIIoCUnityTest
    {

        class UserManager
        {
            CanvasScriptServer.IUser user;

            public UserManager(CanvasScriptServer.IUser user)
            {
                this.user = user;
            }


            public void SetName(string NewName)
            {
                user.Name = NewName;
            }


            public string UserName
            {
                get
                {
                    return user.Name;
                }
            }

        }



        [TestMethod]
        public void PoorMansDITest()
        {
            // Abhängigkeit
            var Anton = new CanvasScriptServer.Mocks.User();

            // Abhängige
            var mgr = new UserManager(Anton);

            mgr.SetName("Anton");

            Assert.AreEqual(Anton.Name, "Anton");

        }


        [TestMethod]
        public void IoCDITest()
        {

            var IoC = new UnityContainer();

            // Registrieren der Typen, die für die Auflösung bestimmter Schnittstellen
            // benötigt werden

            IoC.RegisterType<CanvasScriptServer.IUser, CanvasScriptServer.Mocks.User>();


            var usr = IoC.Resolve<CanvasScriptServer.IUser>();
            var mgr = IoC.Resolve<UserManager>();
                      
            

            mgr.SetName("Anton");

            Assert.AreEqual("Anton", mgr.UserName);

        }
    }
}
