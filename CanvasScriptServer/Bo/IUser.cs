//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: IUser.cs
//  Aufgabe/Fkt...: Schnittstelle der Benutzer- Geschäftsobjekte
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public interface IUser
    {
        /// <summary>
        /// Fachschlüssel eines Benutzers
        /// </summary>
        string Name { get;}

        // nicht sinnvoll: Auf einen Benutzer können Skriptobjekte verweisen, die dieser angelegt hat.
        // Wenn der Benutzer selber auf diese verweist, ergeben sich folgende Probleme
        // 1) beim Hinzufügen und Löschen von Skriptobjekten ist die Verweislist im User anzupassen
        // 2) Über das Repository der Skripte können mittels eines Filters alle Skripte zu einem User bestimmt werden -
        //    die Verweisliste im User ist damit redundant !
        //IEnumerable<ICanvasScript> Scripts { get; }

        DateTime Created { get; }
    }
}
