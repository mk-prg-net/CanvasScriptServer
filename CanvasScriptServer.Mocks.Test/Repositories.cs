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
            var scripts = new Mocks.CanvasScriptsRepository();
            var users = new Mocks.UsersRepository(scripts);


            
            var Anton = users.CreateBoAndAddToCollection("Anton");
            var Berta = users.CreateBoAndAddToCollection("Berta");
            Assert.AreEqual(users.CountAllBo(), 0, "Es wurde keine Eintrag in Users erwartet");            
            
            users.SubmitChanges();
            Assert.AreEqual(users.CountAllBo(), 2, "Es wurden zwei Einträge in Users erwartet");

            var T1 = scripts.CreateBoAndAddToCollection("T1");
            T1.User = Anton;

            scripts.SubmitChanges();            
            Assert.AreEqual(Anton.Scripts.Count(), 1, "Anton sollte ein Skript besitzen");


            var T2 = scripts.CreateBoAndAddToCollection("T2");
            T2.User = Berta;
            scripts.SubmitChanges();
            Assert.AreEqual(Anton.Scripts.Count(), 1, "Anton sollte ein Skript besitzen");
            Assert.AreEqual(Berta.Scripts.Count(), 1, "Berta sollte ein Skript besitzen");


            var T3 = scripts.CreateBoAndAddToCollection("T3");
            T3.User = Berta;

            var T4 = scripts.CreateBoAndAddToCollection("T4");
            T4.User = Anton;

            scripts.SubmitChanges();
            Assert.AreEqual(Anton.Scripts.Count(), 2, "Anton sollte ein Skript besitzen");
            Assert.AreEqual(Berta.Scripts.Count(), 2, "Berta sollte ein Skript besitzen");
            
            scripts.RemoveFromCollection("T3");
            scripts.SubmitChanges();
            Assert.AreEqual(Anton.Scripts.Count(), 2, "Anton sollte ein Skript besitzen");
            Assert.AreEqual(Berta.Scripts.Count(), 1, "Berta sollte ein Skript besitzen");

        }
    }
}
