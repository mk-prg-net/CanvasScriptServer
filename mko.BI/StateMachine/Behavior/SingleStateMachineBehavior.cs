//<unit_header>
//----------------------------------------------------------------
// Copyright 2016 Martin Korneffel 
// 
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: mko.BI
//  Name..........: StartFinalStateBehavior.cs
//  Aufgabe/Fkt...: Verhalten eines Zustandes, der Start- und Endzustand 
//                  in einem ist.
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
    /// Zustandsverhalten: Start und Endzustand gleichzeitig.
    /// </summary>
    public class SingleStateMachineBehavior : IStateBehavior
    {
        public  bool IsStart
        {
            get { return true; }
        }

        public  bool IsFinal
        {
            get { return true; }
        }
    }
}
