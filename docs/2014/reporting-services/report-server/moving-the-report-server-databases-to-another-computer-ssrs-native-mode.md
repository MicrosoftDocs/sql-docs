---
title: "Moving the Report Server Databases to Another Computer (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 44a9854d-e333-44f6-bdc7-8837b9f34416
author: markingmyname
ms.author: maghan
manager: craigg
---
# Moving the Report Server Databases to Another Computer (SSRS Native Mode)
  You can move the report server databases that are used in an installation [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../../includes/ssde-md.md)] to an instance that is on a different computer. Both the reportserver and reportservertempdb databases must be moved or copied together. A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation requires both databases; the reportservertempdb database must be related by name to the primary reportserver database you are moving.  
  
 **[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 Moving a database does not effect scheduled operations that are currently defined for report server items.  
  
-   Schedules will be recreated the first time that you restart the Report Server service.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs that are used to trigger a schedule will be recreated on the new database instance. You do not have to move the jobs to the new computer, but you might want to delete jobs on the computer that will no longer be used.  
  
-   Subscriptions, cached reports, and snapshots are preserved in the moved database. If a snapshot is not picking up refreshed data after the database is moved, clear the snapshot options in Report Manager, click **Apply** to save your changes, re-create the schedule, and click **Apply** again to save your changes.  
  
-   Temporary report and user session data that is stored in reportservertempdb are persisted when you move that database.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides several approaches for moving databases, including backup and restore, attach and detach, and copy. Not all approaches are appropriate for relocating an existing database to a new server instance. The approach that you should use to move the report server database will vary depending on your system availability requirements. The easiest way to move the report server databases is to attach and detach them. However, this approach requires that you take the report server offline while you detach the database. Backup and restore is a better choice if you want to minimize service disruptions, but you must run [!INCLUDE[tsql](../../includes/tsql-md.md)] commands to perform the operations. Copying the database is not recommended (specifically, by using the Copy Database Wizard); it does not preserve permission settings in the database.  
  
> [!IMPORTANT]  
>  The steps provided in this topic are recommended when relocating the report server database is the only change you are making to the existing installation. Migrating an entire [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation (that is, moving the database and changing the identity of the Report Server Windows service that uses the database) requires connection reconfiguration and an encryption key reset.  
  
## Detaching and Attaching the Report Server Databases  
 If you can take the report server offline, you can detach the databases to move them to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to use. This approach preserves permissions in the databases. If you are using a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] database, you must move it to another [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] instance. After you move the databases, you must reconfigure the report server connection to the report server database. If you are running a scale-out deployment, you must reconfigure the report server database connection for each report server in the deployment.  
  
 Use the following steps to move the databases:  
  
1.  Backup the encryption keys for the report server database you want to move. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool backup the keys.  
  
2.  Stop the Report Server service. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to stop the service.  
  
3.  Start [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and open a connection to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the report server databases.  
  
4.  Right-click the report server database, point to Tasks, and click **Detach**. Repeat this step for the report server temporary database.  
  
5.  Copy or move the .mdf and .ldf files to the Data folder of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance you want to use. Because you are moving two databases, make sure that you move or copy all four files.  
  
6.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a connection to the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that will host the report server databases.  
  
7.  Right-click the Databases node, and then click **Attach**.  
  
8.  Click **Add** to select the report server database .mdf and .ldf files that you want to attach. Repeat this step for the report server temporary database.  
  
9. After the databases are attached, verify that the `RSExecRole` is a database role in the report server database and temporary database. `RSExecRole` must have select, insert, update, delete, and reference permissions on the report server database tables, and execute permissions on the stored procedures. For more information, see [Create the RSExecRole](../security/create-the-rsexecrole.md).  
  
10. Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and open a connection to the report server.  
  
11. On the Database page, select the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, and then click **Connect**.  
  
12. Select the report server database that you just moved, and then click **Apply**.  
  
13. On the Encryption Keys page, click Restore. Specify the file that contains the backup copy of the keys and the password to unlock the file.  
  
14. Restart the Report Server service.  
  
## Backing Up and Restoring the Report Server Databases  
 If you cannot take the report server offline, you can use backup and restore to relocate the report server databases. You must use [!INCLUDE[tsql](../../includes/tsql-md.md)] statements to do the backup and restore. After you restore the databases, you must configure the report server to use the database on the new server instance. For more information, see the instructions at the end of this topic.  
  
### Using BACKUP and COPY_ONLY to Backup the Report Server Databases  
 When backing up the databases, set the COPY_ONLY argument. Be sure to back up both of the databases and log files.  
  
```  
-- To permit log backups, before the full database backup, alter the database   
-- to use the full recovery model.  
USE master;  
GO  
ALTER DATABASE ReportServer  
   SET RECOVERY FULL  
  
-- If the ReportServerData device does not exist yet, create it.   
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerData',   
'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\BACKUP\ReportServerData.bak'  
  
-- Create a logical backup device, ReportServerLog.  
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerLog',   
'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\BACKUP\ReportServerLog.bak'  
  
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
  
-- If the ReportServerTempDBData device does not exist yet, create it.   
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerTempDBData',   
'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\BACKUP\ReportServerTempDBData.bak'  
  
-- Create a logical backup device, ReportServerTempDBLog.  
USE master  
GO  
EXEC sp_addumpdevice 'disk', 'ReportServerTempDBLog',   
'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\BACKUP\ReportServerTempDBLog.bak'  
  
-- Back up the full ReportServerTempDB database.  
BACKUP DATABASE ReportServerTempDB  
   TO ReportServerTempDBData  
   WITH COPY_ONLY  
  
-- Back up the ReportServerTempDB log.  
BACKUP LOG ReportServerTempDB  
   TO ReportServerTempDBLog  
   WITH COPY_ONLY  
```  
  
### Using RESTORE and MOVE to Relocate the Report Server Databases  
 When restoring the databases, be sure to include the MOVE argument so that you can specify a path. Use the NORECOVERY argument to perform the initial restore; this keeps the database in a RESTORING state, giving you time to review log backups to determine which one to restore. The final step repeats the RESTORE operation with the RECOVERY argument.  
  
 The MOVE argument uses the logical name of the data file. To find the logical name, execute the following statement: `RESTORE FILELISTONLY FROM DISK='C:\ReportServerData.bak';`  
  
 The following examples include the FILE argument so that you can specify the file position of the log file to restore. To find the file position, execute the following statement: `RESTORE HEADERONLY FROM DISK='C:\ReportServerData.bak';`  
  
 When restoring the database and log files, you should run each RESTORE operation separately.  
  
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
  
### How to Configure the Report Server Database Connection  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and open a connection to the report server.  
  
2.  On the Database page, click **Change Database**. Click **Next**.  
  
3.  Click **Choose an existing report server database**. Click **Next**.  
  
4.  Select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that now hosts the report server database and click **Test Connection**. Click **Next**.  
  
5.  In Database Name, select the report server database that you want to use. Click **Next**.  
  
6.  In Credentials, specify the credentials that the report server will use to connect to the report server database. Click **Next**.  
  
7.  Click **Next** and then **Finish**.  
  
> [!NOTE]  
>  A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation requires that the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance include the `RSExecRole` role. Role creation, login registration, and role assignments occur when you set the report server database connection through the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. If you use alternate approaches (specifically, if you use the rsconfig.exe command prompt utility) to configure the connection, the report server will not be in a working state. You might have to write WMI code to make the report server available. For more information, see [Access the Reporting Services WMI Provider](../tools/access-the-reporting-services-wmi-provider.md).  
  
## See Also  
 [Create the RSExecRole](../security/create-the-rsexecrole.md)   
 [Start and Stop the Report Server Service](start-and-stop-the-report-server-service.md)   
 [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-database-connection-ssrs-configuration-manager.md)   
 [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)   
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)   
 [rsconfig Utility &#40;SSRS&#41;](../tools/rsconfig-utility-ssrs.md)   
 [Configure and Manage Encryption Keys &#40;SSRS Configuration Manager&#41;](../install-windows/ssrs-encryption-keys-manage-encryption-keys.md)   
 [Report Server Database &#40;SSRS Native Mode&#41;](report-server-database-ssrs-native-mode.md)  
  
  
