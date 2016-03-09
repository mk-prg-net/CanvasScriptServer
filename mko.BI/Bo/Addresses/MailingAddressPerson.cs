//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 9.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: MailingAddress.cs
//  Aufgabe/Fkt...: Standardimplementierung der Adresse einer Person 
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

namespace mko.BI.Bo.Addresses
{
    public class MailingAddressPerson : IMailingAddressPerson
    {
        public string Name
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string Street
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }

        public string PostalCode
        {
            get;
            set;
        }
    }
}
