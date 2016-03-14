''' <summary>
''' Berechnet das Verhältnis der Abstände zum Vorgänger und zum Nachfolger eines elements Ei 
''' in einer Zahlenfolge
''' </summary>
''' <remarks></remarks>
Public Class PSRatio

    Public Shared Function Calculate(numSequence As Long()) As Double()

        Dim psRatioSequence(numSequence.Length - 1) As Double
        For i As Integer = 1 To numSequence.Length - 2
            psRatioSequence(i - 1) = CDbl(numSequence(i) - numSequence(i - 1)) / (numSequence(i + 1) - numSequence(i))
        Next

        Return psRatioSequence

    End Function



End Class
