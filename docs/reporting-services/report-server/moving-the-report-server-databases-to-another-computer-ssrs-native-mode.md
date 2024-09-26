---
title: Move report server databases to another computer (native mode)
description: Find out how to move report server databases to a different SQL Server instance. See how to attach and detach the databases or use backup and restore actions.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/26/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom: updatefrequency5

#customer intent: As a SQL Server user, I want to see how to move the report server databases so that I can use them with a different instance of SQL Server.
---

# Move report server databases to another computer (SSRS native mode)

[!INCLUDE[Applies to](../../includes/applies-md.md)] SQL Server Reporting Services (SSRS) native mode

You can move the report server databases that you use in an installation of [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] to an instance that's on a different computer.

[!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] provides several approaches for moving databases:

- Detach and attach. This approach offers the easiest way to move the report server databases. But you need to take the report server offline while the databases are detached.
- Backup and restore. This approach minimizes service disruptions, but you must run Transact-SQL (T-SQL) commands to perform the operations.
- Copy. Copying the database isn't recommended if you use the Copy Database Wizard. It doesn't preserve permission settings in the database.

This article shows you how to use the detach and attach approach and the backup and restore approach.

## Prerequisites

- A configured native mode report server that's used in an installation of SQL Server.
- An instance of SQL Server on a different computer.

## Prepare to move the databases

Keep the following points in mind when you move report server databases:

- You must move or copy the **reportserver** and **reportservertempdb** databases together. An SSRS installation requires both databases.
- The name of the temporary database must be the same as the name of the primary report server database but with a **tempdb** suffix.
- Moving a database doesn't change scheduled operations that are currently defined for report server items.
  - Schedules are re-created the first time that you restart the report server service.
  - [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] agent jobs that are used to trigger a schedule are re-created on the new database instance. You don't have to move the jobs to the new computer, but you might want to delete jobs that are no longer used on the current computer.
  - Subscriptions, cached reports, and snapshots are preserved in the moved database. If a snapshot doesn't pick up refreshed data after you move the database, clear the snapshot options. Then select **Apply** to save your changes, re-create the schedule, and select **Apply** again to save your changes.
  - Temporary report and user session data that's stored in the temporary database is persisted when you move that database.

> [!IMPORTANT]
> The steps in this article are recommended when relocation of the report server database is the only change that you make to the existing installation. When you migrate an entire SSRS installation, you need to reconfigure the connection and reset encryption keys. For example, these steps are required when you move the database and change the identity of the report server Windows service that uses the database.

## Detach and attach the report server databases

