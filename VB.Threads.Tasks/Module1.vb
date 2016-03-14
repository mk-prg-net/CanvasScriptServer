Imports TRD = System.Threading
Imports TPL = System.Threading.Tasks
Module Module1

    Sub LongTerm(Duration As Integer, Symbol As String)
        For i = 0 To Duration
            Debug.Write(Symbol)
            TRD.Thread.Sleep(500)
        Next
    End Sub


    Sub Main()

        '' Eine Berechnung in einem Task verpacken und asynchron starten
        'Dim t1 As New TPL.Task(Sub() LongTerm(5, "1"))

        't1.Start()

        'While (Not t1.IsCompleted)
        '    LongTerm(1, "2")
        '    TRD.Thread.Sleep(250)
        'End While

        'Parametrierbare_Tasks.Exe()

        'Primzahlscan_2_Tasks.Exe()

        Primzahlscan_100_Tasks.Exe()

        'Primzahlscanner_100_Tasks_abbrechbar.Exe()



    End Sub

End Module
