//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.2014
//
//  Projekt.......: mko.BI
//  Name..........: BehaviorOfState.cs
//  Aufgabe/Fkt...: Definiert das Grundverhalten eines Zustandes
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
//  Datum.........: 23.4.2015
//  Änderungen....: Umzug vom Projekt mko.Automaton.StateMachine in mko.BI.
//                  Integration der Zustandsüberführungsfunktion in die Zustandsklasse 
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
    /// Allgemeine Definition eines Zustandsverhaltens. Ein Zustand kann sich wie ein 
    /// Start oder wie ein Endzustand verhalten.
    /// </summary>
    public interface IStateBehavior
    {        

        bool IsStart
        {
            get;
        }

        bool IsFinal
        {
            get;
        }
    }
}
