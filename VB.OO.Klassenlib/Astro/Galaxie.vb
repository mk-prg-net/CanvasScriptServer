Namespace Astro
    Public Class Galaxie
        Inherits Himmelskörper

        Public Sub New(Sterne() As Stern)
            _Sterne = Sterne
        End Sub

        Private _Sterne() As Stern

        Public Overrides ReadOnly Property MasseInErdmassen As Double
            Get
                Return MasseInSonnenmassen * Konstanten.SonnenmasseInKg / Konstanten.ErdmasseInKg
            End Get
        End Property

        Public Overrides ReadOnly Property MasseInSonnenmassen As Double
            Get
                Dim Masse As Double
                For Each Stern As Stern In _Sterne
                    Masse += Stern.MasseInSonnenmassen
                Next

                Return Masse
            End Get
        End Property
    End Class

End Namespace
