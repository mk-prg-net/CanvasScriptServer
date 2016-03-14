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

        public override bool Any(string scriptname)
        {
            return Orm.ScriptsSet.Any(r => r.Name == scriptname);
        }


        public override IQueryable<ICanvasScript> BoCollection
        {
            get { 
                return Orm.ScriptsSet;
            }
        }
        

        public override ICanvasScript CreateBoAndAddToCollection(string scriptName)
        {
            var e = Orm.ScriptsSet.Create();
            Orm.ScriptsSet.Add(e);
            e.Name = scriptName;
            e.Created = DateTime.Now;
            return e;
        }

        public override Func<ICanvasScript, bool> GetBoIDTest(string id)
        {
            throw new NotImplementedException();
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }
        
        public override void RemoveFromCollection(string scriptName)
        {
            if (Orm.ScriptsSet.Any(r => r.Name == scriptName))
            {
                Orm.ScriptsSet.Remove(Orm.ScriptsSet.First(r => r.Name == scriptName));
            }
            else
            {
                throw new Exception("Das zu löschende Script mit dem Namen " + scriptName + "existiert nicht");
            }
        }

        public override void SubmitChanges()
        {
            Orm.SaveChanges();
        }
    }
}
