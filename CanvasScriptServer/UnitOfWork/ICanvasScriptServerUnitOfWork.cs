using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    //public interface ICanvasScriptServerUnitOfWork<TUser, TScript> : IDisposable, mko.BI.Repositories.Interfaces.ISubmitChanges
    //    where TUser : IUser
    //    where TScript : ICanvasScript
    public interface ICanvasScriptServerUnitOfWork : IDisposable, mko.BI.Repositories.Interfaces.ISubmitChanges
    {
        /// <summary>
        /// In UnitOfWork spielt das Repository die Rolle einer filterbare Liste der Scriptautoren. 
        /// </summary>
        UserRepositoryV2 Users
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


        // void createUser(string Name);

        /// <summary>
        /// Prüft, ob der Author bereits erfasset ist. Wenn nicht, wird ein Fehler geworfen.
        /// Unter dem Author wird das Skript mit dem gegebenen Namen angelegt. Der Skript-Name
        /// muss in Bezug auf den Autor eindeutig sein.
        /// </summary>
        /// <param name="Authorname"></param>
        /// <param name="NameOfScript"></param>
        void createScript(string Authorname, string NameOfScript);


        /// <summary>
        /// Löscht einen Autor. Wenn zu diesem noch Skripte verwaltet werden, dann wirft die Methode einen Fehler.
        /// </summary>
        /// <param name="Authorname"></param>
        void deleteUser(string Authorname);


    }
}
