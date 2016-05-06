//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: CanvasScriptRepository.cs
//  Aufgabe/Fkt...: Repository der Canvas- Scripte.
//                  Diese Implementiert lediglich das Filtern sowie den 
//                  Zugriff auf einzelne Scripte, nicht jedoch das anlegen und löschen     
//                  von Scripten. Anlegen und löschen sind nur im Zusammenhang mit der 
//                  Users- Collection sinnvoll.  Sie werden in der UnitOfWork implementiert
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
//  Änderungen....: Refaktoriert in Implementierung neuer Repository- Schnittstellen
//                  (IFilteredAndSortedSetBuilder etc.). Repository verwaltet jetzt wieder
//                  Mengen auf Objkete, die die Schnittstelle ICanvasScript implmentieren. 
//                  Höhere Abstraktion als generisches Repository für TScript Objekte, die ICanvasScript
//                  implementieren.
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public abstract class CanvasScriptRepository :
        mko.BI.Repositories.Interfaces.IGet<ICanvasScript, CanvasScriptKey>,
        mko.BI.Repositories.Interfaces.IGetBoBuilder<ICanvasScriptBuilder, CanvasScriptKey>,
        mko.BI.Repositories.Interfaces.IRemove<CanvasScriptKey>
    {
        // mko.BI.Repositories.Interfaces.IGetBo<ICanvasScript, string>,
        public abstract bool ExistsBo(CanvasScriptKey id);
        public abstract ICanvasScript GetBo(CanvasScriptKey id);


        // mko.BI.Repositories.Interfaces.IGetBoBuilder<ICanvasScriptBuilder, string>
        public abstract ICanvasScriptBuilder GetBoBuilder(CanvasScriptKey id);

        // mko.BI.Repositories.Interfaces.IRemove<ICanvasScript>
        public virtual void RemoveAllBo()
        {
            throw new NotImplementedException();
        }

        public abstract void RemoveBo(CanvasScriptKey id);

        public interface IFilteredAndSortedSetBuilder : mko.BI.Repositories.Interfaces.IFilteredSortedSetBuilder<ICanvasScript>
        {
            /// <summary>
            /// Einschränken auf Skripte, auf deren Namen das Muster passt
            /// </summary>
            /// <param name="pattern"></param>
            void defNameLike(string pattern);

            void OrderByName(bool descending);

            /// <summary>
            /// Einschränken auf Scripte eines Authors
            /// </summary>
            /// <param name="name"></param>
            void defAuthor(string name);

            void OrderByAuthor(bool descending);

            void defCreatedBetween(DateTime begin, DateTime end);

            void OrderByCreated(bool descending);

            void defModifiedBetween(DateTime begin, DateTime end);

            void OrderByModified(bool descending);
        }

        public abstract IFilteredAndSortedSetBuilder getFilteredAndSortedSetBuilder();


    }
}
