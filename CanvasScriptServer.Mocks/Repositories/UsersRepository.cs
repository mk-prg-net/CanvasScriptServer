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
        static List<IUser> _Users = new List<IUser>();

        public override void AddToCollection(IUser entity)
        {
            _Users.Add(new User(entity));
        }

        public override IQueryable<IUser> BoCollection
        {
            get { return _Users.AsQueryable(); }
        }

        public override IUser CreateBo()
        {
            var user = new User();
            user.Scripts = new CanvasScript[]{};
            return user;
        }

        public override IUser CreateBoAndAddToCollection()
        {
            var entity = new User();
            _Users.Add(entity);
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

        public override void RemoveFromCollection(IUser entity)
        {
            var rec = _Users.Find(r => r.Name == entity.Name);
            _Users.Remove(rec);
        }

        public override void SubmitChanges()
        {
            Debug.WriteLine("Änderungen an UserRepository übernommen. Anz:" +_Users.Count);
        }


        public override bool Any(string username)
        {
            return _Users.Any(r => r.Name == username);
        }
    }
}
