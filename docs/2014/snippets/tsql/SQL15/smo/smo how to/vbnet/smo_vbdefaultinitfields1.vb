Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports System.Collections.Specialized
Module SMO_VBDefaultInitFields1

    Sub Main()
        ' <snippetSMO_VBDefaultInitFields1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Assign the Table object type to a System.Type object variable.
        Dim tb As Table
        Dim typ As Type
        tb = New Table
        typ = tb.GetType
        'Assign the current default initialization fields for the Table object type to a 
        'StringCollection object variable.
        Dim sc As StringCollection
        sc = srv.GetDefaultInitFields(typ)
        'Set the default initialization fields for the Table object type to the CreateDate property.
        srv.SetDefaultInitFields(typ, "CreateDate")
        'Retrieve the Schema, Name, and CreateDate properties for every table in AdventureWorks2012.
        'Note that the improvement in performance can be viewed in SQL Profiler.
        For Each tb In db.Tables
            Console.WriteLine(tb.Schema + "." + tb.Name + " " + tb.CreateDate)
        Next
        'Set the default initialization fields for the Table object type back to the original settings.
        srv.SetDefaultInitFields(typ, sc)
        ' </snippetSMO_VBDefaultInitFields1>


    End Sub

End Module
