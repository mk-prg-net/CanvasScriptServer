using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public interface ICanvasScriptServerUnitOfWork<TUser, TScript> : IDisposable, mko.BI.Repositories.Interfaces.ISubmitChanges
        where TUser : IUser
        where TScript : ICanvasScript
    {
        /// <summary>
        /// In UnitOfWork spielt das Repository die Rolle einer filterbare Liste der Scriptautoren. 
        /// </summary>
        UserRepositoryV2<TUser> Users
        {
            get;
        }

        /// <summary>
        /// In UnitOfWork spielt das Repository die Rolle einer filterbare Liste der Scripte.
        /// </summary>
        CanvasScriptRepository<TScript> Scripts
        {
            get;
        }


        void createUser(string Name);

        void createScript(string Authorname, string NameOfScript);

        void deleteUser(string Authorname);

        void deleteScript(string Authorname, string scriptName);

    }
}
