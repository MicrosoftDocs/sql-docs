---
title: "sys.dm_io_virtual_file_stats (Transact-SQL)"
description: sys.dm_io_virtual_file_stats returns I/O statistics for data and log files.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_io_virtual_file_stats"
  - "sys.dm_io_virtual_file_stats_TSQL"
  - "sys.dm_io_virtual_file_stats"
  - "dm_io_virtual_file_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_io_virtual_file_stats dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=aps-pdw-2016 || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_io_virtual_file_stats (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

Returns I/O statistics for data and log files. This dynamic management function replaces the [fn_virtualfilestats](../system-functions/sys-fn-virtualfilestats-transact-sql.md) function.

> [!NOTE]  
> To call this dynamic management view (DMV) from [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], use the name `sys.dm_pdw_nodes_io_virtual_file_stats` [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Syntax

Syntax for SQL Server and Azure SQL Database:

```syntaxsql
sys.dm_io_virtual_file_stats (
    { database_id | NULL } ,
    { file_id | NULL }
)
```

Syntax for Azure Synapse Analytics:

```syntaxsql
sys.dm_pdw_nodes_io_virtual_file_stats
```

## Arguments

#### *database_id* | NULL

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, Azure SQL Database

ID of the database. *database_id* is int, with no default. Valid inputs are the ID number of a database or `NULL`. When `NULL` is specified, all databases in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are returned.

The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified.

#### *file_id* | NULL

**Applies to**: [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and later, Azure SQL Database

ID of the file. *file_id* is int, with no default. Valid inputs are the ID number of a file or `NULL`. When `NULL` is specified, all files on the database are returned.

The built-in function [FILE_IDEX](../../t-sql/functions/file-idex-transact-sql.md) can be specified, and refers to a file in the current database.

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| `database_name` | **sysname** | Database name.<br /><br />For Azure Synapse Analytics, this is the name of the database stored on the node identified by `pdw_node_id`. Each node has one `tempdb` database that has 13 files. Each node also has one database per distribution, and each distribution database has five files. For example, if each node contains four distributions, the results show 20 distribution database files per `pdw_node_id`.<br /><br />**Does not apply to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `database_id` | **smallint** | ID of database.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `file_id` | **smallint** | ID of file. |
| `sample_ms` | **bigint** | Number of milliseconds since the computer was started. This column can be used to compare different outputs from this function.<br /><br />The data type is **int** for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and earlier versions. In these versions, the value will reset to 0 after approximately 25 days of continuous database engine uptime. |
| `num_of_reads` | **bigint** | Number of reads issued on the file. |
| `num_of_bytes_read` | **bigint** | Total number of bytes read on this file. |
| `io_stall_read_ms` | **bigint** | Total time, in milliseconds, that the users waited for reads issued on the file. |
| `num_of_writes` | **bigint** | Number of writes made on this file. |
| `num_of_bytes_written` | **bigint** | Total number of bytes written to the file. |
| `io_stall_write_ms` | **bigint** | Total time, in milliseconds, that users waited for writes to be completed on the file. |
| `io_stall` | **bigint** | Total time, in milliseconds, that users waited for I/O to be completed on the file. |
| `size_on_disk_bytes` | **bigint** | Number of bytes used on the disk for this file. For sparse files, this number is the actual number of bytes on the disk that are used for database snapshots. |
| `file_handle` | **varbinary** | Windows file handle for this file. |
| `io_stall_queued_read_ms` | **bigint** | Total IO latency introduced by IO resource governance for reads. Not nullable. For more information, see [sys.dm_resource_governor_resource_pools](sys-dm-resource-governor-resource-pools-transact-sql.md).<br /><br />**Does not apply to**: [!INCLUDE [ssSQL12](../../includes/sssql11-md.md)] and earlier versions. |
| `io_stall_queued_write_ms` | **bigint** | Total IO latency introduced by IO resource governance for writes. Not nullable.<br /><br />**Does not apply to**: [!INCLUDE [ssSQL12](../../includes/sssql11-md.md)] and earlier versions. |
| `pdw_node_id` | **int** | Identifier of the node for the distribution.<br /><br />**Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] |

## Remarks

The counters are initialized to empty whenever the SQL Server (MSSQLSERVER) service is started.

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require `VIEW SERVER STATE` permission.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return statistics for a log file

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]

The following example returns statistics for the log file in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
SELECT *
FROM sys.dm_io_virtual_file_stats(DB_ID(N'AdventureWorks2025'), 2);
```

### B. Return statistics for file in tempdb

**Applies to**: Azure Synapse Analytics

```sql
SELECT *
FROM sys.dm_pdw_nodes_io_virtual_file_stats
WHERE database_name = 'tempdb'
      AND file_id = 2;
```

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
- [I/O Related Dynamic Management Views and Functions (Transact-SQL)](i-o-related-dynamic-management-views-and-functions-transact-sql.md)
- [sys.database_files](../system-catalog-views/sys-database-files-transact-sql.md)
- [sys.master_files](../system-catalog-views/sys-master-files-transact-sql.md)
