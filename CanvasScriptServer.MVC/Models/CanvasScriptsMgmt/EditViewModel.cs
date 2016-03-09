using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanvasScriptServer.MVC.Models.CanvasScriptsMgmt
{
    public class EditViewModel
    {
        public IUser User { get; set; }

        public ICanvasScript Script { get; set; }

    }
}