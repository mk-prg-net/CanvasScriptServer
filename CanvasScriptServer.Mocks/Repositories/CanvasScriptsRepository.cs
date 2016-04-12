//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016 
//
//  Projekt.......: CanvasScriptServer.Mocks
//  Name..........: CanvasScriptsRepository.cs
//  Aufgabe/Fkt...: Mockup für CanvasScriptsRepository
//                  
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
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        
        
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScriptsRepository : CanvasScriptServer.CanvasScriptRepository
    {
        List<CanvasScript> _ScriptList = new List<CanvasScript>();

        System.Collections.Generic.Queue<Action> cudActions = new Queue<Action>();


        public override IQueryable<ICanvasScript> BoCollection
        {
            get { return _ScriptList.AsQueryable(); }
        }

        public readonly string DefaultScript = "[{\"beginPath\": \"true\"}, {\"strokeStyle\": { \"Style\": \"rgb(255, 255, 0)\"}}, {\"moveTo\": {\"X\": 0, \"Y\":0}}, {\"lineTo\": {\"X\": 100, \"Y\":100}}, { \"closePath\": true}, {\"stroke\": true} ]";
        
        internal void CreateBoAndAddToCollection(IUser user, string NameOfScript)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(NameOfScript));

            var entity = new CanvasScript();
            entity._Name = NameOfScript;
            entity._Created = DateTime.Now;
            entity._Author = user;
            entity._ScriptAsJson = DefaultScript;
            cudActions.Enqueue(() => _ScriptList.Add(entity));                
        }

        public override Func<ICanvasScript, bool> GetBoIDTest(CanvasScriptKey id)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(id.Scriptname));

            return r => r.Author.Name == id.Username && r.Name == id.Scriptname;
        }


        Func<CanvasScript, bool> GetBoIDTestIntern(CanvasScriptKey id)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(id.Scriptname));

            return r => r.Author.Name == id.Username && r.Name == id.Scriptname;
        }


        public override void RemoveFromCollection(CanvasScriptKey id)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(id.Scriptname));

            var entity = _ScriptList.Single(GetBoIDTestIntern(id));
            
            cudActions.Enqueue(() => _ScriptList.Remove(entity));
        }

        public override void SubmitChanges()
        {
            while (cudActions.Any())
            {
                cudActions.Dequeue()();
            }
            Debug.WriteLine("Änderungen an UserRepository übernommen. Anz:" + _ScriptList.Count);   
        }

        public override bool Any(CanvasScriptKey id)
        {
            return _ScriptList.Any(GetBoIDTest(id));
        }


        public override ICanvasScript GetBo(CanvasScriptKey id)
        {
            return _ScriptList.Find(r => r.Author.Name == id.Username && r.Name == id.Scriptname);
        }

        public override ICanvasScriptBuilder GetBoBuilder(CanvasScriptKey id)
        {
            return new Bo.CanvasScriptBuilder(_ScriptList.Find(r => r.Author.Name == id.Username && r.Name == id.Scriptname));
        }
    }
}
