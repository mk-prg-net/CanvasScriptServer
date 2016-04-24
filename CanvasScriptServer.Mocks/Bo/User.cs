using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

using System.Linq;

namespace CanvasScriptServer.Mocks
{
    public class User : IUser
    {

        public User(CanvasScriptRepository<CanvasScript> ScriptsRepository)
        {
            _ScriptRepository = ScriptsRepository;
        }
        CanvasScriptRepository<CanvasScript> _ScriptRepository;

        //public User(IUser user)
        //{
        //    Name = user.Name;
        //    Scripts = user.Scripts;
        //}

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<ICanvasScript> Scripts
        {
            get
            {
                return _ScriptRepository.Get(filter: r => r.Author.Name == Name).ToArray();
            }
        }

    }
}
