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

            unitOfWorks.Users.CreateBoAndAdd("Anton");
            unitOfWorks.Users.CreateBoAndAdd("Berta");

            var allUsers = unitOfWorks.Users.getFilteredSortedSetBuilder().GetSet();
            Assert.AreEqual(0, allUsers.Get().Count(), "Es wurde keine Eintrag in Users erwartet");

            unitOfWorks.SubmitChanges();

            Assert.AreEqual(2, allUsers.Get().Count(), "Es wurden zwei Einträge in Users erwartet");

            var Anton = unitOfWorks.Users.GetBo("Anton");
            var Berta = unitOfWorks.Users.GetBo("Berta");

            unitOfWorks.createScript(Anton.Name, "T1");
            unitOfWorks.SubmitChanges();

            // Mittels Builder die Teilmenge von Antons Scripte definieren
            var AntonScriptsSetBld = unitOfWorks.Scripts.getFilteredAndSortedSetBuilder();
            AntonScriptsSetBld.defAuthor("Anton");

            // Teilmenge erzeugen
            var AntonScripts = AntonScriptsSetBld.GetSet();
            Assert.AreEqual(1, AntonScripts.Count(), "Anton sollte ein Skript besitzen");


            // Mittels Builder die Teilmenge von Bertas Scripte definieren
            var BertaScriptsSetBld = unitOfWorks.Scripts.getFilteredAndSortedSetBuilder();
            BertaScriptsSetBld.defAuthor("Berta");

            // Teilmenge erzeugen
            var BertaScripts = BertaScriptsSetBld.GetSet();
            Assert.AreEqual(0, BertaScripts.Count(), "Berta sollte ein Skript besitzen");

            // Zugriff auf ein Script von Anton, um es zu aktualisieren
            var T1 = unitOfWorks.Scripts.GetBoBuilder(CanvasScriptKey.Create(Anton.Name, "T1"));
            T1.setScript("[]");


            // Neues Script für Berta anlegen
            unitOfWorks.createScript(Berta.Name, "T1");
            unitOfWorks.SubmitChanges();

            Assert.AreEqual(1, AntonScripts.Count(), "Anton sollte ein Skript besitzen");
            Assert.AreEqual("[]", AntonScripts.Get().First().ScriptAsJson, "Antons Script sollte leer sein");
            Assert.AreEqual(1, BertaScripts.Count(), "Berta sollte ein Skript besitzen");


            unitOfWorks.createScript(Berta.Name, "T2");
            unitOfWorks.createScript(Anton.Name, "T2");

            unitOfWorks.SubmitChanges();
            Assert.AreEqual(2, AntonScripts.Count(), "Anton sollte ein Skript besitzen");
            Assert.AreEqual(2, BertaScripts.Count(), "Berta sollte ein Skript besitzen");

            unitOfWorks.Scripts.RemoveBo(CanvasScriptKey.Create(Berta.Name, "T2"));
            unitOfWorks.SubmitChanges();
            Assert.AreEqual(2, AntonScripts.Count(), "Anton sollte zwei Skripte besitzen");
            Assert.AreEqual(1, BertaScripts.Count(), "Berta sollte ein Skript besitzen");

            var SortByScriptCountBld = unitOfWorks.Users.getFilteredSortedSetBuilder();
            SortByScriptCountBld.sortByScriptCount(true);
            var SortByScriptCount = SortByScriptCountBld.GetSet();
            Assert.AreEqual(2, SortByScriptCount.Count());
            Assert.AreEqual("Anton", SortByScriptCount.Get().First().Name);

        }


    }
}
