---
title: "Backing Up and Restoring Databases and Transaction Logs | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "restoring databases [SMO]"
  - "transaction log backups [SMO]"
  - "backing up transaction logs [SMO]"
  - "database backups [SMO]"
  - "restoring transaction logs [SMO]"
  - "transaction log restores [SMO]"
  - "backing up databases [SMO]"
  - "database restores [SMO]"
ms.assetid: 1d7bd180-fd6c-4b38-a87b-351496040542
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Backing Up and Restoring Databases and Transaction Logs
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  In SMO, the <xref:Microsoft.SqlServer.Management.Smo.Backup> class and the <xref:Microsoft.SqlServer.Management.Smo.Restore> class are utility classes that provide the tools to accomplish the specific tasks of backing up and restoring. A <xref:Microsoft.SqlServer.Management.Smo.Backup> object represents a specific backup task that is required instead of a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] object on the server instance.  
  
 If data loss or corruption occurs, the backup must be restored, either fully or partially. Partial restoration uses the <xref:Microsoft.SqlServer.Management.Smo.FileGroupCollection> collection to segment the data to be restored. If the backup is of a transaction log, the data can be restored up to a particular point in time by using the <xref:Microsoft.SqlServer.Management.Smo.Restore.ToPointInTime%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Restore> object. The data can also be validated by using the <xref:Microsoft.SqlServer.Management.Smo.Restore.SqlVerify%2A> method. The recommended backup procedure is to check the integrity of the backup by doing a restore operation and checking the data in the database on a regular basis.  
  
 Like the <xref:Microsoft.SqlServer.Management.Smo.Backup> object, the <xref:Microsoft.SqlServer.Management.Smo.Restore> object does not need to be created by using a **Create** method because it does not represent any object on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The <xref:Microsoft.SqlServer.Management.Smo.Restore> object is a set of properties and methods used to restore a database.  
  
## Examples  
 To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Backing Up Databases and Transaction Logs in Visual Basic  
 This code example shows how to back up an existing database to a file, and then how to restore it.  
  
```  
Imports Microsoft.SqlServer.Management.Common  
Imports Microsoft.SqlServer.Management.Smo  
Imports Microsoft.VisualBasic.MyServices  
  
Module SMO_VBBackup3  
  
    Sub Main()  
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
        bk.BackupSetName = "AdventureWorks 2012 Backup"  
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
        My.Computer.FileSystem.DeleteFile("C:\Program Files\Microsoft SQL Server\MSSQL.12\MSSQL\Backup\Test_Full_Backup1")  
        My.Computer.FileSystem.DeleteFile("C:\Program Files\Microsoft SQL Server\MSSQL.12\MSSQL\Backup\Test_Differential_Backup1")  
    End Sub  
End Module  
```  
  
## Backing Up Databases and Transaction Logs in Visual C#  
 This code example shows how to back up an existing database to a file, and then how to restore it.  
  
