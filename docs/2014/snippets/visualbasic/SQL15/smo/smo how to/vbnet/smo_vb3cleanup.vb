Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common

Module SMO_VB3cleanup

    Sub Main()
        Dim s As Server
        s = New Server
        s.Logins("Jane").Drop()

    End Sub

End Module
