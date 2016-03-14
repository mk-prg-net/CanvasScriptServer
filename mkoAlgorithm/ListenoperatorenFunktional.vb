''' <summary>
''' (C) Martin Korneffel, Herbst 2012
''' Operatoren zur Listenverarbeitung in funktionalen Programmen
''' </summary>
''' <remarks></remarks>
Public Module ListenoperatorenFunktional

    Function Count(Of T)(list As T()) As Integer
        Return list.Length
    End Function

    Function First(Of T)(list As T()) As T
        Return list.First()
    End Function

    Function Rest(Of T)(list As T()) As T()
        Return list.Skip(1).ToArray()
    End Function

    Function Skip(Of T)(list As T(), i As Integer) As T()
        Return list.Skip(i).ToArray()
    End Function

    Function Take(Of T)(list As T(), i As Integer) As T()
        Return list.Take(i).ToArray()
    End Function

    Function Last(Of T)(list As T()) As T
        Return list.Last()
    End Function

    Function Reverse(Of T)(list As T()) As T()
        Return list.Reverse()
    End Function

    Function Swap(Of T)(list As T(), i As Integer, j As Integer) As T()
        Dim nlist As T() = CType(list.Clone(), T())
        Dim mem As T = nlist(i)
        nlist(i) = nlist(j)
        nlist(j) = mem
        Return nlist
    End Function

    Function ForEach(Of T)(list As T(), f As Func(Of T, T)) As T()
        Dim nlist(list.GetUpperBound(0)) As T
        For i As Integer = 0 To list.GetUpperBound(0)
            nlist(i) = f(list(i))
        Next
        Return nlist
    End Function

    Function Equal(Of T)(listA As T(), listB As T()) As Boolean
        If Count(listA) <> Count(listB) Then
            ' Ungleich lange Listen können nie gleich sein
            Return False
        ElseIf Count(listA) > 0 Then
            ' Rückführung auf die Prüfung, das der Anfang gleich ist und der Rest auch
            Return First(listA).Equals(First(listB)) And Equal(Skip(listA, 1), Skip(listB, 1))
        Else
            ' Zwei leere Listen sind immer gleich
            Return True
        End If
    End Function
   

    ''' <summary>
    ''' Hilfsfunktion, welche zwei Listen aneinanderhängt und zurückliefert
    ''' </summary>
    ''' <param name="a">1. Teilliste</param>
    ''' <param name="b">2. Teilliste</param>
    ''' <returns>Verbundliste, die mit 1. Teilliste beginnt und 2. Teilliste endet</returns>
    ''' <remarks></remarks>
    Function Concat(Of T)(a As T(), b As T()) As T()
        Return a.Concat(b).ToArray()
    End Function
    


    ''' <summary>
    ''' Aus zwei gleichlangen Listen wird eine Liste erzeugt, wobei jeder Eintrag ein 
    ''' Tupel ist, und in der 1. Spalte der Wert aus Liste 1, und in der 2. Spalte der 
    ''' Wert aus Liste 2 steht
    ''' </summary>
    ''' <typeparam name="T1"></typeparam>
    ''' <typeparam name="T2"></typeparam>
    ''' <param name="list1"></param>
    ''' <param name="list2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function TupleCombiner(Of T1, T2)(list1 As T1(), list2 As T2()) As Tuple(Of T1, T2)()
        Debug.Assert(list1.Length = list2.Length)
        Dim tuples(list1.GetUpperBound(0)) As Tuple(Of T1, T2)
        For i As Integer = 0 To list1.GetUpperBound(0)
            tuples(i) = New Tuple(Of T1, T2)(list1(i), list2(i))
        Next
        Return tuples
    End Function

    Function TupleCombiner(Of T1, T2, T3)(list1 As T1(), list2 As T2(), list3 As T3()) As Tuple(Of T1, T2, T3)()
        Debug.Assert(list1.Length = list2.Length And list1.Length = list3.Length)
        Dim tuples(list1.GetUpperBound(0)) As Tuple(Of T1, T2, T3)
        For i As Integer = 0 To list1.GetUpperBound(0)
            tuples(i) = New Tuple(Of T1, T2, T3)(list1(i), list2(i), list3(i))
        Next
        Return tuples
    End Function

    Function TupleCombiner(Of T1, T2, T3, T4)(list1 As T1(), list2 As T2(), list3 As T3(), list4 As T4()) As Tuple(Of T1, T2, T3, T4)()
        Debug.Assert(list1.Length = list2.Length And list1.Length = list3.Length And list1.Length = list4.Length)
        Dim tuples(list1.GetUpperBound(0)) As Tuple(Of T1, T2, T3, T4)
        For i As Integer = 0 To list1.GetUpperBound(0)
            tuples(i) = New Tuple(Of T1, T2, T3, T4)(list1(i), list2(i), list3(i), list4(i))
        Next
        Return tuples
    End Function

End Module
