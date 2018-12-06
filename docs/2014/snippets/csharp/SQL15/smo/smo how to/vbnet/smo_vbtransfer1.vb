Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBTransfer1

    Sub Main()
        ' <snippetSMO_VBTransfer1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 2008R2 database
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Create a new database that is to be destination database.
        Dim dbCopy As Database
        dbCopy = New Database(srv, "AdventureWorks2012Copy")
        dbCopy.Create()
        'Define a Transfer object and set the required options and properties.
        Dim xfr As Transfer
        xfr = New Transfer(db)
        xfr.CopyAllTables = True
        xfr.Options.WithDependencies = True
        xfr.Options.ContinueScriptingOnError = True
        xfr.DestinationDatabase = "AdventureWorks2012Copy"
        xfr.DestinationServer = srv.Name
        xfr.DestinationLoginSecure = True
        xfr.CopySchema = True
        'Script the transfer. Alternatively perform immediate data transfer with TransferData method.
        xfr.ScriptTransfer()
        ' </snippetSMO_VBTransfer1>
        dbCopy.Drop()
    End Sub

End Module
