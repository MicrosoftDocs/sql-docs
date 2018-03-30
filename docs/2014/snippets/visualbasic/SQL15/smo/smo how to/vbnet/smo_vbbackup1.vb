Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBBackup1

    Sub Main()


        ' <snippetSMO_VBBackup1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define a Backup object variable. 
        Dim bk As New Backup
        'Specify the type of backup, the description, the name, and the database to be backed up.
        bk.Action = BackupActionType.Database
        bk.BackupSetDescription = "Full backup of AdventureWorks2012"
        bk.BackupSetName = "AdventureWorks2012 Backup"
        bk.Database = "AdventureWorks2012"
        'Declare a BackupDeviceItem by supplying the backup device name in the constructor.
        Dim bdi As BackupDeviceItem
        Dim bkf As String
        bkf = "Test_Backup_" + Now.Year.ToString + Now.Month.ToString + Now.Day.ToString + Now.Hour.ToString + Now.Minute.ToString
        bdi = New BackupDeviceItem(bkf, DeviceType.File)
        'Add the device to the backup.
        bk.Devices.Add(bdi)
        'Set further properties.
        bk.Checksum = True
        bk.ContinueAfterError = True
        bk.Incremental = False
        Dim mydate As New Date
        mydate = New Date(2006, 10, 5)
        bk.ExpirationDate = mydate
        bk.LogTruncation = BackupTruncateLogType.Truncate
        bk.MediaDescription = "file backup"
        bk.Initialize = True
        bk.PercentCompleteNotification = 30
        bk.Restart = True
        bk.RetainDays = 5
        'Run SqlBackup to perform the specified backup on the instance of SQL Server.
        bk.SqlBackup(srv)
        Console.WriteLine("Backup complete")
        ' </snippetSMO_VBBackup1>



    End Sub

End Module
