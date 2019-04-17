Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Smo.Mail
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBMail1

    Sub Main()
        ' <snippetSMO_VBMail1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server()
        'Define the Database Mail service with a SqlMail object variable and reference it using the Server Mail property.
        Dim sm As SqlMail
        sm = srv.Mail
        'Define and create a mail account by supplying the Database Mail service, name, description, display name, and email address arguments in the constructor.
        Dim a As MailAccount
        a = New MailAccount(sm, "AdventureWorks Administrator", "AdventureWorks Automated Mailer", "Mail account for administrative e-mail.", "dba@Adventure-Works.com")
        a.Create()
        ' </snippetSMO_VBMail1>

        a.Drop()



    End Sub

End Module
