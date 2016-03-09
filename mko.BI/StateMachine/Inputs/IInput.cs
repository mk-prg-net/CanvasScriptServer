//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 24.3.2015
//
//  Projekt.......: mko.BI
//  Name..........: IInput.cs
//  Aufgabe/Fkt...: Definiert Grundstruktur aller Eingaben in einen endl. Automaten 
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

namespace mko.BI.StateMachine
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TInputTags">enum, der alle möglichen Arten von Eingaben im Zustand auflistet</typeparam>
    public interface IInput<TInputTags>
        where TInputTags: struct
    {
        /// <summary>
        /// Art/Typcode der Eingabedaten. 
        /// </summary>
        TInputTags Tag { get; set; }
    }
}
