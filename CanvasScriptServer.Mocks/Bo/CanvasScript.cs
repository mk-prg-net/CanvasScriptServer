//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2015
//
//  Projekt.......: CanvasScriptServer.Mocks
//  Name..........: CanvasScript.cs
//  Aufgabe/Fkt...: Test- Attrappe eines Datensatzes zum Verwalten eines
//                  Canvas- Scriptes
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
//  Änderungen....: Refactoring in IMplementierung neuer Schnittstelle ICanvaScript
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScript : ICanvasScript
    {
        public string Name
        {
            get
            {
                return _Name;
            }
            //set;
        }
        internal string _Name;

        public string AuthorName
        {
            get
            {
                return _Author;
            }
        }
        internal string _Author;


        public DateTime Created
        {
            get
            {
                return _Created;
            }
        }
        internal DateTime _Created;

        public DateTime Modified
        {
            get { 
                return _Modified;
            }
        }
        internal DateTime _Modified;

        public string ScriptAsJson
        {
            get
            {
                return _ScriptAsJson;
            }
        }
        internal string _ScriptAsJson;


    }
}
