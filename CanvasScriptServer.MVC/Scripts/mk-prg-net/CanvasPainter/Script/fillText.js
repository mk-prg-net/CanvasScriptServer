//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 3.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: fillText.js
//  Aufgabe/Fkt...: Script- Objekt für fillText- Canvas- Befehl
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

    var cmdName = 'fillText';

    var fillRectProto = Object.create(ScriptProto, {
        type: {
            value: cmdName,
            writable: false,
            enumerable: true,

        },
        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.fillText(this.Text, this.X, this.Y);
            }
        },

        toJSON: {
            value: function () {
                return {
                    fillText: {
                        X: this.X,
                        Y: this.Y,
                        Text: this.Text,
                    }
                }
            }
        }

    });


    function create(x, y, text) {
        return Object.create(fillRectProto, {
            X: {
                value: x,
                writable: false,
                enumerable: true,
            },
            Y: {
                value: y,
                writable: false,
                enumerable: true,
            },
            Text: {
                value: text,
                writable: false,
                enumerable: true,
            },
        });
    }

    function createFromObject(obj) {
        return create(obj.X, obj.Y, obj.Text);
    }


    return {
        Name: cmdName,
        'with': create,
        from: createFromObject

    }
});