//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer.Mocks
//  Name..........: UserRepository.cs
//  Aufgabe/Fkt...: Mockup für Repository der Script- Autoren
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
    public class UsersRepository : CanvasScriptServer.UserRepository
    {
        CanvasScriptRepository _Scripts;
        System.Collections.Generic.Queue<Action> cudActions = new Queue<Action>();

        public UsersRepository(CanvasScriptRepository Scripts)
        {
            _Scripts = Scripts;
        }


        List<IUser> _Users = new List<IUser>();


        public override IQueryable<IUser> BoCollection
        {
            get { return _Users.AsQueryable(); }
        }


        public override IUser CreateBoAndAddToCollection(string Name)
        {
            var entity = new User(_Scripts);
            entity.Name = Name;

            cudActions.Enqueue(() => _Users.Add(entity));
            
            return entity;
        } 

        public override Func<IUser, bool> GetBoIDTest(string id)
        {
            return r => r.Name == id;
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void RemoveFromCollection(string Name)
        {
            var rec = _Users.Find(r => r.Name == Name);
            cudActions.Enqueue(() => {
                var myScriptNames = _Scripts.BoCollection.Where(r => r.User.Name == Name).Select(r => r.Name);
                foreach (var scriptName in myScriptNames)
                {
                    _Scripts.RemoveFromCollection(scriptName);
                }
                _Users.Remove(rec); 

            });
        }

        public override void SubmitChanges()
        {
            while (cudActions.Any())
            {
                cudActions.Dequeue()();
            }
            Debug.WriteLine("Änderungen an UserRepository übernommen. Anz:" +_Users.Count);
        }


        public override bool Any(string username)
        {
            return _Users.Any(r => r.Name == username);
        }
    }
}
