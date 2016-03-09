//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 19.1.2015
//
//  Projekt.......: mko.BI
//  Name..........: Ops.cs
//  Aufgabe/Fkt...: Allgemeine Operationen auf Adressen
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
    public static class Ops
    {
        /// <summary>
        /// Kopiert Ortsangaben
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Copy(ILocation src, ILocation dest){
            dest.City = src.City;
            dest.Country = src.Country;
            dest.PostalCode = src.PostalCode;
            dest.Street = src.Street;
        }

        /// <summary>
        /// Kpoiert Firmenadresse
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Copy(IMailingAddressCompany src, IMailingAddressCompany dest)
        {
            Copy((ILocation)src, dest);
            dest.CompanyName = src.CompanyName;
        }

        /// <summary>
        /// Kopiert Postanschrift einer Person
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public static void Copy(IMailingAddressPerson src, IMailingAddressPerson dest)
        {
            Copy((ILocation)src, dest);
            dest.FirstName = src.FirstName;
            dest.Name = src.Name;
        }




    }
}
