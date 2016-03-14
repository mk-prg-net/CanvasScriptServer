Public Class Dieselauto
    Inherits Auto

    Sub New()
        MyBase.New()
    End Sub

    Sub New(Kennzeichen As String, V0inKmh As Double, Position As Double)
        MyBase.New(Kennzeichen, V0inKmh, Position)

    End Sub

    ' Tanken Methode an die Eigenarten eines Dieselautos anpassen
    Public Shadows Sub tanken(ByVal menge As Double)
        Debug.WriteLine(String.Format("Ich tanke {0:N2}Liter Dieseltreibstoff", menge))
    End Sub

    ' Tanken Methode an die Eigenarten eines Dieselautos anpassen
    Public Overrides Sub tanken_polymorph(ByVal menge As Double)
        Debug.WriteLine(String.Format("Ich tanke {0:N2}Liter Dieseltreibstoff", menge))
    End Sub


End Class
