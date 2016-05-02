//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.4.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: CanvasScriptKey
//  Aufgabe/Fkt...: Fachschlüssel für einen CanvasScript
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
    public struct CanvasScriptKey
    {
        public static CanvasScriptKey Create(string username, string scriptname)
        {
            return new CanvasScriptKey() { Username = username, Scriptname = scriptname };
        }

        public string Username { get; set; }
        public string Scriptname { get; set; }
    }
}
