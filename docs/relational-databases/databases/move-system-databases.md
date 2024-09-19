---
title: Move system databases
description: Learn how to move system databases in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/19/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "moving system databases"
  - "disaster recovery [SQL Server], moving database files"
  - "database files [SQL Server], moving"
  - "data files [SQL Server], moving"
  - "tempdb database [SQL Server], moving"
  - "system databases [SQL Server], moving"
  - "scheduled disk maintenace [SQL Server]"
  - "moving databases"
  - "msdb database [SQL Server], moving"
  - "moving database files"
  - "relocating database files"
  - "planned database relocations [SQL Server]"
  - "master database [SQL Server], moving"
  - "model database [SQL Server], moving"
  - "Resource database [SQL Server]"
  - "databases [SQL Server], moving"
---
# Move system databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to move system databases in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Moving system databases might be useful in the following situations:

- Failure recovery. For example, the database is in suspect mode or has shut down because of a hardware failure.

- Planned relocation.

- Relocation for scheduled disk maintenance.

The following procedures apply to moving database files within the same instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To move a database to another instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or to another server, use the [backup and restore](../backup-restore/back-up-and-restore-of-sql-server-databases.md) operation.

The procedures in this article require the logical name of the database files. To obtain the name, query the name column in the [sys.master_files](../system-catalog-views/sys-master-files-transact-sql.md) catalog view.

> [!IMPORTANT]  
> If you move a system database and later rebuild the `master` database, you must move the system database again because the rebuild operation installs all system databases to their default location.

<a id="Planned"></a>

## Move the system databases

To move a system database data or log file as part of a planned relocation or scheduled maintenance operation, follow these steps. This includes the `model`, `msdb`, and `tempdb` system databases.

> [!IMPORTANT]  
> This procedure applies to all system databases except the `master` and `Resource` databases. See later in this article for steps to move the `master` database. The `Resource` database can't be moved.

1. Record the existing location of the database files you intend to move, by reviewing the [sys.master_files](../system-catalog-views/sys-master-files-transact-sql.md) catalog view.

1. Verify that the service account for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE [ssDE](../../includes/ssde-md.md)] service account can't control the files in their new location, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance doesn't start.

1. For each database file to be moved, run the following statement.

   ```sql
   ALTER DATABASE database_name
       MODIFY FILE (NAME = logical_name, FILENAME = 'new_path\os_file_name');
   ```

   Until the service is restarted, the database continues to use the data and log files in the existing location.

1. Stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to perform maintenance. For more information, see [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).

1. Copy the database file or files to the new location. This step isn't necessary for the `tempdb` system database; those files are created in the new location automatically.

1. Restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or the server. For more information, see [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).

