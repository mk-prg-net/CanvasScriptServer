using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB.Repository
{
    public class CanvasScriptRepository: CanvasScriptServer.CanvasScriptRepository<Scripts>
    {

        public CanvasScriptRepository(CanvasScriptDBContainer Orm)
        {
            this.Orm = Orm;
        }

        CanvasScriptDBContainer Orm;        


        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }
        

        public override void SubmitChanges()
        {
            Orm.SaveChanges();
        }

        public override bool Any(CanvasScriptKey id)
        {
            return Orm.ScriptsSet.Any(r => r.Name == id.Scriptname && r.User.Name.Name == id.Username);
        }

        internal Scripts GetScript(CanvasScriptKey id)
        {
            return Orm.ScriptsSet.FirstOrDefault(r => r.User.Name.Name == id.Username && r.Name == id.Scriptname);
        }

        public override Scripts GetBo(CanvasScriptKey id)
        {
            return GetScript(id);
        }

        public override IEnumerable<Scripts> Get(System.Linq.Expressions.Expression<Func<Scripts, bool>> filter = null, Func<IQueryable<Scripts>, IOrderedQueryable<Scripts>> orderBy = null, string includeProperties = "")
        {
            IQueryable<Scripts> query = Orm.ScriptsSet; //.AsQueryable();

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


        public override Func<Scripts, bool> GetBoIDTest(CanvasScriptKey id)
        {
            return r => r.User.Name.Name == id.Username && r.Name == id.Scriptname;
        }

        public override ICanvasScriptBuilder GetBoBuilder(CanvasScriptKey id)
        {
            return GetScript(id);
        }

        public override void RemoveFromCollection(CanvasScriptKey id)
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

    }
}
