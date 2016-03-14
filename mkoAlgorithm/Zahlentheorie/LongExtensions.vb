' Erweitrungsmethoden für Long
Imports System.Runtime.CompilerServices

Public Module LongExtensions

    ''' <summary>
    ''' Gibt true Zurück, wenn z eine Primzahl ist
    ''' </summary>
    ''' <param name="z"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <Extension()>
    Public Function IsPrim(ByVal z As Long) As Boolean
        Dim istPrimzahl As Boolean, Teiler As Long
        Primzahlen.Test(z, istPrimzahl, Teiler)
        Return istPrimzahl
    End Function

    ''' <summary>
    ''' Gibt eine Liste mit den Primfaktoren, in die die Zahl zerlegt werden kann zurück
    ''' </summary>
    ''' <param name="z"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <Extension()>
    Public Function Factorization(z As Long) As Long()
        Dim pfl As New List(Of Long)
        Primzahlen.Primfaktoren(z, pfl)
        Return pfl.ToArray()
    End Function

    <Extension()>
    Public Function IsEven(z As Long) As Boolean
        Return z Mod 2 = 0
    End Function

    <Extension()>
    Public Function IsOdd(z As Long) As Boolean
        Return z Mod 2 = 1 
    End Function

    <Extension()>
    Public Function ABS(z As Long) As Long
        If z < 0 Then
            Return -z
        ElseIf z > 0 Then
            Return z
        Else
            Return 0
        End If
    End Function




End Module
