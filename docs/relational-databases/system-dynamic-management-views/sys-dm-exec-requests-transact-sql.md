---
title: "sys.dm_exec_requests (Transact-SQL)"
description: sys.dm_exec_requests returns information about each request that is executing in the Database Engine.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mikeray, randolphwest, derekw
ms.date: 10/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_exec_requests_TSQL"
  - "sys.dm_exec_requests"
  - "dm_exec_requests_TSQL"
  - "dm_exec_requests"
helpviewer_keywords:
  - "sys.dm_exec_requests dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =azure-sqldw-latest || =fabric"
---
# sys.dm_exec_requests (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns information about each request that is executing in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For more information about requests, see the [Thread and Task Architecture Guide](../thread-and-task-architecture-guide.md).

> [!NOTE]  
> To call this from dedicated SQL pool in [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] or [!INCLUDE [ssPDW](../../includes/sspdw-md.md)], see [sys.dm_pdw_exec_requests (Transact-SQL)](sys-dm-pdw-exec-requests-transact-sql.md). For serverless SQL pool or [!INCLUDE [fabric](../../includes/fabric.md)] use `sys.dm_exec_requests`.

| Column name | Data type | Description |
| --- | --- | --- |
| `session_id` | **smallint** | ID of the session to which this request is related. Not nullable. |
| `request_id` | **int** | ID of the request. Unique in the context of the session. Not nullable. |
| `start_time` | **datetime** | Timestamp when the request arrived. Not nullable. |
| `status` | **nvarchar(30)** | Status of the request. Can be one of the following values:<br /><br />background<br />rollback<br />running<br />runnable<br />sleeping<br />suspended<br /><br />Not nullable. |
| `command` | **nvarchar(32)** | Identifies the current type of command that is being processed. Common command types include the following values:<br /><br />SELECT<br />INSERT<br />UPDATE<br />DELETE<br />BACKUP LOG<br />BACKUP DATABASE<br />DBCC<br />FOR<br /><br />The text of the request can be retrieved by using `sys.dm_exec_sql_text` with the corresponding `sql_handle` for the request. Internal system processes set the command based on the type of task they perform. Tasks can include the following values:<br /><br />LOCK MONITOR<br />CHECKPOINTLAZY<br />WRITER<br /><br />Not nullable. |
| `sql_handle` | **varbinary(64)** | Is a token that uniquely identifies the batch or stored procedure that the query is part of. Nullable. |
| `statement_start_offset` | **int** | Indicates, in bytes, beginning with 0, the starting position of the currently executing statement for the currently executing batch or persisted object. Can be used together with the `sql_handle`, the `statement_end_offset`, and the `sys.dm_exec_sql_text` dynamic management function to retrieve the currently executing statement for the request. Nullable. |
| `statement_end_offset` | **int** | Indicates, in bytes, starting with 0, the ending position of the currently executing statement for the currently executing batch or persisted object. Can be used together with the `sql_handle`, the `statement_start_offset`, and the `sys.dm_exec_sql_text` dynamic management function to retrieve the currently executing statement for the request. Nullable. |
| `plan_handle` | **varbinary(64)** | Is a token that uniquely identifies a query execution plan for a batch that is currently executing. Nullable. |
| `database_id` | **smallint** | ID of the database the request is executing against. Not nullable.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `user_id` | **int** | ID of the user who submitted the request. Not nullable. |
| `connection_id` | **uniqueidentifier** | ID of the connection on which the request arrived. Nullable. |
| `blocking_session_id` | **smallint** | ID of the session that is blocking the request. If this column is `NULL` or `0`, the request isn't blocked, or the session information of the blocking session isn't available (or can't be identified). For more information, see [Understand and resolve SQL Server blocking problems](/troubleshoot/sql/performance/understand-resolve-blocking).<br /><br />-2 = The blocking resource is owned by an orphaned distributed transaction.<br /><br />-3 = The blocking resource is owned by a deferred recovery transaction.<br /><br />-4 = `session_id` of the blocking latch owner couldn't be determined at this time because of internal latch state transitions.<br /><br />`-5` = `session_id` of the blocking latch owner couldn't be determined because it isn't tracked for this latch type (for example, for an SH latch).<br /><br />By itself, `blocking_session_id` `-5` doesn't indicate a performance problem. `-5` is an indication that the session is waiting on an asynchronous action to complete. Before `-5` was introduced, the same session would have shown `blocking_session_id` `0`, even though it was still in a wait state.<br /><br />Depending on workload, observing `blocking_session_id = -5` may be a common occurrence. |
| `wait_type` | **nvarchar(60)** | If the request is currently blocked, this column returns the type of wait. Nullable.<br /><br />For information about types of waits, see [sys.dm_os_wait_stats (Transact-SQL)](sys-dm-os-wait-stats-transact-sql.md). |
| `wait_time` | **int** | If the request is currently blocked, this column returns the duration in milliseconds, of the current wait. Not nullable. |
| `last_wait_type` | **nvarchar(60)** | If this request has previously been blocked, this column returns the type of the last wait. Not nullable. |
| `wait_resource` | **nvarchar(256)** | If the request is currently blocked, this column returns the resource for which the request is currently waiting. Not nullable. |
| `open_transaction_count` | **int** | Number of transactions that are open for this request. Not nullable. |
| `open_resultset_count` | **int** | Number of result sets that are open for this request. Not nullable. |
| `transaction_id` | **bigint** | ID of the transaction in which this request executes. Not nullable. |
| `context_info` | **varbinary(128)** | CONTEXT_INFO value of the session. Nullable. |
| `percent_complete` | **real** | Percentage of work completed for the following commands:<br /><br />`ALTER INDEX REORGANIZE`<br />`AUTO_SHRINK` option with `ALTER DATABASE`<br />`BACKUP DATABASE`<br />`DBCC CHECKDB`<br />`DBCC CHECKFILEGROUP`<br />`DBCC CHECKTABLE`<br />`DBCC INDEXDEFRAG`<br />`DBCC SHRINKDATABASE`<br />`DBCC SHRINKFILE`<br />`RECOVERY`<br />`RESTORE DATABASE`<br />`ROLLBACK`<br />`TDE ENCRYPTION`<br /><br />Not nullable. |
| `estimated_completion_time` | **bigint** | Internal only. Not nullable. |
| `cpu_time` | **int** | CPU time in milliseconds that is used by the request. Not nullable. |
| `total_elapsed_time` | **int** | Total time elapsed in milliseconds since the request arrived. Not nullable. |
| `scheduler_id` | **int** | ID of the scheduler that is scheduling this request. Nullable. |
| `task_address` | **varbinary(8)** | Memory address allocated to the task that is associated with this request. Nullable. |
| `reads` | **bigint** | Number of reads performed by this request. Not nullable. |
| `writes` | **bigint** | Number of writes performed by this request. Not nullable. |
| `logical_reads` | **bigint** | Number of logical reads that have been performed by the request. Not nullable. |
| `text_size` | **int** | TEXTSIZE setting for this request. Not nullable. |
| `language` | **nvarchar(128)** | Language setting for the request. Nullable. |
| `date_format` | **nvarchar(3)** | DATEFORMAT setting for the request. Nullable. |
| `date_first` | **smallint** | DATEFIRST setting for the request. Not nullable. |
| `quoted_identifier` | **bit** | 1 = QUOTED_IDENTIFIER is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `arithabort` | **bit** | 1 = ARITHABORT setting is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `ansi_null_dflt_on` | **bit** | 1 = ANSI_NULL_DFLT_ON setting is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `ansi_defaults` | **bit** | 1 = ANSI_DEFAULTS setting is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `ansi_warnings` | **bit** | 1 = ANSI_WARNINGS setting is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `ansi_padding` | **bit** | 1 = ANSI_PADDING setting is ON for the request.<br /><br />Otherwise, it's 0.<br /><br />Not nullable. |
| `ansi_nulls` | **bit** | 1 = ANSI_NULLS setting is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `concat_null_yields_null` | **bit** | 1 = CONCAT_NULL_YIELDS_NULL setting is ON for the request. Otherwise, it's 0.<br /><br />Not nullable. |
| `transaction_isolation_level` | **smallint** | Isolation level with which the transaction for this request is created. Not nullable.<br />0 = Unspecified<br />1 = ReadUncomitted<br />2 = ReadCommitted<br />3 = Repeatable<br />4 = Serializable<br />5 = Snapshot |
| `lock_timeout` | **int** | Lock time-out period in milliseconds for this request. Not nullable. |
| `deadlock_priority` | **int** | DEADLOCK_PRIORITY setting for the request. Not nullable. |
| `row_count` | **bigint** | Number of rows that have been returned to the client by this request. Not nullable. |
| `prev_error` | **int** | Last error that occurred during the execution of the request. Not nullable. |
| `nest_level` | **int** | Current nesting level of code that is executing on the request. Not nullable. |
| `granted_query_memory` | **int** | Number of pages allocated to the execution of a query on the request. Not nullable. |
| `executing_managed_code` | **bit** | Indicates whether a specific request is currently executing common language runtime objects, such as routines, types, and triggers. it's set for the full time a common language runtime object is on the stack, even while running [!INCLUDE [tsql](../../includes/tsql-md.md)] from within common language runtime. Not nullable. |
| `group_id` | **int** | ID of the workload group to which this query belongs. Not nullable. |
| `query_hash` | **binary(8)** | Binary hash value calculated on the query and used to identify queries with similar logic. You can use the query hash to determine the aggregate resource usage for queries that differ only by literal values. |
| `query_plan_hash` | **binary(8)** | Binary hash value calculated on the query execution plan and used to identify similar query execution plans. You can use query plan hash to find the cumulative cost of queries with similar execution plans. |
| `statement_sql_handle` | **varbinary(64)** | **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br />`sql_handle` of the individual query.<br /><br />This column is NULL if Query Store isn't enabled for the database. |
| `statement_context_id` | **bigint** | **Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br />The optional foreign key to `sys.query_context_settings`.<br /><br />This column is NULL if Query Store isn't enabled for the database. |
| `dop` | **int** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later.<br /><br />The degree of parallelism of the query. |
| `parallel_worker_count` | **int** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later.<br /><br />The number of reserved parallel workers if this is a parallel query. |
| `external_script_request_id` | **uniqueidentifier** | **Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later.<br /><br />The external script request ID associated with the current request. |
| `is_resumable` | **bit** | **Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later.<br /><br />Indicates whether the request is a resumable index operation. |
| `page_resource` | **binary(8)** | **Applies to**: [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)]<br /><br />An 8-byte hexadecimal representation of the page resource if the `wait_resource` column contains a page. For more information, see [sys.fn_PageResCracker](../system-functions/sys-fn-pagerescracker-transact-sql.md). |
| `page_server_reads` | **bigint** | **Applies to**: Azure SQL Database Hyperscale<br /><br />Number of page server reads performed by this request. Not nullable. |
| `dist_statement_id` | **uniqueidentifier** | **Applies to**: SQL Server 2022 and later versions, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics (serverless pools only), and Microsoft Fabric<br /><br />Unique ID for the statement for the request submitted. Not nullable. |

