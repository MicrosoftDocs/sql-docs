Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBTable1

    Sub Main()
        ' <snippetSMO_VBTable1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Table object variable by supplying the parent database and table name in the constructor. 
        Dim tb As Table
        tb = New Table(db, "Test_Table")
        'Add various columns to the table.
        Dim col1 As Column
        col1 = New Column(tb, "Name", DataType.NChar(50))
        col1.Collation = "Latin1_General_CI_AS"
        col1.Nullable = True
        tb.Columns.Add(col1)
        Dim col2 As Column
        col2 = New Column(tb, "ID", DataType.Int)
        col2.Identity = True
        col2.IdentitySeed = 1
        col2.IdentityIncrement = 1
        tb.Columns.Add(col2)
        Dim col3 As Column
        col3 = New Column(tb, "Value", DataType.Real)
        tb.Columns.Add(col3)
        Dim col4 As Column
        col4 = New Column(tb, "Date", DataType.DateTime)
        col4.Nullable = False
        tb.Columns.Add(col4)
        'Create the table on the instance of SQL Server.
        tb.Create()
        'Add another column.
        Dim col5 As Column
        col5 = New Column(tb, "ExpiryDate", DataType.DateTime)
        col5.Nullable = False
        tb.Columns.Add(col5)
        'Run the Alter method to make the change on the instance of SQL Server.
        tb.Alter()
        'Remove the table from the database.

        tb.Drop()
        ' </snippetSMO_VBTable1>

    End Sub

End Module
