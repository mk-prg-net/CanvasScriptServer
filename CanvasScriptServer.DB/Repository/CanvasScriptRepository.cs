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


        public override IQueryable<ICanvasScript> BoCollection
        {
            get { 
                return Orm.ScriptsSet;
            }
        }
        


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

        public override ICanvasScript GetBo(CanvasScriptKey id)
        {
            return GetScript(id);
        }

        public override Func<ICanvasScript, bool> GetBoIDTest(CanvasScriptKey id)
        {
            return r => r.Author.Name == id.Username && r.Name == id.Scriptname;
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
