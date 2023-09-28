---
title: "Tail-log backups (SQL Server)"
description: In SQL Server, a tail-log backup captures any log records that haven't yet been backed up to prevent data loss and to keep the log chain intact.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/28/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "backing up [SQL Server], tail of log"
  - "transaction log backups [SQL Server], tail-log backups"
  - "NORECOVERY clause"
  - "NO_TRUNCATE clause"
  - "backups [SQL Server], log backups"
  - "tail-log backups"
  - "backups [SQL Server], tail-log backups"
---
# Tail-log backups (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article is relevant only for backup and restore of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases that are using the full or bulk-logged recovery models.

A *tail-log backup* captures any log records that haven't yet been backed up (the *tail of the log*), to prevent work loss and to keep the log chain intact. Before you can recover a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database to its latest point in time, you must back up the tail of its transaction log. The tail-log backup is the last backup of interest in the recovery plan for the database.

Not all restore scenarios require a tail-log backup. You don't need a tail-log backup if the recovery point is contained in an earlier log backup. A tail-log backup is unnecessary if you're moving or replacing (overwriting) a database, and don't need to restore it to a point of time after its most recent backup.

## <a id="TailLogScenarios"></a> Scenarios that require a tail-log backup

We recommend that you take a tail-log backup in the following scenarios:

- If the database is online and you plan to perform a restore operation on the database, begin by backing up the tail of the log. To avoid an error for an online database, you must use the `WITH NORECOVERY` option of the [BACKUP](../../t-sql/statements/backup-transact-sql.md) [!INCLUDE [tsql](../../includes/tsql-md.md)] statement.

- If a database is offline and fails to start and you need to restore the database, first back up the tail of the log. Because no transactions can occur at this time, use the `WITH NO_TRUNCATE` option. `NO_TRUNCATE` is effectively the same as a [copy-only transaction log backup](copy-only-backups-sql-server.md). Using `WITH NORECOVERY` is optional, because no transactions can occur at this time.

- If a database is damaged, try to take a tail-log backup by using the `WITH CONTINUE_AFTER_ERROR` option of the `BACKUP` statement.

  On a damaged database, backing up the tail of the log can succeed only if the log files are undamaged, the database is in a state that supports tail-log backups, and the database doesn't contain any bulk-logged changes. If a tail-log backup can't be created, any transactions committed after the latest log backup are lost.

The following table summarizes the `NORECOVERY`, `NO_TRUNCATE`, and `CONTINUE_AFTER_ERROR` options for `BACKUP`.

| BACKUP LOG option | Comments |
| --- | --- |
| `NORECOVERY` | Use `NORECOVERY` whenever you intend to continue with a restore operation on the database. `NORECOVERY` takes the database into the restoring state. This step guarantees that the database doesn't change after the tail-log backup. The log is truncated unless the `NO_TRUNCATE` option or `COPY_ONLY` option is also specified.<br /><br />**Important:** Avoid using `NO_TRUNCATE`, except when the database is damaged or offline. You may need to put the database into [single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md) to obtain exclusive access before performing the restore with `NORECOVERY`. After the restore, set the database back to multi-user mode. |
| `CONTINUE_AFTER_ERROR` | Use `CONTINUE_AFTER_ERROR` only if you're backing up the tail of a damaged database.<br /><br />When you back up the tail of the log of a damaged database, some of the metadata ordinarily captured in log backups might be unavailable. For more information, see the next section. |

## <a id="IncompleteMetadata"></a> Tail-log backups that have incomplete backup metadata

Tail log backups capture the tail of the log even if the database is offline, damaged, or missing data files. This might cause incomplete metadata from the restore information commands and `msdb`. However, only the metadata is incomplete; the captured log is complete and usable.

If a tail-log backup has incomplete metadata, in the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) table, `has_incomplete_metadata` is set to `1`. Also, in the output of [RESTORE HEADERONLY](../../t-sql/statements/restore-statements-headeronly-transact-sql.md), `HasIncompleteMetadata` is set to `1`.

If the metadata in a tail-log backup is incomplete, the [backupfilegroup](../../relational-databases/system-tables/backupfilegroup-transact-sql.md) table is missing most of the information about filegroups at the time of the tail-log backup. Most of the `backupfilegroup` table columns are `NULL`; the only meaningful columns are as follows:

- `backup_set_id`
- `filegroup_id`
- `type`
- `type_desc`
- `is_readonly`

## <a id="RelatedTasks"></a> Related tasks

To create a tail-log backup, see [Back Up the Transaction Log When the Database Is Damaged (SQL Server)](back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md).

To restore a transaction log backup, see [Restore a Transaction Log Backup (SQL Server)](restore-a-transaction-log-backup-sql-server.md).

## Related content

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Back Up and Restore of SQL Server Databases](back-up-and-restore-of-sql-server-databases.md)
- [Copy-only backups](copy-only-backups-sql-server.md)
- [Transaction Log Backups (SQL Server)](transaction-log-backups-sql-server.md)
- [Apply Transaction Log Backups (SQL Server)](apply-transaction-log-backups-sql-server.md)
- [SQL Server transaction log architecture and management guide](../sql-server-transaction-log-architecture-and-management-guide.md)
