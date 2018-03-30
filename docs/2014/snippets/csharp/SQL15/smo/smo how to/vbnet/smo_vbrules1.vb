Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBRules1

    Sub Main()
        ' <snippetSMO_VBRules1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Declare a Table object variable and reference the Product table.
        Dim tb As Table
        tb = db.Tables("Product", "Production")
        'Define a Rule object variable by supplying the parent database, name and schema in the constructor. 
        'Note that the full namespace must be given for the Rule type to differentiate it from other Rule types.
        Dim ru As Microsoft.SqlServer.Management.Smo.Rule
        ru = New Rule(db, "TestRule", "Production")
        'Set the TextHeader and TextBody properties to define the rule.
        ru.TextHeader = "CREATE RULE [Production].[TestRule] AS"
        ru.TextBody = "@value BETWEEN GETDATE() AND DATEADD(year,4,GETDATE())"
        'Create the rule on the instance of SQL Server.
        ru.Create()
        'Bind the rule to a column in the Product table by supplying the table, schema, and 
        'column as arguments in the BindToColumn method.
        ru.BindToColumn("Product", "SellEndDate", "Production")
        'Unbind from the column before removing the rule from the database.
        ru.UnbindFromColumn("Product", "SellEndDate", "Production")
        ru.Drop()
        ' </snippetSMO_VBRules1>
    End Sub

End Module
