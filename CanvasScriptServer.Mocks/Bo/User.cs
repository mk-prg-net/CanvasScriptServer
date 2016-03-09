using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

namespace CanvasScriptServer.Mocks
{
    public class User : IUser
    {
        /// <summary>
        /// Konstruktor, der bei Dependency- injection mittels Unity herangezogen wird.
        /// User hat zwei Konstruktoren. Diese Mehrdeutigkeit wird mittels InjectionConstruktor
        /// Attribut aufgelöst
        /// </summary>
        [InjectionConstructor]
        public User() { }

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
            get;
            set;
        }
        
    }
}
