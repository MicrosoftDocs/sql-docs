Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports System.Collections.Specialized
Module SMO_VBScripting1

    Sub Main()
        ' <snippetSMO_VBScripting1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a Scripter object and set the required scripting options.
        Dim scrp As Scripter
        scrp = New Scripter(srv)
        scrp.Options.ScriptDrops = False
        scrp.Options.WithDependencies = True
        'Iterate through the tables in database and script each one. Display the script.
        'Note that the StringCollection type needs the System.Collections.Specialized namespace to be included.
        Dim tb As Table
        Dim smoObjects(1) As Urn
        For Each tb In db.Tables
            smoObjects = New Urn(0) {}
            smoObjects(0) = tb.Urn
            If tb.IsSystemObject = False Then
                Dim sc As StringCollection
                sc = scrp.Script(smoObjects)
                Dim st As String
                For Each st In sc
                    Console.WriteLine(st)
                Next
            End If
        Next
        ' </snippetSMO_VBScripting1>


    End Sub

End Module
