Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBFileGroups1

    Sub Main()
        ' <snippetSMO_VBFileGroups1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a FileGroup object called SECONDARY on the database.
        Dim fg1 As FileGroup
        fg1 = New FileGroup(db, "SECONDARY")
        'Call the Create method to create the file group on the instance of SQL Server.
        fg1.Create()
        'Define a DataFile object on the file group and set the FileName property.
        Dim df1 As DataFile
        df1 = New DataFile(fg1, "datafile1")
        df1.FileName = "c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\datafile2.ndf"
        'Call the Create method to create the data file on the instance of SQL Server.
        df1.Create()
        ' </snippetSMO_VBFileGroups1>
        df1.Drop()
        fg1.Drop()

    End Sub

End Module
