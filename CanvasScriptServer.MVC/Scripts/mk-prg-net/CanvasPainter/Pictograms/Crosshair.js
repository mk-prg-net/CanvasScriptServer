//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.3.2016
//
//  Projekt.......: HTML5
//  Name..........: Pictograms.js
//  Aufgabe/Fkt...: Cnvas- Scripte, die Pictogramme erstellen
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

define(['Basics/Defs', 'Script/Script'], function (Defs, Script) {

    "use strict";    

        // Erzeugt ein Script für ein Fadenkreuz
        // mX, mY: Position
        // Phi: Drehung um Fadenkreuzmitte         
    return function (mX, mY, Phi, Style, metaMark) {

        var ang = Phi || 0.0;
        var fst = Style.fillStyle || Defs.FillStyle;
        var sst = Style.strokeStyle || Defs.StrokeStyle;
        var mark = metaMark || 'crosshair';

        var Crosshair = [
            Script.Cmd('metaMark').with(mark),
            Script.Cmd('save').with(),
            Script.Cmd('fillStyle').with(fst),
            Script.Cmd('strokeStyle').with(sst),
            Script.Cmd('translate').with(mX, mY),
            Script.Cmd('rotate').with(Phi),
            Script.Cmd('beginPath').with(),
            Script.Cmd('moveTo').with(-10, 0),
            Script.Cmd('lineTo').with(10, 0),
            Script.Cmd('moveTo').with(0, -10),
            Script.Cmd('lineTo').with(0, 10),
            Script.Cmd('moveTo').with(0, 0),
            Script.Cmd('stroke').with(),
            Script.Cmd('closePath').with(),
            Script.Cmd('restore').with()
        ];
        return Crosshair;
    };   

});