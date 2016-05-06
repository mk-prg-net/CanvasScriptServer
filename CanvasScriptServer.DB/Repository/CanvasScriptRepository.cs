//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.3.2016
//
//  Projekt.......: CanvasScriptServer.DB
//  Name..........: CanvasScriptRepository.cs
//  Aufgabe/Fkt...: Implementierung des CanvasScriptRepository für eine 
//                  mittels EF 6.0 Model First erstellte DB
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 6.5.2016
//  Änderungen....: Eager Loading für WebApi Zugriffe implementiert
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Für Erweiterungsmethode Include
using System.Data.Entity;

namespace CanvasScriptServer.DB.Repository
{
    public partial class CanvasScriptRepository: CanvasScriptServer.CanvasScriptRepository
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
            return Orm.ScriptsSet.Include(r => r.User.Name).Any(r => r.Name == id.Scriptname && r.User.Name.Name == id.Username);
        }

        internal Scripts GetScript(CanvasScriptKey id)
        {
            return Orm.ScriptsSet.Include(r => r.User.Name).FirstOrDefault(r => r.User.Name.Name == id.Username && r.Name == id.Scriptname);
        }

        public override ICanvasScript GetBo(CanvasScriptKey id)
        {
            return new CanvasScriptFlat(GetScript(id));
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
    }
}
