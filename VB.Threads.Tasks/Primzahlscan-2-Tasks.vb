Imports TRD = System.Threading
Imports TPL = System.Threading.Tasks

Module Primzahlscan_2_Tasks

    Sub Exe()

        Dim Stoppuhr As New VB.OO.Klassenlib.StoppUhr
        Stoppuhr.Start()

        ' Task mit Rückgabewert
        Dim t4 As New TPL.Task(Of Long())(Function()
                                              Dim pli As New List(Of Long)
                                              mkoAlgorithm.Primzahlen.findenIn(1, 500000, pli)
                                              Return pli.ToArray()
                                          End Function)

        t4.Start()

        Dim pl = New List(Of Long)
        ' Im Hauptthread einen 2. Task starten
        mkoAlgorithm.Primzahlen.findenIn(500001, 1000000, pl)

        t4.Wait()
        Stoppuhr.Stopp()


        Debug.WriteLine("Anz Primzahlen zw. 1 und 500.000 {0:D}", t4.Result().Count())
        Debug.WriteLine("Anz Primzahlen zw. 500.001 und 1000.000 {0:D}", pl.Count())

        Debug.WriteLine("Zeitauswand: {0:N3} Sec", Stoppuhr.ZeitInSec())



    End Sub

End Module
