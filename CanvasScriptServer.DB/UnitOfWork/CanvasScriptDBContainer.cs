using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB
{
    public partial class CanvasScriptDBContainer : ICanvasScriptServerUnitOfWork
    {
        public void init()
        {
            
        }

        public UserRepositoryV2 Users
        {
            get { 
                
                //return new DB.Repository.UserRepository(this); 
                if(_Users == null) {
                    _Users = new Repository.UserRepository(this);
                }
                return _Users;
            }
        }
        DB.Repository.UserRepository _Users;
        

        public CanvasScriptRepository Scripts
        {
            get {
                if (_Scripts == null)
                {
                    _Scripts = new Repository.CanvasScriptRepository(this);
                }
                return _Scripts; 
            }
        }

        DB.Repository.CanvasScriptRepository _Scripts;


        public void createScript(string Username, string NameOfScript)
        {
            if (Scripts.ExistsBo(CanvasScriptKey.Create(Username, NameOfScript)))
            {
                // vorhandenes Script zurückgeben
                Scripts.GetBo(CanvasScriptKey.Create(Username, NameOfScript));
            }
            else
            {
                if (Users.ExistsBo(Username))
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
            try{
                var nameRec = UserNamesSet.Find(username);
                if (nameRec != null)
                {
                    if (nameRec.User.Scripts.Any())
                    {
                        throw new Exception(mko.TraceHlp.FormatErrMsg(this, "deleteUser", "Name=", username, "Dem Benutzer sind noch Scripte zugeordnet"));
                    }
                    var user = nameRec.User;
                    UserNamesSet.Remove(nameRec);
                    UsersSet.Remove(user);

                }
                else
                {
                    throw new System.Data.RowNotInTableException("Der zu löschende Benutzer " + username + " existiert nicht");
                }
            } catch(Exception ex){
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "deleteUser"), ex);
            }
            
        }

        public void SubmitChanges()
        {
            this.SaveChanges();
        }
    }
}
