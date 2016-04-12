using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace CanvasScriptServer.DB.Test
{
    [TestClass]
    public class RepositoriesTest
    {
        [TestMethod]
        public void UnitOfWorkTest()
        {

            using (CanvasScriptServer.ICanvasScriptServerUnitOfWork UofW = new DB.CanvasScriptDBContainer())
            {
                UofW.createUser("Anton");
                UofW.createUser("Berta");

                UofW.SubmitChanges();

                UofW.createScript("Anton", "T1");
                UofW.createScript("Anton", "T2");

                var Anton = UofW.Users.GetBo("Anton");
                Assert.AreEqual(Anton.Scripts.Count(), 2, "Anton sollte zwei Skripte besitzen");

                UofW.SubmitChanges();

                var T2Bld = UofW.Scripts.GetBoBuilder(CanvasScriptKey.Create("Anton", "T2"));
                T2Bld.setScript("[]");

                var Berta = UofW.Users.GetBo("Berta");
                Assert.AreEqual(Berta.Scripts.Count(), 0, "Berta sollte keine Skripte besitzen");


                UofW.deleteUser(Anton.Name);
                UofW.deleteUser(Berta.Name);

                UofW.SubmitChanges();

                Assert.AreEqual(UofW.Users.CountAllBo(), 0, "Alle Benutzer sollten gelöscht sein");
                Assert.AreEqual(UofW.Scripts.CountAllBo(), 0, "Alle Skripte sollten gelöscht sein");

            }

        }
    }
}
