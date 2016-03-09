//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 5.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: Circle.js
//  Aufgabe/Fkt...: Mauswerkzeug zum zeichnen eines Kreises
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


define(['Geometry/Angle', 'Geometry/Point', 'Basics/StyleDescriptor', 'Script/Script', './DefineWithTwoPoints'],
    function (Angle, Point, StyleDescriptor, Script, DefineWithTwoPoints) {

        return function (idCanvas, done, fail, shapeStyle, constructionLinesStyle) {

            function Circle(CenterPoint, point, Style, fill) {

                var R = Point.cartesianFrom(point).translate(-CenterPoint.X, -CenterPoint.Y).d0;

                var circ = [
                    Script.Cmd('metaMark').with('circ'),
                    Script.Cmd('strokeStyle').with(Style.strokeStyle),
                    Script.Cmd('fillStyle').with(Style.fillStyle),
                    Script.Cmd('beginPath').with(),
                    Script.Cmd('arc').with(CenterPoint.X, CenterPoint.Y, R, 0, 2 * Math.PI, false),
                    Script.Cmd('closePath').with(),
                ];

                if (fill) {
                    circ.push(Script.Cmd('fill').with());
                }
                circ.push(Script.Cmd('stroke').with());


                return circ;
            }

            DefineWithTwoPoints(idCanvas, Circle, done, fail, shapeStyle, constructionLinesStyle);
        }
    });