//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: scale.js
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

    var cmdName = 'scale'

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,

        },
        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.scale(this.Sx, this.Sy);
            }
        },

        toJSON: {
            value: function () {
                return {
                    scale: {
                        Sx: this.Sx,
                        Sy: this.Sy
                    }
                };
            }

        }
    });


    function create(sx, sy) {

        return Object.create(Proto, {
            Sx: {
                value: sx,
                writable: false,
                enumerable: true,
            },
            Sy: {
                value: sy,
                writable: false,
                enumerable: true,
            },

        });
    }

    function createFromObject(obj) {
        return create(obj.Sx, obj.Sy);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }
});