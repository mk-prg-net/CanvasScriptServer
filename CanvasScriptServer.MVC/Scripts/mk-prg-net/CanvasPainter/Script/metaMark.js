//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: metamark.js
//  Aufgabe/Fkt...: Script- Objektfabrik für metamark Befehl.
//                  Dieser dient nur zur Markierung von Abschnitten in einem
//                  Script, an der sich Listenverarbeitungsfunktionen über 
//                  Scriptlisten orientieren können. Der Befehl hat keinerlei    
//                  graphische Ausgabe zur Folge.
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

define(['./ScriptProto'], function (ScriptProto) {

    "use strict";

    var cmdName = 'metaMark';

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,
        },

        plot: {
            value: function (ctx) {
                // Befehl bewirkt keine graphische Ausgabe 
            }
        },

        toJSON: {
            value: function () {
                return {
                    metaMark: {
                        Name: this.Name
                    }
                };
            }
        }

    });


    function create(name) {

        return Object.create(Proto,
            {
                Name: {
                    value: name,
                    writable: false,
                    enumerable: true,
                },

            });
    }

    function createFromObject(obj) {
        return create(obj.Name);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }

});

