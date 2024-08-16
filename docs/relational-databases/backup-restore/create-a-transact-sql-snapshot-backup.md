---
title: "Create a Transact-SQL snapshot backup"
description: This article shows you how to create a Transact-SQL backup in SQL Server using SQL Server Management Studio, Transact-SQL, or PowerShell.
author: perrysk-msft
ms.author: peskount
ms.reviewer: mikeray, randolphwest
ms.date: 08/15/2024
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "backing up databases [SQL Server], full backups"
  - "backups [SQL Server], creating"
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---
# Create a Transact-SQL snapshot backup

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

This article explains what, why, and how to use Transact-SQL snapshot backups. Transact-SQL (T-SQL) snapshot backups were introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

<br />

> [!VIDEO https://learn-video.azurefd.net/vod/player?show=data-exposed&ep=rethink-your-backup-and-recovery-strategy-with-t-sql-snapshot-backup-in-sql-server-2022-data-exposed]

Databases are getting larger and larger every day. Traditionally, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backups are streaming backups. A streaming backup depends on the size of the database. Backup operations consume resources (CPU, memory, I/O, network) which affect throughput of the concurrent OLTP workload during the backup. One way to make the backup performance constant, rather than depend on the size of data, is by performing a snapshot backup using mechanisms provided by the underlying storage hardware or service.

Because the backup itself happens at the hardware level, this feature isn't a pure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] solution. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] must first prepare the data and log files for the snapshot, so that the files are guaranteed to be in a state that can later be restored. Once this step is complete, write operations are paused on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] (read requests are still allowed), and control is handed over to the backup application to complete the snapshot. Once the snapshot is successfully complete, the application must return control back to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] where write operations are then resumed.

Because we must freeze write operations during the snapshot operation, it's essential that the snapshot happens quickly, so that the workload on the server isn't interrupted for an extended period. In the past, users relied on non-Microsoft solutions that were built on top of the SQL Writer service to complete snapshot backups. The SQL Writer service depends on Windows VSS (Volume Shadow Service) along with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] VDI (Virtual Device Interface) to perform the orchestration between [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and the disk-level snapshot.

Backup clients based on the SQL Writer service tend to be complex, and they only work on Windows. With T-SQL snapshot backups, the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] side of the orchestration can be handled with a series of T-SQL commands. This functionality allows users to create their own small backup applications that can run on either Windows or Linux, or even scripted solutions if the underlying storage supports a scripting interface to initiate a snapshot.

Here's a [sample PowerShell script](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/t-sql-snapshot-backup/snapshot-backup-restore-azurevm-single-db.ps1), which demonstrates an end-to-end solution of backing up and restoring a database in an Azure SQL IaaS Virtual Machine. The sample uses the T-SQL snapshot backup capabilities introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## Workflow

The T-SQL snapshot backup syntax decouples the vendor-dependent snapshot mechanism from the suspend and backup operations. With this syntax, you can:

1. Freeze a database with the `ALTER` command, which provides an opportunity for you to perform the snapshot of the underlying storage. After which, you can thaw the database and record the snapshot with the `BACKUP` command.

1. Perform snapshots of multiple databases simultaneously with the new `BACKUP GROUP` and `BACKUP SERVER` commands. With this option, snapshots can be performed at the snapshot granularity of the underlying storage, eliminating the need to perform a snapshot of the same disk multiple times.

1. Perform `FULL` backups and `COPY_ONLY FULL` backups. These backups are recorded in `msdb` as well.

1. Perform point-in-time recovery using log backups taken with the normal streaming approach after the snapshot `FULL` backup. Streaming differential backups are also supported if desired.

> [!NOTE]  
> Differential bitmaps are cleared during the first stage when suspending the database with the `ALTER` command. If the user decides to thaw the database without performing a backup because the snapshot failed or for any other reason, the differential bitmap is invalid. Any subsequent differential backups are more I/O intensive, as they must scan the whole database to do the differential backup. The differential bitmap becomes valid again after a successful snapshot backup.

The following diagram illustrates the high-level workflow of T-SQL snapshot backups:

:::image type="content" source="media/create-a-transact-sql-snapshot-backup/t-sql-snapshot-backup-workflow.png" alt-text="Diagram that shows process from suspend, to snapshot, to backup." lightbox="media/create-a-transact-sql-snapshot-backup/t-sql-snapshot-backup-workflow.png":::

