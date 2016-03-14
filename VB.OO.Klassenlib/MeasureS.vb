Public Class MeasureS
    Implements IDisposable, IComparable

    ' Kontruktoren

    ' überschreiben des Defaultkonstruktors
    Sub New()
        ' Standard hat ein Wegmasz die Länge von 1 m
        value = 1.0
        unit = UnitS.m
    End Sub

    ' alternativer Konstruktor
    Sub New(ByVal value As Double, ByVal unit As UnitS)
        Me.value = value
        Me.unit = unit
    End Sub


    ' Destruktor durch eigene Impelementierung ersetzen
    Protected Overrides Sub Finalize()
        Debug.WriteLine(String.Format("Wegwert von {0:N2} [{1:G}] wird vernichtet", value, unit))
    End Sub


    ' Definieren der möglichen Einheiten als Untermenge von Integer mittels Aufzählungstypen
    Enum UnitS
        mm = 0
        cm = 1
        m = 100
        km = 1000
    End Enum

    ' value und unit gehören zur Benutzerschnittstelle des Objektes und müssen deshalb als 
    ' Public gekennzeichnet werden. 
    Dim _value As Double
    Dim _unit As UnitS

    Public Property value() As Double
        Get
            Return _value
        End Get
        Set(ByVal value As Double)
            _value = value
        End Set
    End Property
    Public Property unit() As UnitS
        Get
            Return _unit
        End Get
        Set(ByVal value As UnitS)
            _unit = value
        End Set
    End Property

    ' ToBaseUnit soll Implementierungsdetail sein und nicht vom Benutzer direkt aufrufbar sein
    ' Deshalb Private -> unsichtbar für Benutzer
    Private Function ToBaseUnit() As Double
        Select Case unit
            Case UnitS.cm
                Return value * 0.01
            Case UnitS.km
                Return value * 1000
            Case UnitS.m
                Return value
            Case UnitS.mm
                Return value * 0.001
            Case Else
                ' Fall: unbekannte Einheit
                Return Double.NaN
        End Select
    End Function


    ' Den Wert in Metern als Eigenschaft modellieren
    ' Propertys gehören per Default zur Benutzerschnittstelle 
    Property valueInMeter() As Double
        Get
            Return ToBaseUnit()
        End Get
        Set(value As Double)
            Me.value = value
            Me.unit = UnitS.m
        End Set
    End Property


    ' überschreiben der aus Objekct geerbten Mthode ToString
    Public Overrides Function ToString() As String

        Return value.ToString() & " [" & unit.ToString() & "]"

    End Function


#Region " IDisposable Support "
    Public Sub Dispose() Implements IDisposable.Dispose
        Debug.WriteLine(String.Format("Wegwert von {0:N2} [{1:G}] wird disposed", value, unit))
        ' Dem GarbageColector mitteilen, dass für dieses Objekt keine Finalisierung(Destruktion) mehr 
        ' stattfinden soll
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    'Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo

    '    ' Annahme: ein Array besteht aus folgenden Elementen:
    '    ' m1, m2, m3, ....
    '    ' dann könnte ein Vergleich beim soriteren folgendermaszen aussehen
    '    ' if m1.CompareTo(m2) > 0 Then ...(Vertauschen)...

    '    ' Downcast in die abgeleitete Klasse
    '    Dim m2 As MeasureS = CType(obj, MeasureS)

    '    If Me.valueInMeter < m2.valueInMeter Then
    '        Return -1
    '    ElseIf valueInMeter > m2.valueInMeter Then
    '        Return 1
    '    Else
    '        Return 0
    '    End If


    'End Function

    Public Function Cmp(obj As Object) As Integer Implements IComparable.CompareTo
        ' Downcast in die abgeleitete Klasse
        Dim m2 As MeasureS = CType(obj, MeasureS)

        If Me.valueInMeter < m2.valueInMeter Then
            Return -1
        ElseIf Me.valueInMeter > m2.valueInMeter Then
            Return 1
        Else
            Return 0
        End If
    End Function

    Public Sub NeueMethode(ByVal x As Integer, ByVal y As String)

    End Sub

    'Shared Function UnitS() As Type
    '    Throw New NotImplementedException
    'End Function


End Class
