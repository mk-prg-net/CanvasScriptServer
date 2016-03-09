//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 28.9.2015
//
//  Projekt.......: mko.BI
//  Name..........: IGetBo
//  Aufgabe/Fkt...: Allgemeine Schnittstelle, die den Zugriff auf ein einzelnes Geschäftsobjekt ermöglicht
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

namespace mko.BI.Repositories.Interfaces
{
    public interface IGetBo<TBo, TBoId>
    {
        //----------------------------------------------------------------------------------------------------------------
        // Test auf Id       

        /// <summary>
        /// Liefert einen Lambda- Ausdruck zurück, mittels der ein Entity auf seine ID abbildet
        /// Entity --> ID
        /// </summary>
        /// <returns></returns>
        Func<TBo, bool> GetBoIDTest(TBoId id);

        /// <summary>
        /// 25.7.2014, mko    
        /// Zugriff auf einzelnes Entity mit der gegebenen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TBo GetBo(TBoId id);

    }
}
