using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntroUnitTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            // Testumgebung aufbauen
            var unitOfWork = new CanvasScriptServer.Mocks.CanvasScriptServerUnitOfWork();

            // Zu  testende Methode ausführen
            unitOfWork.Users.CreateBoAndAdd("Anton");

            unitOfWork.SubmitChanges();

            // Ergebnis prüfen
            var Anton = unitOfWork.Users.GetBo("Anton");

            Assert.IsNotNull(Anton);
            Assert.AreEqual("Anton", Anton.Name);
        }
    }
}
