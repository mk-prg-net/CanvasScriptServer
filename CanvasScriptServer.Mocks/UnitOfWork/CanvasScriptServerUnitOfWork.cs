using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScriptServerUnitOfWork : CanvasScriptServer.ICanvasScriptServerUnitOfWork<User, CanvasScript>
    {
        //public CanvasScriptServerUnitOfWork(UserRepository userRepo, CanvasScriptRepository scriptRepo)            
        //{
        //}

        static CanvasScriptServerUnitOfWork()
        {
            _Scripts = new CanvasScriptsRepository();
            _Users = new UsersRepositoryV2(_Scripts);
        }

        public CanvasScriptServerUnitOfWork()
        {
        }

        static Mocks.CanvasScriptsRepository _Scripts;
        static Mocks.UsersRepositoryV2 _Users;


        public void createScript(string Authorname, string NameOfScript)
        {
            // Der Benutzer muss bereits existieren
            if (Users.Any(Authorname))
            {
                var user = Users.GetBo(Authorname);
                _Scripts.CreateBoAndAddToCollection(user, NameOfScript);                
            }
            else
            {
                throw new Exception("Scriptautor " + Authorname + " existiert nicht");
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

        public void deleteScript(string Authorname, string scriptName)
        {
            // Script aus den Scripten nehmen, die dem Benutzer zugeordnet sind
            Scripts.RemoveFromCollection(CanvasScriptKey.Create(Authorname, scriptName));
        }

        public UserRepositoryV2<User> Users
        {
            get { return _Users; }
        }

        public CanvasScriptRepository<CanvasScript> Scripts
        {
            get { return _Scripts; }
        }

        public void SubmitChanges()
        {
            Users.SubmitChanges();
            Scripts.SubmitChanges();
            Debug.WriteLine("In CanvasScriptServerUnitOfWorks wurden die Änderungen übernommen");
        }

        public void createUser(string Name)
        {
            Users.CreateBoAndAddToCollection(Name);            
        }

        public void deleteUser(string username)
        {
            if (Users.Any(username))
            {
                // Alle Scripte vom Benutzer löschen
                var scriptNames = Scripts.Get(filter: r => r.Author.Name == username).Select(r => r.Name);
                foreach (var name in scriptNames)
                {
                    Scripts.RemoveFromCollection(CanvasScriptKey.Create(username, name));
                }

                // Benutzer löschen
                Users.RemoveFromCollection(username);
            }
        }

        public void Dispose()
        {
            Debug.WriteLine("CanvasScriptServerUnitOfWork wird freigegeben");
        }

    }
}
