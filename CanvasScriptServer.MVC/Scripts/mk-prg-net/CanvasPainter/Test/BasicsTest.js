//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 29.2.2016
//
//  Projekt.......: HTML5
//  Name..........: BasicsTest.js
//  Aufgabe/Fkt...: Test der Basisbibliotheken
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
requirejs(['QUnit', 'Geometry/Angle', 'Geometry/Point'],
    function (QUnit, Angle, Point) {

        QUnit.test("Winkelumrechnungen", function (assert) {
                        
            assert.strictEqual(Math.round(Angle.toRad(0) * 1000000.0), 0.0, 'Das Bogenmaß von 0° sollte 0 sein');
            assert.strictEqual(Math.round(Angle.toRad(90) * 1000000.0), Math.round((Math.PI * 1000000.0)/2), 'Das Bogenmaß von 90° sollte PI sein');
            assert.strictEqual(Math.round(Angle.toRad(180) * 1000000.0), Math.round(Math.PI * 1000000.0), 'Das Bogenmaß von 180° sollte PI/2 sein');
            assert.strictEqual(Math.round(Angle.toRad(270) * 1000000.0), Math.round((3 * Math.PI * 1000000.0) / 2), 'Das Bogenmaß von 270° sollte 3*PI/2 sein');
            assert.strictEqual(Math.round(Angle.toRad(360) * 1000000.0), Math.round((2 * Math.PI * 1000000.0)), 'Das Bogenmaß von 360° sollte 2*PI sein');

            assert.strictEqual(Math.round(Angle.toDeg(0)), 0.0, 'Das Gradmaß 0 rad sollte 0° sein');
            assert.strictEqual(Math.round(Angle.toDeg(Math.PI/2)), 90, 'Das Gradmaß PI/2 sollte 90° sein');
            assert.strictEqual(Math.round(Angle.toDeg(Math.PI)), 180, 'Das Gradmaß PI sollte 180° sein');
            assert.strictEqual(Math.round(Angle.toDeg(3 * Math.PI / 2)), 270, 'Das Gradmaß 3*PI/2 sollte 270° sein');
            assert.strictEqual(Math.round(Angle.toDeg(2 * Math.PI)), 360, 'Das Gradmaß von 2*PI sollte  360° sein');

        });


        QUnit.test("Point in polar- und kartesischen Koordinaten", function (assert) {

            var p0 = Point.cartesianWith(0, 0);

            assert.ok(typeof p0 !== 'undefined', 'Es sollte ein Punkt p0 erzeugt worden sein');
            assert.ok(Point.cartesianIsClassOf(p0), 'Der Punkt sollte vom Typ PointCartesian sein');
            assert.ok(p0.hasOwnProperty('X') && p0.hasOwnProperty('Y'), 'p0 sollte die Eigenschaften X und Y haben');
            assert.equal(p0.X, 0, 'p0.X sollte 0 sein');
            assert.equal(p0.Y, 0, 'p0.Y sollte 0 sein');
            assert.ok('translate' in Object.getPrototypeOf(p0), 'Der Prototype von Point sollte den Member "translate" haben');

            var p1 = p0.translate(3, 5);
            assert.ok(typeof p1 !== 'undefined', 'Es sollte ein Punkt p1 erzeugt worden sein');
            assert.ok(p1.hasOwnProperty('X') && p1.hasOwnProperty('Y'), 'p1 sollte die Eigenschaften X und Y haben');
            assert.equal(p1.X, 3, 'p1.X sollte 3 sein');
            assert.equal(p1.Y, 5, 'p1.Y sollte 5 sein');


            var p2 = Point.cartesianWith(1, 1);

            assert.equal(Math.round(p2.toPolar().R * 1000.0), 1414, '(1,1) sollte einen Abstand von 1,4142 zum Punkt (0,0) haben.');
            assert.equal(Math.round(p2.toPolar().Phi * 1000.0), 785, '(1,1) sollte Abstand von 1,4142 zum Punkt (0,0) haben.');


            var p3 = Point.polarWith(Angle.toRad(45), Math.sqrt(2));
            assert.ok(typeof p3 !== 'undefined', 'Es sollte ein Punkt p3 erzeugt worden sein');
            assert.ok(Point.polarIsClassOf(p3), 'Der Punkt sollte vom Typ PointPolar sein');
            assert.ok(p3.hasOwnProperty('Phi') && p3.hasOwnProperty('R'), 'p3 sollte die Eigenschaften Phi und R haben');
            assert.equal(Math.round(p3.Phi * 1000), Math.round(Math.PI * 1000/4), 'p3.Phi sollte PI/4 sein');
            assert.equal(Math.round(p3.R * 1000000), Math.round(Math.sqrt(2) * 1000000), 'p3.R sollte Wurzel(2) sein');


            assert.ok(Point.isClassOf(p0), 'p0 sollte ein Point sein');
            assert.ok(Point.isClassOf(p1), 'p1 sollte ein Point sein');
            assert.ok(Point.isClassOf(p2), 'p2 sollte ein Point sein');
            assert.ok(Point.isClassOf(p3), 'p3 sollte ein Point sein');

            // Rotation und Streckung

            var p4 = p3.rotate(Angle.toRad(45)).stretch(Math.sqrt(2)).toCartesian();

            assert.ok(Point.isClassOf(p4), 'p4 sollte ein Point sein');
            assert.ok(Point.cartesianIsClassOf(p4), 'Der Punkt sollte vom Typ PointCartesian sein');
            assert.ok(p4.hasOwnProperty('X') && p4.hasOwnProperty('Y'), 'p4 sollte die Eigenschaften X und Y haben');
            assert.equal(Math.round(p4.X * 1000), 0, 'p4.X sollte 0 sein');
            assert.equal(Math.round(p4.Y* 1000), 2000, 'p0.Y sollte 2 sein');


        });

        // QUnit starten.
        QUnit.start();

    });

