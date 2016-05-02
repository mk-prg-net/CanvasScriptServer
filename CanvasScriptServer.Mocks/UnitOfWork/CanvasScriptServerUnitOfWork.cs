using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScriptServerUnitOfWork : CanvasScriptServer.ICanvasScriptServerUnitOfWork
    {
        //public CanvasScriptServerUnitOfWork(UserRepository userRepo, CanvasScriptRepository scriptRepo)            
        //{
        //}

        static List<User> _UsersTab;
        static List<CanvasScript> _ScriptsTab;
        static System.Collections.Generic.Queue<Action> _cudActions;


        static CanvasScriptServerUnitOfWork()
        {
            _UsersTab = new List<User>();
            _ScriptsTab = new List<CanvasScript>();
            _cudActions = new Queue<Action>();       
        }

        Mocks.CanvasScriptsRepository _Scripts;
        Mocks.UsersRepositoryV2 _Users;

        public CanvasScriptServerUnitOfWork()
        {
            _Scripts = new CanvasScriptsRepository(_ScriptsTab, _cudActions);
            _Users = new UsersRepositoryV2(_UsersTab, _ScriptsTab, _cudActions);
        }


        class EqComp : IEqualityComparer<ICanvasScript>
        {

            public bool Equals(ICanvasScript x, ICanvasScript y)
            {
                return x.Name == y.Name && x.AuthorName == y.AuthorName;
            }

            public int GetHashCode(ICanvasScript obj)
            {
                unchecked { 
                    return obj.Name.GetHashCode() * obj.AuthorName.GetHashCode();
                }
            }
        }


        public UserRepositoryV2 Users
        {
            get { return _Users; }
        }

        public CanvasScriptRepository Scripts
        {
            get { return _Scripts; }
        }

        public void SubmitChanges()
        {
            try
            {
                while (_cudActions.Any())
                {
                    _cudActions.Dequeue()();
                }
                Debug.WriteLine("Änderungen an CanvasScriptServerUnitOfWorks übernommen. Anz:" + _UsersTab.Count);

            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "SubmitChanges"), ex);
            }           
        }

        public void createScript(string Authorname, string NameOfScript)
        {
            // Der Benutzer muss bereits existieren
            if (Users.ExistsBo(Authorname))
            {
                if (!Scripts.ExistsBo(CanvasScriptKey.Create(Authorname, NameOfScript)))
                {
                    var user = Users.GetBo(Authorname);

                    var entity = new CanvasScript();
                    entity._Name = NameOfScript;
                    entity._Created = DateTime.Now;
                    entity._Author = user.Name;
                    entity._ScriptAsJson = CanvasScriptServer.Bo.PredefinedScripts.DefaultScript;
                    _cudActions.Enqueue(() => _ScriptsTab.Add(entity));                

                }
                else
                {
                    throw new System.Data.ConstraintException("Das Skript " + NameOfScript + " für den Autor " + Authorname + " existiert bereits");
                }
            }
            else
            {
                throw new System.Data.ConstraintException("Scriptautor " + Authorname + " existiert nicht. Das Skript " + NameOfScript + " konnte nicht angelegt werden");
            }
        }


        public void deleteUser(string username)
        {
            var rec = _UsersTab.Find(r => r.Name == username);
            if (rec != null)
            {
                // Alle Scripte vom Benutzer löschen
                var bld = Scripts.getFilteredAndSortedSetBuilder();
                bld.defAuthor(username);

                var ScriptsOfUser = bld.GetSet();

                foreach (var script in ScriptsOfUser.Get())
                {
                    _cudActions.Enqueue(() => Scripts.RemoveBo(CanvasScriptKey.Create(username, script.Name)));
                }

                // Benutzer löschen
                _cudActions.Enqueue(() =>
                {
                    _UsersTab.Remove(rec);
                });
            }
            else
            {
                throw new System.Data.RowNotInTableException("User " + username + " wurde nicht gefunden.");
            }
        }


        public void Dispose()
        {
            Debug.WriteLine("CanvasScriptServerUnitOfWork wird freigegeben");
        }

    }
}
