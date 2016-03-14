using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public interface ICanvasScriptServerUnitOfWork
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

        /// <summary>
        /// Änderungen durch Transaktionen, die für das UnitOfWork 
        /// </summary>
        void saveChanges();

        IUser createUser(string Name);

        ICanvasScript createScript(string Username, string NameOfScript);

        void deleteUser(string username);

        void deleteScript(string scriptName);

    }
}
