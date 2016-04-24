using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class UsersRepositoryV2 : global::CanvasScriptServer.UserRepositoryV2<User>
    {
        CanvasScriptRepository<CanvasScript> _Scripts;
        System.Collections.Generic.Queue<Action> cudActions = new Queue<Action>();

        public UsersRepositoryV2(CanvasScriptRepository<CanvasScript> Scripts)
        {
            _Scripts = Scripts;
        }


        List<User> _Users = new List<User>();


        public override void CreateBoAndAddToCollection(string Name)
        {
            var entity = new User(_Scripts);
            entity.Name = Name;

            cudActions.Enqueue(() => _Users.Add(entity));            
        } 

        public override Func<User, bool> GetBoIDTest(string id)
        {
            return r => r.Name == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(string Name)
        {
            var rec = _Users.Find(r => r.Name == Name);
            cudActions.Enqueue(() => {
                var myScriptNames = _Scripts.Get(filter: r => r.Author.Name == Name).Select(r => r.Name);
                foreach (var scriptName in myScriptNames)
                {
                    _Scripts.RemoveFromCollection(CanvasScriptKey.Create(Name, scriptName));
                }
                _Users.Remove(rec); 

            });
        }

        public override void SubmitChanges()
        {
            while (cudActions.Any())
            {
                cudActions.Dequeue()();
            }
            Debug.WriteLine("Änderungen an UserRepository übernommen. Anz:" +_Users.Count);
        }


        public override bool Any(string username)
        {
            return _Users.Any(r => r.Name == username);
        }

        public override User GetBo(string id)
        {
            return _Users.Find(r => r.Name == id);
        }

        public override IEnumerable<User> Get(System.Linq.Expressions.Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "")
        {
            IQueryable<User> query = _Users.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }           

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