1. Verify the file change by running the following query. The system databases should report the new physical file locations.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'<database_name>');
   ```

1. Since in Step 5 you copied the database files instead of moving them, now you can safely delete the unused database files from their previous location.

### Follow-up: After moving the `msdb` system database

If the `msdb` database is moved and [Database Mail](../database-mail/database-mail.md) is configured, complete the following extra steps.

1. Verify that [!INCLUDE [ssSB](../../includes/sssb-md.md)] is enabled for the `msdb` database by running the following query.

   ```sql
   SELECT is_broker_enabled
   FROM sys.databases
   WHERE name = N'msdb';
   ```

   If the [!INCLUDE [ssSB](../../includes/sssb-md.md)] isn't enabled for `msdb`, it must be re-enabled for Database Mail to function. For more information, see [ALTER DATABASE ... SET ENABLE_BROKER](../../t-sql/statements/alter-database-transact-sql-set-options.md#service_broker_option-).

   ```sql
   ALTER DATABASE msdb
       SET ENABLE_BROKER
       WITH ROLLBACK IMMEDIATE;
   ```

   Confirm that the value of `is_broker_enabled` is now 1.

1. Verify that Database Mail is working by sending a test mail.

<a id="Failure"></a>

## Failure recovery procedure

If a file must be moved because of a hardware failure, follow these steps to relocate the file to a new location. This procedure applies to all system databases except the `master` and `Resource` databases. The following examples use the Windows command-line prompt and [sqlcmd Utility](../../tools/sqlcmd/sqlcmd-use-utility.md).

> [!IMPORTANT]  
> If the database can't be started, if it's in suspect mode or in an unrecovered state, only members of the sysadmin fixed role can move the file.

1. Verify that the service account for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE [ssDE](../../includes/ssde-md.md)] service account can't control the files in their new location, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance doesn't start.

1. Stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] if it's started.

1. Start the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in `master`-only recovery mode by entering one of the following commands at the command prompt. Using startup parameter 3608 prevents SQL Server from automatically starting and recovering any database except the `master` database. For more information, see [Startup Parameters](../../tools/configuration-manager/sql-server-properties-startup-parameters-tab.md#optional-parameters) and [TF3608](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf3608).

   The parameters specified in these commands are case sensitive. The commands fail when the parameters aren't specified as shown.

   For the default (MSSQLSERVER) instance, run the following command:

   ```cmd
   NET START MSSQLSERVER /f /T3608
   ```

   For a named instance, run the following command:

   ```cmd
   NET START MSSQL$instancename /f /T3608
   ```

   For more information, see [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).

1. Promptly after service startup with trace flag 3608 and `/f`, start a **sqlcmd** connection to the server, to claim the single connection that is available. For example, when executing **sqlcmd** locally on the same server as the default (MSSQLSERVER) instance, and to connect with Active Directory integration authentication, run the following command:

   ```cmd
   sqlcmd
   ```

   To connect to a named instance on the local server, with Active Directory integration authentication:

   ```cmd
   sqlcmd -S localhost\instancename
   ```

   For more information on **sqlcmd** syntax, see [sqlcmd utility](../../tools/sqlcmd/sqlcmd-utility.md).

   For each file to be moved, use **sqlcmd** commands or [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to run the following statement. For more information about using the **sqlcmd** utility, see [sqlcmd - use the utility](../../tools/sqlcmd/sqlcmd-use-utility.md). Once the **sqlcmd** session is open, run the following statement once for each file to be moved:

   ```sql
   ALTER DATABASE database_name
       MODIFY FILE (NAME = logical_name, FILENAME = 'new_path\os_file_name');
   GO
   ```

1. Exit the **sqlcmd** utility or [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

1. Stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, run `NET STOP MSSQLSERVER` in the command-line prompt.

1. Copy the file or files to the new location.

1. Restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, run `NET START MSSQLSERVER` in the command-line prompt.

1. Verify the file change by running the following query.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'<database_name>');
   ```

1. Since in Step 7 you copied the database files instead of moving them, now you can safely delete the unused database files from their previous location.

<a id="master"></a>

## Move the `master` database

To move the `master` database, follow these steps.

1. Verify that the service account for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE [ssDE](../../includes/ssde-md.md)] service account can't control the files in their new location, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance doesn't start.

1. From the **Start** menu, locate and launch **SQL Server Configuration Manager**. For more information on the expected location, see [SQL Server Configuration Manager](../sql-server-configuration-manager.md).

1. In the **SQL Server Services** node, right-click the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (for example, **SQL Server (MSSQLSERVER)**) and choose **Properties**.

1. In the **SQL Server (***instance_name***) Properties** dialog box, select the **Startup Parameters** tab.

1. In the **Existing parameters** box, select the `-d` parameter. In the **Specify a startup parameter** box, change the parameter to the new path of the `master` *data* file. Select **Update** to save the change.

1. In the **Existing parameters** box, select the `-l` parameter. In the **Specify a startup parameter** box, change the parameter to the new path of the `master` *log* file. Select **Update** to save the change.

   The parameter value for the data file must follow the `-d` parameter and the value for the log file must follow the `-l` parameter. The following example shows the parameter values for the default location of the `master` data file.

   ```output
   -dC:\Program Files\Microsoft SQL Server\MSSQL<version>.MSSQLSERVER\MSSQL\DATA\master.mdf
   -lC:\Program Files\Microsoft SQL Server\MSSQL<version>.MSSQLSERVER\MSSQL\DATA\mastlog.ldf
   ```

   If the planned relocation for the `master` data file is `E:\SQLData`, the parameter values would be changed as follows:

   ```output
   -dE:\SQLData\master.mdf
   -lE:\SQLData\mastlog.ldf
   ```

