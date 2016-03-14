using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Achtung: Namespaces von Unity hier deklarieren, das Erweiterungsmethoden
// wie RegisterType<...>() existieren, die sonst unsichtbar bleiben
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;


namespace CanvasScriptServer.MVC.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {

        Microsoft.Practices.Unity.UnityContainer IoCContainer;
        CanvasScriptServer.MVC.Controllers.UserController ctrl;

        [TestInitialize]
        public void TestInit()
        {
            // IoCcontainer anlegen
            IoCContainer = new Microsoft.Practices.Unity.UnityContainer();

            // Typen im IOC Container registrieren

            // Achtung: Mock.User hat zwei Konstruktoren. Mittels Attribut InjectionController
            // wird einer für die DI- ausgewählt
            IoCContainer.RegisterType<IUser, Mocks.User>(new InjectionConstructor("unbekannt"));

            IoCContainer.RegisterType<ICanvasScript, Mocks.CanvasScript>();

            // Achtung: die Repositorys sollen als Singletons ausgewählt werden. Dazu ist ein spezieller 
            // Lifetime- Manager zu setzen
            // https://msdn.microsoft.com/de-de/library/dn178463(v=pandp.30).aspx#_Lifetime_Management
            IoCContainer.RegisterType<UserRepository, Mocks.UsersRepository>(new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());
            IoCContainer.RegisterType<CanvasScriptRepository, Mocks.CanvasScriptsRepository>(new Microsoft.Practices.Unity.ContainerControlledLifetimeManager());

            IoCContainer.RegisterType<ICanvasScriptServerUnitOfWork, Mocks.CanvasScriptServerUnitOfWork>();


        }

        [TestCleanup]
        public void Cleanup()
        {
            IoCContainer.Dispose();
        }

        [TestMethod]
        public void MehrdeutigeKonstruktorenAufloesenTest()
        {
            var user = IoCContainer.Resolve<IUser>();
            Assert.IsNotNull(user, "IUser sollte vom IoC auflösbar sein");
            Assert.IsInstanceOfType(user, typeof(Mocks.User), "IUser sollte als Mocks.User aufgelöst werden");
        }

        /// <summary>
        /// UserRepository wurde mit einem Lifetime- Manager registriert, der das Objekt als Singleton bereitstellt
        /// https://msdn.microsoft.com/de-de/library/dn178463(v=pandp.30).aspx#_Lifetime_Management
        /// </summary>
        [TestMethod]
        public void SingeltonInstanziierungTest()
        {
            var userRepo = IoCContainer.Resolve<UserRepository>();
            Assert.IsNotNull(userRepo, "UserRepository sollte vom IoC auflösbar sein");
            Assert.IsInstanceOfType(userRepo, typeof(Mocks.UsersRepository), "UserRepository sollte als Mocks.UserRepository aufgelöst werden");
            
            var newUser = userRepo.CreateBo();
            newUser.Name = "Anton";
            Assert.AreEqual(userRepo.CountAllBo(), 0, "userRepo sollte noch keine Einträge haben, solange nicht AddToEntityCollection aufgerufen wurde");

            userRepo.AddToCollection(newUser);
            Assert.AreEqual(userRepo.CountAllBo(), 1, "userRepo sollte einen Eintrage nach AddToEntityCollection haben");

            var userRepo2 = IoCContainer.Resolve<UserRepository>();
            Assert.AreEqual(userRepo2.CountAllBo(), 1, "userRepo2 sollte bereits einen Eintrag unmittelbar nach der Instanziierung haben, da IoC UserRepository als Singleton verwaltet");
            

        }
    }
}
