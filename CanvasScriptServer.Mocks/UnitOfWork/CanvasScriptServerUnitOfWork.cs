using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScriptServerUnitOfWork : CanvasScriptServer.ICanvasScriptServerUnitOfWork
    {
        //public CanvasScriptServerUnitOfWork(UserRepository userRepo, CanvasScriptRepository scriptRepo)            
        //{
        //}

        static CanvasScriptServerUnitOfWork()
        {
            _Scripts = new CanvasScriptsRepository();
            _Users = new UsersRepository(_Scripts);
        }

        public CanvasScriptServerUnitOfWork()
        {
        }

        static Mocks.CanvasScriptsRepository _Scripts;
        static Mocks.UsersRepository _Users;

        readonly string DefaultScript =  "[{\"beginPath\": \"true\"}, {\"strokeStyle\": { \"Style\": \"rgb(255, 255, 0)\"}}, {\"moveTo\": {\"X\": 0, \"Y\":0}}, {\"lineTo\": {\"X\": 100, \"Y\":100}}, { \"closePath\": true}, {\"stroke\": true} ]";

        public ICanvasScript createScript(string Username, string NameOfScript)
        {
            // Der Benutzer muss bereits existieren
            if (Users.Any(Username))
            {
                var user = Users.GetBo(Username);
                var newScript = Scripts.CreateBoAndAddToCollection(NameOfScript);                
                newScript.User = user;
                newScript.Created = DateTime.Now;
                newScript.ScriptAsJson = DefaultScript;
                //user.Scripts = user.Scripts.Concat(new ICanvasScript[] { newScript }).ToArray();

                return newScript;
            }
            else
            {
                throw new Exception("Scriptautor " + Username + " existiert nicht");
            }            
        }

        class EqComp : IEqualityComparer<ICanvasScript>
        {

            public bool Equals(ICanvasScript x, ICanvasScript y)
            {
                return x.Name == y.Name && x.Created == y.Created;
            }

            public int GetHashCode(ICanvasScript obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        public void deleteScript(string scriptName)
        {
            // Script aus den Scripten nehmen, die dem Benutzer zugeordnet sind
            Scripts.RemoveFromCollection(scriptName);
        }

        public UserRepository Users
        {
            get { return _Users; }
        }

        public CanvasScriptRepository Scripts
        {
            get { return _Scripts; }
        }

        public void saveChanges()
        {
            Users.SubmitChanges();
            Scripts.SubmitChanges();
            Debug.WriteLine("In CanvasScriptServerUnitOfWorks wurden die Änderungen übernommen");
        }

        public IUser createUser(string Name)
        {
            return Users.CreateBoAndAddToCollection(Name);            
        }

        public void deleteUser(string username)
        {
            if (Users.Any(username))
            {
                // Alle Scripte vom Benutzer löschen
                var scriptNames = Scripts.BoCollection.Where(r => r.User.Name == username).Select(r => r.Name);
                foreach (var name in scriptNames)
                {
                    Scripts.RemoveFromCollection(name);
                }

                // Benutzer löschen
                Users.RemoveFromCollection(username);
            }
        }
    }
}
