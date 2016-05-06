using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

// Für Erweiterungsmethode Include
using System.Data.Entity;


namespace CanvasScriptServer.DB.Repository
{
    public class UserRepository : CanvasScriptServer.UserRepositoryV2
    {
        public UserRepository(CanvasScriptDBContainer Orm)
        {
            this.Orm = Orm;
        }

        private CanvasScriptDBContainer Orm;

        public override void CreateBoAndAdd(string userName)
        {
            var e = Orm.UsersSet.Create();
            e.Name = Orm.UserNamesSet.Create();
            e.Name.Name = userName;
            //e.Name.User = e;
            e.Created = DateTime.Now;
            Orm.UsersSet.Add(e);
        }



        public override IUser GetBo(string id)
        {
            var userName = Orm.UserNamesSet.Find(id);
            if(userName != null){
                return userName.User;
            } else {
                throw new ArgumentOutOfRangeException("Der Benutzer mit der ID" + id + " existiert nicht");
            }           
        }


        public override bool ExistsBo(string id)
        {
            // Suche bezüglich Schlüssel ist schneller als allgemeines .Any(...)
            return null != Orm.UserNamesSet.Find(id);   
        }

        public class FilteredAndSortedSetBuilder : CanvasScriptServer.UserRepositoryV2.IFilteredSortedSetBuilder
        {
            IQueryable<Users> _query;
            private CanvasScriptDBContainer Orm;
            List<mko.BI.Repositories.DefSortOrder<Users>> _SortOrders = new List<mko.BI.Repositories.DefSortOrder<Users>>();

            internal FilteredAndSortedSetBuilder(CanvasScriptDBContainer Orm)
            {
                this.Orm = Orm;
                _query = Orm.UsersSet.Include(r => r.Name);
            }

            public void defNameLike(string pattern)
            {
                _query = _query.Where(r => r.Name.Name.Contains(pattern));
            }

            public void CreatedBetween(DateTime begin, DateTime end)
            {
                _query = _query.Where(r => r.Created >= begin && r.Created <= end);
            }

            public void sortByUserName(bool descending)
            {
                _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Users, string>(r => r.Name.Name, descending));
            }

            public void sortByScriptCount(bool descending)
            {
                _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Users, int>(r => r.Scripts.Count, descending));
            }

            public mko.BI.Repositories.Interfaces.IFilteredSortedSet<IUser> GetSet()
            {
                if (!_SortOrders.Any())
                {
                    _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<Users, DateTime>(r => r.Created, true));
                }
                else { }

                return new mko.BI.Repositories.FilteredSortedSet<Users>(_query, _SortOrders);

            }

        }

        public override UserRepositoryV2.IFilteredSortedSetBuilder getFilteredSortedSetBuilder()
        {
            return new FilteredAndSortedSetBuilder(Orm);
        }
    }
}
