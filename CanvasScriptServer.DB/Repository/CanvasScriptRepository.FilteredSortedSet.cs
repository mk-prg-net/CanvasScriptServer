//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.3.2016
//
//  Projekt.......: CanvasScriptServer.DB
//  Name..........: CanvasScriptRepository.cs
//  Aufgabe/Fkt...: Implementierung des CanvasScriptRepository für eine 
//                  mittels EF 6.0 Model First erstellte DB
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 6.5.2016
//  Änderungen....: Eager Loading für WebApi Zugriffe implementiert
//                  FilteredSortedSet- Dekorator implementiert, der Abhängigkeiten von 
//                  Navigationseigenschaften im Objektrelationalen Mapper behebt.
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Für Erweiterungsmethode Include
using System.Data.Entity;


namespace CanvasScriptServer.DB.Repository
{
    partial class CanvasScriptRepository
    {
        /// <summary>
        /// Decorator für mko.BI.Repositories.FilteredSortedSet.
        /// Get- Methode wird bildet Script- Objekte auf CanvasScriptFlat- Objekte ab. So werden Probleme beim Serialisieren 
        /// von Script- Objekten, die auf Navigationseigenschaften im EF- Model zugreifen, behoben.
        /// </summary>
        public class FilteredSortedSet : mko.BI.Repositories.Interfaces.IFilteredSortedSet<CanvasScriptFlat>
        {
            public FilteredSortedSet(IQueryable<Scripts> query, IEnumerable<mko.BI.Repositories.DefSortOrder<Scripts>> sortOrders)
            {
                _fsset = new mko.BI.Repositories.FilteredSortedSet<Scripts>(query, sortOrders);
            }

            mko.BI.Repositories.FilteredSortedSet<Scripts> _fsset;

            public bool Any()
            {
                return _fsset.Any();
            }

            public long Count()
            {
                return _fsset.Count();
            }

            public IEnumerable<CanvasScriptFlat> Get()
            {
                return _fsset.Get().Select(r => new CanvasScriptFlat(r));
            }
        }


        public class FilteredAnSortedSetBuilder : CanvasScriptServer.CanvasScriptRepository.IFilteredAndSortedSetBuilder
        {


            IQueryable<Scripts> _query;

            internal FilteredAnSortedSetBuilder(DB.CanvasScriptDBContainer Orm)
            {
                _query = Orm.ScriptsSet.Include(r => r.User.Name);
            }


            public void defNameLike(string pattern)
            {
                _query = _query.Where(r => r.Name.Contains(pattern));
            }

            public void defAuthor(string name)
            {
                _query = _query.Where(r => r.User.Name.Name.Contains(name));
            }

            public void defCreatedBetween(DateTime begin, DateTime end)
            {
                _query = _query.Where(r => begin <= r.Created && r.Created <= end);
            }

            public void defModifiedBetween(DateTime begin, DateTime end)
            {
                _query = _query.Where(r => begin <= r.Modified && r.Modified <= end);
            }


            /// <summary>
            /// Soriterkriterien definieren
            /// </summary>

            List<mko.BI.Repositories.DefSortOrder<Scripts>> _DefSortOrders = new List<mko.BI.Repositories.DefSortOrder<Scripts>>();


            public void OrderByName(bool descending)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Scripts, string>(r => r.Name, descending));
            }

            public void OrderByAuthor(bool descending = false)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Scripts, string>(r => r.User.Name.Name, descending));
            }

            public void OrderByCreated(bool descending)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Scripts, DateTime>(r => r.Created, descending));
            }

            public void OrderByModified(bool descending)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Scripts, DateTime>(r => r.Modified, descending));
            }

            public mko.BI.Repositories.Interfaces.IFilteredSortedSet<ICanvasScript> GetSet()
            {
                if (!_DefSortOrders.Any())
                {
                    _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Scripts, DateTime>(r => r.Created, true));
                }
                else {  }

                return new FilteredSortedSet(_query, _DefSortOrders);
            }
        }

        public override CanvasScriptServer.CanvasScriptRepository.IFilteredAndSortedSetBuilder getFilteredAndSortedSetBuilder()
        {
            return new FilteredAnSortedSetBuilder(Orm);
        }

    }
}
