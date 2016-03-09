using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace mko.BI.Test
{
    [TestClass]
    public class Repositories
    {
        [TestMethod]
        public void mko_BI_Repository_Sort()
        {
            var repo = new DB.Kepler.HimmelskoerperRepository();

            var AnzHk1 = repo.CountFilteredBo();
            foreach (var hk in repo.GetFilteredAndSortedListOfBo())
            {
                Debug.WriteLine("Name: " + hk.Name + ", Masse: " + hk.Masse_in_kg);
            }

            repo.SetFilter(new DB.Kepler.HimmelskoerperRepository.HatUmlaufbahnFlt());

            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("-------------------------------------------------------------------------");
            Debug.WriteLine("Gefilterte");
            Debug.WriteLine("");
            Debug.WriteLine("");


            var AnzHk2 = repo.CountFilteredBo();
            foreach (var hk in repo.GetFilteredAndSortedListOfBo())
            {
                Debug.WriteLine("Name: " + hk.Name + ", Masse: " + hk.Masse_in_kg + ", Zentralkörper: " + hk.Umlaufbahn.Zentralobjekt.Name);
            }

            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("-------------------------------------------------------------------------");
            Debug.WriteLine("Gefilterte und sortiert nach Zentralkörper, Masse");
            Debug.WriteLine("");
            Debug.WriteLine("");

            repo.DefSortOrders(new DB.Kepler.HimmelskoerperRepository.SortByZentralkorper(false), new DB.Kepler.HimmelskoerperRepository.SortByMass(true));
            foreach (var hk in repo.GetFilteredAndSortedListOfBo())
            {
                Debug.WriteLine("Name: " + hk.Name + ", Masse: " + hk.Masse_in_kg + ", Zentralkörper: " + hk.Umlaufbahn.Zentralobjekt.Name);
            }
        }
    }
}
