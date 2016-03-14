Public Module Primzahlen

    Sub findenIn(ByVal von As Long, ByVal bis As Long, primzahlen As List(Of Long))

        For kandidat As Long = von To bis Step 1
            Dim ogTeiler As Integer = CInt(Math.Sqrt(CDbl(kandidat)))

            Dim istPrimzahl As Boolean = False
            Dim Teiler As Long
            Test(kandidat, istPrimzahl, Teiler)

            If istPrimzahl Then
                primzahlen.Add(kandidat)
            End If
        Next

    End Sub

    Function findenIn(ByVal von As Long, ByVal bis As Long) As Long()

        Dim Primzahlen As New List(Of Long)
        For kandidat As Long = von To bis Step 1
            Dim ogTeiler As Integer = CInt(Math.Sqrt(CDbl(kandidat)))

            Dim istPrimzahl As Boolean = False
            Dim Teiler As Long
            Test(kandidat, istPrimzahl, Teiler)

            If istPrimzahl Then
                Primzahlen.Add(kandidat)
            End If
        Next

        Return Primzahlen.ToArray()
    End Function

    Sub Test(Prüfling As Long, ByRef IstPrimzahl As Boolean, ByRef Teiler As Long)

        ' 1 als Primzahl ausschließen, da neutrales Element bezüglich Multiplikation
        If Prüfling = 1 Or Prüfling = 2 Then
            IstPrimzahl = False
            Teiler = 1
            Return
        End If

        Teiler = 2

        '  Wiederhole bis Teiler = Prüfling \ 2 + 1
        Dim Grenze As Long = (Prüfling \ 2 + 1)
        Do Until Teiler = Grenze

            ' Wenn Prüfling Mod Teiler = 0 dann 
            If Prüfling Mod Teiler = 0 Then
                '      IstPrimzahl := false
                '      Goto Ende Primzahltest
                'Debug.WriteLine("Teiler von " & Prüfling & " ist " & Teiler)
                IstPrimzahl = False
                Return
                '    Sonst
            Else
                Teiler = Teiler + 1
                'Teiler += 1
                ' Ende Wenn, dann, sonst
            End If

            '  Ende Wiederholung
        Loop

        '  IstPrimzahl = true
        IstPrimzahl = True
        Return

        ' Ende Primzahltest

    End Sub

    ''' <summary>
    ''' Liefert zur Zahl z die Liste der Primfaktoren
    ''' </summary>
    ''' <param name="z"></param>
    ''' <param name="primfaktoren"></param>
    ''' <remarks></remarks>
    Sub Primfaktoren(z As Long, primfaktoren As List(Of Long))

        Dim zz As Long = z
        Dim p = 2

        Do While zz <> 1

            Do While zz Mod p = 0
                zz \= p
                primfaktoren.Add(p)
            Loop

            p = NextPrimzahl(p)
        Loop

    End Sub
    Dim primz() As Long = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31}
    Function NextPrimzahl(p As Long) As Long
        If p < primz.Last() Then
            Return primz.SkipWhile(Function(e) p >= e).First()
        Else
            Do
                p += 2
            Loop Until p.IsPrim()
            Return p
        End If

    End Function

    ' KGV:
    ' 27 = 3 * 3 * 3 
    ' 45 = 3 * 3 * 5
    ' => 135 = 3 * 3 * 3 * 5





End Module