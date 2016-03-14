Imports TRD = System.Threading
Imports TPL = System.Threading.Tasks

Module Primzahlscanner_100_Tasks_abbrechbar
    Sub Exe()



        Dim Stoppuhr As New VB.OO.Klassenlib.StoppUhr
        Stoppuhr.Start()

        Try

            ' Anlegen eines Abbruch- Tokens
            Dim cancelTokSrc As New System.Threading.CancellationTokenSource
            Dim token = cancelTokSrc.Token

            ' Workload partitionieren
            Dim partitionen(99) As Tuple(Of Long, Long)
            For i As Integer = 0 To 99
                partitionen(i) = Tuple.Create(i * 10000L + 1, (i + 1) * 10000L)
            Next

            ' Tasks Starten
            Dim tasks(99) As TPL.Task(Of Long())
            For i As Integer = 0 To 99
                tasks(i) = TPL.Task(Of Long()).Factory.StartNew(Function(part As Tuple(Of Long, Long))
                                                                    ' Abbruch, wenn Cancel- Token signalisiert
                                                                    token.ThrowIfCancellationRequested()
                                                                    Dim pli As New List(Of Long)
                                                                    mkoAlgorithm.Primzahlen.findenIn(part.Item1, part.Item2, pli)
                                                                    Return pli.ToArray()
                                                                End Function,
                                                              partitionen(i),
                                                              token) ' Cancel- Token !
            Next

            Console.Write("Primzahlsuche abbrechen (j/n): ")
            Dim antwort = Console.ReadLine()

            If antwort.Trim().ToLower() = "j" Then
                cancelTokSrc.Cancel()
            End If

            ' Achtung: Auch WaitAll muss mit dem Cancelation- Token abgebrochen werden
            TPL.Task.WaitAll(tasks, token)

            Debug.WriteLine("Alle Primzahlen gefunden. Beginne mit dem zusammenfügen der Teilergebnisse zum Gesamtergebnis")

            ' Sequentieller Teil: Zusammenfügen aller Teilergebnisse zu einem Gesamtergebnis
            Dim res As New LinkedList(Of Long)
            For i = 0 To 99
                For Each p In tasks(i).Result()
                    res.AddLast(p)
                Next
            Next

            Debug.WriteLine("Gesamtergebnis beinhaltet {0:D} Primzahlen", res.Count)

        Catch ex As System.OperationCanceledException
            Debug.WriteLine("Die Primzahlsuche wurde mittels Cancelation- Framework abgebrochen")
        End Try

        Stoppuhr.Stopp()

        Debug.WriteLine("Zeitauswand: {0:N3} Sec", Stoppuhr.ZeitInSec())


    End Sub

End Module