## Remarks

To execute code that is outside [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (for example, extended stored procedures and distributed queries), a thread has to execute outside the control of the non-preemptive scheduler. To do this, a worker switches to preemptive mode. Time values returned by this dynamic management view don't include time spent in preemptive mode.

When executing parallel requests in [row mode](../query-processing-architecture-guide.md#row-mode-execution), [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] assigns a worker thread to coordinate the worker threads responsible for completing tasks assigned to them. In this DMV, only the coordinator thread is visible for the request. The columns `reads`, `writes`, `logical_reads`, and `row_count` are **not updated** for the coordinator thread. The columns `wait_type`, `wait_time`, `last_wait_type`, `wait_resource`, and `granted_query_memory` are **only updated** for the coordinator thread. For more information, see the [Thread and Task Architecture Guide](../thread-and-task-architecture-guide.md).

The `wait_resource` column contains similar information to `resource_description` in [sys.dm_tran_locks (Transact-SQL)](sys-dm-tran-locks-transact-sql.md) but is formatted differently.

## Permissions

If the user has `VIEW SERVER STATE` permission on the server, the user sees all executing sessions on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]; otherwise, the user sees only the current session. `VIEW SERVER STATE` can't be granted in Azure SQL Database so `sys.dm_exec_requests` is always limited to the current connection.

In availability group scenarios, if the secondary replica is set to **read-intent only**, the connection to the secondary must specify its application intent in connection string parameters by adding `applicationintent=readonly`. Otherwise, the access check for `sys.dm_exec_requests` doesn't pass for databases in the availability group, even if `VIEW SERVER STATE` permission is present.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, `sys.dm_exec_requests` requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Examples

### A. Find the query text for a running batch

The following example queries `sys.dm_exec_requests` to find the interesting query and copy its `sql_handle` from the output.

```sql
SELECT * FROM sys.dm_exec_requests;
GO
```

Then, to obtain the statement text, use the copied `sql_handle` with system function `sys.dm_exec_sql_text(sql_handle)`.

```sql
SELECT * FROM sys.dm_exec_sql_text(< copied sql_handle >);
GO
```

### B. Find all locks that a running batch is holding

The following example queries `sys.dm_exec_requests` to find the interesting batch and copy its `transaction_id` from the output.

```sql
SELECT * FROM sys.dm_exec_requests;
GO
```

Then, to find lock information, use the copied `transaction_id` with the system function `sys.dm_tran_locks`.

```sql
SELECT * FROM sys.dm_tran_locks
WHERE request_owner_type = N'TRANSACTION'
    AND request_owner_id = < copied transaction_id >;
GO
```

### C. Find all currently blocked requests

The following example queries `sys.dm_exec_requests` to find information about blocked requests.

```sql
SELECT session_id,
    status,
    blocking_session_id,
    wait_type,
    wait_time,
    wait_resource,
    transaction_id
FROM sys.dm_exec_requests
WHERE status = N'suspended';
GO
```

### D. Order existing requests by CPU

```sql
SELECT
    [req].[session_id],
    [req].[start_time],
    [req].[cpu_time] AS [cpu_time_ms],
    OBJECT_NAME([ST].[objectid], [ST].[dbid]) AS [ObjectName],
    SUBSTRING(
        REPLACE(
            REPLACE(
                SUBSTRING(
                    [ST].[text], ([req].[statement_start_offset] / 2) + 1,
                    ((CASE [req].[statement_end_offset]
                            WHEN -1 THEN DATALENGTH([ST].[text])
                            ELSE [req].[statement_end_offset]
                        END - [req].[statement_start_offset]
                        ) / 2
                    ) + 1
                ), CHAR(10), ' '
            ), CHAR(13), ' '
        ), 1, 512
    ) AS [statement_text]
FROM
    [sys].[dm_exec_requests] AS [req]
    CROSS APPLY [sys].dm_exec_sql_text([req].[sql_handle]) AS [ST]
ORDER BY
    [req].[cpu_time] DESC;
GO
```

## Related content

- [System dynamic management views](system-dynamic-management-views.md)
- [Execution Related Dynamic Management Views and Functions (Transact-SQL)](execution-related-dynamic-management-views-and-functions-transact-sql.md)
- [sys.dm_os_memory_clerks (Transact-SQL)](sys-dm-os-memory-clerks-transact-sql.md)
- [sys.dm_os_sys_info (Transact-SQL)](sys-dm-os-sys-info-transact-sql.md)
- [sys.dm_exec_query_memory_grants (Transact-SQL)](sys-dm-exec-query-memory-grants-transact-sql.md)
- [sys.dm_exec_query_plan (Transact-SQL)](sys-dm-exec-query-plan-transact-sql.md)
- [sys.dm_exec_sql_text (Transact-SQL)](sys-dm-exec-sql-text-transact-sql.md)
- [SQL Server, SQL Statistics object](../performance-monitor/sql-server-sql-statistics-object.md)
- [Query Processing Architecture Guide](../query-processing-architecture-guide.md#degree-of-parallelism-dop)
- [Thread and task architecture guide](../thread-and-task-architecture-guide.md)
- [Transaction locking and row versioning guide](../sql-server-transaction-locking-and-row-versioning-guide.md)
- [Understand and resolve SQL Server blocking problems](/troubleshoot/sql/performance/understand-resolve-blocking)
