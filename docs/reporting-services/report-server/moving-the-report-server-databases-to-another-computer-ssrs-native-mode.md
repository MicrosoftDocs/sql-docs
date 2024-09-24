---
title: Move report server databases to another computer (native mode)
description: Find out how to move report server databases to a different SQL Server instance. See how to attach and detach the databases or use backup and restore actions.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/24/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a SQL Server user, I want to see how to move the report server databases so that I can use them with a different instance of SQL Server.
---

# Move report server databases to another computer (SSRS native mode)

[!INCLUDE[applies](../../includes/applies-md.md)]  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode. 

You can move the report server databases that you use in an installation of [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] to an instance that's on a different computer.

[!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] provides several approaches for moving databases:

- Attach and detach. This approach offers the easiest way to move the report server databases. But you need to take the report server offline while the databases are detached.
- Backup and restore. This approach minimizes service disruptions, but you must run [!INCLUDE[Transact-SQL](../../includes/tsql-md.md)] commands to perform the operations.
- Copy. Copying the database isn't recommended if you use the Copy Database Wizard. It doesn't preserve permission settings in the database.

This article shows you how to use the attach and detach approach and the backup and restore approach.
  
## Prerequisites

- A configured native mode report server that's used in an installation of SQL Server.
- An instance of SQL Server on a different computer.

## Prepare to move the databases

Keep the following points in mind when you move report server databases:

- You must move or copy the **reportserver** and **reportservertempdb** databases together. An SSRS installation requires both databases.
- The name of the temporary database must be the same as the name of the primary report server database but with a **tempdb** suffix.
- Moving a database doesn't change scheduled operations that are currently defined for report server items.  
  - Schedules are re-created the first time that you restart the report server service.  
  - [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] agent jobs that are used to trigger a schedule are re-created on the new database instance. You don't have to move the jobs to the new computer, but you might want to delete jobs on the computer that are no longer used.  
  - Subscriptions, cached reports, and snapshots are preserved in the moved database. If a snapshot doesn't pick up refreshed data after you move the database, clear the snapshot options. Then select **Apply** to save your changes, re-create the schedule, and select **Apply** again to save your changes.
  - Temporary report and user session data that's stored in the temporary database is persisted when you move that database.  

> [!IMPORTANT]  
> The steps in this article are recommended when relocation of the report server database is the only change that you make to the existing installation. Migrating an entire SSRS installation requires connection reconfiguration and an encryption key reset. For example, when you move the database and change the identity of the report server Windows service that uses the database, you need to reconfigure the connection and reset the encryption keys.
  
## Detach and attach the report server databases

If you can take the report server offline, you can detach the databases to move them to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to use. This approach preserves permissions in the databases. If you're using a SQL Server database, you must move it to another SQL Server instance. After you move the databases, you must reconfigure the report server connection to the report server database. If you're running a scale-out deployment, you must reconfigure the report server database connection for each report server in the deployment.  
  
Use the following steps to move the databases:  
  
1. Back up the encryption keys for the report server database you want to move. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to back up the keys.  
  
1. Stop the Report Server service. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to stop the service.  
  
1. Start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and open a connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server databases.  
  
1. Right-click the report server database, point to **Tasks**, and select **Detach**. Repeat this step for the report server temporary database.  
  
1. Copy or move the .mdf and .ldf files to the **Data** folder of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to use. Because you're moving two databases, make sure that you move or copy all four files.  
  
1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a connection to the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server databases.  
  
1. Right-click the Databases node, and then select **Attach**.  
  
1. Select **Add** to select the report server database .mdf and .ldf files that you want to attach. Repeat this step for the report server temporary database.  
  
1. After the databases are attached, verify that the **RSExecRole** is a database role in the report server database and temporary database. **RSExecRole** must have select, insert, update, delete, and reference permissions on the report server database tables, and execute permissions on the stored procedures. For more information, see [Create the RSExecRole](../../reporting-services/security/create-the-rsexecrole.md).  
  
1. Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and open a connection to the report server.  
  
1. On the Database page, select the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, and then select **Connect**.  
  
1. Choose the report server database that you just moved, and then select **Apply**.  
  
1. On the **Encryption Keys** page, select **Restore**. Specify the file that contains the backup copy of the keys and the password to unlock the file.  
  
1. Restart the Report Server service.  
  
## Back up and restore the report server databases

If you can't take the report server offline, you can use backup and restore to relocate the report server databases. You must use [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to do the backup and restore. After you restore the databases, you must configure the report server to use the database on the new server instance. For more information, see the instructions at the end of this article.  
  
### Use BACKUP and COPY_ONLY to back up the report server databases

When backing up the databases, set the `COPY_ONLY` argument. Be sure to back up both of the databases and log files.  
  
