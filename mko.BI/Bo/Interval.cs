//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart 2010
//
//  Projekt.......: mko
//  Name..........: Intervall.cs
//  Aufgabe/Fkt...: Implementierung von Intervallen auf beliebigen Ordnungen
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 2010
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 30.4.2014
//  Änderungen....: Klasse mko.Intervall aus Projekt mko in Klasse mko.Algo.Interval in 
//                  Projekt mko.Algo übertragen
//
//  Version.......: 2.0
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 10.7.2015
//  Änderungen....: Klasse mko.Intervall aus Projekt mko.Algo in mko.BI übertragen. 
//                  Klasse in eine Datenstruktur umgewandelt.
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Bo
{
    [Serializable]
    public struct Interval<T>
        where T : IComparable<T>, new()        
    {

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="Begin"></param>
        /// <param name="End"></param>
        public Interval(T Begin, T End)
        {
            this.Begin = Begin;
            this.End = End;            
        }



        /// <summary>
        /// Wenn true, dann ist das Intervall leer, egal was Begin oder end anzeigt
        /// Intervall ist leer, wenn Begin > End ist ist
        /// Begin == End ist ein Intervall, das genau ein Element enthält  
        /// </summary>
        public bool Empty
        {
            get
            {
                return Begin.CompareTo(End) > 0;
            }
        }

        public T Begin;
        
        public T End;
        

        public override string ToString()
        {
            return "[" + Begin.ToString() + ", " + End.ToString() + "]";
        }

        public bool Contains(T value)
        {
            return (Begin.CompareTo(value) <= 0 && End.CompareTo(value) >= 0);

        }

        /// <summary>
        /// Berechnet das größte Intervall, dass gemeinsam in beiden Intervallen enthalten ist
        /// 
        /// Fälle:
        /// 1)
        /// -----[aaaaaa]----------------
        /// ---------[bbbbb]-------------
        /// ---------[rr]---------------- Result
        /// 
        /// 2)
        /// ---------[aaaaaa]------------
        /// -------[bbbbb]---------------
        /// ---------[rrr]--------------- Result
        /// 
        /// 3)
        /// ---------[aaaaaa]------------
        /// -------[bbbbbbbbbb]----------
        /// ---------[rrrrrr]------------ Result
        /// 
        /// 4)
        /// -------[aaaaaaaaaa]----------
        /// ---------[bbbbbb]------------
        /// ---------[rrrrrr]------------ Result
        /// 
        /// 5)
        /// ---[aaaaaa]------------------
        /// ---------------[bbbbb]-------
        /// -----------[]---------------- Result
        /// 
        /// </summary>
        /// <param name="Second"></param>
        /// <returns></returns>
        public Interval<T> Intersect(Interval<T> Second)
        {
            if (Empty || Second.Empty)
                /// Fall: Schnittmenge mit leeren Intervallen            
                return new Interval<T>();
            else if (Begin.CompareTo(Second.Begin) < 0 && End.CompareTo(Second.End) <= 0)
                /// 1)
                /// -----[aaaaaa]----------------
                /// ---------[bbbbb]-------------
                /// ---------[rr]---------------- Result
                return new Interval<T>(Second.Begin, End);
            else if (Begin.CompareTo(Second.Begin) >= 0 && End.CompareTo(Second.End) > 0)
                /// 2)
                /// ---------[aaaaaa]------------
                /// -------[bbbbb]---------------
                /// ---------[rrr]--------------- Result
                return new Interval<T>(Begin, Second.End);
            else if (Begin.CompareTo(Second.Begin) >= 0 && End.CompareTo(Second.End) <= 0)
                /// 3)
                /// ---------[aaaaaa]------------
                /// -------[bbbbbbbbbb]----------
                /// ---------[rrrrrr]------------ Result
                return new Interval<T>(Begin, End);
            else if (Begin.CompareTo(Second.Begin) < 0 && End.CompareTo(Second.End) > 0)
                /// 4)
                /// -------[aaaaaaaaaa]----------
                /// ---------[bbbbbb]------------
                /// ---------[rrrrrr]------------ Result
                return new Interval<T>(Second.Begin, Second.End);
            else
                /// 5)
                /// ---[aaaaaa]------------------
                /// ---------------[bbbbb]-------
                /// -----------[]---------------- Result
                return new Interval<T>();

        }

        /// <summary>
        /// Berechnet ein Interval, das beide Intervalle einschließt
        /// ---[aaaaaa]------------------
        /// ---------------[bbbbb]-------
        /// ---[rrrrrrrrrrrrrrrrr]------ Result        
        /// </summary>
        /// <param name="Second"></param>
        /// <returns></returns>
        public Interval<T> Union(Interval<T> Second)
        {
            // Berechnen der umschließenden Intervallgrenzen
            T newBegin = (Begin.CompareTo(Second.Begin) <= 0) ? Begin : Second.Begin;
            T newEnd = (End.CompareTo(Second.End) >= 0) ? End : Second.End;

            return new Interval<T>(newBegin, newEnd);
        }

    }
}
