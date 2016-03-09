//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 4.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: SelectPoint.js
//  Aufgabe/Fkt...: Maustool zum Auswählen eines Punktes auswählen
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

define(['Q', 'Geometry/Angle', 'Geometry/Point'], function (Q, Angle, Point) {


    function CreateResult(idCanvas, point, oldPoint) {
        if (oldPoint) {
            return { idCanvas: idCanvas, point: point, oldPoint: oldPoint };
        }
        else {
            return { idCanvas: idCanvas, point: point};
        }
    }


    function round(dblValue) {
        return Math.round(dblValue * 10) / 10;
    }

    return function(idCanvas) {

        // Promise anlegen
        var deferred = Q.defer();
        var offset = $(idCanvas).offset();
        var oldPoint = Point.cartesianWith(-1, -1);

        $(idCanvas).mousemove(function(e) {

            var pM = Point.cartesianWith(round(e.pageX), round(e.pageY)).translate(round(-offset.left), round(-offset.top));

            deferred.notify(CreateResult(idCanvas, pM, oldPoint));

            oldPoint = pM;
        });

        // Punkt wird gewählt- Event- Handler
        $(idCanvas).click(function (e) {

            // Mousemove Event deregistrieren
            $(this).off("mousemove");

            // Click Event deregistrieren
            $(this).off("click");
            
            // Punktkoordinaten bezüglich Canvas- Koordinatensystem bestimmen
            var pM = Point.cartesianWith(round(e.pageX), round(e.pageY)).translate(round(-offset.left), round(-offset.top));

            // Asynchronen Aufruf mit erfolg beenden
            deferred.resolve(CreateResult(idCanvas, pM));

        });

        return deferred.promise;
    }   

});