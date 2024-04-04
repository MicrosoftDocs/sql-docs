---
title: "sys.sp_xtp_force_gc (Transact-SQL)"
description: "Manually release memory related to deleted rows of in-memory data that are eligible for garbage collection."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/27/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_xtp_force_gc_TSQL"
  - "sys.sp_xtp_force_gc"
helpviewer_keywords:
  - "sys.sp_xtp_force_gc"
dev_langs:
  - "TSQL"
---
# sys.sp_xtp_force_gc (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Causes the in-memory engine to release memory related to deleted rows of in-memory data that are eligible for garbage collection, which haven't yet been released by the process.

In cases where a large volume of in-memory data has been released, and where the memory isn't soon be needed for other in-memory data, this procedure can free up memory for other uses. If you anticipate the memory being used soon for other in-memory data, freeing it here would only cause extra overhead, as it would need to be reallocated for the new data.

For more information on Memory-optimized TempDB metadata out of memory errors, see [Memory-optimized tempdb metadata (HkTempDB) out of memory errors](/troubleshoot/sql/admin/memory-optimized-tempdb-out-of-memory).

The `sys.sp_xtp_force_gc` system stored procedure was introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] [CU 1](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate1#2087479) and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] [CU 13](https://support.microsoft.com/topic/kb5005679-cumulative-update-13-for-sql-server-2019-5c1be850-460a-4be4-a569-fe11f0adc535). This stored procedure isn't currently supported on [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_xtp_force_gc
    [ [ @dbname = ] 'database_name' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] '*database_name*'

The database to release unused memory for memory-optimized tables. *@dbname* is **sysname**.

- When the *@dname* parameter isn't specified, only system-level memory structures in the instance are considered.

- When the *@dname* parameter provided is `tempdb`, the memory structures related to [memory-optimized tempdb metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata) are affected.

- When the *@dname* parameter provided is a user database, the memory structures related memory-optimized tables are affected.

Therefore, you may expect to see different results when executing `sys.sp_xtp_force_gc`: without a parameter, with `@dbname = N'tempdb'`, or with `@dbname =` a user database name.

## Return code values

`0` for success. Nonzero for failure.

## Permissions

Requires membership in the **db_owner** fixed database role.

## Remarks

Memory-optimized garbage collection happens normally and automatically in response to memory pressure. You can manually trigger garbage collection with `sys.sp_xtp_force_gc`. You can observe the reduction in memory cleanup in [sys.dm_xtp_system_memory_consumers](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md). In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the `sys.dm_xtp_system_memory_consumers` dynamic management view has improved insights specific to [memory-optimized tempdb metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata).

Contrast with [sys.sp_xtp_checkpoint_force_garbage_collection](sys-sp-xtp-checkpoint-force-garbage-collection-transact-sql.md), which marks checkpoint files used in the merge operation with the log sequence number (LSN) after which they aren't needed and can be garbage collected. Also, `sys.sp_xtp_checkpoint_force_garbage_collection` moves the files whose associated LSN is lower than the log truncation point to FILESTREAM garbage collection.

Prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], execute this stored procedure twice.

## Examples

To execute garbage cleanup on system-level memory structures and Memory-optimized TempDB metadata in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]:

```sql
EXEC sys.sp_xtp_force_gc N'tempdb';
GO

EXEC sys.sp_xtp_force_gc;
GO
```

To execute garbage cleanup on system-level memory structures and Memory-optimized TempDB metadata prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]:

```sql
EXEC sys.sp_xtp_force_gc N'tempdb';
GO

EXEC sys.sp_xtp_force_gc N'tempdb';
GO

EXEC sys.sp_xtp_force_gc;
GO

EXEC sys.sp_xtp_force_gc;
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)](sys-sp-xtp-checkpoint-force-garbage-collection-transact-sql.md)
- [sys.dm_xtp_system_memory_consumers (Transact-SQL)](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md)
- [In-Memory OLTP (In-Memory Optimization)](../in-memory-oltp/overview-and-usage-scenarios.md)
- [Memory-optimized tempdb metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata)
- [Memory-optimized tempdb metadata (HkTempDB) out of memory errors](/troubleshoot/sql/admin/memory-optimized-tempdb-out-of-memory)
