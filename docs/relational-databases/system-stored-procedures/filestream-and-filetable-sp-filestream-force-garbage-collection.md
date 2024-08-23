---
title: "sp_filestream_force_garbage_collection (Transact-SQL)"
description: Forces the FILESTREAM garbage collector to run, deleting any unneeded FILESTREAM files.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_filestream_force_garbage_collection"
  - "sp_filestream_force_garbage_collection_TSQL"
helpviewer_keywords:
  - "FILESTREAM [SQL Server]"
  - "sp_filestream_force_garbage_collection"
dev_langs:
  - "TSQL"
---
# sp_filestream_force_garbage_collection (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Forces the FILESTREAM garbage collector (GC) to run, deleting any unneeded FILESTREAM files.

A FILESTREAM container can't be removed until all the deleted files within it are cleaned up by the GC. The FILESTREAM GC runs automatically. However, if you need to remove a container before the GC has run, you can use `sp_filestream_force_garbage_collection` to run the GC manually.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_filestream_force_garbage_collection
    [ @dbname = ] 'database_name'
    [ , [ @filename = ] 'logical_file_name' ]
[ ; ]
```

## Arguments

#### [ @dbname = ] '*database_name*'

Signifies the name of the database to run the GC on.

*@dbname* is **sysname**. If not specified, current database is assumed.

#### [ @filename = ] '*logical_file_name*'

Specifies the logical name of the FILESTREAM container to run the GC on. *@filename* is optional. If no logical filename is specified, the GC cleans all FILESTREAM containers in the specified database.

## Return code values

| Value | Description |
| --- | --- |
| `0` | Operation success |
| `1` | Operation failure |

## Result set

| Value | Description |
| --- | --- |
| `file_name` | Indicates the FILESTREAM container name |
| `num_collected_items` | Indicates the number of FILESTREAM items (files or directories) that have been garbage collected (deleted) in this container. |
| `num_marked_for_collection_items` | Indicates the number of FILESTREAM items (files or directories) that have been marked for GC in this container. These items haven't been deleted yet, but might be eligible for deletion following the GC phase. |
| `num_unprocessed_items` | Indicates the number of eligible FILESTREAM items (files or directories) that weren't processed for GC in this FILESTREAM container. Items might be unprocessed for various reasons, including:<br /><br />- Files that need to be pinned down because a log backup or checkpoint hasn't been taken.<br /><br />- Files in the FULL or BULK_LOGGED recovery model.<br /><br />- There's a long-running active transaction.<br /><br />- The replication log reader job hasn't run. See the white paper [FILESTREAM Storage in SQL Server 2008](/previous-versions/sql/sql-server-2008/hh461480(v=msdn.10)) for more information. |
| `last_collected_xact_seqno` | Returns the last corresponding log sequence number (LSN) up to which the files have been garbage collected for the specified FILESTREAM container. |

## Remarks

Explicitly runs the FILESTREAM garbage collection task to completion on the requested database (and FILESTREAM container). The GC process removes files that are no longer needed. The time needed for this operation to complete depends on the size of the FILESTREAM data in that database or container, and the amount of DML activity that recently occurred on the FILESTREAM data. Though this operation can be run with the database online, this might affect the performance of the database during its run due to various I/O activities done by the GC process.

> [!NOTE]  
> It's recommended that this operation be run only when necessary and outside usual operation hours.

Multiple invocations of this stored procedure can be run simultaneously only on separate containers or separate databases.

Owing to two-phase operations, the stored procedure should be run twice to actually delete underlying FILESTREAM files.

Garbage collection relies on log truncation. Therefore, if files were deleted recently on a database using the full recovery model, they are garbage-collected only after a log backup of those transaction log portions is taken, and the log portion is marked inactive. On a database using the simple recovery model, a log truncation occurs after a `CHECKPOINT` has been issued against the database.

## Permissions

Requires membership in the **db_owner** database role.

## Examples

The following examples run the GC for FILESTREAM containers in the `fsdb` database.

### A. Specify no container

```sql
USE fsdb;
GO
EXEC sp_filestream_force_garbage_collection @dbname = N'fsdb';
```

### B. Specify a container

```sql
USE fsdb;
GO
EXEC sp_filestream_force_garbage_collection @dbname = N'fsdb',
    @filename = N'FSContainer';
```

## Related content

- [FILESTREAM (SQL Server)](../blob/filestream-sql-server.md)
- [FileTables (SQL Server)](../blob/filetables-sql-server.md)
- [FILESTREAM and FileTable dynamic management views (Transact-SQL)](../system-dynamic-management-views/filestream-and-filetable-dynamic-management-views-transact-sql.md)
- [FILESTREAM and FileTable catalog views (Transact-SQL)](../system-catalog-views/filestream-and-filetable-catalog-views-transact-sql.md)
- [sp_kill_filestream_non_transacted_handles (Transact-SQL)](filestream-and-filetable-sp-kill-filestream-non-transacted-handles.md)
