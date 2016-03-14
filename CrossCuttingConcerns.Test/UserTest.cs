using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;


namespace CrossCuttingConcerns.Test
{
    [TestClass]
    public class UserTest
    {
        UnityContainer ioc;

        [TestInitialize]
        public void InitTest()
        {
            ioc = new UnityContainer();

            // Interception- Erweiterung registrieren
            ioc.AddNewExtension<Interception>();
            ioc.RegisterType<CanvasScriptServer.IUser, CanvasScriptServer.Mocks.User>
                (
                    new InjectionConstructor("unbekannt"),

                    // Interceptor Chain einleiten
                    new Interceptor<InterfaceInterceptor>(),

                    // definieren der Inteceptoren
                    new InterceptionBehavior<LoggingInterceptionBehavior>()
                );

            ioc.RegisterType<CanvasScriptServer.UserRepository, CanvasScriptServer.Mocks.UsersRepository>
                (
                // Interceptor Chain einleiten
                    new Interceptor<VirtualMethodInterceptor>(),

                    // definieren der Inteceptoren
                    new InterceptionBehavior<LoggingInterceptionBehavior>()
                );
        }

        [TestCleanup]
        public void Cleanup()
        {
            ioc.Dispose();
        }

        [TestMethod]
        public void UserLoggingTest()
        {
            var newUser = ioc.Resolve<CanvasScriptServer.IUser>();

            newUser.Name = "Anton";

            newUser.ToString();

            var userRepo = ioc.Resolve<CanvasScriptServer.UserRepository>();

            var newUser2 = userRepo.CreateBo();
            newUser2.Name = "Berta";

        }
    }
}
