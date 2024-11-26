---
title: Move user databases
description: Learn how to move user databases in SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/19/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "disaster recovery [SQL Server], moving database files"
  - "database files [SQL Server], moving"
  - "data files [SQL Server], moving"
  - "editions [SQL Server], moving databases between"
  - "moving full-text catalogs"
  - "scheduled disk maintenance [SQL Server]"
  - "moving databases"
  - "full-text catalogs [SQL Server], moving"
  - "moving database files"
  - "moving user databases"
  - "relocating database files"
  - "planned database relocations [SQL Server]"
  - "databases [SQL Server], moving"
---
# Move user databases

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can move the data, log, and full-text catalog files of a user database to a new location by specifying the new file location in the `FILENAME` clause of the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement. This method applies to moving database files within the same instance [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. To move a database to another instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or to another server, use [backup and restore](../backup-restore/back-up-and-restore-of-sql-server-databases.md) or [detach and attach operations](move-a-database-using-detach-and-attach-transact-sql.md).

> [!NOTE]  
> This article covers moving user database files. For moving system database files, see [Move system databases](move-system-databases.md).

## Considerations

When you move a database onto another server instance, to provide a consistent experience to users and applications, you might have to recreate some or all the metadata for the database. For more information, see [Manage Metadata When Making a Database Available on Another Server](manage-metadata-when-making-a-database-available-on-another-server.md).

Some features of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] change the way that the [!INCLUDE [ssDE](../../includes/ssde-md.md)] stores information in the database files. These features are restricted to specific editions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. A database that contains these features can't be moved to an edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that doesn't support them. Use the `sys.dm_db_persisted_sku_features` dynamic management view to list all edition-specific features that are enabled in the current database.

The procedures in this article require the logical name of the database files. To obtain the name, query the name column in the [sys.master_files](../system-catalog-views/sys-master-files-transact-sql.md) catalog view.

Full-text catalogs are integrated into the database rather than being stored in the file system. The full-text catalogs move automatically when you move a database.

> [!NOTE]  
> Make sure the service account for the [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md) has permissions to the new file location in the file system. For more information, see [Configure file system permissions for Database Engine access](../../database-engine/configure-windows/configure-file-system-permissions-for-database-engine-access.md).

## Planned relocation procedure

To move a data or log file as part of a planned relocation, follow these steps:

1. For each file to be moved, run the following statement.

   ```sql
   ALTER DATABASE database_name
       MODIFY FILE (NAME = logical_name, FILENAME = 'new_path\os_file_name');
   ```

1. Run the following statement to bring the database offline.

   ```sql
   ALTER DATABASE database_name
       SET OFFLINE;
   ```

   This action requires exclusive access to the database. If another connection is open to the database, the `ALTER DATABASE` statement is blocked until all connections are closed. To override this behavior, use the [`WITH <termination>` clause](../../t-sql/statements/alter-database-transact-sql-set-options.md#with-termination-). For example, to automatically roll back and disconnect all other connections to the database, use:

   ```sql
   ALTER DATABASE database_name
        SET OFFLINE
        WITH ROLLBACK IMMEDIATE;
   ```

1. Move the file or files to the new location.

1. Run the following statement.

   ```sql
   ALTER DATABASE database_name
       SET ONLINE;
   ```

1. Verify the file change by running the following query.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'<database_name>');
   ```

## Relocation for scheduled disk maintenance

To relocate a file as part of a scheduled disk maintenance process, follow these steps:

1. For each file to be moved, run the following statement.

   ```sql
   ALTER DATABASE database_name
       MODIFY FILE (NAME = logical_name, FILENAME = 'new_path\os_file_name');
   ```

1. To perform maintenance, stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or shut down the system. For more information, see [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md).

1. Move the file or files to the new location.

1. Restart the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or the server. For more information, see [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)

1. Verify the file change by running the following query.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'<database_name>');
   ```

## Failure recovery procedure

If a file must be moved because of a hardware failure, use the following steps to relocate the file to a new location.

> [!IMPORTANT]  
> If the database can't be started, that is it's in suspect mode or in an unrecovered state, only members of the sysadmin fixed role can move the file.

1. Stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] if it was already started.

1. Start the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in `master`-only recovery mode by entering one of the following commands at the command prompt.

   - For the default (MSSQLSERVER) instance, run the following command.

     ```cmd
     NET START MSSQLSERVER /f /T3608
     ```

   - For a named instance, run the following command.

     ```cmd
     NET START MSSQL$instancename /f /T3608
     ```

     For more information, see [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md). For information about Linux, see [Start, stop, and restart SQL Server services on Linux](../../linux/sql-server-linux-start-stop-restart-sql-server-services.md).

1. For each file to be moved, use **sqlcmd** commands or [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to run the following statement.

   ```sql
   ALTER DATABASE database_name
       MODIFY FILE (NAME = logical_name, FILENAME = 'new_path\os_file_name');
   ```

   For more information about how to use the **sqlcmd** utility, see [sqlcmd - use the utility](../../tools/sqlcmd/sqlcmd-use-utility.md).

1. Exit the **sqlcmd** utility or [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)].

1. Stop the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Move the file or files to the new location.

1. Start the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For example, run: `NET START MSSQLSERVER`.

1. Verify the file change by running the following query.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'<database_name>');
   ```

## Examples

The following example moves the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] log file to a new location as part of a planned relocation.

1. Make sure you are in the context of the `master` database.

   ```sql
   USE master;
   GO
   ```

1. Return the logical file name.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'AdventureWorks2022')
         AND type_desc = N'LOG';
   GO
   ```

1. Set the database offline.

   ```sql
   ALTER DATABASE AdventureWorks2022
       SET OFFLINE;
   GO
   ```

1. Physically move the file to a new location. In the following statement, modify the path specified in `FILENAME` to the new location of the file on your server.

   ```sql
   ALTER DATABASE AdventureWorks2022
       MODIFY FILE (NAME = AdventureWorks2022_Log, FILENAME = 'C:\NewLoc\AdventureWorks2022_Log.ldf');
   GO
   
   ALTER DATABASE AdventureWorks2022
       SET ONLINE;
   GO
   ```

1. Verify the new location.

   ```sql
   SELECT name,
          physical_name AS CurrentLocation,
          state_desc
   FROM sys.master_files
   WHERE database_id = DB_ID(N'AdventureWorks2022')
         AND type_desc = N'LOG';
   ```

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Database detach and attach (SQL Server)](database-detach-and-attach-sql-server.md)
- [Move system databases](move-system-databases.md)
- [Move database files](move-database-files.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Start, stop, pause, resume, and restart SQL Server services](../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)
