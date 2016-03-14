Public Module PrimzahlScannerFunktional

    ''' <summary>
    ''' Funktionale Implementierung des Primzahltestes
    ''' </summary>
    ''' <param name="z"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IstPrimzahl(z As Long) As Boolean
        Return TesteAufTeiler(z, 2)
    End Function

    ''' <summary>
    ''' Hilfsfunktion zur funktionalen Implementierung des Primzahltestes
    ''' </summary>
    ''' <param name="z">zu prüfende Zahl</param>
    ''' <param name="KandidatTeiler">Teilbarkeit bezüglich dieses Wertes wird geprüft</param>
    ''' <returns>True, wenn der KandidatTeiler oder ein größerer Wert Teiler ist</returns>
    ''' <remarks></remarks>
    Private Function TesteAufTeiler(ByRef z As Long, KandidatTeiler As Long) As Boolean
        If KandidatTeiler >= z \ 2 + 1 Then
            Return True
        Else
            Return z Mod KandidatTeiler <> 0 And TesteAufTeiler(z, KandidatTeiler + 1)
        End If
    End Function

    ''' <summary>
    ''' Liefert alle Primzahlen in einem Intervall [a,b]
    ''' </summary>
    ''' <param name="a">Intervallbeginn</param>
    ''' <param name="b">Intervallende</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AllePrimzahlenIn(a As Long, b As Long) As Long()
        If IstPrimzahl(a) And a <> 1 Then
            Return ListeErweiternUmPrimzahlenIn(a + 1, b, {a})
        Else
            Return ListeErweiternUmPrimzahlenIn(a + 1, b, {})
        End If
    End Function


    ''' <summary>
    ''' Erweitert eine übergebene Liste um alle Primzahlen zw. z und b
    ''' </summary>
    ''' <param name="z">Intervallbeginn</param>
    ''' <param name="b">Intervallende</param>
    ''' <param name="pzl">Liste, die um Primzahlen erweitert werden soll</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListeErweiternUmPrimzahlenIn(z As Long, ByRef b As Long, pzl As Long()) As Long()
        If z > b Then
            Return pzl
        ElseIf IstPrimzahl(z) Then
            Return ListeErweiternUmPrimzahlenIn(z + 1, b, Concat(pzl, {z}))
        Else
            Return ListeErweiternUmPrimzahlenIn(z + 1, b, pzl)
        End If

    End Function

End Module
