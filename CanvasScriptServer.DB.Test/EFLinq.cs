using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;
using System.Diagnostics;

namespace CanvasScriptServer.DB.Test
{
    [TestClass]
    public class EFLinq
    {
        DB.CanvasScriptDBContainer orm;

        [TestInitialize]
        public void Init()
        {
            // Anlegen des objektrelationalen Mappers
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
        public void CreateUpdateDeleteTest()
        {

            // Anlegen vom Benutzer Anton
            var user = orm.UsersSet.Create();
            user.Created = DateTime.Now;
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
