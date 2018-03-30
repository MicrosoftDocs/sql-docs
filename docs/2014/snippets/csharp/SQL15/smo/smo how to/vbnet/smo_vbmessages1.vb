Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBMessages1

    Sub Main()
        ' <snippetSMO_VBMessages1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference an existing system message using the ItemByIdAndLanguage method.
        Dim msg As SystemMessage
        msg = srv.SystemMessages.ItemByIdAndLanguage(14126, "us_english")
        'Display the message ID and  text.
        Console.WriteLine(msg.ID.ToString + " " + msg.Text)
        ' </snippetSMO_VBMessages1>
    End Sub

End Module
