Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBCertificate1

    Sub Main()

        ' <snippetSMO_VBCertificate1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Certificate object variable by supplying the parent database and name in the constructor.
        Dim c As Certificate
        c = New Certificate(db, "Test_Certificate")
        'Set the start date, expiry date, and description.
        c.StartDate = DateValue("January 01, 2007")
        c.Subject = "This is a test certificate."
        c.ExpirationDate = DateValue("January 01, 2008")
        'Create the certificate on the instance of SQL Server by supplying the certificate password argument.
        c.Create("pGFD4bb925DGvbd2439587y")
        ' </snippetSMO_VBCertificate1>
        c.Drop()

    End Sub

End Module
