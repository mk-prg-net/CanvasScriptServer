using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.BI.Repositories
{
    public class FiltersCombine<TEntity>
    {
        //------------------------------------------------------------------------------------------
        // Filtern

        // Liste aller Filter
        public List<Filter<TEntity>> AllFilter = new List<Filter<TEntity>>();

        public void LetFilter(Filter<TEntity> srcFilter)
        {
            AllFilter.Add(srcFilter);
        }

        object prepareValue(object val)
        {
            return val;
        }

        public int filterRowsCount(IQueryable<TEntity> tab)
        {
            return filter(tab).Count();
        }

        public IQueryable<TEntity> filter(IQueryable<TEntity> tab)
        {
            if (tab == null)
                return null;
            if (AllFilter.Count == 0)
                // Fall: keine Filterung erwünscht
                return tab;
            else
            {
                // Wandeln in Array, damit Linq to Object angewendet werden kann
                //TEntity[] tabA = tab.ToArray();

                // Anwenden aller Filter
                foreach (Filter<TEntity> flt in AllFilter)
                {
                    tab = flt.filterImpl(tab);
                }

                return tab;
            }
        }
    }
}
