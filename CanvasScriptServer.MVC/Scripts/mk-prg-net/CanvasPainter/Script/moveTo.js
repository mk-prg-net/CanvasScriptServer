//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: moveTo.js
//  Aufgabe/Fkt...: Script- Objekt für moveTo- Canvas- Befehl
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

define(['Geometry/Point', './ScriptProto'], function (Point, ScriptProto) {

    "use strict";

    var cmdName = 'moveTo'

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,

        },
        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.moveTo(this.X, this.Y);
            }
        },

        // Serialisierungsformat definieren.
        toJSON: {
            value: function () {
                return {
                    moveTo: { X: this.X, Y: this.Y }
                }
            },
        }

    });


    function create(X, Y) {

        return Object.create(Proto, {
            X: {
                value: X,
                writable: false,
                enumerable: true,
            },
            Y: {
                value: Y,
                writable: false,
                enumerable: true,
            },
        });
    }

    function createFromObject(obj) {
        return create(obj.X, obj.Y);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }
});