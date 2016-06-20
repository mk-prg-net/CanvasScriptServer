using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Notwending, um Linq einzusetzen
using System.Linq;

namespace IntroEntityFramework
{
    [TestClass]
    public class EntityFrameworkDemo
    {
        [TestMethod]
        public void EntityFrameworkDemo_CreateUserTest()
        {
            // 1. Objektrelationalen Mapper anlegen
            var orm = new CanvasScriptsDBEntities();


            // 2. Tabelle der Benutzer zugreifen
            var AntonName = orm.UserNamesSet.Find("Anton");

            if (AntonName == null)
            {
                // Anton anlegen, da noch nícht vorhanden
                
                AntonName = orm.UserNamesSet.Create();
                AntonName.Name = "Anton";

                orm.UserNamesSet.Add(AntonName);

                var Anton = orm.UsersSet.Create();
                Anton.Created = DateTime.Now;
                Anton.UserNamesSet.Add(AntonName);

                orm.UsersSet.Add(Anton);

                // Änderungen in die Datenbank schreiben
                orm.SaveChanges();

            }

            AntonName = orm.UserNamesSet.Find("Anton");

            Assert.IsNotNull(AntonName);

        }


        bool IstPlanet(DB.Kepler.EF60.Himmelskoerper hk)
        {
            return hk.HimmelskoerperTyp.Name == "Planet";
        }

        [TestMethod]
        public void EntityFrameworkDemo_QueryKeplerDB()
        {
            // 1. Objektrelationalen Mapper anlegen
            using (var orm = new DB.Kepler.EF60.KeplerDBEntities())
            {

                // Liste aller Planeten
                var allePlaneten = orm.HimmelskoerperTab.ToArray().Where(IstPlanet);
                var allPlaneten2 = orm.HimmelskoerperTab.Where(r => r.HimmelskoerperTyp.Name == "Planet");

                var dieErde = orm.HimmelskoerperTab.Where(r => r.Name == "Erde").First();
                var auchDieErde = orm.HimmelskoerperTab.Single(r => r.Name == "Erde");
                var auchDieErde2 = orm.HimmelskoerperTab.First(r => r.Name == "Erde");

                // Alle Plaeten mit einem Durchmesser kleiner der Erde
                var alleKleinerAlsErde = orm.HimmelskoerperTab.Where(r => r.HimmelskoerperTyp.Name == "Planet" && r.Sterne_Planeten_MondeTab.Aequatordurchmesser_in_km < dieErde.Sterne_Planeten_MondeTab.Aequatordurchmesser_in_km);





            }

            // 
        }
    }
}
