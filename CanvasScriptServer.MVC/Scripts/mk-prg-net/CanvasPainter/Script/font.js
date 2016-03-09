//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 3.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: font.js
//  Aufgabe/Fkt...: Script- Objektfabrik für font Canvas Befehl
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

    var cmdName = 'font';

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,
        },

        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.font = this.Font;
            }
        },

        toJSON: {
            value: function () {
                return {
                    font: {
                        Font: this.Font
                    }
                };
            }
        }

    });


    function create(font) {

        return Object.create(Proto,
            {
                Font: {
                    value: font,
                    writable: false,
                    enumerable: true,
                },

            });
    }

    function createFromObject(obj) {
        return create(obj.font);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }

});

