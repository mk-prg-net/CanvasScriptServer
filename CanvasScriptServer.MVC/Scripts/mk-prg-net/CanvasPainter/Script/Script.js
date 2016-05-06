//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 9.6.2015
//
//  Projekt.......: HTML5
//  Name..........: CanvasScript.js
//  Aufgabe/Fkt...: Objekte zum Aufzeichnen von Canvas Zeichenoperationen
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 1.3.2016
//  Änderungen....: Als RequireJS Modul implementiert. Komplettes Refactoring
//
//</unit_history>
//</unit_header>        

define(['Geometry/Point',
        './beginPath',
        './closePath',
        './fill',
        './fillRect',
        './strokeRect',
        './clearRect',
        './fillStyle',
        './lineTo',
        './moveTo',
        './stroke',
        './strokeStyle',
        './translate',
        './rotate',
        './scale',
        './save',
        './restore',
        './bezierCurveTo',
        './quadraticCurveTo',
        './font',
        './fillText',
        './metaMark',
        './arc'],
    function (Point,
        beginPath,
        closePath,
        fill,
        fillRect,
        strokeRect,
        clearRect,
        fillStyle,
        lineTo,
        moveTo,
        stroke,
        strokeStyle,
        translate,
        rotate,
        scale,
        save,
        restore,
        bezierCurveTo,
        quadraticCurveTo,
        font,
        fillText,
        metaMark,
        arc) {

        "use strict";

        var ScriptFactories = {};

        ScriptFactories[beginPath.Name] = beginPath;
        ScriptFactories[closePath.Name] = closePath;
        ScriptFactories[fill.Name] = fill;
        ScriptFactories[fillRect.Name] = fillRect;
        ScriptFactories[strokeRect.Name] = strokeRect;
        ScriptFactories[clearRect.Name] = clearRect;
        ScriptFactories[fillStyle.Name] = fillStyle;
        ScriptFactories[lineTo.Name] = lineTo;
        ScriptFactories[moveTo.Name] = moveTo;
        ScriptFactories[stroke.Name] = stroke;
        ScriptFactories[strokeStyle.Name] = strokeStyle;
        ScriptFactories[arc.Name] = arc;
        ScriptFactories[translate.Name] = translate;
        ScriptFactories[rotate.Name] = rotate;
        ScriptFactories[scale.Name] = scale;
        ScriptFactories[save.Name] = save;
        ScriptFactories[restore.Name] = restore;
        ScriptFactories[bezierCurveTo.Name] = bezierCurveTo;
        ScriptFactories[quadraticCurveTo.Name] = quadraticCurveTo;
        ScriptFactories[font.Name] = font;
        ScriptFactories[fillText.Name] = fillText;
        ScriptFactories[metaMark.Name] = metaMark;


        return {

            // Liefert Klassenfabrik eines Canvas- Kommandos als Funktionsobjekt zurück.
            Cmd: function(cmdName){
                return ScriptFactories[cmdName];
            },

            //// Erzeugt ein Canvas- Kommando aus den Daten des übergebenen Objektes
            //CmdFromObject: function (cmdName) {
            //    return ScriptFactories[cmdName].createFromObject;
            //},

            // Führt ein Script auf einen Canvas- Context aus.
            // ctx    : Contextobjekt eines Canvas
            // Scripts: Array mit CanvasScript Objekte. Werden nacheinander ausgeführt 
            plot: function (Scripts, ctx) {

                for (var i = 0, anz = Scripts.length; i < anz; i++) {
                    Scripts[i].plot(ctx);
                }
                //Scripts.forEach(function (script) {
                //    script.plot(ctx);
                //});
            },

            // Stellt aus einer Liste von Objekten im json- Format wieder ein Script her
            from: function (jsonObjects) {

                var self = this;

                var scripts = jsonObjects.map(function (script) {
                    // Name der Scriptfunktion bestimmen
                    let name = Object.getOwnPropertyNames(script)[0];

                    // Scriptfunktion aus Objekt erzeugen
                    return self.Cmd(name).from(script[name]);                    
                });

                return scripts;
            }

        };
    });



