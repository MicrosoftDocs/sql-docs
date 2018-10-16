Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.VisualBasic.MyServices


Module SMO_VBBackup3







    Sub Main()
        ' <snippetSMO_VBBackup3>

        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Store the current recovery model in a variable.
        Dim recoverymod As Integer
        recoverymod = db.DatabaseOptions.RecoveryModel
        'Define a Backup object variable. 
        Dim bk As New Backup
        'Specify the type of backup, the description, the name, and the database to be backed up.
        bk.Action = BackupActionType.Database
        bk.BackupSetDescription = "Full backup of AdventureWorks2012"
        bk.BackupSetName = "AdventureWorks2012 2008R2 Backup"
        bk.Database = "AdventureWorks2012"
        'Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.
        Dim bdi As BackupDeviceItem
        bdi = New BackupDeviceItem("Test_Full_Backup1", DeviceType.File)
        'Add the device to the Backup object.
        bk.Devices.Add(bdi)
        'Set the Incremental property to False to specify that this is a full database backup.
        bk.Incremental = False
        'Set the expiration date.
        Dim backupdate As New Date
        backupdate = New Date(2006, 10, 5)
        bk.ExpirationDate = backupdate
        'Specify that the log must be truncated after the backup is complete.
        bk.LogTruncation = BackupTruncateLogType.Truncate
        'Run SqlBackup to perform the full database backup on the instance of SQL Server.
        bk.SqlBackup(srv)
        'Inform the user that the backup has been completed.
        Console.WriteLine("Full Backup complete.")
        'Remove the backup device from the Backup object.
        bk.Devices.Remove(bdi)
        'Make a change to the database, in this case, add a table called test_table.
        Dim t As Table
        t = New Table(db, "test_table")
        Dim c As Column
        c = New Column(t, "col", DataType.Int)
        t.Columns.Add(c)
        t.Create()
        'Create another file device for the differential backup and add the Backup object.
        Dim bdid As BackupDeviceItem
        bdid = New BackupDeviceItem("Test_Differential_Backup1", DeviceType.File)
        'Add the device to the Backup object.
        bk.Devices.Add(bdid)
        'Set the Incremental property to True for a differential backup.
        bk.Incremental = True
        'Run SqlBackup to perform the incremental database backup on the instance of SQL Server.
        bk.SqlBackup(srv)
        'Inform the user that the differential backup is complete.
        Console.WriteLine("Differential Backup complete.")
        'Remove the device from the Backup object.
        bk.Devices.Remove(bdid)
        'Delete the AdventureWorks2012 database before restoring it.
        srv.Databases("AdventureWorks2012").Drop()
        'Define a Restore object variable.
        Dim rs As Restore
        rs = New Restore
        'Set the NoRecovery property to true, so the transactions are not recovered.
        rs.NoRecovery = True
        'Add the device that contains the full database backup to the Restore object.
        rs.Devices.Add(bdi)
        'Specify the database name.
        rs.Database = "AdventureWorks2012"
        'Restore the full database backup with no recovery.
        rs.SqlRestore(srv)
        'Inform the user that the Full Database Restore is complete.
        Console.WriteLine("Full Database Restore complete.")
        'Remove the device from the Restore object.
        rs.Devices.Remove(bdi)
        'Set te NoRecovery property to False.
        rs.NoRecovery = False
        'Add the device that contains the differential backup to the Restore object.
        rs.Devices.Add(bdid)
        'Restore the differential database backup with recovery.
        rs.SqlRestore(srv)
        'Inform the user that the differential database restore is complete.
        Console.WriteLine("Differential Database Restore complete.")
        'Remove the device.
        rs.Devices.Remove(bdid)
        'Set the database recovery mode back to its original value.
        srv.Databases("AdventureWorks2012").DatabaseOptions.RecoveryModel = recoverymod
        'Drop the table that was added.
        srv.Databases("AdventureWorks2012").Tables("test_table").Drop()
        srv.Databases("AdventureWorks2012").Alter()
        'Remove the backup files from the hard disk.
        My.Computer.FileSystem.DeleteFile("C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\Test_Full_Backup1")
        My.Computer.FileSystem.DeleteFile("C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Backup\Test_Differential_Backup1")

        ' </snippetSMO_VBBackup3>


    End Sub

End Module


