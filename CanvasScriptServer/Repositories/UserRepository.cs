//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: UserRepository.cs
//  Aufgabe/Fkt...: Repository der Script- Autoren
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

namespace CanvasScriptServer
{
    public abstract class UserRepository : mko.BI.Repositories.BoCo<IUser, string>        
    {
        public class SortName : mko.BI.Repositories.DefSortOrderCol<IUser, string>
        {
            public SortName(bool Descending) : base(r => r.Name, Descending) { }
        }


        public UserRepository()
            : base(new SortName(true)) { }

    }
}
