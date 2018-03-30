' <snippetApplicationComponentStorePathvb>
Class ApplicationTests
        Sub Main(ByVal args() As String)
            Dim app As Application =  New Application() 
            Dim pipelinecomp As String =  app.ComponentStorePath 
            Console.WriteLine(pipelinecomp)
        End Sub
End Class
' </snippetApplicationComponentStorePathvb>