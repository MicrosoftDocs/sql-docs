Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBXMLIndex1

    Sub Main()
        ' <snippetSMO_VBXMLIndex1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server()
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Table object variable and add an XML type column. 
        Dim tb As Table
        tb = New Table(db, "XmlTable")
        Dim col1 As Column
        'This sample requires that an XML schema type called MySampleCollection exists on the database.
        col1 = New Column(tb, "XMLValue", DataType.Xml("MySampleCollection"))
        'Add another integer column that can be made into a unique, primary key.
        tb.Columns.Add(col1)
        Dim col2 As Column
        col2 = New Column(tb, "Number", DataType.Int)
        col2.Nullable = False
        tb.Columns.Add(col2)
        'Create the table of the instance of SQL Server.
        tb.Create()
        'Create a unique, clustered, primary key index on the integer column. This is required for an XML index.
        Dim cp As Index
        cp = New Index(tb, "clusprimindex")
        cp.IsClustered = True
        cp.IndexKeyType = IndexKeyType.DriPrimaryKey
        Dim cpcol As IndexedColumn
        cpcol = New IndexedColumn(cp, "Number", True)
        cp.IndexedColumns.Add(cpcol)
        cp.Create()
        'Define and XML Index object variable by supplying the parent table and the XML index name arguments in the constructor.
        Dim i As Index
        i = New Index(tb, "xmlindex")
        Dim ic As IndexedColumn
        ic = New IndexedColumn(i, "XMLValue", True)
        i.IndexedColumns.Add(ic)
        'Create the XML index on the instance of SQL Server. 
        i.Create()
        ' </snippetSMO_VBXMLIndex1>
        i.Drop()
        tb.Drop()


    End Sub

End Module
