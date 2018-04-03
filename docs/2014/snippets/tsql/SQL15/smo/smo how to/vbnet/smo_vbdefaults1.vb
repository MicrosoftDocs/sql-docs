Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBDefaults1

    Sub Main()
        ' <snippetSMO_VBDefaults1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Default object variable by supplying the parent database and the default name 
        'in the constructor.
        Dim def As [Default]
        def = New [Default](db, "Test_Default2")
        'Set the TextHeader and TextBody properties that define the default.
        def.TextHeader = "CREATE DEFAULT [Test_Default2] AS"
        def.TextBody = "GetDate()"
        'Create the default on the instance of SQL Server.
        def.Create()
        'Declare a Column object variable and reference a column in the AdventureWorks2012 database.
        Dim col As Column
        col = db.Tables("SpecialOffer", "Sales").Columns("StartDate")
        'Bind the default to the column.
        def.BindToColumn("SpecialOffer", "StartDate", "Sales")
        'Unbind the default from the column and remove it from the database.
        def.UnbindFromColumn("SpecialOffer", "StartDate", "Sales")
        def.Drop()
        ' </snippetSMO_VBDefaults1>
    End Sub

End Module
