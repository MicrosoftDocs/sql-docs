Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports system.collections.Specialized
Module SMO_VBBackup2

    Sub Main()
        ' <snippetSMO_VBBackup2>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Note, to use the StringCollection type the System.Collections.Specialized system namespace must be included in the imports statements.
        Dim sc As StringCollection
        'Run the CheckTables method and display the results from the returned StringCollection variable.
        sc = db.CheckTables(RepairType.None)
        Dim c As Integer
        For c = 0 To sc.Count - 1
            Console.WriteLine(sc.Item(c))
        Next
        ' </snippetSMO_VBBackup2>
    End Sub

End Module
