Module Module1

    Sub Main()

        Dim T1 = AsynchronJob(1, 50000L)
        Dim T2 = AsynchronJob(1000000000L, 1000000050L)
        Dim T3 = AsynchronJob(1000000051L, 1000000100L)
        Dim T4 = AsynchronJob(1000000101L, 1000000150L)
        Dim T5 = AsynchronJob(1000000151L, 1000000200L)
        'Dim res6 = AsynchronJob(1000005001L, 1000006000L)

        Dim pCount = 0
        While (Not Task.WaitAll(New Task() {T1, T2, T3, T4, T5}, 500))
            ' Warten, bis die Asynchrone Methode beendet wurde
            pCount += 1

            If pCount < 20 Then
                Debug.Write(".")
            Else
                Debug.WriteLine("")
                pCount = 0
            End If

        End While


        Dim all = T1.Result.Concat(T2.Result.Concat(T3.Result.Concat(T4.Result.Concat(T5.Result))))

        ' Minimum und Maximum mittels PLinq bestimmen
        ' Daten werden mittels AsParallel partitioniert
        Dim min = all.AsParallel().Min()
        Dim max = all.AsParallel().Max()



    End Sub


    Async Function AsynchronJob(start As Long, ende As Long) As Task(Of Double())
        ' Ab hier wird vom Haupttask ein weiterer Task abgespalten, eine Referenz auf diesen 
        ' an den Rufer zurückgeliefert. Für den Rufer hat der Worker hier geendet.

        ' Im neuen Task erfolgt die Primzahlberechnung. Bedingt durch Await werden alle 
        ' nachfolgenden Schritte solange blockiert, bis der Task endet
        Dim listePrim1 = Await Task.Run(Function() mkoAlgorithm.Primzahlen.findenIn(start, ende))

        ' Hat der erste Task geendet, dann geht es hier weiter. Es wird ein weiterer Task gestartet
        Dim psRatio = Await Task.Run(Function() mkoAlgorithm.PSRatio.Calculate(listePrim1.ToArray()))

        Await Task.Run(Sub()
                           Debug.WriteLine("psRatio in [" & start & ", " & ende & "]")
                           For Each ps In psRatio
                               Debug.WriteLine(ps)
                           Next
                       End Sub)

        Return psRatio

    End Function

End Module
