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
            set
            {
                throw new NotImplementedException();
            }
        }


        string IUser.Name
        {
            get
            {
                return Name.Name;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
