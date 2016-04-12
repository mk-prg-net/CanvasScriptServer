using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB
{
    partial class Users : IUser
    {
        IEnumerable<ICanvasScript> IUser.Scripts
        {
            get
            {
                return Scripts.ToArray();
            }
        }


        string IUser.Name
        {
            get
            {
                return Name.Name;
            }
        }
    }
}
