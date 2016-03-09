//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 23.4.2015
//
//  Projekt.......: mko.BI
//  Name..........: Dateiname
//  Aufgabe/Fkt...: Schnittstelle von Zuständen, die  bezüglich der Eingaben streng typiserte 
//                  Zustandsüberführungsfunktion anbietet.
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
    /// Struktur der Zustandsüberführungsfunktion eine
    /// </summary>
    /// <typeparam name="TInput">Basisklasse der möglichen Eingaben in diesem Zustand</typeparam>
    /// <typeparam name="TInputTags">enum, der alle TypID's von Eingaben im Zustand auflistet</typeparam>
    /// <typeparam name="TStateFactory">Typ der Zustandsfabrik</typeparam>
    public interface IStateTransition<TInput, TInputTags, TStateFactory>
        where TInput : IInput<TInputTags>
        where TInputTags : struct
    {
        /// <summary>
        /// Liefert eine Zustandsfabrik. Wird bei der Berechnung der Zustandsübergänge benötigt.
        /// </summary>
        /// <returns></returns>
        TStateFactory StateFactory { get; set; }

        State Transition(TInput input);
    }
}
