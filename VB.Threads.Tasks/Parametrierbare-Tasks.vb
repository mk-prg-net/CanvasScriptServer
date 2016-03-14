Imports TRD = System.Threading
Imports TPL = System.Threading.Tasks

Module Parametrierbare_Tasks
    Sub Exe()

        ' Eine Berechnung auf mehrere parametriernbare Tasks verteilen
        Dim t2 As New TPL.Task(Sub(p As Tuple(Of Integer, String)) LongTerm(p.Item1, p.Item2), _
                               New Tuple(Of Integer, String)(5, "3"))
        Dim t3 As New TPL.Task(Sub(p As Tuple(Of Integer, String)) LongTerm(p.Item1, p.Item2), _
                               New Tuple(Of Integer, String)(5, "4"))

        t2.Start()
        t3.Start()

        ' Warten, bis alle Tasks fertig sind
        TPL.Task.WaitAll(t2, t3)


    End Sub

End Module
