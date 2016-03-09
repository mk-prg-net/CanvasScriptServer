//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.9.2015
//
//  Projekt.......: mko.BI
//  Name..........: ICrud.cs
//  Aufgabe/Fkt...: Allgemeine Einfüge und Löschoperationen auf Repositories                  
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
    public interface ICrud<TBo, TBoId>
    {

        //----------------------------------------------------------------------------------------------------------------
        // Einfügen, Löschen und Aktualisieren

        /// <summary>
        /// Erzeugt ein neues Entity. Kann bei der Implementierung eines Insert- Befehls 
        /// eines Geschäftsobjektes eingesetzt werden, um z.B. den Schlüssel des Entities zu definieren
        /// </summary>
        /// <returns></returns>
        TBo CreateBo();


        /// <summary>
        /// Hinzufügen eines Entity zu einer Entitycollection. Erst durch 
        /// SubmitChanges wird das Entity der Datenbank hinzugefügt.
        /// </summary>
        /// <param name="entity"></param>
        void AddToCollection(TBo entity);


        /// <summary>
        /// Ein Entity für das Löschen in der EntityCollection markieren
        /// </summary>
        /// <param name="entity"></param>
        void RemoveFromCollection(TBo entity);


        /// <summary>
        /// Löschen des durch die ID definierten Entity
        /// </summary>
        /// <param name="id"></param>
        void RemoveFromCollection(TBoId id);

        /// <summary>
        /// Löschen aller Entities
        /// </summary>
        void RemoveAll();


        /// <summary>
        /// Aktualisierungen am ORMContext mit der Datenbank abgleichen
        /// </summary>
        void SubmitChanges();

    }
}
