---
title: "sys.dm_db_file_space_usage (Transact-SQL)"
description: sys.dm_db_file_space_usage (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/23/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_file_space_usage"
  - "sys.dm_db_file_space_usage_TSQL"
  - "sys.dm_db_file_space_usage"
  - "dm_db_file_space_usage_TSQL"
helpviewer_keywords:
  - "sys.dm_db_file_space_usage dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_file_space_usage (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns space usage information for each data file in the database.

> [!NOTE]  
> To call this from [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_db_file_space_usage`.  [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

| Column name | Data type | Description |
| --- | --- | --- |
| `database_id` | **smallint** | Database ID.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `file_id` | **smallint** | File ID.<br /><br />`file_id` maps to `file_id` in [sys.dm_io_virtual_file_stats](sys-dm-io-virtual-file-stats-transact-sql.md) and to fileid in [sys.sysfiles](../../relational-databases/system-compatibility-views/sys-sysfiles-transact-sql.md). |
| `filegroup_id` | **smallint** | **Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions.<br /><br />Filegroup ID. |
| `total_page_count` | **bigint** | **Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions.<br /><br />Total number of pages in the data file. |
| `allocated_extent_page_count` | **bigint** | **Applies to**: [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions.<br /><br />Total number of pages in the allocated extents in the data file. |
| `unallocated_extent_page_count` | **bigint** | Total number of pages in the unallocated extents in the data file.<br /><br />Unused pages in allocated extents aren't included. |
| `version_store_reserved_page_count` | **bigint** | Total number of pages in the uniform extents allocated for the version store. Version store pages are never allocated from mixed extents.<br /><br />IAM pages aren't included, because they are always allocated from mixed extents. PFS pages are included if they are allocated from a uniform extent.<br /><br />For more information, see [sys.dm_tran_version_store (Transact-SQL)](sys-dm-tran-version-store-transact-sql.md). |
| `user_object_reserved_page_count` | **bigint** | Total number of pages allocated from uniform extents for user objects in the database. Unused pages from an allocated extent are included in the count.<br /><br />IAM pages aren't included, because they are always allocated from mixed extents. PFS pages are included if they are allocated from a uniform extent.<br /><br />You can use the `total_pages` column in the [sys.allocation_units](../../relational-databases/system-catalog-views/sys-allocation-units-transact-sql.md) catalog view to return the reserved page count of each allocation unit in the user object. However, the `total_pages` column includes IAM pages. |
| `internal_object_reserved_page_count` | **bigint** | Total number of pages in uniform extents allocated for internal objects in the file. Unused pages from an allocated extent are included in the count.<br /><br />IAM pages aren't included, because they are always allocated from mixed extents. PFS pages are included if they are allocated from a uniform extent.<br /><br />There is no catalog view or dynamic management object that returns the page count of each internal object. |
| `mixed_extent_page_count` | **bigint** | Total number of allocated and unallocated pages in allocated mixed extents in the file. Mixed extents contain pages allocated to different objects. This count does include all the IAM pages in the file. |
| `modified_extent_page_count` | **bigint** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and later versions.<br /><br />Total number of pages modified in allocated extents of the file since last full database backup. The modified page count can be used to track the number of differential changes in the database since last full backup, to decide if differential backup is needed. |
| `pdw_node_id` | **int** | **Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]<br /><br />The identifier for the node that this distribution is on. |
| `distribution_id` | **int** | **Applies to**: [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], [!INCLUDE [ssPDW](../../includes/sspdw-md.md)]<br /><br />The unique numeric ID associated with the distribution. |

## Remarks

Page counts are always at the extent level. Therefore, page count values are always a multiple of eight. The extents that contain Global Allocation Map (GAM) and Shared Global Allocation Map (SGAM) allocation pages are allocated uniform extents. They aren't included in the previously described page counts. For more information about pages and extents, see [Pages and Extents Architecture Guide](../../relational-databases/pages-and-extents-architecture-guide.md).

The content of the current version store is in [sys.dm_tran_version_store](sys-dm-tran-version-store-transact-sql.md). Version store pages are tracked at the file level instead of the session and task level, because they are global resources. A session may generate versions, but the versions can't be removed when the session ends. Version store cleanup must consider the longest running transaction that needs access to the particular version. The longest running transaction related to version store clean-up can be discovered by viewing the elapsed_time_seconds column in [sys.dm_tran_active_snapshot_database_transactions](sys-dm-tran-active-snapshot-database-transactions-transact-sql.md).

Frequent changes in the `mixed_extent_page_count` column may indicate heavy use of SGAM pages. When this occurs, you may see many PAGELATCH_UP waits in which the wait resource is an SGAM page. For more information, see [sys.dm_os_waiting_tasks (Transact-SQL)](sys-dm-os-waiting-tasks-transact-sql.md), [sys.dm_os_wait_stats (Transact-SQL)](sys-dm-os-wait-stats-transact-sql.md), and [sys.dm_os_latch_stats (Transact-SQL)](sys-dm-os-latch-stats-transact-sql.md).

## User objects

The following objects are included in the user object page counters:

- User-defined tables and indexes
- System tables and indexes
- Global temporary tables and indexes
- Local temporary tables and indexes
- Table variables
- Tables returned in the table-valued functions

## Internal objects

Internal objects are only in `tempdb`. The following objects are included in the internal object page counters:

- Work tables for cursor or spool operations and temporary large object (LOB) storage
- Work files for operations such as a hash join
- Sort runs

## Relationship cardinalities

| From | To | Relationship |
| --- | --- | --- |
| `sys.dm_db_file_space_usage.database_id`, `file_id` | `sys.dm_io_virtual_file_stats.database_id`, `file_id` | One-to-one |

## Permissions

On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions, and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, requires VIEW SERVER PERFORMANCE STATE permission on the server.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.

## Examples

### Determine the amount of free space in `tempdb`

The following query returns the total number of free pages and total free space in megabytes (MB) available in all data files in `tempdb`.

```sql
USE tempdb;
GO

SELECT
    SUM(unallocated_extent_page_count) AS [free pages],
    (SUM(unallocated_extent_page_count) * 1.0 / 128) AS [free space in MB]
FROM sys.dm_db_file_space_usage;
```

### Determine the amount of space used by user objects

The following query returns the total number of pages used by user objects and the total space used by user objects in `tempdb`.

```sql
USE tempdb;
GO

SELECT
    SUM(user_object_reserved_page_count) AS [user object pages used],
    (SUM(user_object_reserved_page_count) * 1.0 / 128) AS [user object space in MB]
FROM sys.dm_db_file_space_usage;
```

## See also

- [Dynamic Management Views and Functions (Transact-SQL)](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [Database Related Dynamic Management Views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
- [sys.dm_db_task_space_usage (Transact-SQL)](sys-dm-db-task-space-usage-transact-sql.md)
- [sys.dm_db_session_space_usage (Transact-SQL)](sys-dm-db-session-space-usage-transact-sql.md)
