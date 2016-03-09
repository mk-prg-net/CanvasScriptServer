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
        static List<ICanvasScript> _Scripts = new List<ICanvasScript>();

        public override void AddToCollection(ICanvasScript entity)
        {
            _Scripts.Add(entity);
        }

        public override IQueryable<ICanvasScript> BoCollection
        {
            get { return _Scripts.AsQueryable(); }
        }

        public override ICanvasScript CreateBo()
        {            
            return new CanvasScript();
        }

        public override ICanvasScript CreateBoAndAddToCollection()
        {
            var entity = new CanvasScript();
            _Scripts.Add(entity);
            return entity;
        }

        public override Func<ICanvasScript, bool> GetBoIDTest(string id)
        {
            return r => r.Name == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(ICanvasScript entity)
        {
            _Scripts.Remove(entity);
        }

        public override void SubmitChanges()
        {
            Debug.WriteLine("Änderungen an UserRepository übernommen. Anz:" + _Scripts.Count);   
        }

        public override bool Any(string scriptname)
        {
            return _Scripts.Any(r => r.Name == scriptname);
        }
    }
}
