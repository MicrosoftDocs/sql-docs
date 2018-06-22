Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common


Module SMO_VBMethods2

    Sub Main()
        ' <snippetSMO_VBMethods2>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference a table on the AdventureWorks2012 database.
        Dim tb As Table
        tb = srv.Databases("AdventureWorks2012").Tables("Employee", "HumanResources")
        'Call one of the Table object methods and supply the FillFactor argument.
        tb.RebuildIndexes(70)
        ' </snippetSMO_VBMethods2>
    End Sub

End Module
