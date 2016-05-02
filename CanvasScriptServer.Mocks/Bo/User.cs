//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016 
//
//  Projekt.......: CanvasScriptServer.Mocks
//  Name..........: User.cs
//  Aufgabe/Fkt...: Mockup für User
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

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

        public User(string Name)
        {
            _Name = Name;
            _Created = DateTime.Now;
        }
        
        public string Name
        {
            get
            {
                return _Name;
            }
        }
        string _Name;


        public DateTime Created
        {
            get { 
                return _Created;
            }
        }
        DateTime _Created;
    }
}
