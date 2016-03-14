using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB
{
    partial class Scripts : ICanvasScript
    {

        IUser ICanvasScript.User
        {
            get
            {
                return User;
            }
            set
            {
                
            }
        }

    }
}
