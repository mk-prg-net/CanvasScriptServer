using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB.UnitOfWork
{
    public partial class CanvasScriptDBContainer : ICanvasScriptServerUnitOfWork
    {

        UserRepository userRepo;

        public UserRepository Users
        {
            get { throw new NotImplementedException(); }
        }

        public CanvasScriptRepository Scripts
        {
            get { throw new NotImplementedException(); }
        }

        public void saveChanges()
        {
            throw new NotImplementedException();
        }

        public IUser createUser(string Name)
        {
            throw new NotImplementedException();
        }

        public ICanvasScript createScript(string Username, string NameOfScript)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public void deleteScript(string scriptName)
        {
            throw new NotImplementedException();
        }
    }
}
