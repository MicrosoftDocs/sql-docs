---
title: "sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)"
description: "Marks source files used in the merge operation with the log sequence number (LSN) after which they aren't needed and can be garbage collected."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/14/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_xtp_checkpoint_force_garbage_collection_TSQL"
  - "sys.sp_xtp_checkpoint_force_garbage_collection"
helpviewer_keywords:
  - "sys.sp_xtp_checkpoint_force_garbage_collection"
dev_langs:
  - "TSQL"
---
# sys.sp_xtp_checkpoint_force_garbage_collection (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Marks source files used in the merge operation with the log sequence number (LSN) after which they aren't needed and can be garbage collected. Also, `sys.sp_xtp_checkpoint_force_garbage_collection` moves the files whose associated LSN is lower than the log truncation point to FILESTREAM garbage collection.

Contrast with [sys.sp_xtp_force_gc](sys-sp-xtp-force-gc-transact-sql.md), which causes the in-memory engine to release memory related to deleted rows of in-memory data that are eligible for garbage collection, which haven't yet been released by the process.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_xtp_checkpoint_force_garbage_collection
    [ [ @dbname = ] 'database_name' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] '*database_name*'

The database to run garbage collection on. The default is the current database. *@dbname* is **sysname**.

## Return code values

`0` for success. Nonzero for failure.

## Result set

A returned row contains the following information:

| Column | Description |
| --- | --- |
| `num_collected_items` | Indicates the number of files that have been moved to FILESTREAM garbage collection. The log sequence number (LSN) of these files is less than the LSN of log truncation point. |
| `num_marked_for_collection_items` | Indicates the number of data/delta files whose LSN has been updated with the log blockID of the end-of-log LSN. |
| `last_collected_xact_seqno` | Returns the last corresponding LSN up to which the files have been moved to FILESTREAM garbage collection. |

## Remarks

You can manually trigger garbage collection with another system stored procedure, `sys.sp_xtp_force_gc`. You can observe the reduction in memory cleanup in [sys.dm_xtp_system_memory_consumers](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md).

In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the [sys.dm_xtp_system_memory_consumers](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md) dynamic management view has improved insights specific to [memory-optimized tempdb metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata).

## Permissions

Requires membership in the **db_owner** fixed database role.

## Examples

To mark unneeded source files for garbage collection in the `tempdb` database, use the following sample script:

```sql
EXEC sys.sp_xtp_checkpoint_force_garbage_collection N'tempdb';
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [sys.sp_xtp_force_gc (Transact-SQL)](sys-sp-xtp-force-gc-transact-sql.md)
- [In-Memory OLTP (In-Memory Optimization)](../in-memory-oltp/overview-and-usage-scenarios.md)
- [sys.dm_xtp_system_memory_consumers](../system-dynamic-management-views/sys-dm-xtp-system-memory-consumers-transact-sql.md)
