Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBStoredProcs1

    Sub Main()
        ' <snippetSMO_VBStoredProcs1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a StoredProcedure object variable by supplying the parent database and name arguments in the constructor.
        Dim sp As StoredProcedure
        sp = New StoredProcedure(db, "GetLastNameByEmployeeID")
        'Set the TextMode property to false and then set the other object properties.
        sp.TextMode = False
        sp.AnsiNullsStatus = False
        sp.QuotedIdentifierStatus = False
        'Add two parameters.
        Dim param As StoredProcedureParameter
        param = New StoredProcedureParameter(sp, "@empval", DataType.Int)
        sp.Parameters.Add(param)
        Dim param2 As StoredProcedureParameter
        param2 = New StoredProcedureParameter(sp, "@retval", DataType.NVarChar(50))
        param2.IsOutputParameter = True
        sp.Parameters.Add(param2)
        'Set the TextBody property to define the stored procedure.
        Dim stmt As String
        stmt = " SELECT @retval = (SELECT LastName FROM Person.Person AS p JOIN HumanResources.Employee AS e ON p.BusinessEntityID = e.BusinessEntityID AND e.BusinessEntityID = @empval )"
        sp.TextBody = stmt
        'Create the stored procedure on the instance of SQL Server.
        sp.Create()
        'Modify a property and run the Alter method to make the change on the instance of SQL Server.   
        sp.QuotedIdentifierStatus = True
        sp.Alter()
        'Remove the stored procedure.
        sp.Drop()
        ' </snippetSMO_VBStoredProcs1>
    End Sub

End Module
