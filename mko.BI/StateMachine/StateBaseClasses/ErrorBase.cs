//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.2.2015
//
//  Projekt.......: mko.Algo.Automaton
//  Name..........: ErrorBase.cs
//  Aufgabe/Fkt...: Workflow: Basisklasse von Fehlerzuständen
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
//  Datum.........: 4.2.2016
//  Änderungen....: BehaviorOfState gegen IStateBehavior ausgetauscht
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
    public class Error : State
    {
        /// <summary>
        /// Standardverhalten im Fehlerfall: Endzustand
        /// </summary>
        public Error() : base(new FinalStateBehavior()) { }

        /// <summary>
        /// Mit diesem Konstruktor kann das Standardverhalten im Fehlerfall (= Endzustand)
        /// übersteuert werden.
        /// </summary>
        /// <param name="beahvior"></param>
        public Error(IStateBehavior beahvior, params State[] Next) : base(beahvior, Next) { }


        /// <summary>
        /// ursprüngliche Ausnahme, die zum Wechsel in den Fehlerzustand zwang
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Informelle Fehlerbeschreibung
        /// </summary>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Zustand, in dem der ursprüngliche Fehler auftrat
        /// </summary>
        public State FaultyState { get; set; }
    }
}
