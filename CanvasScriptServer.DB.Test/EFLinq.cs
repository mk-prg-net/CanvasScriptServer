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
                user.Name = orm.UserNamesSet.Create();
                user.Name.Name = "Anton";
                orm.UsersSet.Add(user);

                // Anton um zwei Scripte bereichnern

                var script1 = orm.ScriptsSet.Create();

                script1.Name = "T1";
                script1.User = user;
                script1.Created = DateTime.Now;
                script1.Modified = script1.Created;
                script1.ScriptAsJson = "[]";               

                user.Scripts.Add(script1);

                var script2 = orm.ScriptsSet.Create();
                script2.Name = "T2";
                script2.User = user;
                script2.Created = DateTime.Now;
                script2.Modified = script2.Created;
                script2.ScriptAsJson = "[{\"beginPath\": true}, {\"strokeStyle\": \"#FF0000\"}, {\"lineTo\": {X: 100, Y: 100}}]";   
                user.Scripts.Add(script2);                

                orm.SaveChanges();

                var userFromDB = orm.UserNamesSet.Single(r => r.Name == "Anton").User;

                Assert.AreEqual(userFromDB.Scripts.Count(), 2, "Anton sollte zwei Scripte besitzen");

                // Prüfen der referentiellen Integrität
                orm.UserNamesSet.Remove(userFromDB.Name);
                orm.UsersSet.Remove(userFromDB);
                orm.SaveChanges();

                Assert.IsFalse(orm.UserNamesSet.Any(r => r.Name == "Anton"), "Anton sollte nicht mehr existieren");

                Assert.IsFalse(orm.ScriptsSet.Any(r => r.User.Name.Name == "Anton"), "Für Anton sollten keine Scripte mehr da sein");


            }


        }
    }
}
