Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBProperties3

    Sub Main()
        ' <snippetSMO_VBProperties3>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Set properties on the uspGetEmployeeManagers stored procedure on the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        Dim sp As StoredProcedure
        sp = db.StoredProcedures("uspGetEmployeeManagers")
        sp.AnsiNullsStatus = False
        sp.QuotedIdentifierStatus = False
        'Iterate through the properties of the stored procedure and display.
        'Note the Property object requires [] parentheses to distinguish it from the Visual Basic key word.
        Dim p As [Property]
        For Each p In sp.Properties
            Console.WriteLine(p.Name & p.Value)
        Next
        ' </snippetSMO_VBProperties3>

    End Sub

End Module
