using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB
{
    public partial class CanvasScriptDBContainer : ICanvasScriptServerUnitOfWork<Users, Scripts>
    {   

        public UserRepositoryV2<Users> Users
        {
            get { return new DB.Repository.UserRepository(this); }
        }

        public CanvasScriptRepository<Scripts> Scripts
        {
            get { return new DB.Repository.CanvasScriptRepository(this); }
        }

        public void saveChanges()
        {
            SaveChanges();
        }

        public void createUser(string Name)
        {
            Users.CreateBoAndAddToCollection(Name);            
        }

        public void createScript(string Username, string NameOfScript)
        {
            if (Scripts.Any(CanvasScriptKey.Create(Username, NameOfScript)))
            {
                // vorhandenes Script zurückgeben
                Scripts.GetBo(CanvasScriptKey.Create(Username, NameOfScript));
            }
            else
            {
                if (Users.Any(Username))
                {
                    var e = ScriptsSet.Create();
                    ScriptsSet.Add(e);
                    e.Name = NameOfScript;
                    e.User = UserNamesSet.Find(Username).User;
                    e.Created = DateTime.Now;
                    e.Modified = e.Created;
                    e.ScriptAsJson = "[]";

                    ScriptsSet.Add(e);
                } else {
                    throw new ArgumentException("Der Benutzer mit dem Namen " + Username + " existiert nicht.", "Username");
                }
            }
        }

        public void deleteUser(string username)
        {
            Users.RemoveFromCollection(username);
        }

        public void deleteScript(string scriptName)
        {
        }



        public void deleteScript(string Authorname, string scriptName)
        {
            Scripts.RemoveFromCollection(CanvasScriptKey.Create(Authorname, scriptName));
        }

        public void SubmitChanges()
        {
            this.SaveChanges();
        }
    }
}
