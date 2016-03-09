//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 25.2.2016
//
//  Projekt.......: HTML5
//  Name..........: Point.js
//  Aufgabe/Fkt...: Klassen zur Darstellung von Punkten
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
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 29.2.2016
//  Änderungen....: Implementieren als RequireJS- Modul
//
//</unit_history>
//</unit_header>        

define(function () {

    var PointProto = Object.create(Object, {

        // Definiert die den Dimensionen des Raumes, von denen Point ein Teil ist
        rank: {
            value: 2,
            writable: false,
            configurable: false,
            enumerable: true
        }
    });


    // Prototyp von Punten im kartesischen Koordinatensystem
    var PointCartesianProto = Object.create(PointProto,
        {
            translate: {
                // Berechnet einen neuen Punkt, der zum aktuellen um tx, ty verschoben ist
                value: function (tx, ty) {
                    return createPointCartesian(this.X + tx, this.Y + ty);
                },
            }
        });

    // Implementierung der Klassenfabrik für kartesiche 2D- Koordinaten
    function createPointCartesian(x, y) {
        var p = Object.create(PointCartesianProto);

        Object.defineProperty(p, "X",
        {
            writable: false,
            configurable: false,
            enumerable: true,
            value: x
        });

        Object.defineProperty(p, "Y",
        {
            writable: false,
            configurable: false,
            enumerable: true,
            value: y,
        });

        // Abstand zum Nullpunkt berechnen
        var _d0 = 0;
        Object.defineProperty(p, 'd0',
        {

            configurable: true,
            enumerable: true,
            get: function () {
                _d0 = Math.sqrt(this.X * this.X + this.Y * this.Y);

                Object.defineProperty(this, 'd0',
                    {
                        configurable: false,
                        enumerable: true,

                        get: function () {
                            return _d0;
                        }
                    });

                return _d0;
            }
        });

        // In Polarkoordinaten umrechnen (Lazy loading mittels self def. function !)
        var _pPolar;
        p.toPolar = function () {
            _pPolar = createPointPolar(Math.asin(this.Y / this.d0), this.d0);
            this.toPolar = function () {
                return _pPolar;
            }
            return _pPolar;
        }

        return p;
    }

    // Implementierung der Klassenfabrik, die 2D- Koordinate aus den X-Y- Koordinatenangaben eines Objektes erzeugt
    function createPointCartesianFromObject(obj) {
        if ('X' in obj && 'Y' in obj) {
            return createPointCartesian(obj.X, obj.Y);
        } else {
            throw TypeError('Das Objekt stellt keine X und Y - Koordinaten bereit. Ein neuer Punkt kann nicht erzeugt werden');
        }
    }


    // poloar ---- poloar ---- polar ---- polar
    // Prototyp von Punkten im Polarkoordinatensystem
    var PointPolarProto = Object.create(PointProto,
        {
            // Punkt um 0- Punkt drehen
            rotate: {
                value: function (angleInRad) {
                    return createPointPolar(this.Phi + angleInRad, this.R);
                }
            },

            // Ortsvektor des Punktes in seiner Länge stauchen/strecken
            stretch: {
                value: function (factor) {
                    return createPointPolar(this.Phi, this.R * factor);
                }
            },
        });

    // IMplementierung der Klassenfabrik für Polarkoordinaten
    function createPointPolar(phi, r) {

        var p = Object.create(PointPolarProto);

        Object.defineProperty(p, "Phi",
            {
                get: function () {
                    return phi;
                }
            });

        Object.defineProperty(p, "R",
            {
                get: function () {
                    return r;
                }
            });

        // In Polarkoordinaten umrechnen (Lazy loading mittels self def. function !)
        var _pCartesian;
        p.toCartesian = function () {
            _pCartesian = createPointCartesian(this.R * Math.cos(this.Phi), this.R * Math.sin(this.Phi));
            this.toCartesian = function () {
                return _pCartesian;
            }
            return _pCartesian;
        }

        return p;

    }

    // Implementierung der Klassenfabrik, die 2D- Koordinate aus den X-Y- Koordinatenangaben eines Objektes erzeugt
    function createPointPolarFromObject(obj) {
        if ('Phi' in obj && 'R' in obj) {
            return createPointPolar(obj.Phi, obj.R);
        } else {
            throw TypeError('Das Objekt stellt keine Phi und R - Koordinaten bereit. Ein neuer Punkt kann nicht erzeugt werden');
        }
    }


    // Modulschnittstelle
    return {
        // Klassenfabrik, die Punkt in kartesischen Koordinaten erzeugt.
        cartesianWith: createPointCartesian,

        cartesianFrom: createPointCartesianFromObject,

        // Klassenfabrik, die Punkt in Polarkoordinaten erzeugt.
        polarWith: createPointPolar,

        polarFrom: createPointPolarFromObject,


        // true, wenn obj eine Instanz von Point ist
        isClassOf: function (obj) {
            var proto = Object.getPrototypeOf(obj);
            if (proto) {
                proto = Object.getPrototypeOf(proto);
                if (proto) {
                    return (proto === PointProto);
                } else
                    return false;
            } else
                return false;
        },

        // true, wenn obj eine Instanz eines Punktes in kartesichen Koordinaten ist
        cartesianIsClassOf: function (obj) {
            var proto = Object.getPrototypeOf(obj);
            if (proto) {
                return (proto === PointCartesianProto)
            } else
                return false;
        },

        // true, wenn obj eine Instanz eines Punktes in Polarkoordinaten ist
        polarIsClassOf: function (obj) {
            var proto = Object.getPrototypeOf(obj);
            if (proto) {
                return (proto === PointPolarProto)
            } else
                return false;
        },


    }
});

