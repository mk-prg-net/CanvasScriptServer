Public Class StoppUhr

    Dim _TicksBeimStart As Long
    Dim _TicksBeimStopp As Long

    Sub New()

        Reset()

    End Sub

    Sub Start()
        Reset()
    End Sub

    Sub Stopp()
        _TicksBeimStopp = DateTime.Now.Ticks
    End Sub

    Sub Reset()
        _TicksBeimStart = DateTime.Now.Ticks
        _TicksBeimStopp = _TicksBeimStart
    End Sub

    Function ZeitInMs() As Double
        Return New TimeSpan(_TicksBeimStopp - _TicksBeimStart).TotalMilliseconds
    End Function

    Function ZeitInSec() As Double
        Return New TimeSpan(_TicksBeimStopp - _TicksBeimStart).TotalSeconds
    End Function





End Class
