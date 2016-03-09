//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: DefineWithTwoPoints.js
//  Aufgabe/Fkt...: Allg. Mauswerkzeug zum zeichnen eines einer Figur, die
//                  mit zwei Mausklicks definiert werden kann
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


define(['Q', 'Geometry/Angle', 'Geometry/Point', 'Basics/StyleDescriptor', 'Script/Script', 'Pictograms/Crosshair', './SelectPoint'],
    function (Q, Angle, Point, StyleDescriptor, Script, Crosshair, SelectPoint) {

        return function (idCanvas, createShape, done, fail, shapeStyle, constructionLinesStyle) {

            var canvas = $(idCanvas).get(0);
            var ctx = canvas.getContext('2d');

            var shapeStyle = shapeStyle || StyleDescriptor.create();
            var constructionLinesStyle = constructionLinesStyle ||
                (function () {
                    var descr = StyleDescriptor.create();
                    descr.fillStyle = '#ff0000';
                    descr.strokeStyle = '#ff0000';
                    return descr;
                })();


            // aktuelle Darstellung sichern als Bitmap
            var savedImage = new Image()
            savedImage.src = $(idCanvas).get(0).toDataURL("image/png")

            var startPoint = null;            

            function rubberBandRect(point, Style) {

                // Radiuslinie zeichnen
                var rline = [
                    Script.Cmd('strokeStyle').with(Style.strokeStyle),
                    Script.Cmd('beginPath').with(),
                    Script.Cmd('moveTo').with(startPoint.X, startPoint.Y),
                    Script.Cmd('lineTo').with(point.X, point.Y),
                    Script.Cmd('closePath').with(),
                    Script.Cmd('stroke').with(),
                ];

                return rline.concat(createShape(startPoint, point, Style, false));
            }

            // Maus Tools testen
            SelectPoint(idCanvas)
                .progress(function (PointData) {

                    // Hintergrund wiederherstellen
                    ctx.drawImage(savedImage, 0, 0);

                    // Fadenkreuz- Script erzeugen und zeichnen
                    var cross = Crosshair(PointData.point.X, PointData.point.Y, 0, constructionLinesStyle);
                    Script.plot(cross, ctx);

                })
                .then(function (PointData) {

                    // Hintergrund wiederherstellen
                    ctx.drawImage(savedImage, 0, 0);

                    startPoint = PointData.point;

                    // Fadenkreuz- Script erzeugen und zeichnen
                    var cross = Crosshair(startPoint.X, startPoint.Y, 0, shapeStyle);
                    Script.plot(cross, ctx);

                    // Neuen Grundzustand sichern
                    savedImage.src = $(idCanvas).get(0).toDataURL("image/png")

                    // Zweiten Punkt zur definition der Figur festlegen
                    SelectPoint(PointData.idCanvas)
                        .progress(function (PointData) {

                            // Hintergrund wiederherstellen
                            ctx.drawImage(savedImage, 0, 0);

                            Script.plot(rubberBandRect(PointData.point, constructionLinesStyle), ctx);

                        })
                        .then(function (PointData) {

                            // Hintergrund wiederherstellen
                            ctx.drawImage(savedImage, 0, 0);

                            var shape = createShape(startPoint, PointData.point, shapeStyle, true);

                            Script.plot(shape, ctx);

                            done(shape);

                        });

                })
                .catch(function (err) {
                    fail(err);
                }).finally(function () {
                })
                .done();
        };

    });