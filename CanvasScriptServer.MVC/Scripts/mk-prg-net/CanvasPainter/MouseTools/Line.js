//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: Line.js
//  Aufgabe/Fkt...: Mauswerkzeug zum zeichnen einer Linie
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

            function Line(startPoint, point, Style, fill) {

                var line = [
                    Script.Cmd('metaMark').with('Line'),
                    Script.Cmd('strokeStyle').with(Style.strokeStyle),
                    Script.Cmd('fillStyle').with(Style.fillStyle),
                ];

                line.push(Script.Cmd('moveTo').with(startPoint.X, startPoint.Y));
                line.push(Script.Cmd('lineTo').with(point.X, point.Y));
                line.push(Script.Cmd('stroke').with());



                return line;
            }


            DefineWithTwoPoints(idCanvas, Line, done, fail, shapeStyle, constructionLinesStyle)
        }

    });