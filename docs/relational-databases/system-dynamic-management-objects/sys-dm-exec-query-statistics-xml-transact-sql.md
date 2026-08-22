---
title: "sys.dm_exec_query_statistics_xml (Transact-SQL)"
description: sys.dm_exec_query_statistics_xml returns query execution plan for in-flight requests. Use this DMV to retrieve showplan XML with transient statistics.
author: rwestMSFT
ms.author: randolphwest
ms.date: 03/31/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_exec_query_statistics_xml"
  - "sys.dm_exec_query_statistics_xml_TSQL"
  - "dm_exec_query_statistics_xml_TSQL"
  - "dm_exec_query_statistics_xml"
helpviewer_keywords:
  - "sys.dm_exec_query_statistics_xml management view"
dev_langs:
  - "TSQL"
---
# sys.dm_exec_query_statistics_xml (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

Returns query execution plan for in-flight requests. Use this DMV to retrieve showplan XML with transient statistics.

## Syntax

```syntaxsql
sys.dm_exec_query_statistics_xml(session_id)
```

## Arguments

#### *session_id*

The session ID executing the batch to be looked up. *session_id* is **smallint**. *session_id* can be obtained from the following dynamic management objects:

- [sys.dm_exec_requests](sys-dm-exec-requests-transact-sql.md)
- [sys.dm_exec_sessions](sys-dm-exec-sessions-transact-sql.md)
- [sys.dm_exec_connections](sys-dm-exec-connections-transact-sql.md)

## Table returned

| Column Name | Data Type | Description |
| --- | --- | --- |
| `session_id` | **smallint** | ID of the session. Not nullable. |
| `request_id` | **int** | ID of the request. Not nullable. |
| `sql_handle` | **varbinary(64)** | A token that uniquely identifies the batch or stored procedure that the query is part of. Nullable. |
| `plan_handle` | **varbinary(64)** | A token that uniquely identifies a query execution plan for a batch that is currently executing. Nullable. |
| `query_plan` | **xml** | Contains the runtime Showplan representation of the query execution plan that is specified with `plan_handle` containing partial statistics. The Showplan is in XML format. One plan is generated for each batch that contains, for example ad hoc [!INCLUDE [tsql](../../includes/tsql-md.md)] statements, stored procedure calls, and user-defined function calls. Nullable. |

## Limitations

Owing to a possible random access violation (AV) while executing a monitoring stored procedure with the `sys.dm_exec_query_statistics_xml` DMV, the Showplan XML attribute `<ParameterList>` value `ParameterRuntimeValue` was removed in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU 26 and [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 12. This value could be useful while troubleshooting long running stored procedures. You can re-enable this value in [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU 31, [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] CU 19, and later versions, using [trace flag 2446](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf2446). This trace flag enables the collection of the runtime parameter value at the cost of introducing extra overhead.

> [!CAUTION]  
> Trace flag 2446 isn't meant to be enabled continuously in a production environment, but only for time-limited troubleshooting purposes. Using this trace flag introduces extra and possibly significant CPU and memory overhead, as it creates a Showplan XML fragment with runtime parameter information, whether the `sys.dm_exec_query_statistics_xml` DMV is called or not.

In [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], [!INCLUDE [Azure SQL Database](../../includes/ssazure-sqldb.md)], and [!INCLUDE [Azure SQL Managed Instance](../../includes/ssazuremi-md.md)], you can accomplish the same functionality at the database level using the `FORCE_SHOWPLAN_RUNTIME_PARAMETER_COLLECTION` option in [ALTER DATABASE SCOPED CONFIGURATION (Transact-SQL)](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

## Remarks

This system function is available starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 1. For more information, see [KB 3190871](https://support.microsoft.com/help/3190871).

This system function works under both **standard** and **lightweight** query execution statistics profiling infrastructure. For more information, see [Query Profiling Infrastructure](../performance/query-profiling-infrastructure.md).

Under the following conditions, no Showplan output is returned in the `query_plan` column of the returned table for `sys.dm_exec_query_statistics_xml`:

- If the query plan that corresponds to the specified *session_id* is no longer executing, the `query_plan` column of the returned table is null. For example, this condition might occur if there's a time delay between when the plan handle was captured and when it was used with `sys.dm_exec_query_statistics_xml`

Due to a limitation in the number of nested levels allowed in the **xml** data type, `sys.dm_exec_query_statistics_xml` can't return query plans that meet or exceed 128 levels of nested elements. In earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this condition prevented the query plan from returning and generates error 6335. In [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] Service Pack 2 and later versions, the `query_plan` column returns `NULL`.

## Permissions

Requires `VIEW SERVER STATE` permission on the server, in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions.

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server, in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

Requires the `VIEW DATABASE STATE` permission in the database, on [!INCLUDE [ssSDS](../../includes/sssds-md.md)] Premium Tiers.

Requires the **Server admin** or a [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account on [!INCLUDE [ssSDS](../../includes/sssds-md.md)] Standard and Basic Tiers.

## Examples

### A. Look at live query plan and execution statistics for a running batch

The following example queries `sys.dm_exec_requests` to find the interesting query and copy its `session_id` from the output.

```sql
SELECT *
FROM sys.dm_exec_requests;
GO
```

Then, to obtain the live query plan and execution statistics, use the copied `session_id` with system function `sys.dm_exec_query_statistics_xml`. Run this query in a different session than the session in which your query is running.

```sql
SELECT * FROM sys.dm_exec_query_statistics_xml(< copied session_id >);
GO
```

Or, combined for all running requests. Run this query in a different session than the session in which your query is running.

```sql
SELECT eqs.query_plan,
       er.session_id,
       er.request_id,
       er.database_id,
       er.start_time,
       er.[status],
       er.wait_type,
       er.wait_resource,
       er.last_wait_type,
       (er.cpu_time / 1000) AS cpu_time_sec,
       (er.total_elapsed_time / 1000) / 60 AS elapsed_time_minutes,
       (er.logical_reads * 8) / 1024 AS logical_reads_KB,
       er.granted_query_memory,
       er.dop,
       er.row_count,
       er.query_hash,
       er.query_plan_hash
FROM sys.dm_exec_requests AS er
CROSS APPLY sys.dm_exec_query_statistics_xml(session_id) AS eqs
WHERE er.session_id <> @@SPID;
GO
```

## Related content

- [Set trace flags with DBCC TRACEON (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Database related dynamic management views (Transact-SQL)](database-related-dynamic-management-views-transact-sql.md)