```tsql
-- To permit log backups, before the full database backup, alter the database   
-- to use the full recovery model.  
USE master;  
GO  
ALTER DATABASE ReportServer  
   SET RECOVERY FULL  

-- First back up the database without using the COPY_ONLY argument.
-- This step is needed after you change the recovery model to full.
-- It prevents a 4214 error from occurring.
USE master;  
GO
BACKUP DATABASE ReportServer
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP\ReportServerExtraBackup.bak'
GO

-- If the ReportServerData device does not exist yet, create it.   
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerData',   
'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP\ReportServerData.bak'  
  
-- Create a logical backup device, ReportServerLog.  
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerLog',   
'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP\ReportServerLog.bak'  
  
-- Back up the full ReportServer database.  
BACKUP DATABASE ReportServer  
   TO ReportServerData  
   WITH COPY_ONLY  
  
-- Back up the ReportServer log.  
BACKUP LOG ReportServer  
   TO ReportServerLog  
   WITH COPY_ONLY  
  
-- To permit log backups, before the full database backup, alter the database   
-- to use the full recovery model.  
USE master;  
GO  
ALTER DATABASE ReportServerTempdb  
   SET RECOVERY FULL  

-- First back up the database without using the COPY_ONLY argument.
-- This step is needed after you change the recovery model to full.
-- It prevents a 4214 error from occurring.
USE master;  
GO
BACKUP DATABASE ReportServerTempdb
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP\ReportServerTempdbExtraBackup.bak'
GO

-- If the ReportServerTempDBData device does not exist yet, create it.   
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerTempDBData',   
'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP\ReportServerTempDBData.bak'  
  
-- Create a logical backup device, ReportServerTempDBLog.  
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerTempDBLog',   
'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP\ReportServerTempDBLog.bak'  
  
-- Back up the full ReportServerTempDB database.  
BACKUP DATABASE ReportServerTempDB  
   TO ReportServerTempDBData  
   WITH COPY_ONLY  
  
-- Back up the ReportServerTempDB log.  
BACKUP LOG ReportServerTempDB  
   TO ReportServerTempDBLog  
   WITH COPY_ONLY  
```  
  
### Use RESTORE and MOVE to relocate the report server databases

When restoring the databases, be sure to include the MOVE argument so that you can specify a path. Use the `NORECOVERY` argument to perform the initial restore. This argument keeps the database in a `RESTORING` state, giving you time to review log backups to determine which one to restore. The final step repeats the `RESTORE` operation with the `RECOVERY` argument.  
  
The `MOVE` argument uses the logical name of the data file. To find the logical name, execute the following statement:

```tsql
RESTORE FILELISTONLY FROM DISK='C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\ReportServerData.bak';
```

find-logical-name-query.png

The following examples include the `FILE` argument so that you can specify the file position of the log file to restore. To find the file position, execute the following statement:

```tsql
RESTORE HEADERONLY FROM DISK='C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\ReportServerData.bak';
```  
  
find-file-position-query.png


When restoring the database and log files, you should run each `RESTORE` operation separately.  
  
```
-- Restore the report server database and move to new instance folder   
RESTORE DATABASE ReportServer  
   FROM DISK='C:\ReportServerData.bak'  
   WITH NORECOVERY,   
      MOVE 'ReportServer' TO   
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\ReportServer.mdf',   
      MOVE 'ReportServer_log' TO  
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\ReportServer_Log.ldf';  
GO  
  
-- Restore the report server log file to new instance folder   
RESTORE LOG ReportServer  
   FROM DISK='C:\ReportServerData.bak'  
   WITH NORECOVERY, FILE=2  
      MOVE 'ReportServer' TO   
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\ReportServer.mdf',   
      MOVE 'ReportServer_log' TO  
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\ReportServer_Log.ldf';  
GO  
  
-- Restore and move the report server temporary database  
RESTORE DATABASE ReportServerTempdb  
   FROM DISK='C:\ReportServerTempDBData.bak'  
   WITH NORECOVERY,   
      MOVE 'ReportServerTempDB' TO   
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\ReportServerTempDB.mdf',   
      MOVE 'ReportServerTempDB_log' TO  
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\REportServerTempDB_Log.ldf';  
GO  
  
-- Restore the temporary database log file to new instance folder   
RESTORE LOG ReportServerTempdb  
   FROM DISK='C:\ReportServerTempDBData.bak'  
   WITH NORECOVERY, FILE=2  
      MOVE 'ReportServerTempDB' TO   
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\ReportServerTempDB.mdf',   
      MOVE 'ReportServerTempDB_log' TO  
         'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\Data\REportServerTempDB_Log.ldf';  
GO  
  
-- Perform final restore  
RESTORE DATABASE ReportServer  
   WITH RECOVERY  
GO  
  
-- Perform final restore  
RESTORE DATABASE ReportServerTempDB  
   WITH RECOVERY  
GO  
```  
  
### How to configure the report server database connection  
  
1. Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and open a connection to the report server.  
  
1. On the **Database** page, select **Change Database**. Choose **Next**.  
  
1. Select **Choose an existing report server database**. Choose **Next**.  
  
1. Select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that now hosts the report server database and choose **Test Connection**. Select **Next**.  
  
1. In **Database Name**, select the report server database that you want to use. Select **Next**.  
  
1. In **Credentials**, specify the credentials that the report server uses to connect to the report server database. Select **Next**.  
  
1. Select **Next**, and then choose **Finish**.  
  
> [!NOTE]  
> A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation requires that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance include the **RSExecRole** role. Role creation, login registration, and role assignments occur when you set the report server database connection through the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. If you use alternate approaches (specifically, if you use the rsconfig.exe command prompt utility) to configure the connection, the report server will not be in a working state. You might have to write WMI code to make the report server available. For more information, see [Access the Reporting Services WMI Provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).  

## Related content

[Create the RSExecRole](../../reporting-services/security/create-the-rsexecrole.md)   
[Start and stop the report server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)   
[Configure a report server database connection](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
[Configure the unattended execution account](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)   
[Report Server Configuration Manager](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
[rsconfig utility](../../reporting-services/tools/rsconfig-utility-ssrs.md)   
[Configure and manage encryption keys](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)   
[Report server database](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
