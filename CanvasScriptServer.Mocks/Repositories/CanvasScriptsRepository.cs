//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016 
//
//  Projekt.......: CanvasScriptServer.Mocks
//  Name..........: CanvasScriptsRepository.cs
//  Aufgabe/Fkt...: Mockup für CanvasScriptsRepository
//                  
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
//  Datum.........: 25.4.2016
//  Änderungen....: Refactioring in Implementierung neuer Schnittstelle ICanvasScriptRepository
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScriptsRepository : CanvasScriptServer.CanvasScriptRepository
    {
        List<CanvasScript> _ScriptList;
        System.Collections.Generic.Queue<Action> _cudActions;

        public CanvasScriptsRepository(List<CanvasScript> ScriptList, System.Collections.Generic.Queue<Action> cudActions)
        {
            _ScriptList = ScriptList;
            _cudActions = cudActions;
        }

        Func<CanvasScript, bool> GetBoIDTestIntern(CanvasScriptKey id)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(id.Scriptname));

            return r => r.AuthorName == id.Username && r.Name == id.Scriptname;
        }


        public override void RemoveBo(CanvasScriptKey id)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(id.Scriptname));

            var entity = _ScriptList.Single(GetBoIDTestIntern(id));
            
            _cudActions.Enqueue(() => _ScriptList.Remove(entity));
        }


        public override bool ExistsBo(CanvasScriptKey id)
        {
            return _ScriptList.Any(GetBoIDTestIntern(id));
        }


        public override ICanvasScript GetBo(CanvasScriptKey id)
        {
            return _ScriptList.Find(r => r.AuthorName == id.Username && r.Name == id.Scriptname);
        }

        /// <summary>
        /// Builder für eingeschränkte und sortierte Teilmengen
        /// </summary>
        public class FilteredAndSortedSetBuilder : CanvasScriptServer.CanvasScriptRepository.IFilteredAndSortedSetBuilder
        {
            /// <summary>
            /// Einschränkungen (Filter) auf der Menge definieren
            /// </summary>

            IQueryable<CanvasScript> query;

            public FilteredAndSortedSetBuilder( List<CanvasScript> ScriptList)
            {
                query = ScriptList.AsQueryable();
            }

            public void defNameLike(string pattern)
            {
                query = query.Where(r => r.Name.Contains(pattern));
            }

            public void defAuthor(string name)
            {
                query = query.Where(r => r.AuthorName == name);
            }

            public void defCreatedBetween(DateTime begin, DateTime end)
            {
                query = query.Where(r => r.Created >= begin && r.Created <= end);
            }

            public void defModifiedBetween(DateTime begin, DateTime end)
            {
                query = query.Where(r => r.Modified >= begin && r.Modified <= end);
            }

            /// <summary>
            /// Soriterkriterien definieren
            /// </summary>

            List<mko.BI.Repositories.DefSortOrder<CanvasScript>> _DefSortOrders = new List<mko.BI.Repositories.DefSortOrder<CanvasScript>>();


            public void OrderByName(bool descending)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<CanvasScript, string>(r => r.Name, descending));
            }

            public void OrderByAuthor(bool descending = false)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<CanvasScript, string>(r => r.AuthorName, descending));
            }

            public void OrderByCreated(bool descending)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<CanvasScript, DateTime>(r => r.Created, descending));
            }

            public void OrderByModified(bool descending)
            {
                _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<CanvasScript, DateTime>(r => r.Modified, descending));
            }

            /// <summary>
            /// Bilden der eingeschränkten und sortierten Teilmenge
            /// </summary>
            /// <returns></returns>
            public mko.BI.Repositories.Interfaces.IFilteredSortedSet<ICanvasScript> GetSet()
            {
                if (!_DefSortOrders.Any())
                {
                    _DefSortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<CanvasScript, DateTime>(r => r.Created, true));
                }
                return new mko.BI.Repositories.FilteredSortedSet<CanvasScript>(query, _DefSortOrders);
            }


        }

        /// <summary>
        /// Liefert einen Builder, mit dem eine eingeschränkte (gefilterte) und sortierte Teilmenge von CanvasScripten definiert werden kann
        /// </summary>
        /// <returns></returns>
        public override CanvasScriptRepository.IFilteredAndSortedSetBuilder getFilteredAndSortedSetBuilder()
        {
            return new FilteredAndSortedSetBuilder(_ScriptList);
        }

        /// <summary>
        /// Builder zum Aktualisieren eines Datensatzes abrufen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override ICanvasScriptBuilder GetBoBuilder(CanvasScriptKey id)
        {
            return new Bo.CanvasScriptBuilder(_ScriptList.Find(r => r.AuthorName == id.Username && r.Name == id.Scriptname));
        }

    }
}
