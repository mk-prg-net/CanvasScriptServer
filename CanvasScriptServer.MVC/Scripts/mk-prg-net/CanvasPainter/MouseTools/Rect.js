//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: Rect.js
//  Aufgabe/Fkt...: Mauswerkzeug zum zeichnen eines Rechtecks
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

            function Rect(startPoint, point, Style, fill) {

                var topLeft = Point.cartesianWith(Math.min(startPoint.X, point.X), Math.min(startPoint.Y, point.Y));
                var width = Math.max(startPoint.X, point.X) - topLeft.X;
                var heigth = Math.max(startPoint.Y, point.Y) - topLeft.Y;

                var rect = [
                    Script.Cmd('metaMark').with('Rect'),
                    Script.Cmd('strokeStyle').with(Style.strokeStyle),
                    Script.Cmd('fillStyle').with(Style.fillStyle),                    
                ];

                if (fill) {
                    rect.push(Script.Cmd('fillRect').with(topLeft.X, topLeft.Y, width, heigth));
                }

                rect.push(Script.Cmd('strokeRect').with(topLeft.X, topLeft.Y, width, heigth));               


                return rect;
            }


            DefineWithTwoPoints(idCanvas, Rect, done, fail, shapeStyle, constructionLinesStyle)
        }

    });