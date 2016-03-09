using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace CanvasScriptServer.MVC.Models.UserMgmt
{
    public class UserViewModel
    {
        [Required]        
        public string Name
        {
            get;
            set;
        }

    }
}