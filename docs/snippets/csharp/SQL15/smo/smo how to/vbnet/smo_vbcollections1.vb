Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module VBCollections1

    Sub Main()
        ' <snippetSMO_VBCollections1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Modify a property using the Databases, Tables, and Columns collections to reference a column.
        srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("ModifiedDate").Nullable = True
        'Call the Alter method to make the change on the instance of SQL Server.
        srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("ModifiedDate").Alter()
        ' </snippetSMO_VBCollections1>
        srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("ModifiedDate").Nullable = False
        srv.Databases("AdventureWorks2012").Tables("Person", "Person").Columns("ModifiedDate").Alter()

    End Sub

End Module