If you can take the report server offline, you can use the detach and attach approach. Specifically, you detach the databases from your current [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance. Then you move them and attach them to the instance that you want to use. This approach preserves permissions in the databases.

After you move the databases, you must reconfigure the report server connection to the report server database. If you run a scale-out deployment, you must reconfigure the report server database connection for each report server in the deployment.

To use the detach and attach approach, take the steps in the following sections.

### Detach the databases

1. Open Report Server Configuration Manager.

1. Use the Encryption Keys page to back up the encryption keys for the report server database that you want to move.

1. Use the Report Server Status page to stop the report server service.

1. Open [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] [!INCLUDE[Management Studio](../../includes/ssmanstudio-md.md)] and connect to the [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance that hosts the report server databases.

1. Right-click the report server database, select **Tasks**, and then select **Detach**. Repeat this step for the report server temporary database.

### Attach the databases

1. Find the .mdf and .ldf files for your current [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance. They're located in the Data folder.

   :::image type="content" source="media/moving-the-report-server-databases-to-another-computer-ssrs-native-mode/data-folder-database-files.png" alt-text="Screenshot of File Explorer. In the Data folder, .mdf and .ldf files are highlighted for the report server database and the temporary database.":::

1. Copy or move the .mdf and .ldf files to the Data folder of the [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance that you want to use. Because you're moving two databases, make sure that you move or copy all four files.

1. In [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] [!INCLUDE[Management Studio](../../includes/ssmanstudio-md.md)], open a connection to the new [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance that hosts the report server databases.

1. Right-click the **Databases** node, and then select **Attach**.

1. Select **Add** to select the report server database .mdf and .ldf files that you want to attach. Repeat this step for the report server temporary database.

### Complete the configuration

1. Verify that the databases that you attached have the **RSExecRole** role. You must configure **RSExecRole** for select, insert, update, delete, and reference permissions on the report server database tables, and execute permissions on the stored procedures. For more information, see [Create the RSExecRole](../../reporting-services/security/create-the-rsexecrole.md).

1. Start Report Server Configuration Manager and open a connection to the report server.

1. On the Database page, select the new [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance, and then select **Connect**.

1. Select the report server database that you just moved, and then select **Apply**.

1. On the Encryption Keys page, select **Restore**. Specify the file that contains the backup copy of the keys and the password to unlock the file.

1. Restart the report server service.

## Back up and restore the report server databases

If you can't take the report server offline, you can use the backup and restore approach to relocate the report server databases. With this approach, you must use T-SQL statements.

The steps in the following sections show you how to back up and restore the databases and also how to configure the report server to use the databases on the new server instance.

### Use BACKUP and COPY_ONLY to back up the report server databases

To back up the databases, open SQL Server Management Studio, and then run the following statements in a query window. These statements use the `COPY_ONLY` argument, and they back up both databases and log files.

Before you run these statements, replace the \<path-to-backup-folder\> placeholder with the path to the Backup folder of your current instance, such as `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP`.

```tsql
-- To permit log backups, alter the database to use the full recovery model
-- before you run the full database backup.
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
TO DISK = '<path-to-backup-folder>\ReportServerExtraBackup.bak'
GO

-- If the ReportServerData device doesn't exist yet, create it.
USE master
GO
EXEC sp_addumpdevice 'disk', 'ReportServerData',
'<path-to-backup-folder>\ReportServerData.bak'

-- Create a logical backup device, ReportServerLog.
USE master
GO
EXEC sp_addumpdevice 'disk', 'ReportServerLog',
'<path-to-backup-folder>\ReportServerLog.bak'

-- Back up the full ReportServer database.
BACKUP DATABASE ReportServer
   TO ReportServerData
   WITH COPY_ONLY

-- Back up the ReportServer log.
BACKUP LOG ReportServer
   TO ReportServerLog
   WITH COPY_ONLY

-- To permit log backups, alter the database to use the full recovery model
-- before you run the full database backup.
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
TO DISK = '<path-to-backup-folder>\ReportServerTempdbExtraBackup.bak'
GO

-- If the ReportServerTempDBData device doesn't exist yet, create it.
USE master
GO
EXEC sp_addumpdevice 'disk', 'ReportServerTempDBData',
'<path-to-backup-folder>\ReportServerTempDBData.bak'

-- Create a logical backup device, ReportServerTempDBLog.
USE master
GO
EXEC sp_addumpdevice 'disk', 'ReportServerTempDBLog',
'<path-to-backup-folder>\ReportServerTempDBLog.bak'

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

To restore the databases, open SQL Server Management Studio, and then run the following statements in a query window.

In these statements:

- The `RESTORE` operations for the database and log files run separately.

- The `MOVE` argument provides a way for you to specify a path. This argument uses the logical name of the data file. To find the logical name, run the following statement. First replace the \<path-to-report-server-database-backup-file\> placeholder with the path to your report server database backup file, such as `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\ReportServerData.bak`.

  ```tsql
  RESTORE FILELISTONLY FROM DISK='<path-to-report-server-database-backup-file>';
  ```

  You can find the logical name in the **LogicalName** column of the output.

  :::image type="content" source="media/moving-the-report-server-databases-to-another-computer-ssrs-native-mode/find-logical-name-query.png" alt-text="Screenshot of a RESTORE FILELISTONLY statement in a SQL Server Management Studio query window. In the output, the LogicalName column is highlighted.":::

  You can run a similar statement to find the logical name of your temporary database:

  ```tsql
  RESTORE FILELISTONLY FROM DISK='<path-to-temporary-database-backup-file>';
  ```

- The `FILE` argument provides a way for you to specify the file position of the log file to restore. To find the file position, run the following statement. First replace the \<path-to-report-server-database-backup-file\> placeholder with the path to your report server database backup file, such as `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\ReportServerData.bak`.

  ```tsql
  RESTORE HEADERONLY FROM DISK='<path-to-report-server-database-backup-file>';
  ```

  You can find the file position in the **Position** column of the output.

  :::image type="content" source="media/moving-the-report-server-databases-to-another-computer-ssrs-native-mode/find-file-position-query.png" alt-text="Screenshot of a RESTORE HEADERONLY statement in a SQL Server Management Studio query window. In the output, the Position column is highlighted.":::

  You can run a similar statement to find the file position of your temporary database:

  ```tsql
  RESTORE HEADERONLY FROM DISK='<path-to-temporary-database-backup-file>';
  ```

- The `NORECOVERY` argument performs the initial restore. This argument keeps the database in a `RESTORING` state, which gives you time to review log backups to determine which one to restore.

- The final step repeats the `RESTORE` operation with the `RECOVERY` argument.

Before you run these statements, replace the following placeholders with appropriate values:

| Placeholder | Description | Example |
| --- | --- | --- |
| \<path-to-backup-folder\> | The path to the Backup folder of your current instance | `C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\BACKUP` |
| \<path-to-new-data-folder\> | The path to the Data folder of your new instance | `C:\Program Files\Microsoft SQL Server\MSSQL16.NEWINSTANCE\MSSQL\DATA` |
| \<report-server-database-logical-name\> | The logical name of your report server database | `ReportServer` |
| \<report-server-database-log-logical-name\> | The logical name of your report server database log | `ReportServer_log` |
| \<report-server-database-log-file-position\> | The file position of the report server database log file | 2 |
| \<temporary-database-logical-name\> | The logical name of your temporary database | `ReportServerTempDB` |
| \<temporary-database-log-logical-name\> | The logical name of your temporary database log | `ReportServerTempDB_log` |
| \<temporary-database-log-file-position\> | The file position of the temporary database log file | 2 |

```tsql
-- Restore the report server database and move it to the new instance folder.
RESTORE DATABASE ReportServer
   FROM DISK='<path-to-backup-folder>\ReportServerData.bak'
   WITH NORECOVERY,
      MOVE '<report-server-database-logical-name>' TO
         '<path-to-new-data-folder>\ReportServer.mdf',
      MOVE '<report-server-database-log-logical-name>' TO
         '<path-to-new-data-folder>\ReportServer_Log.ldf';
GO

-- Restore the report server log file to the new instance folder.
RESTORE LOG ReportServer
   FROM DISK='<path-to-backup-folder>\ReportServerData.bak'
   WITH NORECOVERY, FILE=<report-server-database-log-file-position>,
      MOVE '<report-server-database-logical-name>' TO
         '<path-to-new-data-folder>\ReportServer.mdf',
      MOVE '<report-server-database-log-logical-name>' TO
         '<path-to-new-data-folder>\ReportServer_Log.ldf';
GO

-- Restore and move the report server temporary database.
RESTORE DATABASE ReportServerTempdb
   FROM DISK='<path-to-backup-folder>\ReportServerTempDBData.bak'
   WITH NORECOVERY,
      MOVE '<temporary-database-logical-name>' TO
         '<path-to-new-data-folder>\ReportServerTempDB.mdf',
      MOVE '<temporary-database-log-logical-name>' TO
         '<path-to-new-data-folder>\ReportServerTempDB_Log.ldf';
GO

-- Restore the temporary database log file to the new instance folder.
RESTORE LOG ReportServerTempdb
   FROM DISK='<path-to-backup-folder>\ReportServerTempDBData.bak'
   WITH NORECOVERY, FILE=<temporary-database-log-file-position>,
      MOVE '<temporary-database-logical-name>' TO
         '<path-to-new-data-folder>\ReportServerTempDB.mdf',
      MOVE '<temporary-database-log-logical-name>' TO
         '<path-to-new-data-folder>\ReportServerTempDB_Log.ldf';
GO

-- Perform the final restore operation on the report database.
RESTORE DATABASE ReportServer
   WITH RECOVERY
GO

-- Perform the final restore operation on the temporary database.
RESTORE DATABASE ReportServerTempDB
   WITH RECOVERY
GO
```

### Configure the report server database connection

1. Start Report Server Configuration Manager and open a connection to the report server.

1. On the Database page, select **Change Database**.

1. On the Change Database page, select **Choose an existing report server database**, and then select **Next**.

1. For **Server Name**, enter the [!INCLUDE[SQL Server](../../includes/ssnoversion-md.md)] instance that now hosts the report server database, and then select **Test Connection**.

1. After you test the connection, select **Next**.

1. For **Report Server Database**, select the report server database that you want to use, and then select **Next**.

1. Under **Credentials**, specify the credentials that the report server uses to connect to the report server database, and then select **Next**.

1. Select **Next**, and then select **Finish**.

> [!NOTE]
> In an SSRS installation, the [!INCLUDE[SQL Server Database Engine](../../includes/ssdenoversion-md.md)] instance must include the **RSExecRole** role. Role creation, login registration, and role assignments occur when you use Report Server Configuration Manager to set the report server database connection. If you use alternate approaches, such as the rsconfig.exe command-prompt utility, the report server isn't in a working state. In that case, you might have to write Windows Management Instrumentation (WMI) code to make the report server available. For more information, see [Access the Reporting Services WMI provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).

## Related content

- [Report server database (SSRS native mode)](../../reporting-services/report-server/report-server-database-ssrs-native-mode.md)
- [Configure a report server database connection (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)
- [Configure and manage encryption keys (Report Server Configuration Manager)](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)
