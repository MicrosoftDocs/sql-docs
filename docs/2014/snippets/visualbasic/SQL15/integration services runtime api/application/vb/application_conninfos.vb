' <snippetApplication_connInfosVB>
class ApplicationTests1
        Sub Main(ByVal args() As String)
          Dim app As Application =  New Application() 
 
          Dim cis As ConnectionInfos =  app.ConnectionInfos 
          For Each x As ConnectionInfo In cis
              Console.WriteLine("Connection Type" + x.ConnectionType)
          Next
        End Sub
End Class
' </snippetApplication_connInfosVB>