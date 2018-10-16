Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBConfigure1

    Sub Main()
        ' <snippetSMO_VBConfigure1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Display information about the instance of SQL Server in Information and Settings.
        Console.WriteLine("OS Version = " & srv.Information.OSVersion)
        Console.WriteLine("State = " & srv.Settings.State.ToString)
        'Display information specific to the current user in UserOptions.
        Console.WriteLine("Quoted Identifier support = " & srv.UserOptions.QuotedIdentifier)
        'Modify server settings in Settings.

        srv.Settings.LoginMode = ServerLoginMode.Integrated
        'Modify settings specific to the current connection in UserOptions.
        srv.UserOptions.AbortOnArithmeticErrors = True
        'Run the Alter method to make the changes on the instance of SQL Server.
        srv.Alter()
        ' </snippetSMO_VBConfigure1>

        srv.Settings.LoginMode = ServerLoginMode.Mixed
        srv.Alter()
    End Sub

End Module
