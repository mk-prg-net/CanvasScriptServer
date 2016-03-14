' Erweitrungsmethoden für Long
Imports System.Runtime.CompilerServices

Public Module FunctionalExtensions

    ''' <summary>
    ''' Curry- Operator: Bindet den 1. Parameter einer binären Funktion an eine übergebene Konstante, verpackt ihn in 
    ''' einer unären Funktion und gibt diese zurück 
    ''' Curry(f(x,y), a) -> fc(y) äquivalent f(a, y)
    ''' </summary>
    ''' <typeparam name="TP1"></typeparam>
    ''' <typeparam name="TP2"></typeparam>
    ''' <typeparam name="TRet"></typeparam>
    ''' <param name="f"></param>
    ''' <param name="a"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Curry(Of TP1, TP2, TRet)(f As Func(Of TP1, TP2, TRet), a As TP1) As Func(Of TP2, TRet)
        Return Function(b) f(a, b)
    End Function

    ''' <summary>
    ''' Curry- Operator für binäre Action
    ''' </summary>
    ''' <typeparam name="TP1"></typeparam>
    ''' <typeparam name="TP2"></typeparam>
    ''' <param name="f"></param>
    ''' <param name="a"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function Curry(Of TP1, TP2)(f As Action(Of TP1, TP2), a As TP1) As Action(Of TP2)
        Return Sub(b) f(a, b)
    End Function

End Module
