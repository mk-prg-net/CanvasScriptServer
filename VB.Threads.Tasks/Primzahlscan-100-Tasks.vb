Imports TRD = System.Threading
Imports TPL = System.Threading.Tasks

Module Primzahlscan_100_Tasks

    Sub Exe()

        Dim Stoppuhr As New VB.OO.Klassenlib.StoppUhr
        Stoppuhr.Start()

        ' Workload partitionieren
        Dim partitionen(99) As Tuple(Of Long, Long)
        For i As Integer = 0 To 99
            partitionen(i) = New Tuple(Of Long, Long)(i * 10000 + 1, (i + 1) * 10000)
        Next

        ' Tasks Starten
        Dim tasks(99) As TPL.Task(Of Long())
        For i As Integer = 0 To 99
            tasks(i) = TPL.Task(Of Long()).Factory.StartNew(Function(part As Tuple(Of Long, Long))
                                                                Dim pli As New List(Of Long)
                                                                mkoAlgorithm.Primzahlen.findenIn(part.Item1, part.Item2, pli)
                                                                Return pli.ToArray()
                                                            End Function,
                                                          partitionen(i))
        Next

        TPL.Task.WaitAll(tasks)

        Debug.WriteLine("Alle Primzahlen gefunden. Beginne mit dem zusammenfügen der Teilergebnisse zum Gesamtergebnis")

        ' Sequentieller Teil: Zusammenfügen aller Teilergebnisse zu einem Gesamtergebnis
        Dim res As New LinkedList(Of Long)
        For i = 0 To 99
            For Each p In tasks(i).Result()
                res.AddLast(p)
            Next
        Next

        Stoppuhr.Stopp()

        Debug.WriteLine("Gesamtergebnis beinhaltet {0:D} Primzahlen", res.Count)
        Debug.WriteLine("Zeitauswand: {0:N3} Sec", Stoppuhr.ZeitInSec())


    End Sub

End Module
