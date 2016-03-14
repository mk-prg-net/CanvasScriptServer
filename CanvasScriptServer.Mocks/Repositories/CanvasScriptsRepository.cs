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
        //static List<ICanvasScript> _Scripts = new List<ICanvasScript>();
        List<ICanvasScript> _Scripts = new List<ICanvasScript>();

        System.Collections.Generic.Queue<Action> cudActions = new Queue<Action>();


        public override IQueryable<ICanvasScript> BoCollection
        {
            get { return _Scripts.AsQueryable(); }
        }


        public override ICanvasScript CreateBoAndAddToCollection(string Name)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(Name));

            var entity = new CanvasScript();
            entity.Name = Name;
            entity.Created = DateTime.Now;

            cudActions.Enqueue(() => _Scripts.Add(entity));
            
            return entity;
        }

        public override Func<ICanvasScript, bool> GetBoIDTest(string id)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(id));

            return r => r.Name == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(string Name)
        {
            System.Diagnostics.Contracts.Contract.Requires(!string.IsNullOrWhiteSpace(Name));

            var entity = _Scripts.Single(r => r.Name == Name);
            
            cudActions.Enqueue(() => _Scripts.Remove(entity));
        }

        public override void SubmitChanges()
        {
            while (cudActions.Any())
            {
                cudActions.Dequeue()();
            }
            Debug.WriteLine("Änderungen an UserRepository übernommen. Anz:" + _Scripts.Count);   
        }

        public override bool Any(string scriptname)
        {
            return _Scripts.Any(r => r.Name == scriptname);
        }
    }
}
