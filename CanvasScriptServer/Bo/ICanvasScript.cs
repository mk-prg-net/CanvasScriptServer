//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: ICanvasScript.cs
//  Aufgabe/Fkt...: Schnittstelle der Canvas- Scripte
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
//  Änderungen....: Eigenschaft IUser Author geändert in string NameAuthor
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
    public interface ICanvasScript
    {
        /// <summary>
        /// Name des Scriptes
        /// </summary>
        string Name { get;}

        /// <summary>
        /// Name des Benutzers, dem das Script zugeordnet ist
        /// </summary>
        string AuthorName { get;}

        /// <summary>
        /// Tag,  an dem das Script erstellt wurde
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Änderungsdatum
        /// </summary>
        DateTime Modified { get; }

        /// <summary>
        /// Script als JSON- String. Wird im Browser mittels der CanvasPainter- JavaScript
        /// Bibliothek bearbeitet
        /// </summary>
        string ScriptAsJson { get;}
    }
}
