
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 12.2014
//
//  Projekt.......: mko.BI
//  Name..........: State.cs
//  Aufgabe/Fkt...: Basisklasse der Zustände endlicher Automaten.
//                  Die endlichen Automaten sind Grundlage von Workflows.
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.4.2015
//  Änderungen....: Umzug vom Projekt mko.Automaton.StateMachine in mko.BI.
//                  Integration der Zustandsüberführungsfunktion in die Zustandsklasse 
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 4.2.2016
//  Änderungen....: BehaviorOfState gegen IStateBehavior ausgetauscht.
//                  Next gibt im Fall eines Endzustandes Liste mit Endzustand selber zurück.
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.StateMachine
{
    public class State : IStateBehavior
    {
        /// <summary>
        /// Liste der möglichen Folgezustände
        /// </summary>
        public virtual State[] Next
        {
            get
            {
                // 4.2.2016, mko, Rückgabe von this falls isFinal == true
                if (IsFinal)
                    return new State[] { this };
                else 
                    return _Next;
            }
        }

        State[] _Next;

        /// <summary>
        /// Initialisiert die Liste der Folgezustände dieses Zustands
        /// </summary>
        /// <param name="Next"></param>
        protected virtual void InitNext(State[] Next)
        {
            if (Next != null)
                _Next = Next;
            else
                _Next = new State[] { };
        }
        

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="behavior">Definiert das grundsätzlich Verhalten des Zustandes (Start-, Normal- oder Finaler Zustand)</param>
        /// <param name="Next">Liste der möglichen Folgezustände</param>        
        public State(IStateBehavior behavior, params State[] Next)
        {
            InitBehavior(behavior);
            InitNext(Next);
        }

        /// <summary>
        /// Initialisiert das Verhalten eines Zustandes
        /// </summary>
        /// <param name="behavior"></param>
        protected virtual void InitBehavior(IStateBehavior behavior)
        {
            _IsFinal = behavior.IsFinal;
            _IsStart = behavior.IsStart;            
        }

        /// <summary>
        /// Zustand als Startzustand markieren
        /// </summary>
        /// <returns></returns>
        public static IStateBehavior CreateStartStateBehavior()
        {
            return new StartStateBehavior();
        }

        /// <summary>
        /// Zustand als gewöhnlichen Zustand markieren
        /// </summary>
        /// <returns></returns>
        public static IStateBehavior CreateNormalStateBehavior()
        {
            return new NormalStateBehavior();
        }

        /// <summary>
        /// Zustand als Endzustand markieren
        /// </summary>
        /// <returns></returns>
        public static IStateBehavior CreateFinalStateBehavior()
        {
            return new FinalStateBehavior();
        }

        /// <summary>
        /// Zustand als einzigen Zustand eines deterministischen Automaten markieren,
        /// der Start- und Endzustand in Einem ist.
        /// </summary>
        /// <returns></returns>
        public static IStateBehavior CreateSingleStateMachineBehavior()
        {
            return new SingleStateMachineBehavior();
        }



        /// <summary>
        /// Wenn True, dann ist der Zustand ein Startzustand
        /// </summary>
        public virtual bool IsStart
        {
            get
            {
                return _IsStart;
            }
        }
        bool _IsStart;

        /// <summary>
        /// Wenn True, dann ist der Zustand ein Endzustand
        /// </summary>
        public virtual bool IsFinal
        {
            get
            {
                return _IsFinal;
            }
        }
        bool _IsFinal;


        /// <summary>
        /// Wenn True, dann ist das Objekt ein Zustand vom Typ einer 
        /// von State abgeleiteten Klasse, die neben der Darstellung des Zustandes an sich 
        /// auch noch die mit dem Zustand verknüpften Zustand der Laufzeitumgebung speichert.
        /// Konvention:
        /// Eine Klasse mit der Basisklasse State, deren Name das Suffix "Context" hat, wird als
        /// Context- Klasse erkannt.
        /// </summary>
        public virtual bool IsContext
        {
            get
            {
                string name = GetType().Name;
                if (name.EndsWith("Context"))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Name des Zustandes. Wird aus dem Namen der Klasse ermittelt.
        /// Konvention: Wenn der Name der Klasse Context oder Base lautet, 
        ///             dann wird der letzte Partikel des Namespce zurückgeliefert.
        ///             Voraussetzung ist, die gesamte Klassenhirarchie für einen 
        ///             Zustand in einem eineindeutigen Namespace eingeschlossen ist.
        /// </summary>
        public virtual string Name
        {
            get
            {
                var type = GetType();
                string n = type.Name.ToLower();
                if (n == "context" || n == "base")
                {
                    return type.Namespace.Split('.').Last();
                }
                else
                {
                    return type.Name;
                }
            }
        }
    }
}
