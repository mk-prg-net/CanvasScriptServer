Public Class Auto

    ''' <summary>
    ''' Konstruktor für ein Auto
    ''' </summary>
    ''' <param name="Kennzeichen"></param>
    ''' <param name="V0inKmh"></param>
    ''' <param name="Position"></param>
    ''' <remarks></remarks>
    Public Sub New(Kennzeichen As String, V0inKmh As Double, Position As Double)
        _Kennzeichen = Kennzeichen
        _VinKmh = V0inKmh
        _EntfernungABStartInKm = Position
    End Sub

    ''' <summary>
    ''' Default- Konstruktor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _Kennzeichen = "-- -- --"
        _VinKmh = 0.0
        _EntfernungABStartInKm = 0.0
    End Sub

    ''' <summary>
    ''' Letzter Wunsch vor dem killen durch den GC
    ''' </summary>
    ''' <remarks></remarks>
    Protected Overrides Sub Finalize()
        Debug.WriteLine(Kennzeichen & " wird vom GC gelöscht")
    End Sub

    Dim _Kennzeichen As String
    ReadOnly Property Kennzeichen As String
        Get
            Return _Kennzeichen
        End Get
    End Property

    ''' <summary>
    ''' Abstand vom Startpunkt der Autobahn. Dienst in einer Simulation als Bezugspunkt,
    ''' um die Position der Farhzeuge auf den Autobahnfahrstreifen zu definieren
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntfernungABStartInKm As Double
        Get
            Return _EntfernungABStartInKm
        End Get
        Set(value As Double)
            _EntfernungABStartInKm = value
        End Set
    End Property
    Dim _EntfernungABStartInKm As Double

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntfernungAbStartInMeter As Double
        Get
            Return _EntfernungABStartInKm * 1000.0
        End Get
        Set(value As Double)
            _EntfernungABStartInKm = value / 1000.0
        End Set
    End Property

    ' VEreinfachte Form einer Property (für den Anfang)
    Property EntfernungABStartInKm1 As Double

    ''' <summary>
    ''' Nummer des Fahrstreifens, auf dem sich das Auto gerade befindet. Nummerierung erfolgt 
    ''' von rechts nach links und beginnt mit 0 (rechter Fahrstreifen hat Nummer 0).
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Fahrstreifen As Integer
        Get
            Return _Farstreifen
        End Get
    End Property
    Dim _Farstreifen As Integer

    ''' <summary>
    ''' Momentangeschwindigkeit des Autos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property VinKmh As Double
        Get
            Return _VinKmh
        End Get
    End Property
    Dim _VinKmh As Double


    Public Sub tanken(ByVal menge As Double)
        Debug.WriteLine(String.Format("Ich tanke {0:N2}Liter Treibstoff", menge))
    End Sub


    ' Anlegen einer virtuellen Funktionstabelle, so dass immer auf die Implementierung in der abgeleiteten
    ' Klasse verwiesen wird-> realisert durch Schlüsselwort "overridable"
    Public Overridable Sub tanken_polymorph(ByVal menge As Double)
        Debug.WriteLine(String.Format("Ich tanke {0:N2}Liter Treibstoff", menge))
    End Sub



    Public Delegate Sub DgHupen(Anzahl As Integer)

    Public Event Hupen As DgHupen

    Public Sub Bremsen(a As Double, dt As Double)
        _EntfernungABStartInKm += VinKmh * dt - a * dt * dt / 2
        _VinKmh -= (a * dt)
    End Sub

    Public Sub Beschleunigen(a As Double, dt As Double)
        _EntfernungABStartInKm += VinKmh * dt + a * dt * dt / 2
        _VinKmh += (a * dt)
    End Sub

    Public Sub Fahren(dt As Double)
        _EntfernungABStartInKm += VinKmh * dt
    End Sub

    Public Function ÜberholenBeginnen() As Integer
        _Farstreifen += 1
        Return _Farstreifen
    End Function

    Public Function ÜberholenBeenden() As Integer
        _Farstreifen -= 1
        If _Farstreifen < 0 Then
            Throw New Exception("Auf dem Seitenstreifen gelandet")
        End If
        Return _Farstreifen
    End Function

    ''' <summary>
    ''' In einem Simulationsschritt werden z.B. die Abstände zu anderen Verkehrsteilnehmern ermittelt 
    ''' und bewertet. In Abhängigkeit von der Bewertung wird reagiert (Ereignisse ausgelöst)
    ''' </summary>
    ''' <param name="Teilnehmer"></param>
    ''' <remarks></remarks>
    Public Sub VerkehrsteilnehmerBewerten(Teilnehmer As Auto, ByRef Abstand As Double, ByRef SollÜberholtWerden As Boolean)
        ' Abstand berechnen
        Abstand = Teilnehmer.EntfernungABStartInKm - Me.EntfernungABStartInKm

        If Fahrstreifen = Teilnehmer.Fahrstreifen Then
            If Abstand > 0 And Abstand < VinKmh / 2.0 Then
                ' Halber Tachoabstand unterschritten
                If Abstand < 10 Then
                    ' gefählich...
                    SollÜberholtWerden = True
                    RaiseEvent Hupen(1)
                    If Abstand < 5 Then
                        ' ... sehr gefährlich
                        Debug.Write(Me.Kennzeichen + " hat ein Problem mit " + Teilnehmer.Kennzeichen + ": ")
                        RaiseEvent Hupen(2)
                    End If
                End If
            End If
        End If

    End Sub

End Class
