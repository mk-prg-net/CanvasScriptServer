//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.5.2016
//
//  Projekt.......: CanvasScriptServer.DB
//  Name..........: CanvasScriptFlat
//  Aufgabe/Fkt...: Hilfsklasse. Wrapper für Users Objekte in Fällen, wo
//                  trotz Eager- Loading es zum Problem beim Zugriff auf 
//                  Navigffationseigenschaften kommt (z.B. WebApi- Serialisirung in JSon)
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

namespace CanvasScriptServer.DB
{
    public class CanvasScriptFlat : ICanvasScript
    {
        public CanvasScriptFlat(Scripts script)
        {
            _DbScripts = script;
            _AuthorName = script.User.Name.Name;
        }

        Scripts _DbScripts;

        public string Name
        {
            get {
                return _DbScripts.Name;
            }
        }

        public string AuthorName
        {
            get { 
                return _AuthorName;
            }
        }
        string _AuthorName;

        public DateTime Created
        {
            get { 
                return _DbScripts.Created;
            }
        }

        public DateTime Modified
        {
            get { 
                return _DbScripts.Modified; }

        }

        public string ScriptAsJson
        {
            get { return _DbScripts.ScriptAsJson; }
        }
    }
}
