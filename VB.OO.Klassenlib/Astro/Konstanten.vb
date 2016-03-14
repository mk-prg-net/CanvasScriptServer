Public Module Konstanten

    Public Const ErdmasseInKg As Double = 5.974E+24
    Public Const ErdmondmasseInKg As Double = 7.349E+22
    Public Const MarsmasseInKg As Double = 6.419E+23
    Public Const PhobosmasseInKg As Double = 1.072E+16
    Public Const DeimosmasseInKg As Double = 1.8E+15
    Public Const SaturnmasseInKg As Double = 5.685E+26
    Public Const JupitermasseInKg As Double = 1.899E+27
    Public Const GanymedmasseInKg As Double = 1.482E+23
    Public Const KallistomasseInKg As Double = 1.076E+23
    Public Const EuropamasseInKg As Double = 4.8E+22
    Public Const IomasseInKg As Double = 8.94E+22
    Public Const SonnenmasseInKg As Double = 1.9884E+30


    ''' <summary>
    '''  Siehe https://de.wikipedia.org/wiki/Spektralklasse
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Spektralklasse
        O
        B
        A
        F
        G
        K
        M
        L
        T
        Y
        R
        N
        S
    End Enum

    Public Enum Spektralklasse_Farbe
        blau
        blau_weiss
        weiss
        weiss_gelb
        gelb
        orange
        rot_orange
        rot
        Infrarot
    End Enum

    Public Spektralklasse_Temperatur_Beziehung As New Dictionary(Of Spektralklasse, Tuple(Of Double, Double, Spektralklasse_Farbe)) From _
        {
            {Spektralklasse.O, Tuple.Create(30000.0, 50000.0, Spektralklasse_Farbe.blau)},
            {Spektralklasse.B, Tuple.Create(10000.0, 28000.0, Spektralklasse_Farbe.blau_weiss)},
            {Spektralklasse.A, Tuple.Create(7500.0, 9750.0, Spektralklasse_Farbe.weiss)},
            {Spektralklasse.F, Tuple.Create(6000.0, 7350.0, Spektralklasse_Farbe.weiss_gelb)},
            {Spektralklasse.G, Tuple.Create(5000.0, 5900.0, Spektralklasse_Farbe.gelb)},
            {Spektralklasse.K, Tuple.Create(3500.0, 4850.0, Spektralklasse_Farbe.orange)},
            {Spektralklasse.M, Tuple.Create(2000.0, 3350.0, Spektralklasse_Farbe.rot_orange)},
            {Spektralklasse.L, Tuple.Create(1300.0, 2000.0, Spektralklasse_Farbe.rot)},
            {Spektralklasse.T, Tuple.Create(600.0, 1300.0, Spektralklasse_Farbe.rot)},
            {Spektralklasse.Y, Tuple.Create(200.0, 600.0, Spektralklasse_Farbe.Infrarot)},
            {Spektralklasse.R, Tuple.Create(3500.0, 5400.0, Spektralklasse_Farbe.rot_orange)},
            {Spektralklasse.N, Tuple.Create(2000.0, 3500.0, Spektralklasse_Farbe.rot_orange)},
            {Spektralklasse.S, Tuple.Create(1900.0, 3500.0, Spektralklasse_Farbe.rot)}
        }

End Module
