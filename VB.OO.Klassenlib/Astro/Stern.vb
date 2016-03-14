Namespace Astro

    Public Class Stern
        Inherits Himmelskörper

        Public Sub New(MasseInSonnenmassen As Double, SpektralklasseStern As Spektralklasse)
            _Masse = MasseInSonnenmassen
            _Spektralklasse = SpektralklasseStern
        End Sub

        Private _Masse As Double
        Private _Spektralklasse As Spektralklasse

        Public Overrides ReadOnly Property MasseInErdmassen As Double
            Get
                Return _Masse * Konstanten.SonnenmasseInKg / Konstanten.ErdmasseInKg
            End Get
        End Property


        Public Overrides ReadOnly Property MasseInSonnenmassen As Double
            Get
                Return _Masse
            End Get
        End Property

        Public ReadOnly Property Spektralklasse As Konstanten.Spektralklasse
            Get
                Return _Spektralklasse
            End Get
        End Property

    End Class

End Namespace