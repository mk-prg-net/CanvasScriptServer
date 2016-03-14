Public Module NummericFunktional


    Function Summe(liste() As Double) As Double
        If Count(liste) = 0 Then
            Return 0
        ElseIf Count(liste) = 1 Then
            Return liste(0)
        Else
            Return First(liste) + Summe(Skip(liste, 1))
        End If
    End Function

    Function Potenz(Basis, Exponent)
        If Basis <> 0 And Exponent = 0 Then
            Return 1
        ElseIf Basis = 0 Then
            Return Double.NaN
        Else
            Return Basis * Potenz(Basis, Exponent - 1)
        End If
    End Function


End Module
