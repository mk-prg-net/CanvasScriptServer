// Ausprobieren der Module

// 1. Konfigurieren von RequireJS
requirejs.config({
    //By default load any module IDs from js/lib
    baseUrl: '/Scripts/mk-prg-net',

    paths: {
        "QUnit": "../qunit",
        "Q": "../q"
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
requirejs(['QUnit', 'NumberTheory/PrimNumbers', 'NumberTheory/PrimNumbersWithPromise', 'Async/CancellationTokenFactory'],
    function (QUnit, PrimNumbers, PrimNumbersWithPromise, CancelationTokenFactory) {

        QUnit.test("PrimeNumbers.Test", function (assert) {

            assert.ok(PrimNumbers.Test(2), "2 sollte als Primzahl erkannt werden");
            assert.ok(!PrimNumbers.Test(9), "9 Zahlen sollten als nicht Primzahl erkannt werden");
            assert.ok(!PrimNumbers.Test(41 * 51), '' + (41 * 51) + " sollte als nicht Primzahl erkannt werden");
            assert.ok(PrimNumbers.Test(53), "51 sollte als Primzahl erkannt werden");

        });


        // Test einer asynchronen Methode !
        QUnit.test('PrimeNumbers.Scan', function (assert) {

            // QUnit wird solange angehalten, bis der die asynchrone Methode endet.
            // assert.async liefert einen Callback zurück, über den QUnit wieder 
            // gestartet werden kann.

            var doneAsync = assert.async(1);

            PrimNumbers.Scan(1, 10000,
                // done
                function (Cancellationtoken, PrimNums) {

                    assert.ok(!Cancellationtoken.stop, "Am Ende der asynchronen Methode sollte das Cancellationtoken keinen Stop signalisieren.");
                    assert.ok(true, "Wie zu erwarten lief die Primzahlsuche fehlerfrei. " + PrimNums.length + " Primzahlen gefunden");
                    doneAsync();
                },

                // fail
                function (err) {
                    // 
                    assert.ok(true, "Fail: " +  err);
                    doneAsync();
                },

                // progress info
                function (begin) {
                    assert.ok(true, "Eine erfolgreich Frotschrittsanzeige. Abschnitt ab " + begin + " verarbeitet");
                    console.log("Primzahlscanner jetzt bei " + begin);

                    if (begin > 5000)
                        throw "Keine Lust auf Arbeit";
                });
        });

        // Test einer asynchronen Methode !
        QUnit.test('PrimeNumbersWithPromise.Scan', function (assert) {

            // QUnit wird solange angehalten, bis der die asynchrone Methode endet.
            // assert.async liefert einen Callback zurück, über den QUnit wieder 
            // gestartet werden kann.

            var doneAsync = assert.async(1);

            var ctoken = CancelationTokenFactory.create();

            PrimNumbersWithPromise.ScanWithCancel(ctoken, 1, 10000)
                .then(
                // done
                function (Result) {

                    assert.ok(!Result.ctoken.stop, "Am Ende der asynchronen Methode sollte das Cancellationtoken keinen Stop signalisieren.");
                    assert.ok(true, "Wie zu erwarten lief die Primzahlsuche fehlerfrei. " + Result.res.length + " Primzahlen gefunden");
                })
                .progress(
                // progress info
                function (begin) {
                    assert.ok(true, "Eine erfolgreich Frotschrittsanzeige. Abschnitt ab " + begin + " verarbeitet");
                    console.log("Primzahlscanner jetzt bei " + begin);
                })
                .catch(
                // fail
                function (err) {
                    // 
                    assert.ok(false, err);
                })
                .finally(function () {
                    doneAsync();
                })
                .done();
        });


        // start QUnit.
        //QUnit.load();

        // Testrunner von QUnit mauell starten, da bei Einsatz von RequireJS 
        // dieser nicht automatisch gestartet wird.
        QUnit.start();

    });
