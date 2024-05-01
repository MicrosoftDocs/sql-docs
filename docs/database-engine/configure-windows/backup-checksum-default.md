---
title: "Configure the backup checksum default (server configuration option)"
description: Find out about the backup checksum default option. See how to use it to turn backup checksum on or off during SQL Server backup and restore operations.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/28/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Configure the backup checksum default (server configuration option)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the backup checksum default setting to enable or disable backup checksum during all backup and restore operations at the instance level.

To configure checking for errors for individual backup or restore operations, see [Enable or disable backup checksums during backup or restore (SQL Server)](../../relational-databases/backup-restore/enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md).

The following table describes the valid values:

| Value | Meaning |
| --- | --- |
| `0` (default) | Disabled |
| `1` | Enabled |

To enable backup checksum for all backup and restore operations at the instance level, run the following command:

```sql
EXEC sp_configure 'backup checksum default', 1;
RECONFIGURE;
```

The setting takes effect immediately.

## Usage scenarios

You can use the backup checksum default setting to provide error-management options (`CHECKSUM` and `NO_CHECKSUM`) when using backup applications or utilities that don't natively expose these options. You might also use this option when you use utilities such as [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] log shipping or the Backup database task from [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] maintenance plans. These utilities and the associated Transact-SQL stored procedures don't provide an option to include the `CHECKSUM` option during backup.

## More information

If the page checksum validation fails during the backup operation, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] stops the backup operation and reports error message 3043. For more information on the error and troubleshooting steps, see the error page for [MSSQLSERVER_3043](../../relational-databases/errors-events/mssqlserver-3043-database-engine-error.md).

When you use the explicit `NO_CHECKSUM` option in the `BACKUP` command, the backup checksum default server option is overridden.

To determine whether checksum was being used during a backup to protect a backup set, use one of the following methods:

- The `HasBackupChecksums` flag in the output of the `RESTORE HEADERONLY` command. For example:

  ```sql
  RESTORE headeronly FROM disk = 'c:\temp\master.bak'
  ```

- The `has_backup_checksums` column in the `backupset` system table in the `msdb` database. For example:

  ```sql
  SELECT has_backup_checksums, database_name, *
  FROM msdb..backupset
  ```

If the backup is performed by using the `CHECKSUM` option, the restore operation automatically performs the validation, and then displays error message 3183. For more information on the error and troubleshooting steps, see the error page for [MSSQLSERVER_3183](../../relational-databases/errors-events/mssqlserver-3183-database-engine-error.md).

## SQL Server 2012 and earlier versions

In [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and earlier versions, this option doesn't exist. You need to use Trace Flag 3023 to enable the `CHECKSUM` option as a default for the `BACKUP` command. Trace Flag 3023 can be used dynamically by using a `DBCC TRACEON` statement, or it can be used as a startup parameter.

### Dynamic usage

```sql
DBCC TRACEON(3023, -1);
BACKUP DATABASE...;
DBCC TRACEOFF(3023, -1);
```

### Startup parameter usage

Add the trace flag as a startup parameter to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] (`-T3023`), and then stop and restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service.

## Related content

- [Enable or disable backup checksums during backup or restore (SQL Server)](../../relational-databases/backup-restore/enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)
- [sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
