//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 16.5.2014
//
//  Projekt.......: mko.BI
//  Name..........: IMailingAddress.cs
//  Aufgabe/Fkt...: Schnittstelle von Objekten, die Postanschriften speichern
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2012
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
using System.ComponentModel.DataAnnotations;

namespace mko.BI
{
    public interface IMailingAddress
    {
        string Company { get; set; }


        string Name { get; set; }
        string FirstName { get; set; }
        string Street { get; set; }
        string City { get; set; }
        string Country { get; set; }

        /// <summary>
        /// Postleitzahl
        /// </summary>
        string PostalCode { get; set; }

    }
}
