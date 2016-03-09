//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.3.2016
//
//  Projekt.......: HTML5
//  Name..........: DuckTyping.js
//  Aufgabe/Fkt...: Tools zum Implementieren von Duck- Typing
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
        execIfObjectHasProperties: function (obj, PropNameList, func) {

            // Liste der fehlenden Eigenschaften
            var missingProps = [];

            while (PropNameList.length > 0) {
                var propName = PropNameList.shift();

                if (!Object.hasOwnProperty(propName)){
                    missingProps.push(propName);
                }
            }

            if (missingProps.length > 0) {
                throw TypeError()
            }

            
        }
    }
});
