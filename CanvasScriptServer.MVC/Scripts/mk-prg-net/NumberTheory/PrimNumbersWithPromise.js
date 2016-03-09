
//<unit_header>
//----------------------------------------------------------------
// Copyright 2016 Martin Korneffel
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 23.2.2016
//
//  Projekt.......: HTML5
//  Name..........: PrimNumbersWithPromise.js
//  Aufgabe/Fkt...: Implementierung des asynchronen Primizahlscanners
//                  mittels Promises aus der Bibliothek Q.js
//                  (wurde als NuGet- Paket installiert)
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


define(["Async/CancellationTokenFactory", "./PrimNumbers", "Q"], function (CancellationTokenFactory, PrimNumbers, Q) {

    var PrimNumbersWithPromise = {};


    // doneCallback(CancellationToken, PrimNums[])
    // progressInfo(Anzahl bereits gefundener)
    PrimNumbersWithPromise.ScanWithCancel = function (CancellationToken, begin, end) {

        // Promise anlegen
        var deferred = Q.defer();

        // Liste der gefundenen Primzahlen anlegen
        var primNums = [];

        primScanPart(CancellationToken, primNums, begin, end, deferred);

        return deferred.promise;
    };


    function CreateResult(CancellationToken, primNums) {
        return { ctoken: CancellationToken, res: primNums };
    }


    function primScanPart(CancellationToken, primNums, begin, end, deferred) {


        try {

            if (CancellationToken.stop) {
                // Vorzeitigen Stop einer asynchronen Aufgabe signalisieren. 
                // Korrespondiert mit promise.then(function(CancelToken, primNums){...}) 
                deferred.resolve(CreateResult(CancellationToken, primNums));
            } else {

                // Über den Prozessfortschritt informieren. 
                // Korrespondiert mit promise.progress(function(begin){...})
                deferred.notify(begin);

                var EndOfPart = Math.min(begin + 1000, end);

                console.log("begin: " + begin + " endPart: " + EndOfPart);

                for (var i = begin; i < EndOfPart; i++) {
                    if (PrimNumbers.Test(i))
                        primNums.push(i);
                }

                if (EndOfPart == end)
                    // Erfolgreiche Fertigstellung einer asynchronen Aufgabe signalisieren. 
                    // Korrespondiert mit promise.then(function(CancelToken, primNums){...}) 
                    deferred.resolve(CreateResult(CancellationToken, primNums));
                else
                    // Ein weiter zu bearbeitendes Teilstück der Aufgabe in als Callback in ein
                    // Event verpacken und in die nächste Runde delegieren - Kernfunktion von Async
                    setTimeout(primScanPart, 0, CancellationToken, primNums, begin + 1000, end, deferred);

            }

        } catch (err) {
            console.log("mko.algo.primScanPart ist abgestürtzt: " + err);
            deferred.reject(err);
        }
    };

    return PrimNumbersWithPromise;



})