using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Linq.Expressions;

namespace CanvasScriptServer
{
    public abstract class UserRepositoryV2<TUser> :        
        mko.BI.Repositories.Interfaces.ICreateUpdate<string>,
        mko.BI.Repositories.Interfaces.IRemove<string>,
        mko.BI.Repositories.Interfaces.IGetBo<TUser, string>,
        mko.BI.Repositories.Interfaces.ISubmitChanges
        where TUser : IUser

    {
        // mko.BI.Repositories.Interfaces.ICreateUpdate<ICanvasScript>,
        public abstract void CreateBoAndAddToCollection(string id);       

        // mko.BI.Repositories.Interfaces.IRemove<ICanvasScript>,
        public abstract void RemoveAll();        
        public abstract void RemoveFromCollection(string id);

        // mko.BI.Repositories.Interfaces.ISubmitChanges
        public abstract void SubmitChanges();

        // mko.BI.Repositories.Interfaces.IGetBo<ICanvasScript, string>,
        public abstract bool Any(string id);
        public abstract TUser GetBo(string id);
        public abstract Func<TUser, bool> GetBoIDTest(string id);

        

        public abstract IEnumerable<TUser> Get(Expression<Func<TUser, bool>> filter = null,
                                               Func<IQueryable<TUser>, IOrderedQueryable<TUser>> orderBy = null,
                                               string includeProperties = "");
        
    }
}
