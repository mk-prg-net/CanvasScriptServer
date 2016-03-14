Module Module1
    Sub LongTerm(ByVal Duration As Integer, ByVal symbol As String)

        For i As Integer = 0 To Duration
            Debug.Write(symbol)
            System.Threading.Thread.Sleep(500)
        Next

    End Sub

    ' VB- Compiler erzeugt gem. folgender Anweisung eine Klasse, die Einsprungadressen vom 
    ' Typ Worker verwalten kann
    Delegate Sub DGWorker(ByVal auftrag As Integer, ByVal symbol As String)


    Sub Main()

        Debug.WriteLine("Erster Auftrag")
        LongTerm(3, "1")

        Debug.WriteLine("Zweiter Auftrag")
        Dim dg As New Action(Of Integer, String)(AddressOf LongTerm)

        dg.Invoke(3, "2")

        ' Delegates nutzen, um asynchron eine Methode zu starten
        Debug.WriteLine("Auftrag 3 und 4 parallel starten")
        Dim ares As IAsyncResult = dg.BeginInvoke(20, "3", Nothing, Nothing)

        'ares.AsyncWaitHandle.WaitOne()

        While (Not ares.IsCompleted)
            System.Threading.Thread.Sleep(500)
            Debug.Write("X")
        End While

        'ares.AsyncWaitHandle.WaitOne()
        dg.EndInvoke(ares)

        Debug.WriteLine("Alle Worker sind fertig")


    End Sub

End Module
