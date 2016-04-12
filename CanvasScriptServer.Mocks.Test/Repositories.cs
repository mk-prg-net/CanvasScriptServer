using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace CanvasScriptServer.Mocks.Test
{
    [TestClass]
    public class RepositoriesTests
    {
        [TestMethod]
        public void UserRepositoryTest()
        {

            var unitOfWorks = new Mocks.CanvasScriptServerUnitOfWork();   
            
            unitOfWorks.createUser("Anton");
            unitOfWorks.createUser("Berta");
            Assert.AreEqual(0, unitOfWorks.Users.CountAllBo(), "Es wurde keine Eintrag in Users erwartet");            

            unitOfWorks.SubmitChanges();

            Assert.AreEqual(2, unitOfWorks.Users.CountAllBo(), "Es wurden zwei Einträge in Users erwartet");

            var Anton = unitOfWorks.Users.GetBo("Anton");
            var Berta = unitOfWorks.Users.GetBo("Berta");

            unitOfWorks.createScript(Anton.Name, "T1");
            unitOfWorks.SubmitChanges();
            Assert.AreEqual(1, Anton.Scripts.Count(), "Anton sollte ein Skript besitzen");

            var T1 = unitOfWorks.Scripts.GetBoBuilder(CanvasScriptKey.Create(Anton.Name, "T1"));
            T1.setScript("[]");
            unitOfWorks.createScript(Berta.Name, "T1");            
            unitOfWorks.SubmitChanges();

            Assert.AreEqual(1, Anton.Scripts.Count(), "Anton sollte ein Skript besitzen");
            Assert.AreEqual("[]", Anton.Scripts.First().ScriptAsJson, "Antons Script sollte leer sein");
            Assert.AreEqual(1, Berta.Scripts.Count(), "Berta sollte ein Skript besitzen");


            unitOfWorks.createScript(Berta.Name, "T2");            
            unitOfWorks.createScript(Anton.Name, "T2");            

            unitOfWorks.SubmitChanges();
            Assert.AreEqual(2, Anton.Scripts.Count(), "Anton sollte ein Skript besitzen");
            Assert.AreEqual(2, Berta.Scripts.Count(), "Berta sollte ein Skript besitzen");
            
            unitOfWorks.Scripts.RemoveFromCollection(CanvasScriptKey.Create(Berta.Name, "T2"));
            unitOfWorks.SubmitChanges();
            Assert.AreEqual(2, Anton.Scripts.Count(), "Anton sollte zwei Skripte besitzen");
            Assert.AreEqual(1, Berta.Scripts.Count(), "Berta sollte ein Skript besitzen");

        }
    }
}
