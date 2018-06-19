Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBSchemas1

    Sub Main()
        ' <snippetSMO_VBSchemas1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Schema object variable by supplying the parent database and name arguments in the constructor.
        Dim sch As Schema
        sch = New Schema(db, "MySchema1")
        sch.Owner = "dbo"
        'Create the schema on the instance of SQL Server.
        sch.Create()
        'Define an ObjectPermissionSet that contains the Update and Select object permissions.
        Dim obperset As ObjectPermissionSet
        obperset = New ObjectPermissionSet()
        obperset.Add(ObjectPermission.Select)
        obperset.Add(ObjectPermission.Update)
        'Grant the set of permissions on the schema to the guest account.
        sch.Grant(obperset, "guest")
        'Define a Table object variable by supplying the parent database, name and schema arguments in the constructor.
        Dim tb As Table
        tb = New Table(db, "MyTable", "MySchema1")
        Dim mycol As Column
        mycol = New Column(tb, "Date", DataType.DateTime)
        tb.Columns.Add(mycol)
        tb.Create()
        'Modify the owner of the schema and run the Alter method to make the change on the instance of SQL Server.
        sch.Owner = "guest"
        sch.Alter()
        'Run the Drop method for the table and the schema to remove them.
        tb.Drop()
        sch.Drop()
        ' </snippetSMO_VBSchemas1>
    End Sub

End Module