```  
using Microsoft.SqlServer.Management.Common;  
using Microsoft.SqlServer.Management.Smo;  
  
class A {  
   public static void Main() {  
      // Connect to the local, default instance of SQL Server.   
      Server srv = new Server();  
      // Reference the AdventureWorks2012 database.   
      Database db = default(Database);  
      db = srv.Databases["AdventureWorks2012"];  
  
      // Store the current recovery model in a variable.   
      int recoverymod;  
      recoverymod = (int)db.DatabaseOptions.RecoveryModel;  
  
      // Define a Backup object variable.   
      Backup bk = new Backup();  
  
      // Specify the type of backup, the description, the name, and the database to be backed up.   
      bk.Action = BackupActionType.Database;  
      bk.BackupSetDescription = "Full backup of Adventureworks2012";  
      bk.BackupSetName = "AdventureWorks2012 Backup";  
      bk.Database = "AdventureWorks2012";  
  
      // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.   
      BackupDeviceItem bdi = default(BackupDeviceItem);  
      bdi = new BackupDeviceItem("Test_Full_Backup1", DeviceType.File);  
  
      // Add the device to the Backup object.   
      bk.Devices.Add(bdi);  
      // Set the Incremental property to False to specify that this is a full database backup.   
      bk.Incremental = false;  
  
      // Set the expiration date.   
      System.DateTime backupdate = new System.DateTime();  
      backupdate = new System.DateTime(2006, 10, 5);  
      bk.ExpirationDate = backupdate;  
  
      // Specify that the log must be truncated after the backup is complete.   
      bk.LogTruncation = BackupTruncateLogType.Truncate;  
  
      // Run SqlBackup to perform the full database backup on the instance of SQL Server.   
      bk.SqlBackup(srv);  
  
      // Inform the user that the backup has been completed.   
      System.Console.WriteLine("Full Backup complete.");  
  
      // Remove the backup device from the Backup object.   
      bk.Devices.Remove(bdi);  
  
      // Make a change to the database, in this case, add a table called test_table.   
      Table t = default(Table);  
      t = new Table(db, "test_table");  
      Column c = default(Column);  
      c = new Column(t, "col", DataType.Int);  
      t.Columns.Add(c);  
      t.Create();  
  
      // Create another file device for the differential backup and add the Backup object.   
      BackupDeviceItem bdid = default(BackupDeviceItem);  
      bdid = new BackupDeviceItem("Test_Differential_Backup1", DeviceType.File);  
  
      // Add the device to the Backup object.   
      bk.Devices.Add(bdid);  
  
      // Set the Incremental property to True for a differential backup.   
      bk.Incremental = true;  
  
      // Run SqlBackup to perform the incremental database backup on the instance of SQL Server.   
      bk.SqlBackup(srv);  
  
      // Inform the user that the differential backup is complete.   
      System.Console.WriteLine("Differential Backup complete.");  
  
      // Remove the device from the Backup object.   
      bk.Devices.Remove(bdid);  
  
      // Delete the AdventureWorks2012 database before restoring it  
      // db.Drop();  
  
      // Define a Restore object variable.  
      Restore rs = new Restore();  
  
      // Set the NoRecovery property to true, so the transactions are not recovered.   
      rs.NoRecovery = true;  
  
      // Add the device that contains the full database backup to the Restore object.   
      rs.Devices.Add(bdi);  
  
      // Specify the database name.   
      rs.Database = "AdventureWorks2012";  
  
      // Restore the full database backup with no recovery.   
      rs.SqlRestore(srv);  
  
      // Inform the user that the Full Database Restore is complete.   
      Console.WriteLine("Full Database Restore complete.");  
  
      // reacquire a reference to the database  
      db = srv.Databases["AdventureWorks2012"];  
  
      // Remove the device from the Restore object.  
      rs.Devices.Remove(bdi);  
  
      // Set the NoRecovery property to False.   
      rs.NoRecovery = false;  
  
      // Add the device that contains the differential backup to the Restore object.   
      rs.Devices.Add(bdid);  
  
      // Restore the differential database backup with recovery.   
      rs.SqlRestore(srv);  
  
      // Inform the user that the differential database restore is complete.   
      System.Console.WriteLine("Differential Database Restore complete.");  
  
      // Remove the device.   
      rs.Devices.Remove(bdid);  
  
      // Set the database recovery mode back to its original value.  
      db.RecoveryModel = (RecoveryModel)recoverymod;  
  
      // Drop the table that was added.   
      db.Tables["test_table"].Drop();  
      db.Alter();  
  
      // Remove the backup files from the hard disk.  
      // This location is dependent on the installation of SQL Server  
      System.IO.File.Delete("C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\Test_Full_Backup1");  
      System.IO.File.Delete("C:\\Program Files\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\Test_Differential_Backup1");  
   }  
}  
```  
  
## Backing Up Databases and Transaction Logs in PowerShell  
 This code example shows how to back up an existing database to a file, and then how to restore it.  
  
