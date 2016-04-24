﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace CanvasScriptServer.DB.Repository
{
    public class UserRepository : CanvasScriptServer.UserRepositoryV2<Users>
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


        public override void CreateBoAndAddToCollection(string userName)
        {
            var e = Orm.UsersSet.Create();
            e.Name = Orm.UserNamesSet.Create();
            e.Name.Name = userName;
            Orm.UsersSet.Add(e);
        }

        public override Func<Users, bool> GetBoIDTest(string id)
        {
            return e => e.Name.Name == id;
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

        public override Users GetBo(string id)
        {
            var userName = Orm.UserNamesSet.Find(id);
            if(userName != null){
                return userName.User;
            } else {
                throw new ArgumentOutOfRangeException("Der Benutzer mit der ID" + id + " existiert nicht");
            }           
        }

        public override IEnumerable<Users> Get(System.Linq.Expressions.Expression<Func<Users, bool>> filter = null, Func<IQueryable<Users>, IOrderedQueryable<Users>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Users> query = Orm.UsersSet; //.AsQueryable();

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
