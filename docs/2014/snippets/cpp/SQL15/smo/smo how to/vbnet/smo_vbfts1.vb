Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBFTS1



    Sub Main()
        ' <snippetSMO_VBFTS1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Reference the ProductCategory table.
        Dim tb As Table
        tb = db.Tables("ProductCategory", "Production")
        'Define a FullTextCatalog object variable by specifying the parent database and name arguments in the constructor.
        Dim ftc As FullTextCatalog
        ftc = New FullTextCatalog(db, "Test_Catalog")
        ftc.IsDefault = True
        'Create the Full Text Search catalog on the instance of SQL Server.
        ftc.Create()
        'Define a FullTextIndex object varaible by supplying the parent table argument in the constructor.
        Dim fti As FullTextIndex
        fti = New FullTextIndex(tb)
        'Define a FullTextIndexColumn object variable by supplying the parent index and column name arguments in the constructor.
        Dim ftic As FullTextIndexColumn
        ftic = New FullTextIndexColumn(fti, "Name")
        'Add the indexed column to the index.
        fti.IndexedColumns.Add(ftic)
        fti.ChangeTracking = ChangeTracking.Automatic
        'Specify the unique index on the table that is required by the Full Text Search index.
        fti.UniqueIndexName = "AK_ProductCategory_Name"
        'Specify the catalog associated with the index.
        fti.CatalogName = "Test_Catalog"
        'Create the Full Text Search index on the instance of SQL Server.
        fti.Create()
        ' </snippetSMO_VBFTS1>
        fti.Drop()
        ftc.Drop()

    End Sub

End Module
