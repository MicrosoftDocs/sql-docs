'<snippetColumn_VB_IsFileStream.VB> 
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SqlServer.Management.Smo

Module Module1

    Sub Main()
        'Connect to the local, default instance of SQL Server. 
        Dim svr As New Server()

        'Define a Database object instance by supplying the server 
        'and the database name arguments in the constructor. 
        Dim myDB As Database = svr.Databases("MyPhotoDatabase")

        'Connect to the MyPhotos table. 
        Dim tbl As Table = myDB.Tables("MyPhotos")

        Console.WriteLine("{0,-8} {1,-12}", "Column", "IsFileStream")

        For Each c As Column In tbl.Columns
            Console.WriteLine("{0,-8} {1,-12}", c.Name, c.IsFileStream)
        Next
    End Sub

End Module
'</snippetColumn_VB_IsFileStream.VB> 
