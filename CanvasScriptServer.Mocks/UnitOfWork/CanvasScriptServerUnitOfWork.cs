using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScriptServerUnitOfWork : CanvasScriptServer.CanvasScriptServerUnitOfWork
    {
        public CanvasScriptServerUnitOfWork(UserRepository userRepo, CanvasScriptRepository scriptRepo)
            : base(userRepo, scriptRepo)
        {}

        public override ICanvasScript CreateScript(string Username, string NameOfScript)
        {
            var newScript = base.CreateScript(Username, NameOfScript);

            if (Users.Any(Username))
            {
                var user = Users.GetBo(Username);
                user.Scripts = user.Scripts.Concat(new ICanvasScript[] { newScript }).ToArray();
            }

            return newScript;
        }

        class EqComp : IEqualityComparer<ICanvasScript>
        {

            public bool Equals(ICanvasScript x, ICanvasScript y)
            {
                return x.Name == y.Name && x.Created == y.Created;
            }

            public int GetHashCode(ICanvasScript obj)
            {
                return obj.Name.GetHashCode();
            }
        }

        public override void deleteScript(string scriptName)
        {
            // Script aus den Scripten nehmen, die dem Benutzer zugeordnet sind

            var script = Scripts.GetBo(scriptName);
            var user = script.User;

            user.Scripts = user.Scripts.Except(new ICanvasScript[] { script }, new EqComp()).ToArray();

            base.deleteScript(scriptName);            
        }



    }
}
