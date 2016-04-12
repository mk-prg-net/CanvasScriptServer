using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasScriptServer.DB
{
    partial class Scripts : ICanvasScript, ICanvasScriptBuilder
    {

        IUser ICanvasScript.Author
        {
            get
            {
                return User;
            }
        }

        void ICanvasScriptBuilder.setScript(string Script)
        {
            this.ScriptAsJson = Script;
        }
    }
}
