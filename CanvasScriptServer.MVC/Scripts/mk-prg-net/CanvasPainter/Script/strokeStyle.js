//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: strokeStyle.js
//  Aufgabe/Fkt...: Script- Objektfabrik für beginPath Canvas Befehl
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

    var cmdName = 'strokeStyle';

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,
        },

        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.strokeStyle = this.Style;
            }
        },

        toJSON: {
            value: function () {
                return {
                    strokeStyle: {
                        Style: this.Style
                    }
                }
            }
        }
    });


    function create(Style) {

        return Object.create(Proto,
            {
                Style: {
                    value: Style,
                    writable: false,
                    enumerable: true,
                },

            });
    }

    function createFromObject(obj) {
        return create(obj.Style);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }

});

