//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 
//
//  Projekt.......: GB.Dispo.Base
//  Name..........: TelComAddress.cs
//  Aufgabe/Fkt...: Implementierung der Schnittstelle für Telekommunikationsadressen
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
    public class TelComAddress : ITelComAddress
    {

        public static void Copy(ITelComAddress from, ITelComAddress to)
        {
            to.Kind = from.Kind;
            to.Address = from.Address;
        }

        /// <summary>
        /// Art der Adresse
        /// </summary>
        [EnumDataType(typeof(AddressType))]
        public AddressType Kind { get; set; }

        /// <summary>
        /// Adresse
        /// </summary>
        public string Address { get; set; }
    }

}
