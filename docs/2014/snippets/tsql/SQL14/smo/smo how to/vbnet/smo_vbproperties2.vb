Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBProperties2

    Sub Main()
        ' <snippetSMO_VBProperties2>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Create a new table in the AdventureWorks2012 database. 
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        Dim tb As Table
        'Specify the parent database, table schema and the table name in the constructor.
        tb = New Table(db, "Test_Table", "HumanResources")
        'Add columns because the table requires columns before it can be created. 
        Dim c1 As Column
        'Specify the parent table, the column name and data type in the constructor.
        c1 = New Column(tb, "ID", DataType.Int)
        tb.Columns.Add(c1)
        c1.Nullable = False
        c1.Identity = True
        c1.IdentityIncrement = 1
        c1.IdentitySeed = 0
        Dim c2 As Column
        c2 = New Column(tb, "Name", DataType.NVarChar(100))
        c2.Nullable = False
        tb.Columns.Add(c2)
        tb.AnsiNullsStatus = True
        'Create the table on the instance of SQL Server.
        tb.Create()
        ' </snippetSMO_VBProperties2>
        tb.Drop()
    End Sub

End Module
