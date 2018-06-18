Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBProperties1

    Sub Main()
        ' <snippetSMO_VBProperties1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Get a property.
        Console.WriteLine(srv.Information.Version)
        'Set a property.
        srv.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql
        ' </snippetSMO_VBProperties1>
    End Sub

End Module
