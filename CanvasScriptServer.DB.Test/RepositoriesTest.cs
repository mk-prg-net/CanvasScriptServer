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

            var Berta = UofW.Users.GetBo("Berta");
            Assert.AreEqual("Berta", Berta.Name);


            UofW.createScript(Berta.Name, "T3");
            UofW.createScript(Berta.Name, "T5");
            UofW.createScript(Berta.Name, "T2");

            UofW.createScript(Anton.Name, "T2");
            UofW.createScript(Anton.Name, "T3");
            UofW.createScript(Anton.Name, "T1");

            UofW.SubmitChanges();

            // Menge aller Benutzer über Repository bilden
            var AllUsersBld = UofW.Users.getFilteredSortedSetBuilder();
            var AllUsers = AllUsersBld.GetSet();

            Assert.AreEqual(3, AllUsers.Count());

            // Menge aller Scripte über Repository bilden
            var AllScriptsBld = UofW.Scripts.getFilteredAndSortedSetBuilder();
            AllScriptsBld.OrderByAuthor(false);
            AllScriptsBld.OrderByName(true);

            var AllScripts = AllScriptsBld.GetSet();
            Assert.AreEqual(6, AllScripts.Count());            

            var allScriptsSorted = AllScripts.Get().ToArray();

            Assert.AreEqual("Anton", allScriptsSorted[0].AuthorName);
            Assert.AreEqual("T3", allScriptsSorted[0].Name);

            Assert.AreEqual("Anton", allScriptsSorted[1].AuthorName);
            Assert.AreEqual("T2", allScriptsSorted[1].Name);

            Assert.AreEqual("Anton", allScriptsSorted[2].AuthorName);
            Assert.AreEqual("T1", allScriptsSorted[2].Name);

            Assert.AreEqual("Berta", allScriptsSorted[3].AuthorName);
            Assert.AreEqual("T5", allScriptsSorted[3].Name);

            Assert.AreEqual("Berta", allScriptsSorted[4].AuthorName);
            Assert.AreEqual("T3", allScriptsSorted[4].Name);

            Assert.AreEqual("Berta", allScriptsSorted[5].AuthorName);
            Assert.AreEqual("T2", allScriptsSorted[5].Name);



            // Teilmenge aller Scripte von Anton bilden
            var AntonsScriptsBld = UofW.Scripts.getFilteredAndSortedSetBuilder();
            AntonsScriptsBld.defAuthor(Anton.Name);

            var AntonsScripts = AntonsScriptsBld.GetSet();
            
            Assert.AreEqual(AntonsScripts.Count(), 3, "Anton sollte drei Skripte besitzen");


            var T2Bld = UofW.Scripts.GetBoBuilder(CanvasScriptKey.Create(Anton.Name, "T2"));
            T2Bld.setScript("[{\"beginPath\": true}, {\"strokeStyle\": \"#FF0000\"}, {\"lineTo\": {X: 100, Y: 100}}]");


            // Alle Scripte von Berta auflisten
            var Caesar = UofW.Users.GetBo("Cäsar");
            Assert.AreEqual("Cäsar", Caesar.Name);


            var CaesarsScriptsBld = UofW.Scripts.getFilteredAndSortedSetBuilder();
            CaesarsScriptsBld.defAuthor(Caesar.Name);

            var CaesarsScripts = CaesarsScriptsBld.GetSet();
            Assert.IsFalse(CaesarsScripts.Any(), "Cäsar sollte keine Skripte besitzen");

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

            //foreach (var script in AntonsScripts.Get())
            //{
            //    UofW.Scripts.RemoveBo(CanvasScriptKey.Create(script.AuthorName, script.Name));
            //}

            //UofW.deleteUser(Anton.Name);
            //UofW.SubmitChanges();

            //var BertasScriptsBld = UofW.Scripts.getFilteredAndSortedSetBuilder();
            //BertasScriptsBld.defAuthor(Berta.Name);

            //var BertasScripts = BertasScriptsBld.GetSet();

            //foreach (var script in BertasScripts.Get())
            //{
            //    UofW.Scripts.RemoveBo(CanvasScriptKey.Create(script.AuthorName, script.Name));
            //}

            //UofW.deleteUser(Berta.Name);
            //UofW.SubmitChanges();


            //Assert.IsFalse(AllUsers.Any(), "Alle Benutzer sollten gelöscht sein");
            //Assert.IsFalse(AllScripts.Any(), "Alle Skripte sollten gelöscht sein");

        }

    }
}
