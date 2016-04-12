using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer
{
    public struct CanvasScriptKey
    {
        public static CanvasScriptKey Create(string username, string scriptname)
        {
            return new CanvasScriptKey() { Username = username, Scriptname = scriptname };
        }

        public string Username { get; set; }
        public string Scriptname { get; set; }
    }
}