```  
#Backing up and restoring a Database from PowerShell  
  
#Connect to the local, default instance of SQL Server.  
  
#Get a server object which corresponds to the default instance  
$srv = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Server  
  
#Reference the AdventureWorks database.  
$db = $srv.Databases["AdventureWorks"]  
  
#Store the current recovery model in a variable.  
$recoverymod = $db.DatabaseOptions.RecoveryModel  
  
#Create a Backup object  
$bk = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Backup  
  
#set to backup the database  
$bk.Action = [Microsoft.SqlServer.Management.SMO.BackupActionType]::Database  
  
#Set back up properties  
$bk.BackupSetDescription = "Full backup of AdventureWorks"  
$bk.BackupSetName = "AdventureWorks Backup"  
$bk.Database = "AdventureWorks"  
  
#Declare a BackupDeviceItem by supplying the backup device file name in the constructor,   
#and the type of device is a file.  
$dt = [Microsoft.SqlServer.Management.SMO.DeviceType]::File  
$bdi = New-Object -TypeName Microsoft.SqlServer.Management.SMO.BackupDeviceItem `  
-argumentlist "Test_FullBackup1", $dt  
  
#Add the device to the Backup object.  
$bk.Devices.Add($bdi)  
  
#Set the Incremental property to False to specify that this is a full database backup.  
$bk.Incremental = $false  
  
#Set the expiration date.  
$bk.ExpirationDate = get-date "10/05/2006"  
  
#Specify that the log must be truncated after the backup is complete.  
$bk.LogTruncation = [Microsoft.SqlServer.Management.SMO.BackupTruncateLogType]::Truncate  
  
#Run SqlBackup to perform the full database backup on the instance of SQL Server.  
$bk.SqlBackup($srv)  
  
#Inform the user that the backup has been completed.  
"Full Backup complete."  
  
#Remove the backup device from the Backup object.  
$bk.Devices.Remove($bdi)  
  
#Make a change to the database, in this case, add a table called test_table.  
$t = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Table -argumentlist $db, "test_table"  
$type = [Microsoft.SqlServer.Management.SMO.DataType]::int  
$c = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Column -argumentlist $t, "col", $type       
$t.Columns.Add($c)  
$t.Create()  
  
#Create another file device for the differential backup and add the Backup object.  
# $dt is file backup device  
$bdid = New-Object -TypeName Microsoft.SqlServer.Management.SMO.BackupDeviceItem `  
-argumentlist "Test_DifferentialBackup1", $dt  
#Add this device to the backup set  
$bk.Devices.Add($bdid)  
  
#Set the Incremental property to True for a differential backup.  
$bk.Incremental = $true  
  
#Run SqlBackup to perform the incremental database backup on the instance of SQL Server.  
$bk.SqlBackup($srv)  
  
#Inform the user that the differential backup is complete.  
"Differential Backup complete."  
  
#Remove the device from the Backup object.  
$bk.Devices.Remove($bdid)  
  
#Delete the AdventureWorks database before restoring it.  
$db.Drop()  
  
#Define a Restore object variable.  
$rs = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Restore  
  
#Set the NoRecovery property to true, so the transactions are not recovered.  
$rs.NoRecovery = $true  
  
#Add the device that contains the full database backup to the Restore object.  
$rs.Devices.Add($bdi)  
  
#Specify the database name.  
$rs.Database = "AdventureWorks"  
#Restore the full database backup with no recovery.  
$rs.SqlRestore($srv)  
  
#Inform the user that the Full Database Restore is complete.  
"Full Database Restore complete."  
  
#Remove the device from the Restore object.  
$rs.Devices.Remove($bdi)  
  
#Set the NoRecovery property to False.  
$rs.NoRecovery = $false  
  
#Add the device that contains the differential backup to the Restore object.  
$rs.Devices.Add($bdid)  
  
#Restore the differential database backup with recovery.  
$rs.SqlRestore($srv)  
  
#Inform the user that the differential database restore is complete.  
"Differential Database Restore complete."  
  
#Remove the device.  
$rs.Devices.Remove($bdid)  
  
#Set the database recovery mode back to its original value.  
$db = $srv.Databases["AdventureWorks"]  
$db.DatabaseOptions.RecoveryModel = $recoverymod  
  
#Drop the table that was added.  
$db.Tables["test_table"].Drop()  
$db.Alter()  
  
#Delete the backup files - the exact location depends on your installation  
del "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\Test_FullBackup1"  
del "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\Test_DifferentialBackup1"  
```  
  
