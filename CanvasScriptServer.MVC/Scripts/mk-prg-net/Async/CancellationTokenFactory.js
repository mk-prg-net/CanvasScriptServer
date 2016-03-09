//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2015
//
//  Projekt.......: HTML5
//  Name..........: CancellationToken.js
//  Aufgabe/Fkt...: Einheitliche Implementierung von Abbruchtoken. 
//                  Über dieses kann der Rufer den asynchronen Berechnungsprozess vorzeitig stoppen.
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 23.2.16
//  Änderungen....: Als RequireJS- Modul gekapselt
//
//</unit_history>
//</unit_header>   

define(function() {   

    var CancellationTokenFactory = {
        create: function () {
            return { stop: false };
        }
    }

    return CancellationTokenFactory;

})
