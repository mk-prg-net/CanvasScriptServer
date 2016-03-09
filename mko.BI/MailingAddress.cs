//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.5.2014
//
//  Projekt.......: mko.BI
//  Name..........: MailingAddress
//  Aufgabe/Fkt...: Implementierung der Schnittstelle für Postanschriften
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
    public class MailingAddress : IMailingAddress
    {

        public static void Copy(IMailingAddress from, IMailingAddress to)
        {
            to.Company = from.Company;
            to.City = from.City;
            to.FirstName = from.FirstName;
            to.Name = from.Name;
            to.PostalCode = from.PostalCode;
            to.Street = from.Street;
        }

        public string Company { get; set; }

        [Required]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        /// <summary>
        /// Postleitzahl
        /// </summary>
        public string PostalCode { get; set; }
    }
}
