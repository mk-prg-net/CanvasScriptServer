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
        /// <summary>
        /// Konstruktor, der bei Dependency- injection mittels Unity herangezogen wird.
        /// User hat zwei Konstruktoren. Diese Mehrdeutigkeit wird mittels InjectionConstruktor
        /// Attribut aufgelöst
        /// </summary>
        public User() { }

        [InjectionConstructor]
        public User(string name)
        {
            Name = name;
        }

        public User(CanvasScriptRepository ScriptsRepository)
        {
            _ScriptRepository = ScriptsRepository;
        }
        CanvasScriptRepository _ScriptRepository;

        public User(IUser user)
        {
            Name = user.Name;
            Scripts = user.Scripts;
        }

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<ICanvasScript> Scripts
        {
            get
            {
                return _ScriptRepository.BoCollection.Where(r => r.User.Name == Name).ToArray();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

    }
}