The middle snapshot step requires you to initiate the snapshot on the underlying storage. The following diagram shows an example of how a backup script could work with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to complete the snapshot backup process:

:::image type="content" source="media/create-a-transact-sql-snapshot-backup/backup.png" alt-text="Diagram shows example of how the backup script can work with SQL Server to complete the backup process." lightbox="media/create-a-transact-sql-snapshot-backup/backup.png":::

Similarly, a restore script could work as follows:

:::image type="content" source="media/create-a-transact-sql-snapshot-backup/restore.png" alt-text="Diagram shows how the restore script can work with SQL Server to complete the restore task from a snapshot backup." lightbox="media/create-a-transact-sql-snapshot-backup/restore.png":::

## Limitations

The maximum number of databases you can back up with this feature is 64. If there are more than 64 databases on the server, you see the following error:

```output
Error message:
Msg 925, Level 19, State 1, Line 4
Maximum number of databases used for each query has been exceeded. The maximum allowed is 64.
```

## Examples

The following sections show different T-SQL commands used to perform snapshot backup to disk. When a snapshot backup is written to disk, only the metadata connected to the snapshot backup is written to the file. The output doesn't contain any of the database contents except for the header and the file contents. The shell file created as part of performing snapshot backup should be used with the actual snapshot URI to make a complete backup. A `RESTORE` of a database from this file requires the user to copy the database files from the snapshot URI to the mount point, before issuing the `RESTORE` command. Users are able to run all the traditional T-SQL commands, like `RESTORE HEADERONLY` and `RESTORE FILELISTONLY`, on this snapshot backup metadata file, along with `RESTORE DATABASE`. The syntax supports writing snapshot backup metadata to `DISK` or `URL`. The snapshot backup sets can also be appended just like streaming backup sets into a single file.

> [!NOTE]  
> For backup to URL, block blobs are preferred, although page blobs are supported for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Windows. For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux and containers, only block blobs are supported.

### A. Suspend a single user database for snapshot backup and record a database backup

```sql
ALTER DATABASE testdb1
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON;

BACKUP DATABASE testdb1
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;
```

### B. Suspend multiple user databases for snapshot backup

If multiple databases are on the same underlying disk, you can suspend multiple databases with the following command.

```sql
ALTER SERVER CONFIGURATION
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON
(GROUP = (testdb1, testdb2));

BACKUP GROUP testdb1, testdb2
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;
```

### C. Suspend all user databases on the server for snapshot backup

If all the user databases on the server need to be suspended, use the following command.

```sql
ALTER SERVER CONFIGURATION
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON;

BACKUP SERVER
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;
```

> [!NOTE]  
> None of these commands support suspending system databases (`master`, `model`, and `msdb`) for snapshot backup.

### D. Suspend multiple user databases with a single command

Record a snapshot of all the user databases on the server into a single backup set:

```sql
ALTER SERVER CONFIGURATION
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON
(GROUP = (testdb1, testdb2));

BACKUP GROUP testdb1, testdb2
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;
```

> [!NOTE]  
> By default, `SUSPEND_FOR_SNAPSHOT_BACKUP` commands clear the differential bitmap. If you prefer to perform a copy only backup, use the `COPY_ONLY` keyword as shown in the following examples.

### E. Perform copy-only snapshot backups

Since the differential bitmap is cleared before the freeze, `SUSPEND_FOR_SNAPSHOT_BACKUP` provides an option (`COPY_ONLY`) not to clear the differential bitmap before the freeze.

```sql
ALTER DATABASE testdb1
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON
(MODE = COPY_ONLY);

BACKUP DATABASE testdb1
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;

ALTER SERVER CONFIGURATION
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON
(GROUP = (testdb1, testdb2), MODE = COPY_ONLY);

BACKUP GROUP testdb1, testdb2
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;

ALTER SERVER CONFIGURATION
SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON
(MODE = COPY_ONLY);

BACKUP SERVER
TO DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FORMAT;
```

> [!NOTE]  
> It isn't necessary to use `COPY_ONLY` on the `BACKUP` command, as it's already specified when suspending the database for snapshot backup.

### F. Back up a database with data and log files on different drives

If you have a database with data files (`.mdf` and `.ndf`) across multiple drives, and the transaction log file (`.ldf`) on a different drive, you can perform a snapshot backup as follows:

