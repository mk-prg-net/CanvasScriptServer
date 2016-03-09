//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: rotate.js
//  Aufgabe/Fkt...: Script- Objekt für rotate- Canvas- Befehl.
//                  Dieser fügt eine Drehung in den Stapel ein,
//                  auf dessen oberster Ebene die Zeichenbefehle operieren, 
//                  und dessen unterste Ebene die Cnvas- Zeichefläche ist.
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

define(['./ScriptProto'], function (ScriptProto) {

    "use strict";

    var cmdName = 'rotate'

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,

        },
        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.rotate(this.Phi);
            }
        },

        toJSON: {
            value: function () {
                return {
                    rotate: {
                        Phi: this.Phi
                    }
                };
            }
        }
    });


    function create(phi) {

        return Object.create(Proto, {
            Phi: {
                value: phi,
                writable: false,
                enumerable: true,
            },
        });
    }

    function createFromObject(obj) {
        return create(obj.Phi);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }
});