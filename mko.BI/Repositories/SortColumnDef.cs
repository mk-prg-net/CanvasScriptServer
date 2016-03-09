
//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 17.11.2011
//
//  Projekt.......: mkoItDb
//  Name..........: SortColumnDef.cs
//  Aufgabe/Fkt...: Definition der Sortierung einer Tabellenspate in einer 
//                  Gridview.
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
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.BI.Repositories
{
    /// <summary>
    /// Beschreibt nach welcher Spalte wie sortiert werden soll
    /// </summary>
    public class SortColumnDef : ICloneable
    {
        public SortColumnDef() { SortDescending = false; }

        /// <summary>
        /// Typ der Geschäftsobjketes, aus dem die sortierende Spalte stammt
        /// </summary>
        public Type BoType { get; set; }

        /// <summary>
        /// Name der Spalte, nach der sortiert werden soll
        /// </summary>
        public string ColName { get; set; }

        const int PrimBase = 100003;
        // 6.7.2014, mko
        // PrimBaseMulti berechnet
        const int PrimBaseMulti = int.MaxValue / PrimBase;
        public override int GetHashCode()
        {
            return PrimBase * (BoType.GetHashCode()) % (PrimBaseMulti) + ColName.GetHashCode() % PrimBase;
        }

        /// <summary>
        /// Soll absteigend (true) oder aufsteigend sortiert werden ?
        /// </summary>
        public bool SortDescending { get; set; }

        public object Clone()
        {
            return new SortColumnDef()
            {
                SortDescending = this.SortDescending,
                ColName = this.ColName,
                BoType = this.BoType
            };
        }
    }

}
