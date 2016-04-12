//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.3.2016
//
//  Projekt.......: CanvasScriptServer.Mocks
//  Name..........: CanvasScriptBuilder.cs
//  Aufgabe/Fkt...: Implementierung des ScriptBuilders. Dient zum speichern von
//                  Änderungen in Skripten
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

namespace CanvasScriptServer.Mocks.Bo
{
    public class CanvasScriptBuilder : ICanvasScriptBuilder
    {

        public CanvasScriptBuilder(CanvasScript script)
        {
            _script = script;
        }

        CanvasScript _script;      

        public void setScript(string Script)
        {
            _script._ScriptAsJson = Script;
            _script._Modified = DateTime.Now;

        }
    }
}
