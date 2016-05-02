using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Diagnostics;

namespace CanvasScriptServer.DB.Test
{
    [TestClass]
    public class RepositoriesTest
    {
        DB.CanvasScriptDBContainer orm;

        [TestInitialize]
        public void Init()
        {
            orm = new DB.CanvasScriptDBContainer();
            orm.Database.Delete();
            orm.Database.CreateIfNotExists();
            orm.Database.Log = log => System.Diagnostics.Debug.WriteLine(log);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Debug.WriteLine("Test beendet");
        }


        [TestMethod]
        public void CanvasScriptServer_DB_UnitOfWork()
        {
            CanvasScriptServer.ICanvasScriptServerUnitOfWork UofW = orm;
            UofW.Users.CreateBoAndAdd("Anton");
            UofW.Users.CreateBoAndAdd("Berta");
            UofW.Users.CreateBoAndAdd("Cäsar");

            UofW.SubmitChanges();

            var Anton = UofW.Users.GetBo("Anton");
            Assert.AreEqual("Anton", Anton.Name);

            UofW.createScript(Anton.Name, "T1");
            UofW.createScript(Anton.Name, "T2");

            UofW.SubmitChanges();

            // Menge aller Benutzer über Repository bilden
            var AllUsersBld = UofW.Users.getFilteredSortedSetBuilder();
            var AllUsers = AllUsersBld.GetSet();

            Assert.AreEqual(3, AllUsers.Count());

            // Menge aller Scripte über Repository bilden
            var AllScripts = UofW.Scripts.getFilteredAndSortedSetBuilder().GetSet();

            Assert.AreEqual(2, AllScripts.Count());            

            // Teilmenge aller Scripte von Anton bilden
            var AntonsScriptsBld = UofW.Scripts.getFilteredAndSortedSetBuilder();
            AntonsScriptsBld.defAuthor(Anton.Name);

            var AntonsScripts = AntonsScriptsBld.GetSet();
            
            Assert.AreEqual(AntonsScripts.Count(), 2, "Anton sollte zwei Skripte besitzen");


            var T2Bld = UofW.Scripts.GetBoBuilder(CanvasScriptKey.Create(Anton.Name, "T2"));
            T2Bld.setScript("[{\"beginPath\": true}, {\"strokeStyle\": \"#FF0000\"}, {\"lineTo\": {X: 100, Y: 100}}]");

            var Berta = UofW.Users.GetBo("Berta");
            Assert.AreEqual("Berta", Berta.Name);

            // Alle Scripte von Berta auflisten
            var BertasScriptsBld = UofW.Scripts.getFilteredAndSortedSetBuilder();
            BertasScriptsBld.defAuthor(Berta.Name);

            var BertasScripts = BertasScriptsBld.GetSet();            
            Assert.IsFalse(BertasScripts.Any(), "Berta sollte keine Skripte besitzen");

            UofW.deleteUser("Cäsar");
            UofW.SubmitChanges();

            Assert.AreEqual(2, AllUsers.Count());

            try
            {
                UofW.deleteUser(Anton.Name);
                Assert.Fail("Anton darf sich nich löschen lassen, solange ihm Skripte zugeordnet sind");
            }
            catch (Exception ex)
            {
                
            }

            foreach (var script in AntonsScripts.Get())
            {
                UofW.Scripts.RemoveBo(CanvasScriptKey.Create(script.AuthorName, script.Name));
            }

            UofW.deleteUser(Anton.Name);
            UofW.SubmitChanges();

            UofW.deleteUser(Berta.Name);
            UofW.SubmitChanges();


            Assert.IsFalse(AllUsers.Any(), "Alle Benutzer sollten gelöscht sein");
            Assert.IsFalse(AllScripts.Any(), "Alle Skripte sollten gelöscht sein");

        }

    }
}
