using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public interface ICanvasScriptServerUnitOfWork : IDisposable, mko.BI.Repositories.Interfaces.ISubmitChanges
    {
        /// <summary>
        /// In UnitOfWork spielt das Repository die Rolle einer filterbare Liste der Scriptautoren. 
        /// </summary>
        UserRepository Users
        {
            get;
        }

        /// <summary>
        /// In UnitOfWork spielt das Repository die Rolle einer filterbare Liste der Scripte.
        /// </summary>
        CanvasScriptRepository Scripts
        {
            get;
        }


        void createUser(string Name);

        void createScript(string Authorname, string NameOfScript);

        void deleteUser(string Authorname);

        void deleteScript(string Authorname, string scriptName);

    }
}
