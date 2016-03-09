//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: DefSortOrder.cs
//  Aufgabe/Fkt...: Basisklasse für Funktoren, die eine Sortierung definieren.
//                  Hervorgegangen aus mkoIt.Db.SortColumnDef vom 17.11.2011.
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
//  Datum.........: 15.4.2015
//  Änderungen....: Rückgabetypen von IOrderedEnumerable auf IOrderedQueryable umgestellt. 
//                  hierdurch können Sortierungen z.B. direkt auf dem SQL Server ausgeführt werden.
//</unit_history>
//</unit_header>        
        
using System;        
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories
{
    public abstract class DefSortOrder<TBo>
        where TBo : class
    {
        public DefSortOrder(bool Descending)
        {
            _Descending = Descending;
        }

        bool _Descending;
        public bool Descending {
            get
            {
                return _Descending;
            }
        }

        protected abstract IOrderedQueryable<TBo> MainOrderByCol(IQueryable<TBo> tab);
        protected abstract IOrderedQueryable<TBo> MainOrderDescByCol(IQueryable<TBo> tab);
        public IOrderedQueryable<TBo> MainOrder(IQueryable<TBo> tab)
        {
            if (Descending)
                return MainOrderDescByCol(tab);
            else
                return MainOrderByCol(tab);

        }


        protected abstract IOrderedQueryable<TBo> ThenOrderByCol(IOrderedQueryable<TBo> tab);
        protected abstract IOrderedQueryable<TBo> ThenOrderDescByCol(IOrderedQueryable<TBo> tab);
        public IOrderedQueryable<TBo> ThenOrder(IOrderedQueryable<TBo> tab)
        {
            if (Descending)
                return ThenOrderDescByCol(tab);
            else
                return ThenOrderByCol(tab);
                
        }

        //const int PrimBase = 100003;
        //// 6.7.2014, mko
        //// PrimBaseMulti berechnet
        //const int PrimBaseMulti = int.MaxValue / PrimBase;
        //public override int GetHashCode()
        //{
        //    return PrimBase * typeof(TBo).GetHashCode() % (PrimBaseMulti) + ColName.GetHashCode() % PrimBase;
        //}

    }
}