1. Suspend the database (which freezes the write I/O on both data and log files).

   ```sql
   ALTER SERVER CONFIGURATION
   SET SUSPEND_FOR_SNAPSHOT_BACKUP = ON;
   ```

1. Take a snapshot of *all* the underlying disks where the database data and log files are present. This step is hardware dependent.

1. Perform the backup using the `METADATA_ONLY` option, which creates the output containing the snapshot backup metadata (`.bkm`).

   ```sql
   BACKUP DATABASE testdb1
   TO DISK = 'D:\Temp\db.bkm'
   WITH METADATA_ONLY;
   ```

To restore this backup at a later stage, follow these steps:

1. Mount or attach the snapshot disks on the VM where you want to restore.

1. Use the `.bkm` file (from step 3 in the previous list) when you perform a database restore.

1. If the drives are different during restore, use the `MOVE` option for the logical files to place them in the required destination. For an example, see [Example N](#n-restore-the-database-with-a-different-name).

### G. Tag the backupset

You can use the `MEDIANAME` and `MEDIADESCRIPTION` options in the backup command to tag the URI associated with the snapshot. This use allows the backup file to carry the underlying snapshot information along with the database metadata. You can also use the `NAME` and `DESCRIPTION` options to tag the URI with the individual backupset snapshot.

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] doesn't interpret the `LABEL` information in any way. It does however help the user to view the URI associated with the snapshot backup with the `RESTORE LABELONLY` command.

You could then attach the snapshot disks located at the URI to the VM to restore the snapshot. The snapshot URI stored in the `MEDIANAME` and `MEDIADESCRIPTION` is also then available for viewing in the `msdb` database table `dbo.backupmediaset`.

### H. Output of snapshot backup with RESTORE HEADERONLY

The output with `RESTORE HEADERONLY` looks like the following sample, if the database, group, and server are executed in sequence and written to the same output file:

```sql
RESTORE HEADERONLY
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY;
```

### I. Output of snapshot backup with RESTORE FILELISTONLY

The output with `RESTORE FILELISTONLY` displays the first backup set by default:

```sql
RESTORE FILELISTONLY
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY;
```

### J. Filter RESTORE FILELISTONLY output to a backup set

To specifically select a certain backup set from multiple backup sets with `RESTORE FILELISTONLY`, use the `FILE` clause that is already supported on `RESTORE FILELISTONLY`.

```sql
RESTORE FILELISTONLY
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FILE = 3;
```

:::image type="content" source="media/create-a-transact-sql-snapshot-backup/output-to-backup-set.png" alt-text="Screenshot of SSMS output to backup set from query.":::

### K. Filter RESTORE FILELISTONLY output to a database

To further select a single database from multiple databases within the selected backup set with `RESTORE FILELISTONLY`, use the `FILE` clause with the `DBNAME` clause. The `DBNAME` clause can be used only on snapshot backup sets.

```sql
RESTORE FILELISTONLY
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FILE = 3, DBNAME = 'testdb3';
```

:::image type="content" source="media/create-a-transact-sql-snapshot-backup/filter-db-restore-filelistonly.png" alt-text="Screenshot of results of filtering RESTORE FILELISTONLY output to a database.":::

### L. Restore a snapshot database

Restoring a database from snapshot backup is like *attaching* a database. Run the restore command without the `RECOVERY` option, if the database needs to be attached without recovery. By default, `RESTORE` selects the first database in the snapshot backup set. The following example restores `testdb1`. If `testdb1` already exists on the server, include the `REPLACE` clause. You need to mount the database files before you run `RESTORE`.

```sql
RESTORE DATABASE testdb1
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FILE = 3, REPLACE, --> no DBNAME clause - restore first database in backup set
MOVE 'testdb1' TO 'D:\Temp\snap\testdb1.mdf',
MOVE 'testdb1_log' TO 'D:\Temp\snap\testdb1_log.ldf';
```

### M. Restore a snapshot database listed in the middle

If the database that needs to be `RESTORED` is in the middle, specify the database to be restored with the `DBNAME` clause. The following syntax restores the specified database in the `DBNAME` clause.

```sql
RESTORE DATABASE testdb3
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FILE = 3, DBNAME = 'testdb3', --> restores testdb3 database
MOVE 'testdb3' TO 'D:\Temp\snap\testdb3.mdf',
MOVE 'testdb3_log' TO 'D:\Temp\snap\testdb3_log.ldf',
NORECOVERY;
```

