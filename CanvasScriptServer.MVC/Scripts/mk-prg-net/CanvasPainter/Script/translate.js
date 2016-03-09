//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 2.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: translate.js
//  Aufgabe/Fkt...: Script- Objekt für translate- Canvas- Befehl.
//                  Dieser fügt eine Verschiebetransformation in den Stapel ein,
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

    var cmdName = 'translate'

    var Proto = Object.create(ScriptProto, {
        Name: {
            value: cmdName,
            writable: false,
            enumerable: true,

        },
        plot: {
            value: function (ctx) {
                // ctx:  Canvas- Context
                ctx.translate(this.Tx, this.Ty);
            }
        },

        toJSON: {
            value: function () {
                return {
                    translate: {
                        Tx: this.Tx,
                        Ty: this.Ty
                    }
                };
            }
        }
    });


    function create(Tx, Ty) {

        return Object.create(Proto, {
            Tx: {
                value: Tx,
                writable: false,
                enumerable: true,
            },
            Ty: {
                value: Ty,
                writable: false,
                enumerable: true,
            },
        });
    }

    function createFromObject(obj) {
        return create(obj.Tx, obj.Ty);
    }

    return {
        Name: cmdName,
        'with': create,
        from: createFromObject
    }
});