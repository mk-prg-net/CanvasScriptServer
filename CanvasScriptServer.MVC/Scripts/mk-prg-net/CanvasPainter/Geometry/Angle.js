//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.2.2016
//
//  Projekt.......: HTML5
//  Name..........: Angle.js
//  Aufgabe/Fkt...: Winkelumrechungen deg--> rad
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

define(function () {
    
    return {

        // Umrechung von Altgrad (Vollkreis <=> 360°)        
        toRad: function(deg){
            return Math.PI * deg / 180.0;
        },

        // Umrechnung von Bogenmaß (180° <=> PI) in Altgrad
        toDeg: function (rad) {
            return 180 * rad / Math.PI;
        }



    }

});
