//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2016
//
//  Projekt.......: CanvasScriptServer
//  Name..........: CanvasScriptRepository.cs
//  Aufgabe/Fkt...: Repository der Canvas- Scripte.
//                  Diese Implementiert lediglich das Filtern sowie den 
//                  Zugriff auf einzelne Scripte, nicht jedoch das anlegen und löschen     
//                  von Scripten. Anlegen und löschen sind nur im Zusammenhang mit der 
//                  Users- Collection sinnvoll.  Sie werden in der UnitOfWork implementiert
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
    public abstract class CanvasScriptRepository : mko.BI.Repositories.BoCo<ICanvasScript>, 
        mko.BI.Repositories.Interfaces.IGetBo<ICanvasScript, CanvasScriptKey>,
        mko.BI.Repositories.Interfaces.IGetBoBuilder<ICanvasScriptBuilder, CanvasScriptKey>,
        mko.BI.Repositories.Interfaces.IRemove<CanvasScriptKey>,
        mko.BI.Repositories.Interfaces.ISubmitChanges
    {
        public class SortName : mko.BI.Repositories.DefSortOrderCol<ICanvasScript, string>
        {
            public SortName(bool Descending) : base(r => r.Name, Descending) { }
        }

        public CanvasScriptRepository()
            : base(new SortName(true)) { }

        // mko.BI.Repositories.Interfaces.IGetBo<ICanvasScript, string>,
        public abstract bool Any(CanvasScriptKey id);
        public abstract ICanvasScript GetBo(CanvasScriptKey id);
        public abstract Func<ICanvasScript, bool> GetBoIDTest(CanvasScriptKey id);

        // mko.BI.Repositories.Interfaces.IGetBoBuilder<ICanvasScriptBuilder, string>
        public abstract ICanvasScriptBuilder GetBoBuilder(CanvasScriptKey id);

        // mko.BI.Repositories.Interfaces.IRemove<ICanvasScript>
        public virtual void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public abstract void RemoveFromCollection(CanvasScriptKey id);

        public abstract void SubmitChanges();
    }
}
