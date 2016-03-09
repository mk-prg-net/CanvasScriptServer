//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.2.2015
//
//  Projekt.......: Gbl.BI.Lab.Concrete
//  Name..........: Cancel.cs
//  Aufgabe/Fkt...: Workflow: allg. Abbruch 
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
//  Datum.........: 24.4.2015
//  Änderungen....: Verallgemeinert zu mko.BI.StateMachine.Cancel.
//                  Eigenschaft Reason hinzugefügt.
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

    public class Cancel : State
    {
        public Cancel() : base(new FinalStateBehavior()) { }

        public Cancel(IStateBehavior beahvior, params State[] Next) : base(beahvior, Next) { }

        /// <summary>
        /// Zustand, in dem der Workflow vorzeitig abgebrochen wurde.
        /// </summary>
        public State CanceldState { get; set; }

        /// <summary>
        /// Grund des Vorzeitigen Abbruchs (informelle Beschreibung)
        /// </summary>
        public string Reason { get; set; }
    }
}
