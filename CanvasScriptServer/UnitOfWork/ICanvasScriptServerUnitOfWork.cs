using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public interface ICanvasScriptServerUnitOfWork
    {
        UserRepository Users
        {
            get;
        }


        CanvasScriptRepository Scripts
        {
            get;
        }


        void SaveChanges();


        IUser CreateUser(string Name);

        ICanvasScript CreateScript(string Username, string NameOfScript);



        void deleteUser(string username);

        void deleteScript(string scriptName);

    }
}