## Running Database Integrity Checks in Visual Basic  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides data integrity checking. This code example runs a database consistency type check on the specified database. In this example, <xref:Microsoft.SqlServer.Management.Smo.Database.CheckTables%2A> is used, but <xref:Microsoft.SqlServer.Management.Smo.Database.CheckAllocations%2A>, <xref:Microsoft.SqlServer.Management.Smo.Database.CheckCatalog%2A>, or <xref:Microsoft.SqlServer.Management.Smo.Database.CheckIdentityValues%2A> can be used similarly.  
  
> [!NOTE]  
>  The <xref:System.Collections.Specialized.StringCollection> object requires a reference to the namespace using the `imports System.Collections.Specialized` statement.  
  
```  
Imports Microsoft.SqlServer.Management.Smo  
Imports Microsoft.SqlServer.Management.Common  
Imports System.Collections.Specialized  
Module S  
   Sub Main()  
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
   End Sub  
End Module  
```  
  
## Running Database Integrity Checks in Visual C#  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides data integrity checking. This code example runs a database consistency type check on the specified database. In this example, <xref:Microsoft.SqlServer.Management.Smo.Database.CheckTables%2A> is used, but <xref:Microsoft.SqlServer.Management.Smo.Database.CheckAllocations%2A>, <xref:Microsoft.SqlServer.Management.Smo.Database.CheckCatalog%2A>, or <xref:Microsoft.SqlServer.Management.Smo.Database.CheckIdentityValues%2A> can be used similarly.  
  
> [!NOTE]  
>  The <xref:System.Collections.Specialized.StringCollection> object requires a reference to the namespace using the `imports System.Collections.Specialized` statement.  
  
```  
using Microsoft.SqlServer.Management.Common;  
using Microsoft.SqlServer.Management.Smo;  
using System;  
  
class A {  
   public static void Main() {  
      // Connect to the local, default instance of SQL Server.   
      Server srv = new Server();  
  
      // Reference the AdventureWorks2012 database.             
      Database db = srv.Databases["AdventureWorks2012"];  
  
      // Note, to use the StringCollection type the System.Collections.Specialized system namespace must be included in the imports statements.   
      System.Collections.Specialized.StringCollection sc;  
  
      // Run the CheckTables method and display the results from the returned StringCollection variable.   
      sc = db.CheckTables(RepairType.None);  
  
      foreach (string c in sc) {  
         Console.WriteLine(c);  
      }  
   }  
}  
```  
  
## Running Database Integrity Checks in PowerShell  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] provides data integrity checking. This code example runs a database consistency type check on the specified database. In this example, <xref:Microsoft.SqlServer.Management.Smo.Database.CheckTables%2A> is used, but <xref:Microsoft.SqlServer.Management.Smo.Database.CheckAllocations%2A>, <xref:Microsoft.SqlServer.Management.Smo.Database.CheckCatalog%2A>, or <xref:Microsoft.SqlServer.Management.Smo.Database.CheckIdentityValues%2A> can be used similarly.  
  
> [!NOTE]  
>  The <xref:System.Collections.Specialized.StringCollection> object requires a reference to the namespace using the `imports System.Collections.Specialized` statement.  
  
```  
# Set the path context to the local, default instance of SQL Server and get a reference to AdventureWorks2012  
CD \sql\localhost\default\databases  
$db = get-item Adventureworks2012  
  
$sc = $db.CheckTables([Microsoft.SqlServer.Management.SMO.RepairType]::None)  
foreach ($c in $sc)  
{  
    $c  
 }  
```  
  
  