1. Select **OK** to save the changes permanently and close the **SQL Server (***instance_name***) Properties** dialog box.

1. Stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by right-clicking the instance name and choosing **Stop**.

1. Copy the `master.mdf` and `mastlog.ldf` files to the new location.

1. Restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Verify the file change for the `master` database by running the following query.

    ```sql
    SELECT name,
           physical_name AS CurrentLocation,
           state_desc
    FROM sys.master_files
    WHERE database_id = DB_ID('master');
    ```

1. At this point [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] should run normally. However Microsoft recommends also adjusting the registry entry at `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\instance_ID\Setup`, where *instance_ID* is like `MSSQL13.MSSQLSERVER`. In that hive, change the `SQLDataRoot` value to the new path of the new location of the `master` database files. Failure to update the registry can cause patching and upgrading to fail.

1. Since in Step 9 you copied the database files instead of moving them, now you can safely delete the unused database files from their previous location.

<a id="Resource"></a>

## Move the resource database

The location of the `Resource` database is `\<drive>:\Program Files\Microsoft SQL Server\MSSQL\<version>.<instance_name>\MSSQL\Binn\`. The database can't be moved.

<a id="Follow"></a>

## Follow-up: After moving all system databases

If you moved all of the system databases to a new drive or volume, or to another server with a different drive letter, make the following updates.

- Change the SQL Server Agent log path. If you don't update this path, SQL Server Agent fails to start.

- Change the database default location. Creating a new database might fail if the drive letter and path specified as the default location don't exist.

#### Change the SQL Server Agent log path

If you have moved all of the system databases to a new volume or have migrated to another server with a different drive letter, and the path of the SQL Agent error log file `SQLAGENT.OUT` no longer exists, make the following updates.

1. From SQL Server Management Studio, in **Object Explorer**, expand **SQL Server Agent**.

1. Right-click **Error Logs** and select **Configure**.

1. In the **Configure SQL Server Agent Error Logs** dialog box, specify the new location of the SQLAGENT.OUT file. The default location is `C:\Program Files\Microsoft SQL Server\MSSQL\<version>.<instance_name>\MSSQL\Log\`.

#### Change the database default location

1. From SQL Server Management Studio, in **Object Explorer**, connect to the desired [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. Right-click the instance and select **Properties**.

1. In the **Server Properties** dialog box, select **Database Settings**.

1. Under **Database Default Locations**, browse to the new location for both the data and log files.

1. Stop and start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service to complete the change.

<a id="Examples"></a>

## Examples

### A. Move the `tempdb` database

The following example moves the `tempdb` data and log files to a new location as part of a planned relocation.

> [!TIP]  
> Take this opportunity to review your `tempdb` files for optimal size and placement. For more information, see [Optimizing tempdb performance in SQL Server](tempdb-database.md#optimizing-tempdb-performance-in-sql-server).

Because `tempdb` is recreated each time the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is started, you don't have to physically move the data and log files. The files are created in the new location when the service is restarted in step 4. Until the service is restarted, `tempdb` continues to use the data and log files in the existing location.

1. Determine the logical file names of the `tempdb` database and their current location on the disk.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation
   FROM sys.master_files
   WHERE database_id = DB_ID(N'tempdb');
   GO
   ```

1. Verify that the service account for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)] has full permissions to the new location of the files. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md). If the [!INCLUDE [ssDE](../../includes/ssde-md.md)] service account can't control the files in their new location, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance doesn't start.

1. Change the location of each file by using `ALTER DATABASE`.

   ```sql
   USE master;
   GO

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = tempdev, FILENAME = 'E:\SQLData\tempdb.mdf');
   GO

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = templog, FILENAME = 'F:\SQLLog\templog.ldf');
   GO
   ```

   Until the service is restarted, `tempdb` continues to use the data and log files in the existing location.

1. Stop and restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Verify the file change.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'tempdb');
   ```

1. Delete the unused `tempdb` files from their original location.

## Related content

- [Resource database](resource-database.md)
- [tempdb database](tempdb-database.md)
- [master database](master-database.md)
- [msdb database](msdb-database.md)
- [model database](model-database.md)
- [Move user databases](move-user-databases.md)
- [Move database files](move-database-files.md)
- [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)
- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Rebuild system databases](rebuild-system-databases.md)
