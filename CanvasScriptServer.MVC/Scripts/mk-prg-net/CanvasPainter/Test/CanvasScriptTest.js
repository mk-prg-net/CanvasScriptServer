//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 1.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: CanvasRobotTest.js
//  Aufgabe/Fkt...: Test der Automatisierungskomponente
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
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/Scripts/mk-prg-net/CanvasPainter',

    paths: {
        "QUnit": "../../qunit",
        "Q": "../../q"
    },

    shim: {
        'QUnit': {
            exports: 'QUnit',
            init: function () {
                QUnit.config.autoload = false;
                QUnit.config.autostart = false;
            }
        }
    }
});


// 2. Starten der Anwendung
requirejs(['Q', 'QUnit', 'Geometry/Angle', 'Geometry/Point', 'Basics/StyleDescriptor', 'Script/Script', 'Pictograms/Crosshair', 'MouseTools/SelectPoint'],
    function (Q, QUnit, Angle, Point, StyleDescriptor, Script, Crosshair, SelectPoint) {


        function SetTrafo(FK, Phi, newTx, newTy) {
            return FK.map(function (script) {

                if (script.Name === 'rotate') {
                    return Script.Cmd('rotate').with(Angle.toRad(Phi));
                }
                else if (script.Name === 'translate') {
                    return Script.Cmd('translate').with(newTx, newTy);
                } else {
                    return script;
                }
            });
        }



        QUnit.test("Pictogramme zeichnen", function (assert) {

            var canvas = $("#pane").get(0);
            var ctx = canvas.getContext('2d');
            var width = $('#pane').attr('width');
            var height = $('#pane').attr('height');

            var canvasInit = [
                Script.Cmd('fillStyle').with('#000000'),
                Script.Cmd('fillRect').with(0, 0, width, height),
            ];

            Script.plot(canvasInit, ctx);

            var CrosshairStyle = (function(){
                var descr = StyleDescriptor.create();
                descr.fillStyle = 'rgb(255, 0, 0)';
                descr.strokeStyle = 'rgb(255, 0, 0)';
                return descr;
            })();

            var FK = Crosshair(100, 100, 0, CrosshairStyle);
            assert.ok(FK.length > 0, 'Das Fadenkreuz sollte aus mehreren Elementen bestehen');

            Script.plot(FK, ctx);

            var FK2 = SetTrafo(FK, 30, 200, 100);
            Script.plot(FK2, ctx);

            var FK3 = SetTrafo(FK, 45, 200, 200);
            Script.plot(FK3, ctx);


            var FK4 = SetTrafo(FK, 0, 100, 200);
            Script.plot(FK4, ctx);


            var nonLin = [
                Script.Cmd('save').with(),

                Script.Cmd('strokeStyle').with('#ffaaee'),
                Script.Cmd('fillStyle').with('#aaffee'),
                Script.Cmd('translate').with(250, 200),
                Script.Cmd('font').with('24px sans serif'),

                Script.Cmd('metaMark').with("Textausgabe"),
                Script.Cmd('fillText').with(10, 20, 'Das ist ein Test'),

                // Figur aus quadratischen und kubischen Linien zeichnen
                Script.Cmd('metaMark').with("Linien"),
                Script.Cmd('beginPath').with(),
                Script.Cmd('moveTo').with(0, 0),
                Script.Cmd('quadraticCurveTo').with(100, 100, 100, 0),
                Script.Cmd('bezierCurveTo').with(150, 200, 150, 100, 100, 200),
                Script.Cmd('quadraticCurveTo').from({
                    X: 0,
                    Y: 0,
                    cpX: 0,
                    cpY: 200,
                }),
                Script.Cmd('closePath').with(),
                Script.Cmd('stroke').with(),

                // Kreis zeichnen
                Script.Cmd('metaMark').with('Kreise'),
                Script.Cmd('beginPath').with(),
                Script.Cmd('arc').with(200, 100, 75, Angle.toRad(270), Angle.toRad(90), false),
                Script.Cmd('closePath').with(),
                Script.Cmd('stroke').with(),


                // Rechtecke Zeichnen     
                Script.Cmd('metaMark').with('Rechtecke'),
                Script.Cmd('beginPath').with(),
                Script.Cmd('strokeRect').with(300, 0, 150, 100),
                Script.Cmd('fillRect').with(310, 10, 130, 80),
                Script.Cmd('clearRect').with(320, 20, 110, 60),
                Script.Cmd('closePath').with(),

                Script.Cmd('restore').with()

            ];

            Script.plot(nonLin, ctx);

            var jsonScript = JSON.stringify(nonLin);
            var jsonPart = JSON.stringify(Script.Cmd('lineTo').with(10, 20));

            var obj = JSON.parse(jsonPart);
            assert.ok('lineTo' in obj, 'Das eingelesene JSON- Objekt sollte eine lintTo Eigenschaft haben');
            if ('lineTo' in obj) {
                var lineTo = Script.Cmd('lineTo').from(obj.lineTo);
                assert.strictEqual(lineTo.X, 10.0, 'lineTo.X sollte 10 sein');
                assert.strictEqual(lineTo.Y, 20.0, 'lineTo.Y sollte 20 sein');
            }

            var funcList = [];
            var jsonObjects = JSON.parse(jsonScript);
            jsonObjects.map(function (script) {
                let name = Object.getOwnPropertyNames(script)[0];
                let param = JSON.stringify(script[name]);
                funcList.push(name + '(' + param + ")");
            })

        });


        QUnit.test('Select Point', function (assert) {

            var doneAsync = assert.async(1);
            var ctx = $('#pane').get(0).getContext('2d');
            ctx.strokeStyle = '#ffffff';
            //ctx.globalCompositeOperation = 'xor';

            var circStyle = circStyle ||
                (function () {
                    var descr = StyleDescriptor.create();
                    descr.fillStyle = '#ffffff';
                    descr.strokeStyle = '#ffffff';
                    return descr;
                })();
            var constructionLinesStyle = constructionLinesStyle ||
                (function () {
                    var descr = StyleDescriptor.create();
                    descr.fillStyle = '#ff0000';
                    descr.strokeStyle = '#ff0000';
                    return descr;
                })();


            // aktuelle Darstellung sichern als Bitmap
            var savedImage = new Image()
            savedImage.src = $('#pane').get(0).toDataURL("image/png")

            var CenterPoint = null;

            function Circle(point, Style) {

                var R = Point.cartesianFrom(point).translate(-CenterPoint.X, -CenterPoint.Y).d0;

                var circ = [
                    Script.Cmd('strokeStyle').with(Style.strokeStyle),
                    Script.Cmd('fillStyle').with(Style.fillStyle),
                    Script.Cmd('beginPath').with(),
                    Script.Cmd('arc').with(CenterPoint.X, CenterPoint.Y, R, 0, 2 * Math.PI, false),
                    Script.Cmd('closePath').with(),
                    Script.Cmd('stroke').with(),
                ];

                return circ;
            }

            function rubberBandCircel(point, Style) {

                // Radiuslinie zeichnen
                var rline = [
                    Script.Cmd('strokeStyle').with(Style.strokeStyle),
                    Script.Cmd('beginPath').with(),
                    Script.Cmd('moveTo').with(CenterPoint.X, CenterPoint.Y),
                    Script.Cmd('lineTo').with(point.X, point.Y),
                    Script.Cmd('closePath').with(),
                    Script.Cmd('stroke').with(),
                ];

                return rline.concat(Circle(point, Style));
            }

            // Maus Tools testen
            SelectPoint('#pane')
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

                    CenterPoint = PointData.point;

                    // Fadenkreuz- Script erzeugen und zeichnen
                    var cross = Crosshair(CenterPoint.X, CenterPoint.Y, 0, circStyle);
                    Script.plot(cross, ctx);

                    // Neuen Grundzustand sichern
                    savedImage.src = $(PointData.idCanvas).get(0).toDataURL("image/png")

                    // Radius des Kreises festlegen
                    SelectPoint(PointData.idCanvas)
                        .progress(function (PointData) {

                            // Hintergrund wiederherstellen
                            ctx.drawImage(savedImage, 0, 0);

                            Script.plot(rubberBandCircel(PointData.point, constructionLinesStyle), ctx);

                        })
                        .then(function (PointData) {

                            // Hintergrund wiederherstellen
                            ctx.drawImage(savedImage, 0, 0);

                            Script.plot(Circle(PointData.point, circStyle), ctx);

                            // Neuen Grundzustand sichern
                            savedImage.src = $(PointData.idCanvas).get(0).toDataURL("image/png")

                        });

                })
                .catch(function (err) {
                    assert.ok(false);
                    doneAsync();
                }).finally(function () {
                    assert.ok(true);
                    doneAsync();
                })
                .done();
        });

        // QUnit starten.
        QUnit.start();

    });

