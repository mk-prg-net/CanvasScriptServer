//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: DefSortOrderCol.cs
//  Aufgabe/Fkt...: Sortierreihenfolge bezüglich einer Spalte durch einen Lambda- Selektor definieren
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
//  Datum.........: 15.4.2015
//  Änderungen....: Rückgabetypen von IOrderedEnumerable auf IOrderedQueryable umgestellt. 
//                  hierdurch können Sortierungen z.B. direkt auf dem SQL Server ausgeführt werden.
//                  Dazu musste der Lambda- Spaltenselektor von Func<TBo, TCol> auf 
//                  Expression<Func<TBo, TCol>> abgeändert werden. Siehe auch http://stackoverflow.com/questions/7479911/include-orderby-delegate-in-method-parameters
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace mko.BI.Repositories
{
    public class DefSortOrderCol<TBo, TCol>: DefSortOrder<TBo>
        where TBo : class
    {

        public DefSortOrderCol(Expression<Func<TBo, TCol>> ColSelector, bool Descending)
            : base(Descending)
        {
            _ColSelector = ColSelector;
        }

        Expression<Func<TBo, TCol>> _ColSelector;

        protected override IOrderedQueryable<TBo> MainOrderByCol(IQueryable<TBo> tab)
        {
            return tab.OrderBy(_ColSelector);
        }

        protected override IOrderedQueryable<TBo> MainOrderDescByCol(IQueryable<TBo> tab)
        {
            return tab.OrderByDescending(_ColSelector);
        }

        protected override IOrderedQueryable<TBo> ThenOrderByCol(IOrderedQueryable<TBo> tab)
        {
            return tab.ThenBy(_ColSelector);
        }

        protected override IOrderedQueryable<TBo> ThenOrderDescByCol(IOrderedQueryable<TBo> tab)
        {
            return tab.ThenByDescending(_ColSelector);
        }
    }
}
