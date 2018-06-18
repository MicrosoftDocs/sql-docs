Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBForeignKeys1

    Sub Main()
        ' <snippetSMO_VBForeignKeys1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Declare a Table object variable and reference the Employee table.
        Dim tbe As Table
        tbe = db.Tables("Employee", "HumanResources")
        'Declare another Table object variable and reference the EmployeeDepartmentHistory table.
        Dim tbea As Table
        tbea = db.Tables("EmployeeDepartmentHistory", "HumanResources")
        'Define a Foreign Key object variable by supplying the EmployeeDepartmentHistory as the parent table and the foreign key name in the constructor.
        Dim fk As ForeignKey
        fk = New ForeignKey(tbea, "test_foreignkey")
        'Add BusinessEntityID as the foreign key column.
        Dim fkc As ForeignKeyColumn
        fkc = New ForeignKeyColumn(fk, "BusinessEntityID", "BusinessEntityID")
        fk.Columns.Add(fkc)
        'Set the referenced table and schema.
        fk.ReferencedTable = "Employee"
        fk.ReferencedTableSchema = "HumanResources"
        'Create the foreign key on the instance of SQL Server.
        fk.Create()
        ' </snippetSMO_VBForeignKeys1>
        fk.Drop()
    End Sub

End Module
