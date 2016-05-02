using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB.Repository
{
    public class CanvasScriptRepository: CanvasScriptServer.CanvasScriptRepository
    {

        public CanvasScriptRepository(CanvasScriptDBContainer Orm)
        {
            this.Orm = Orm;
        }

        CanvasScriptDBContainer Orm;        


        public override void RemoveAllBo()
        {
            throw new NotImplementedException();
        }
        

        public override bool ExistsBo(CanvasScriptKey id)
        {
            return Orm.ScriptsSet.Any(r => r.Name == id.Scriptname && r.User.Name.Name == id.Username);
        }

        internal Scripts GetScript(CanvasScriptKey id)
        {
            return Orm.ScriptsSet.FirstOrDefault(r => r.User.Name.Name == id.Username && r.Name == id.Scriptname);
        }

        public override ICanvasScript GetBo(CanvasScriptKey id)
        {
            return GetScript(id);
        }

        public override ICanvasScriptBuilder GetBoBuilder(CanvasScriptKey id)
        {
            return GetScript(id);
        }

        public override void RemoveBo(CanvasScriptKey id)
        {
            var script = GetScript(id);
            if (null != script)
            {
                Orm.ScriptsSet.Remove(script);                
            }
            else
            {
                throw new Exception("Das zu löschende Script mit dem Namen " + id.Scriptname + "existiert nicht");
            }            
        }


        public class FilteredAnSortedSetBuilder : CanvasScriptServer.CanvasScriptRepository.IFilteredAndSortedSetBuilder
        {


            IQueryable<Scripts> _query;
            List<mko.BI.Repositories.DefSortOrder<Scripts>> _SortOrders = new List<mko.BI.Repositories.DefSortOrder<Scripts>>();

            internal FilteredAnSortedSetBuilder(DB.CanvasScriptDBContainer Orm)
            {
                _query = Orm.ScriptsSet;
            }



            public void defNameLike(string pattern)
            {
                _query = _query.Where(r => r.Name.Contains(pattern));
            }

            public void defAuthor(string name)
            {
                _query = _query.Where(r => r.User.Name.Name.Contains(name));
            }

            public void defCreatedBetween(DateTime begin, DateTime end)
            {
                _query = _query.Where(r => begin <= r.Created && r.Created <= end);
            }

            public void defModifiedBetween(DateTime begin, DateTime end)
            {
                _query = _query.Where(r => begin <= r.Modified && r.Modified <= end);
            }

            public mko.BI.Repositories.Interfaces.IFilteredSortedSet<ICanvasScript> GetSet()
            {
                if (!_SortOrders.Any())
                {
                    _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Scripts, DateTime>(r => r.Created, true));
                }
                else { }

                return new mko.BI.Repositories.FilteredSortedSet<Scripts>(_query, _SortOrders);
            }
        }


        public override CanvasScriptServer.CanvasScriptRepository.IFilteredAndSortedSetBuilder getFilteredAndSortedSetBuilder()
        {
            return new FilteredAnSortedSetBuilder(Orm);
        }
    }
}
