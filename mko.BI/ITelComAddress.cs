//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 16.5.2014
//
//  Projekt.......: mko.BI
//  Name..........: ITelComAddress.cs
//  Aufgabe/Fkt...: Schnittstelle von Objekten, die Telekommunikationsadressen
//                  speichern.
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

namespace mko.BI
{
    /// <summary>
    /// Auflistung aller Adresstypen
    /// </summary>
    public enum AddressType
    {
        // Telefon, Festnetz
        Tel,

        // Mobiltelefon
        Mobil,

        Fax,
        Email,
        Web
    }


    public interface ITelComAddress
    {
        /// <summary>
        /// Art der Adresse
        /// </summary>        
        AddressType Kind { get; set; }

        /// <summary>
        /// Adresse
        /// </summary>
        string Address { get; set; }

    }
}
