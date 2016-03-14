using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace CanvasScriptServer.DB.Test
{
    [TestClass]
    public class EFLinq
    {
        [TestMethod]
        public void CreateUpdateDeleteTest()
        {
            // Anlegen des objektrelationalen Mappers
            using (var orm = new DB.CanvasScriptDBContainer())
            {
                // Anlegen vom Benutzer Anton
                var user = orm.UsersSet.Create();                
                user.Name = "Anton";
                orm.UsersSet.Add(user);

                // Anton um zwei Scripte bereichnern

                var script1 = orm.ScriptsSet.Create();

                script1.Name = "T1";
                script1.ScriptJSON = "[]";

                script1.User = user;

                user.Scripts.Add(script1);

                var script2 = orm.ScriptsSet.Create();
                script2.Name = "T2";
                script2.ScriptJSON = "[{\"beginPath\": true}, {\"strokeStyle\": \"#FF0000\"}, {\"lineTo\": {X: 100, Y: 100}}]";

                user.Scripts.Add(script2);
                script2.User = user;

                orm.SaveChanges();

                var userFromDB = orm.UsersSet.Single(r => r.Name == "Anton");

                Assert.AreEqual(userFromDB.Scripts.Count(), 2, "Anton sollte zwei Scripte besitzen");

                // Prüfen der referentiellen Integrität
                orm.UsersSet.Remove(userFromDB);
                orm.SaveChanges();

                Assert.IsFalse(orm.UsersSet.Any(r => r.Name == "Anton"), "Anton sollte nicht mehr existieren");

                Assert.IsFalse(orm.ScriptsSet.Any(r => r.User.Name == "Anton"), "Für Anton sollten keine Scripte mehr da sein");


            }


        }
    }
}
