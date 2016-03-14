Public Class Benzinauto
    Inherits Auto

    Sub New()
        MyBase.New()
    End Sub

    Sub New(Kennzeichen As String, V0inKmh As Double, Position As Double)
        MyBase.New(Kennzeichen, V0inKmh, Position)

    End Sub


    ' Tanken Methode an die Eigenarten eines Benzinautos anpassen
    Public Overrides Sub tanken_polymorph(ByVal menge As Double)
        Debug.WriteLine(String.Format("Ich tanke {0:N2}Liter Benzintreibstoff", menge))
    End Sub

End Class