### N. Restore the database with a different name

You can restore the database with a different name. If the database that needs to be `RESTORED` is in the middle, specify the database to be restored with the `DBNAME` clause. The following syntax restores the specified database with the `DBNAME` clause and renames it to `testdb33`.

```sql
RESTORE DATABASE testdb33 --> renames the specified database testdb3 to testdb33.
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FILE = 3, DBNAME = 'testdb3', --> original name specified here
MOVE 'testdb3' TO 'D:\Temp\snap\testdb3.mdf',
MOVE 'testdb3_log' TO 'D:\Temp\snap\testdb3_log.ldf',
NORECOVERY;
```

### O. Use RESTORE BACKUPSETONLY to extract databases from a backup set containing multiple databases

A snapshot backup set containing multiple databases from a group or server snapshot can be split with the `RESTORE BACKUPSETONLY` command. This command produces one backup set per database.

If a server snapshot contains three databases in a backup file containing a single backup set, the following command generates three backup sets, one for each database. It creates a directory with `<file_name_prefix>_<unique_time_stamp>` for the output files.

```sql
RESTORE BACKUPSETONLY
FROM DISK = 'D:\Temp\db1.bkm'
WITH METADATA_ONLY;
```

### P. Use RESTORE BACKUPSETONLY to extract a specific database in a backup set containing multiple databases

`RESTORE BACKUPSETONLY` supports the `DBNAME` parameter if the user wants to output one database out of the three databases in the backup set. It also supports the `FILE` parameter to filter multiple backup sets in the backup file.

```sql
RESTORE BACKUPSETONLY
FROM DISK = 'D:\Temp\db.bkm'
WITH METADATA_ONLY, FILE = 3, DBNAME = 'testdb2';
```

### Q. Monitor the suspend status and locks acquired

You can use the following dynamic management views (DMVs):

- `sys.dm_server_suspend_status` (view the suspend status)
- `sys.dm_tran_locks` (view the locks acquired)

### R. List backupset details

The following sample script lists backupset information for Transact-SQL snapshot backups.

```sql
SELECT database_name,
    type,
    backup_size,
    backup_start_date,
    backup_finish_date,
    is_snapshot
FROM msdb.dbo.backupset
WHERE is_snapshot = 1;
```

### S. Check if a database was suspended for snapshot backup

The following sample script outputs database level properties for databases that are suspended for snapshot backup.

```sql
SELECT SERVERPROPERTY('SuspendedDatabaseCount');
SELECT SERVERPROPERTY('IsServerSuspendedForSnapshotBackup');
SELECT DATABASEPROPERTYEX('db1', 'IsDatabaseSuspendedForSnapshotBackup');
```

### T. Sample T-SQL troubleshooting script

The following sample script detects suspended databases on the server, and unsuspends them if necessary.

```sql
IF (SERVERPROPERTY('IsServerSuspendedForSnapshotBackup') = 1)
BEGIN
    --full server suspended, requires server level thaw
    PRINT 'Full server is suspended, requires server level thaw'

    ALTER SERVER CONFIGURATION
    SET SUSPEND_FOR_SNAPSHOT_BACKUP = OFF
END
ELSE
BEGIN
    IF (SERVERPROPERTY('SuspendedDatabaseCount') > 0)
    BEGIN
        DECLARE @curdb SYSNAME
        DECLARE @sql NVARCHAR(500)

        DECLARE mycursor CURSOR FAST_FORWARD
        FOR
        SELECT db_name
        FROM sys.dm_server_suspend_status;

        OPEN mycursor

        FETCH NEXT
        FROM mycursor
        INTO @curdb

        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT 'unfreezing DB ' + @curdb

            SET @sql = 'ALTER DATABASE ' + @curdb + ' SET SUSPEND_FOR_SNAPSHOT_BACKUP = OFF'

            EXEC sp_executesql @SQL

            FETCH NEXT
            FROM mycursor
            INTO @curdb
        END

        PRINT 'All DB unfrozen'

        CLOSE mycursor;

        DEALLOCATE mycursor;
    END
    ELSE
        -- no suspended database, thus no user action needed.
        PRINT 'No database/server is suspended for snapshot backup'
END
```

## Related content

- [backupmediaset (Transact-SQL)](../system-tables/backupmediaset-transact-sql.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
