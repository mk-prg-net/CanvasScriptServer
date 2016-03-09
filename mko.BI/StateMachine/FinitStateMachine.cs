//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 26.11.2014
//
//  Projekt.......: mko.Algo
//  Name..........: FinitStateMachin.cs
//  Aufgabe/Fkt...: Minimalistischer Zustandsautomat
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.0.7.0
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.4.2015
//  Änderungen....: Umzug vom Projekt mko.Automaton.StateMachine in mko.BI.
//                  Integration der Zustandsüberführungsfunktion in die Zustandsklasse 
//
//  Version.......: 1.0.8.0
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 27.7.2015
//  Änderungen....: ActiveState als virtuell gekennzeichnet
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
    public abstract class FinitStateMachine<TStateFactory>
    {
        /// <summary>
        /// Klassenfabrik zum Erzeugen von Zuständen
        /// </summary>
        public abstract TStateFactory StateFactory { get; }

        /// <summary>
        /// Verweist auf den aktuellen Zustand, in dem sich der Automat befindet
        /// </summary>
        public virtual State ActiveState { get; set; }

        /// <summary>
        /// Ermöglicht den Zugriff auf den aktiven Zustand streng typisiert. Falls die Konvertierung fehlschlägt, wird eine
        /// spezielle Exception geworfen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T CastToExpectedStateType<T>()
            where T : State
        {
            if (ActiveState is T)
                return (T)ActiveState;
            else
                throw new UnexcpectedStateException(ActiveState);
        }

        /// <summary>
        ///  Liefert den Datentyp des aktuellen Zustandes
        /// </summary>
        /// <returns></returns>
        public Type TypeOfActiveState
        {
            get
            {
                return ActiveState.GetType();
            }
        }


        /// <summary>
        /// Liefert den aktuellen Zustand in seinem Exakten typ zurück.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetActiveState<T>()
            where T : State
        {
            return CastToExpectedStateType<T>();
        }

    }
}
