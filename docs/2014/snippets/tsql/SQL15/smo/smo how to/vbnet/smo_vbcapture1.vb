Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBCapture1

    Sub Main()
        ' <snippetSMO_VBCapture1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Set the execution mode to CaptureSql for the connection.
        srv.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql
        'Make a modification to the server that is to be captured.
        srv.UserOptions.AnsiNulls = True
        srv.Alter()
        'Iterate through the strings in the capture buffer and display the captured statements.
        Dim s As String
        For Each s In srv.ConnectionContext.CapturedSql.Text
            Console.WriteLine(s)
        Next
        'Execute the captured statements.
        srv.ConnectionContext.ExecuteNonQuery(srv.ConnectionContext.CapturedSql.Text)
        'Revert to immediate execution mode. 
        srv.ConnectionContext.SqlExecutionModes = SqlExecutionModes.ExecuteSql
        ' </snippetSMO_VBCapture1>
        srv.UserOptions.AnsiNulls = False
    End Sub

End Module
