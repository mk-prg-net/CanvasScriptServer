Public Module Permutationen

    ''' <summary>
    ''' Berechnet die Anzahl der Permutationen über einer Menge von 
    ''' n Elementen. 
    ''' </summary>
    ''' <param name="n"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Fact(n)
        If n = 0 Then
            Return 1
        Else
            Return n * Fact(n - 1)
        End If
    End Function


    Public Function Permutationen(n As Long) As Long()()
        If n = 0 Then
            Return {New Long() {}}
        ElseIf n = 1 Then
            Return {New Long() {1L}}
        Else
            Return PTupelList(PTupelStart(n), Fact(n))
        End If
    End Function

    Public Function PTupelStart(n As Long) As Long()
        If n = 1 Then
            Return {1L}
        Else
            Return Concat(PTupelStart(n - 1), {n})
        End If
    End Function

    Function PTupelList(Tupel() As Long, i As Long) As Long()()
        If i = 1 Then
            Return {Tupel}
        Else
            Return Concat({Tupel}, PTupelList(NextTupelF(i - 1, Tupel), i - 1))
        End If
    End Function

    'Function NextTupel(i As Long, tupel() As Long)
    '    If i Mod tupel.Length Then
    '        If (i \ tupel.Length).IsOdd Then
    '            ' Tauschen am Anfang
    '            Return Concat({tupel(1), tupel(0)}, Skip(tupel, 2))
    '        Else
    '            ' Tauschen am Ende
    '            Return Concat(Take(tupel, tupel.Length - 2), {tupel(tupel.Length - 2), tupel(tupel.Length - 1)})
    '        End If
    '    Else
    '        If (i \ tupel.Length).IsOdd Then
    '            ' Nach rechts schieben
    '            Return Concat({tupel(1), tupel(0)}, Skip(tupel, 2))
    '        Else
    '            ' Nach Links schieben
    '            Return Concat(Concat(Take(tupel, (i Mod tupel.Length) - 2), {tupel(i Mod tupel.Length), tupel((i Mod tupel.Length) - 1)}), Skip((tupel, i Mod tupel.Length) + 1))
    '        End If
    '    End If
    'End Function

    Function NextTupelF(i As Long, tupel() As Long) As Long()
        If i Mod Count(tupel) = 0 Then
            ' Mit dem ursprünglich ersten Element wurde einmal vollständig von Rechts nach Links, oder 
            ' von Links nach Rechts durchlaufen
            If (i \ Count(tupel)).IsOdd Then
                ' Tauschen am Anfang
                Return Swap(tupel, 0, 1)
            Else
                ' Tauschen am Ende
                Return Swap(tupel, Count(tupel) - 2, Count(tupel) - 1)
            End If
        Else
            ' Tauschen

            If (i \ Count(tupel)).IsOdd Then
                ' Tauschen am Anfang
                Return Swap(tupel, Count(tupel) - i Mod Count(tupel), Count(tupel) - i Mod Count(tupel) - 1)
            Else
                ' Tauschen am Ende
                Return Swap(tupel, i Mod Count(tupel), i Mod Count(tupel) - 1)
            End If

        End If
    End Function

End Module
