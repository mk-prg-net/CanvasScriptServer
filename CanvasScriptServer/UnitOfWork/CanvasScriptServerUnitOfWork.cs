using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public class CanvasScriptServerUnitOfWork : ICanvasScriptServerUnitOfWork
    {

        UserRepository _Users;
        CanvasScriptRepository _Scripts;

        public CanvasScriptServerUnitOfWork(UserRepository userRepo, CanvasScriptRepository scriptRepo)
        {
            _Users = userRepo;
            _Scripts = scriptRepo;
        }


        public UserRepository Users
        {
            get
            {
                return _Users;
            }
        }


        public CanvasScriptRepository Scripts
        {
            get
            {
                return _Scripts;
            }
        }


        public void SaveChanges()
        {
            Users.SubmitChanges();
            Scripts.SubmitChanges();
        }


        public virtual IUser CreateUser(string Name)
        {
            if (!Users.Any(Name))
            {
                var user = Users.CreateBo();                
                user.Name = Name;
                Users.AddToCollection(user);
                return user;
            }
            else
            {
                return Users.GetBo(Name);
            }
        }
        


        public virtual ICanvasScript CreateScript(string Username, string NameOfScript)
        {
            var newScript = Scripts.CreateBo();
            newScript.User = Users.GetBo(Username);
            newScript.Created = DateTime.Now;
            newScript.Name = NameOfScript;
            newScript.ScriptAsJson = "[{\"beginPath\": \"true\"}, {\"strokeStyle\": { \"Style\": \"rgb(255, 255, 0)\"}}, {\"moveTo\": {\"X\": 0, \"Y\":0}}, {\"lineTo\": {\"X\": 100, \"Y\":100}}, { \"closePath\": true}, {\"stroke\": true} ]";

            Scripts.AddToCollection(newScript);

            return newScript;

        }



        public virtual void deleteUser(string username)
        {
            if (Users.Any(username))
            {
                var user = Users.GetBo(username);
                Users.RemoveFromCollection(user);
            }
        }

        public virtual void deleteScript(string scriptName)
        {
            if (Scripts.Any(scriptName))
            {
                var user = Scripts.GetBo(scriptName);
                Scripts.RemoveFromCollection(scriptName);
            }
        }


    }
}
