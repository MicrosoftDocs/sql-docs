'<snippetFileGroup_VB_ctor_and_IsFileStream> 
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
        Dim db As New Database(svr, "MyPhotos")

        'Create the database on the instance of SQL Server. 
        db.Create()

        'Define a FileGroup object instance for creation of FILESTREAM 
        'groups by supplying the database, group name, and supplying 
        'true for the isFileStream parameters. 

        Dim myFileGroup As New FileGroup(db, "MyPhotoGroup", True)

        If myFileGroup.IsFileStream = True Then
            Console.WriteLine("MyPhotoGroup is enabled for the creation of a FILESTREAM file group.")
        End If

        'Remove the database to clean up. 
        db.Drop()

    End Sub
End Module
'</snippetFileGroup_VB_ctor_and_IsFileStream> 
