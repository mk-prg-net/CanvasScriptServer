//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: strokeStyle.js
//  Aufgabe/Fkt...: Script- Objektfabrik für strokeStyle Canvas Befehl
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

define(['./ScriptProto'], function (ScriptProto) {

    "use strict";

    var cmdName = 'fillStyle';

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,
        },

        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.fillStyle = this.Style;
            }
        },

        toJSON: {
            value: function () {
                return {
                    fillStyle: {
                        Style: this.Style
                    }
                };
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

