//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: ScriptProto.js
//  Aufgabe/Fkt...: Prototyp aller Scriptobjekte
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

        // Zu jeder Scripttyp- Id einen Bezeichner abrufen
        toString: function (pre, post) {

            if (!post) {
                post = "";
            }

            if (!pre) {
                pre = "";
            }

            function format(txt) {
                return pre + txt + post;
            }

            return format(this.Name);
        }
    };
});

