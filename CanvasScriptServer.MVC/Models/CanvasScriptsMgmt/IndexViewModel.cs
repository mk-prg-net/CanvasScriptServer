using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CanvasScriptServer.MVC.Models.CanvasScriptsMgmt
{
    public class IndexViewModel
    {
        public IndexViewModel(IUser User, IEnumerable<ICanvasScript> Lists)
        {
            _User = User;
            _Lists = Lists;
        }

        public IUser User
        {
            get
            {
                return _User;    
            }
        }
        IUser _User;


        public IEnumerable<ICanvasScript> Scripts
        {
            get
            {
                return _Lists;
            }
        }
        IEnumerable<ICanvasScript> _Lists;

    }
}