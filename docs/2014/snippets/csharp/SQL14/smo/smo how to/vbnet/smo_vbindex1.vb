Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common

Module SMO_VBIndex1

    Sub Main()
        ' <snippetSMO_VBIndex1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Declare a Table object and reference the HumanResources table.
        Dim tb As Table
        tb = db.Tables("Employee", "HumanResources")
        'Define an Index object variable by providing the parent table and index name in the constructor.
        Dim idx As Index
        idx = New Index(tb, "TestIndex")
        'Add indexed columns to the index.
        Dim icol1 As IndexedColumn
        icol1 = New IndexedColumn(idx, "BusinessEntityID", True)
        idx.IndexedColumns.Add(icol1)
        Dim icol2 As IndexedColumn
        icol2 = New IndexedColumn(idx, "HireDate", True)
        idx.IndexedColumns.Add(icol2)
        'Set the index properties.
        idx.IndexKeyType = IndexKeyType.DriUniqueKey
        idx.IsClustered = False
        idx.FillFactor = 50
        'Create the index on the instance of SQL Server.
        idx.Create()
        'Modify the page locks property.
        idx.DisallowPageLocks = True
        'Run the Alter method to make the change on the instance of SQL Server.
        idx.Alter()
        'Remove the index from the table.
        idx.Drop()
        ' </snippetSMO_VBIndex1>
    End Sub

End Module
