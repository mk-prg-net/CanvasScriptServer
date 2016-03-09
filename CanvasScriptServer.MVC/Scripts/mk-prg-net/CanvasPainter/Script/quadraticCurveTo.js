//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: quadraticCurveTo.js
//  Aufgabe/Fkt...: Script- Objekt für quadraticCurveTo- Canvas- Befehl
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

    var cmdName = 'quadraticCurveTo';

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,

        },
        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.quadraticCurveTo(this.cpX, this.cpY, this.X, this.Y);
            }
        },

        toJSON: {
            value: function () {
                return {
                    bezierCurveTo: {
                        X: this.X,
                        Y: this.Y,
                        cpX: this.cpX,
                        cpY: this.cpY,
                    }
                };
            }
        }

    });


    function create(X, Y, cpX, cpY) {

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
            cpX: {
                value: cpX,
                writable: false,
                enumerable: true,
            },
            cpY: {
                value: cpY,
                writable: false,
                enumerable: true,
            },

        });
    }

    function createFromObject(obj) {
        return create(obj.X, obj.Y, obj.cpX, obj.cpY);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }
});