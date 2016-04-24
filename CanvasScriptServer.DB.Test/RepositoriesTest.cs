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

            using (var UofW = new DB.CanvasScriptDBContainer())
            {
                UofW.createUser("X");
                UofW.createUser("Y");

                UofW.SubmitChanges();

                UofW.createScript("X", "T1");
                UofW.createScript("Y", "T2");

                var Anton = UofW.Users.GetBo("X");
                Assert.AreEqual(1, Anton.Scripts.Count(), "Anton sollte zwei Skripte besitzen");

                UofW.SubmitChanges();

                var T2Bld = UofW.Scripts.GetBoBuilder(CanvasScriptKey.Create("X", "T1"));
                T2Bld.setScript("[]");

                var Berta = UofW.Users.GetBo("Y");
                Assert.AreEqual(1, Berta.Scripts.Count(), "Berta sollte keine Skripte besitzen");


                UofW.deleteUser(Anton.Name.Name);
                UofW.deleteUser(Berta.Name.Name);

                UofW.SubmitChanges();

                Assert.AreEqual(0, UofW.Users.Get().Count(), "Alle Benutzer sollten gelöscht sein");
                Assert.AreEqual(0, UofW.Scripts.Get().Count(), "Alle Skripte sollten gelöscht sein");

            }

        }
    }
}
