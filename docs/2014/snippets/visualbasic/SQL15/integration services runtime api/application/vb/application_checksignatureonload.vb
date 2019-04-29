' <snippetApplication_CheckSignatureOnLoadVB>
Class ApplicationTests
    Sub Main(ByVal args() As String)
        Dim app As Application =  New Application() 
 
        Dim sig As Boolean =  app.CheckSignatureOnLoad 
        Console.Writeline(sig)
    End Sub
End Class
' </snippetApplication_CheckSignatureOnLoadVB>