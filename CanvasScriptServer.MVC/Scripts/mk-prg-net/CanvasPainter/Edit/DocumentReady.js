//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 6.3.2016
//
//  Projekt.......: CanvasPainter
//  Name..........: DocumentReady.js
//  Aufgabe/Fkt...: Eventhandler für Edit- View der CanvasPainter ASPX Anwendung
//                  registrieren.
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 8.3.2016
//  Änderungen....: Erweitert für CanvasScriptServer
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
requirejs(['Geometry/Angle', 'Geometry/Point', 'Script/Script', 'Basics/StyleDescriptor', 'MouseTools/SelectPoint', 'MouseTools/Circle', 'MouseTools/Rect', 'MouseTools/Line'],
    function (Angle, Point, Script, StyleDescriptor, SelectPoint, CircleMouseTool, RectMouseTool, LineMouseTool) {

        "use strict";

        // Gesamte Zeichnung
        var Drawing = [];

        var picKey = "pic"
        var idCanvas = '#pane';
        var idScripts = "#Scripts";

        // Namen des zu editierenden Scriptes bestimmen
        var userName = $('#UserName').val();
        var scriptName = $('#ScriptName').val();


        // Sichern der Zeichnung im Browser
        function SavePic() {

            if (typeof localStorage !== "undefined") {
                localStorage.setItem(picKey, JSON.stringify(Drawing));
            }
        }

        function PrintScriptListing(jsonScripts) {
            // Die Liste der Canvas- Befehle ausgeben
            var i = 0;
            $(idScripts).empty().append(jsonScripts.map(function (script) {
                i++;
                return '<tr><td>' + i.toString() + '</td><td><code>' + JSON.stringify(script, null, 3) + '</code></td></tr>';                
            }));

        }

        // Wiederherstellen der Zeichung aus dem lokalen Speicher des Browsers
        function RestorePic(ctx) {
            if (localStorage && localStorage.length > 0 && localStorage.key(0) === picKey) {
                var strPic = localStorage.getItem(picKey);
                var jsonScripts = JSON.parse(strPic);

                Drawing = Script.from(jsonScripts);
                Script.plot(Drawing, ctx);

                // Die Liste der Canvas- Befehle ausgeben
                PrintScriptListing(jsonScripts);
            }
        }


        function MakeRequestScriptUrl(userName, scriptName) {
            return '/api/CanvasScriptWebApi?userName=' + userName + '&scriptName=' + scriptName;
        }

        // Wiederherstellen vom Server
        function RestoreFromServer(userName, scriptName, ctx) {
            var url = MakeRequestScriptUrl(userName, scriptName);

            // Script beim Start vom Server abrufen
            $.ajax({
                method: "GET",
                //dataType: "json",
                url: url,
                data: '',
                cache: false
            }).done(function (Data, status, req) {

                // Es hat geklappt
                console.log(Data.toString());
                var jsonObjListFromServer = JSON.parse(Data);

                // Aus den Objekten ein echtes Canvas-Script aufbauen
                //var scriptsFromServer = Script.from(jsonObjListFromServer);
                Drawing = Script.from(jsonObjListFromServer);

                // Script zeichnen
                //Script.plot(scriptsFromServer, ctx);
                Script.plot(Drawing, ctx);

                // Die Liste der Canvas- Befehle ausgeben
                PrintScriptListing(jsonObjListFromServer);


            }).fail(function (jqXHR, textStatus, errorThrown) {

                // Leider ein Fehler
                console.log(jqXHR.status.toString());
            });
        }


        function SaveToServer(userName, scriptName, ctx) {

            var url = MakeRequestScriptUrl(userName, scriptName);            

            var Param = {
                userName: userName,
                scriptName: scriptName,
                scriptJson: JSON.stringify(Drawing)
            };

            var jsonScript = JSON.stringify(Drawing);

            // Script beim Start vom Server abrufen
            $.ajax({
                method: "POST",
                //dataType: "json",
                url: url,
                data: Param,
                cache: false
            }).done(function (Data, status, req) {

                // Es hat geklappt
                console.log(Data.toString());
                var jsonObjListFromServer = JSON.parse(Data);

                // Aus den Objekten ein echtes Canvas-Script aufbauen
                var scriptsFromServer = Script.from(jsonObjListFromServer);

                // Script zeichnen
                Script.plot(scriptsFromServer, ctx);

            }).fail(function (jqXHR, textStatus, errorThrown) {

                // Leider ein Fehler
                console.log(jqXHR.status.toString());
            });

        }


        $(document).ready(function () {

            var canvas = $(idCanvas).get(0);
            var ctx = canvas.getContext('2d');

            var width = $(idCanvas).attr('width');
            var height = $(idCanvas).attr('height');

            var shapeStyle = (function () {
                var sd = StyleDescriptor
                sd.fillStyle = '#ff4444';
                sd.strokeStyle = '#ff4444';
                return sd;
            })();


            var canvasInit = [
                Script.Cmd('fillStyle').with('#000000'),
                Script.Cmd('fillRect').with(0, 0, width, height),
            ];

            Script.plot(canvasInit, ctx);

            RestoreFromServer(userName, scriptName, ctx);

            // Lokal sichern
            $("#Save").click(SavePic);

            // Auf Server sichern
            $("#SaveToServer").click(function () {
                SaveToServer(userName, scriptName, ctx);
            });


            $("#Restore").click(function () {
                RestorePic(ctx);
            });


            $("#RestoreFromServer").click(function () {
                RestoreFromServer(userName, scriptName, ctx);
            });


            // Alles löschen
            $("#ClearCanvas").click(function () {

                Script.plot(canvasInit, ctx);
                // Aufzeichnungen löschen
                Drawing = [];
                $(idScripts).empty()
            });


            $("#red").click(function () {
                shapeStyle = (function () {
                    var sd = StyleDescriptor
                    sd.fillStyle = '#ff4444';
                    sd.strokeStyle = '#ff4444';
                    return sd;
                })();
            });

            $("#green").click(function () {
                shapeStyle = (function () {
                    var sd = StyleDescriptor
                    sd.fillStyle = '#99cc00';
                    sd.strokeStyle = '#99cc00';
                    return sd;
                })();
            });


            $("#blue").click(function () {
                shapeStyle = (function () {
                    var sd = StyleDescriptor
                    sd.fillStyle = '#33b5e5';
                    sd.strokeStyle = '#33b5e5';
                    return sd;
                })();
            });


            $("#LineTo").click(function () {

                LineMouseTool(idCanvas,
                    function (line) { // done

                        PrintScriptListing(line);

                        //$(idScripts).append(rect.map(function (script) {
                        //    return '<tr><td><code>' + JSON.stringify(script, null, 3) + '</code></td></tr>';
                        //}));

                        // Bestehende Zeichnung erweitern
                        line.unshift(Drawing.length, 0);
                        [].splice.apply(Drawing, line);

                    },
                    function (err) { // fail

                    },
                    shapeStyle);
            });


            $("#Circle").click(function () {

                CircleMouseTool(idCanvas, 
                    function (circ) { // done

                        PrintScriptListing(circ);

                        //$(idScripts).append(circ.map(function (script) {
                        //    return '<tr><td><code>' + JSON.stringify(script, null, 3) + '</code></td></tr>';
                        //}));

                        // Bestehende Zeichnung erweitern
                        circ.unshift(Drawing.length, 0);
                        [].splice.apply(Drawing, circ);

                    },
                    function (err) { // fail

                    },
                    shapeStyle);
            });

            $("#Rect").click(function () {

                RectMouseTool(idCanvas,
                    function (rect) { // done

                        PrintScriptListing(rect);

                        //$(idScripts).append(rect.map(function (script) {
                        //    return '<tr><td><code>' + JSON.stringify(script, null, 3) + '</code></td></tr>';
                        //}));

                        // Bestehende Zeichnung erweitern
                        rect.unshift(Drawing.length, 0);
                        [].splice.apply(Drawing, rect);
                        
                    },
                    function (err) { // fail

                    },
                    shapeStyle);
            });



        });

    });

