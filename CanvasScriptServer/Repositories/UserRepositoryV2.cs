//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: UserRepository.cs
//  Aufgabe/Fkt...: Repository der Canvas Script- Autoren.
//                  Diese implementiert lediglich das Filtern sowie den 
//                  Zugriff auf einzelne Scripte, nicht jedoch das Löschen     
//                  von Scripten. Beim Löschen muss geprüft werden, ob dem Autor noch Skripte
//                  zugerodnet sind. Die Löschoeration wird in die Reposities überspannende UnitOfWork verlagert
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
//                  Mengen auf Objkete, die die Schnittstelle IUser implmentieren. 
//                  Höhere Abstraktion als generisches Repository für TUser Objekte, die IUser
//                  implementieren.
//</unit_history>
//</unit_header>        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Linq.Expressions;

namespace CanvasScriptServer
{
    public abstract class UserRepositoryV2 :        
        mko.BI.Repositories.Interfaces.ICreate<string>,
        mko.BI.Repositories.Interfaces.IGet<IUser, string>
    {
        // mko.BI.Repositories.Interfaces.ICreateUpdate<ICanvasScript>,
        public abstract void CreateBoAndAdd(string id);       

        // mko.BI.Repositories.Interfaces.IGetBo<ICanvasScript, string>,
        public abstract bool ExistsBo(string id);
        public abstract IUser GetBo(string id);

        // FilteredSortedSetBuilder
        public interface IFilteredSortedSetBuilder : mko.BI.Repositories.Interfaces.IFilteredSortedSetBuilder<IUser>
        {
            /// <summary>
            /// Einschränken auf Benutzer, auf deren Namen das folgende Filter passt.
            /// </summary>
            /// <param name="pattern"></param>
            void defNameLike(string pattern);

            /// <summary>
            /// Einschränken auf Benutzer, die in einem gegebenen Zeitraum angelegt wurden.
            /// </summary>
            /// <param name="begin"></param>
            /// <param name="end"></param>
            void CreatedBetween(DateTime begin, DateTime end);

            /// <summary>
            /// Sortieren nach Benutzername
            /// </summary>
            /// <param name="descending"></param>
            void sortByUserName(bool descending);

            /// <summary>
            /// Sortieren nach Anzahl der Scripte, die einem Benutzer zugerodnet sind.
            /// </summary>
            /// <param name="descending"></param>
            void sortByScriptCount(bool descending);
        }

        /// <summary>
        /// Bilderobjekt erzeugen, mit dem eine gefilterte und sortierte Teilmenge von Benutzern beschrieben 
        /// werden kann.
        /// </summary>
        /// <returns></returns>
        public abstract IFilteredSortedSetBuilder getFilteredSortedSetBuilder();      

        
    }
}
