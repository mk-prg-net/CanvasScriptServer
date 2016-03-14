Public Module StatFunktional

    ''' <summary>
    ''' Minimum in einer Liste suchen
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Min(Of T As IComparable)(list As T()) As T
        If Count(list) = 0 Then
            Throw New Exception()
        ElseIf Count(list) = 1 Then
            Return list(0)
        ElseIf First(list).CompareTo(Min(Skip(list, 1))) < 0 Then
            Return First(list)
        Else
            Return Min(Skip(list, 1))
        End If
    End Function

    ''' <summary>
    ''' Maximum in einer Liste suchen
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="list"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Max(Of T As IComparable)(list As T()) As T
        If Count(list) = 0 Then
            Throw New Exception()
        ElseIf Count(list) = 1 Then
            Return list(0)
        ElseIf First(list).CompareTo(Min(Skip(list, 1))) > 0 Then
            Return First(list)
        Else
            Return Min(Skip(list, 1))
        End If
    End Function

End Module
