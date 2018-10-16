Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBFileGroups3

    Sub Main()
        ' <snippetSMO_VBFileGroups3>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a LogFile object and set the database, name, and file name properties in the constructor.
        Dim lf1 As LogFile
        lf1 = New LogFile(db, "logfile1", "c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\logfile1.ldf")
        'Set the file growth to 6%.
        lf1.GrowthType = FileGrowthType.Percent
        lf1.Growth = 6
        'Run the Create method to create the log file on the instance of SQL Server.
        lf1.Create()
        'Alter the growth percentage.
        lf1.Growth = 7
        lf1.Alter()
        'Remove the log file.
        lf1.Drop()
        ' </snippetSMO_VBFileGroups3>
    End Sub

End Module
