Namespace Astro

    ''' <summary>
    ''' Da die Klasse eine abstrakte Methode enthält, muss sie als abstrakt 
    ''' gekennzeichnet werden.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Himmelskörper
        ''' <summary>
        ''' Name des Himmelskörpers
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Name As String

        ''' <summary>
        ''' Masse des Himmelskörpers.
        ''' Eine allgemeine Berechnungsvorschrift für Massen existiert nicht.
        ''' Deshalb wird hier sinnloserweise 0 zurückgegeben, da ein
        ''' Funktionsrumpf bei virtuellen Funktionen definiert werden muss
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable ReadOnly Property MasseInErdmassen As Double
            Get
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Abstracte Methoden sind virtuelle Methoden, für die keine sinvolle 
        ''' implementierung in der Basisklasse existiert
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride ReadOnly Property MasseInSonnenmassen As Double


    End Class

End Namespace
