using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB.Repository
{
    public class UserRepository : CanvasScriptServer.UserRepository
    {
        public UserRepository(CanvasScriptDBContainer Orm)
        {
            this.Orm = Orm;
        }

        private CanvasScriptDBContainer Orm;
        

        public override bool Any(string username)
        {
            // Suche bezüglich Schlüssel ist schneller als allgemeines .Any(...)
            return null != Orm.UserNamesSet.Find(username);            
        }


        public override IQueryable<IUser> BoCollection
        {
            get { 
                return Orm.UsersSet;
            }
        }


        public override IUser CreateBoAndAddToCollection(string userName)
        {
            var e = Orm.UsersSet.Create();
            e.Name = Orm.UserNamesSet.Create();
            e.Name.Name = userName;
            Orm.UsersSet.Add(e);
            return e;
        }

        public override Func<IUser, bool> GetBoIDTest(string id)
        {
            return e => e.Name == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(string userName)
        {
            var uN = Orm.UserNamesSet.Find(userName);
            if (uN != null)
            {
                var user = uN.User;
                Orm.UserNamesSet.Remove(uN);
                Orm.UsersSet.Remove(user);
            }            
        }

        public override void SubmitChanges()
        {
            Orm.SaveChanges();
        }
    }
}
