using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.Mocks
{
    public class CanvasScript: ICanvasScript
    {
        public string Name
        {
            get;
            set;
        }

        public IUser User
        {
            get;
            set;
        }

        public DateTime Created
        {
            get;
            set;
        }

        public string ScriptAsJson
        {
            get;
            set;
        }
    }
}
