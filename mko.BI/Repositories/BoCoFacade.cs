//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 9.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: BoCoFacade.cs
//  Aufgabe/Fkt...: Fassade eines Repositories von Geschäftsobjekten. Hinter der Fassade können spezielle 
//                  Repositories und Datenbanklayer eingesetzt werden, um den Zugriff auf die Geschäftsobjekte und die Filterung 
//                  der Geschäftsobjektmengen zu implementieren.
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

namespace mko.BI.Repositories
{

    public abstract class BoCoFacade<TBo, TBoId>
        where TBo : class //, new()
    {
        /// <summary>
        /// Zugriff auf alle Entities
        /// </summary>
        /// <returns></returns>
        public abstract IQueryable<TBo> GetBoAll();


        /// <summary>
        /// 25.7.2014, mko    
        /// Zugriff auf einzelnes Entity mit der gegebenen id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TBo GetBo(TBoId id);
        
        /// <summary>
        /// Wendet auf die Menge der Entities alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge
        /// </summary>
        /// <returns></returns>
        public abstract IQueryable<TBo> GetBoFiltered();

        /// <summary>
        /// Wendet auf die Menge der Entites alle definierten Filter an. Das Ergebnis ist die 
        /// gefilterte Menge. Diese wird anschließend sortiert
        /// </summary>
        /// <returns></returns>
        public abstract IQueryable<TBo> GetBoFilteredAndSorted();

        public abstract void Delete(TBoId id);

        public abstract void Insert(TBo entity);

        public abstract void SubmitChanges();

    }
}
