---
title: "sys.dm_db_log_stats (Transact-SQL)"
description: sys.dm_db_log_stats returns summary level attributes and information on transaction log files of databases.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/28/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_db_log_stats_TSQL"
  - "sys.dm_db_log_stats"
  - "sys.dm_db_log_stats_TSQL"
  - "dm_db_log_stats"
helpviewer_keywords:
  - "sys.dm_db_log_stats dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_db_log_stats (Transact-SQL)

[!INCLUDE [sqlserver2016sp2-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016sp2-asdb-asdbmi-fabricsqldb.md)]

The `sys.dm_db_log_stats` dynamic management function returns summary level attributes and information on transaction log files of databases. Use this information for monitoring and diagnostics of transaction log health.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.dm_db_log_stats ( database_id )
```

## Arguments

#### { *database_id* | NULL | DEFAULT }

The ID of the database. *database_id* is **int**. Valid inputs are the ID number of a database, `NULL`, or `DEFAULT`. The default value is `NULL`. `NULL` and `DEFAULT` are equivalent values in the context of current database.

You can also specify the built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) for *database_id*.

## Tables returned

| Column name | Data type | Description |
| --- | --- | --- |
| `database_id` | **int** | Database ID.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `recovery_model` | **nvarchar(60)** | Recovery model of the database. Possible values include:<br /><br />`SIMPLE`<br />`BULK_LOGGED`<br />`FULL` |
| `log_min_lsn` <sup>1</sup> | **nvarchar(24)** | Current start log sequence number (LSN) in the transaction log. |
| `log_end_lsn` <sup>1</sup> | **nvarchar(24)** | log sequence number (LSN) of the last log record in the transaction log. |
| `current_vlf_sequence_number` <sup>2</sup> | **bigint** | Current virtual log file (VLF) sequence number at the time of execution. |
| `current_vlf_size_mb` <sup>2</sup> | **float** | Current virtual log file (VLF) size in MB. |
| `total_vlf_count` <sup>2</sup> | **bigint** | Total number of virtual log files (VLFs) in the transaction log. |
| `total_log_size_mb` | **float** | Total transaction log size in MB. |
| `active_vlf_count` <sup>2</sup> | **bigint** | Total number of active virtual log files (VLFs) in the transaction log. |
| `active_log_size_mb` | **float** | Total active transaction log size in MB. |
| `log_truncation_holdup_reason` | **nvarchar(60)** | Log truncation holdup reason. The value is same as `log_reuse_wait_desc` column of `sys.databases`. (For more detailed explanations of these values, see [The transaction log](../logs/the-transaction-log-sql-server.md)).<br />Possible values include:<br /><br />`NOTHING`<br />`CHECKPOINT`<br />`LOG_BACKUP`<br />`ACTIVE_BACKUP_OR_RESTORE`<br />`ACTIVE_TRANSACTION`<br />`DATABASE_MIRRORING`<br />`REPLICATION`<br />`DATABASE_SNAPSHOT_CREATION`<br />`LOG_SCAN`<br />`AVAILABILITY_REPLICA`<br />`OLDEST_PAGE`<br />`XTP_CHECKPOINT`<br />`OTHER TRANSIENT` |
| `log_backup_time` | **datetime** | Last transaction log backup start time. |
| `log_backup_lsn` <sup>1</sup> | **nvarchar(24)** | Last transaction log backup log sequence number (LSN). |
| `log_since_last_log_backup_mb`&nbsp;<sup>1</sup> | **float** | Log size in MB since last transaction log backup log sequence number (LSN). |
| `log_checkpoint_lsn` <sup>1</sup> | **nvarchar(24)** | Last checkpoint log sequence number (LSN). |
| `log_since_last_checkpoint_mb` <sup>1</sup> | **float** | Log size in MB since last checkpoint log sequence number (LSN). |
| `log_recovery_lsn` <sup>1</sup> | **nvarchar(24)** | Recovery log sequence number (LSN) of the database. If `log_recovery_lsn` occurs before the checkpoint LSN, `log_recovery_lsn` is the oldest active transaction LSN, otherwise `log_recovery_lsn` is the checkpoint LSN. |
| `log_recovery_size_mb` <sup>1</sup> | **float** | Log size in MB since log recovery log sequence number (LSN). |
| `recovery_vlf_count` <sup>2</sup> | **bigint** | Total number of virtual log files (VLFs) to be recovered, if there was failover or server restart. |

<sup>1</sup> For more information, see [log sequence number (LSN)](../sql-server-transaction-log-architecture-and-management-guide.md#transaction-log-logical-architecture).

<sup>2</sup> For more information, see [virtual log file (VLF)](../sql-server-transaction-log-architecture-and-management-guide.md#transaction-log-physical-architecture).

## Remarks

When you run `sys.dm_db_log_stats` against a database that's participating in an availability group as a secondary replica, the query returns only a subset of the fields described in the previous table. Currently, the query returns only `database_id`, `recovery_model`, and `log_backup_time` when run against a secondary database.

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require the `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require the `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Examples

### A. Determine databases in a SQL Server instance with high number of VLFs

The following query returns the databases with more than 100 VLFs in the log files. Large numbers of VLFs can affect the database startup, restore, and recovery time.

```sql
SELECT name AS 'Database Name',
       total_vlf_count AS 'VLF count'
FROM sys.databases AS s
    CROSS APPLY sys.dm_db_log_stats(s.database_id)
WHERE total_vlf_count > 100;
```

### B. Determine databases in a SQL Server instance with transaction log backups older than 4 hours

The following query determines the last log backup start times for the databases in the instance.

```sql
SELECT name AS 'Database Name',
       log_backup_time AS 'last log backup start time'
FROM sys.databases AS s
    CROSS APPLY sys.dm_db_log_stats(s.database_id);
```

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [Database related dynamic management views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
- [sys.dm_db_log_space_usage](sys-dm-db-log-space-usage-transact-sql.md)
- [sys.dm_db_log_info](sys-dm-db-log-info-transact-sql.md)
