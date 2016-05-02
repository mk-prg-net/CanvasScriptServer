using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class UsersRepositoryV2 : global::CanvasScriptServer.UserRepositoryV2
    {
        List<User> _Users;
        internal List<CanvasScript> _Scripts;
        System.Collections.Generic.Queue<Action> _cudActions;

        public UsersRepositoryV2(List<User> Users, List<CanvasScript> Scripts, System.Collections.Generic.Queue<Action> cudActions)
        {
            _Users = Users;
            _Scripts = Scripts;
            _cudActions = cudActions;
        }        
        public override void CreateBoAndAdd(string Name)
        {
            if (!_Users.Any(r => r.Name == Name))
            {
                var entity = new User(Name);
                _cudActions.Enqueue(() => _Users.Add(entity));
            }
            else
            {
                throw new System.Data.ConstraintException("Es existiert bereits ein User mit dem Namen" + Name);
            }
        } 


        public override bool ExistsBo(string username)
        {
            return _Users.Any(r => r.Name == username);
        }

        public override IUser GetBo(string id)
        {
            return _Users.Find(r => r.Name == id);
        }

        public class FilteredAndSortedSetBuilder : CanvasScriptServer.UserRepositoryV2.IFilteredSortedSetBuilder
        {
            IQueryable<User> _query;
            List<CanvasScript> _Scripts;
            List<mko.BI.Repositories.DefSortOrder<User>> _SortOrders = new List<mko.BI.Repositories.DefSortOrder<User>>();

            
            internal FilteredAndSortedSetBuilder(List<User> Users, List<CanvasScript> Scripts)
            {
                _query = Users.AsQueryable();
                _Scripts = Scripts;
            }

            // Filterkriterien

            public void defNameLike(string pattern)
            {
                _query = _query.Where(r => r.Name.Contains(pattern));
            }

            public void CreatedBetween(DateTime begin, DateTime end)
            {
                _query = _query.Where(r => r.Created >= begin && r.Created <= end);
            }

            // Soriterkriterien
            public void sortByUserName(bool descending)
            {
                _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<User, string>(r => r.Name, descending));
            }

            public void sortByCreated(bool descending)
            {
                _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<User, DateTime>(r => r.Created, descending));
            }

            public void sortByScriptCount(bool descending)
            {
                _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<User, int>(r => _Scripts.Count(rr => rr.AuthorName == r.Name), descending));
            }


            /// <summary>
            /// Teilmenge mit den definierten Einschränkungen und Sortierkriterien erzeugen
            /// </summary>
            /// <returns></returns>
            mko.BI.Repositories.Interfaces.IFilteredSortedSet<IUser> mko.BI.Repositories.Interfaces.IFilteredSortedSetBuilder<IUser>.GetSet()
            {
                if (!_SortOrders.Any())
                {
                    _SortOrders.Add(new mko.BI.Repositories.DefSortOrderCol<User, DateTime>(r => r.Created, true));
                }
                else { }

                return new mko.BI.Repositories.FilteredSortedSet<User>(_query, _SortOrders);
            }
        }


        public override UserRepositoryV2.IFilteredSortedSetBuilder getFilteredSortedSetBuilder()
        {
            return new FilteredAndSortedSetBuilder(_Users, _Scripts);
        }
    }
}
