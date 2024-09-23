---
title: "sys.dm_pdw_exec_requests (Transact-SQL)"
description: sys.dm_pdw_exec_requests holds information about all requests currently or recently active in Azure Synapse Analytics dedicated SQL pools and Analytics Platform System (PDW).
author: jacinda-eng
ms.author: jacindaeng
ms.reviewer: wiassaf, randolphwest
ms.date: 04/23/2024
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_exec_requests (Transact-SQL)

[!INCLUDE [applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

Holds information about all requests currently or recently active in Azure Synapse Analytics dedicated SQL pools and Analytics Platform System (PDW). It lists one row per request/query.

> [!NOTE]  
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)] For serverless SQL pool, use [sys.dm_exec_requests](sys-dm-exec-requests-transact-sql.md).

| Column name | Data type | Description | Range |
| --- | --- | --- | --- |
| `request_id` | **nvarchar(32)** | Key for this view. Unique numeric ID associated with the request. | Unique across all requests in the system. |
| `session_id` | **nvarchar(32)** | Unique numeric ID associated with the session in which this query was run. See [sys.dm_pdw_exec_sessions](sys-dm-pdw-exec-sessions-transact-sql.md). | |
| `status` | **nvarchar(32)** | Current status of the request. | `Running`, `Suspended`, `Completed`, `Cancelled`, `Failed`. |
| `submit_time` | **datetime** | Time at which the request was submitted for execution. | Valid **datetime** smaller or equal to the current time and `start_time`. |
| `start_time` | **datetime** | Time at which the request execution was started. | `NULL` for queued requests; otherwise, valid **datetime** smaller or equal to current time. |
| `end_compile_time` | **datetime** | Time at which the engine completed compiling the request. | `NULL` for requests that haven't been compiled yet; otherwise a valid **datetime** less than `start_time` and less than or equal to the current time. |
| `end_time` | **datetime** | Time at which the request execution completed, failed, or was canceled. | `NULL` for queued or active requests; otherwise, a valid **datetime** smaller or equal to current time. |
| `total_elapsed_time` | **int** | Time elapsed in execution since the request was started, in milliseconds. | Between 0 and the difference between `submit_time` and `end_time`.<br /><br />If `total_elapsed_time` exceeds the maximum value for an integer, `total_elapsed_time` continues to be the maximum value. This condition generates the warning "The maximum value has been exceeded."<br />The maximum value in milliseconds is the same as 24.8 days. |
| `label` | **nvarchar(255)** | Optional label string associated with some `SELECT` query statements. | Any string containing `a-z`, `A-Z`,`0-9`,`_`. |
| `error_id` | **nvarchar(36)** | Unique ID of the error associated with this step, if any. | `NULL` if no error occurred. The `request_id` can be matched with the `request_id` in [sys.dm_pdw_errors](sys-dm-pdw-errors-transact-sql.md) to view all of the listed errors for that specific `request_id`. |
| `database_id` | **int** | Identifier of database used by explicit context (for example, `USE DB_X`). | See `database_id` in [sys.databases](../system-catalog-views/sys-databases-transact-sql.md). |
| `command` | **nvarchar(4000)** | Holds the full text of the request as submitted by the user. | Any valid query or request text. Queries that are longer than 4,000 bytes are truncated. |
| `resource_class` | **nvarchar(20)** | The workload group used for this request. | Static Resource Classes<br />`staticrc10`<br />`staticrc20`<br />`staticrc30`<br />`staticrc40`<br />`staticrc50`<br />`staticrc60`<br />`staticrc70`<br />`staticrc80`<br /><br />Dynamic Resource Classes<br />`SmallRC`<br />`MediumRC`<br />`LargeRC`<br />`XLargeRC` |
| `importance` | **nvarchar(128)** | The Importance setting the request executed at. This is the relative importance of a request in this workload group and across workload groups for shared resources. Importance specified in the classifier overrides the workload group importance setting.<br /><br />**Applies to**: Azure Synapse Analytics | `NULL`<br />`low`<br />`below_normal`<br />`normal` (default)<br />`above_normal`<br />high |
| `group_name` | **sysname** | For requests utilizing resources, `group_name` is the name of the workload group the request is running under. If the request doesn't utilize resources, `group_name` is `NULL`.<br /><br />**Applies to**: Azure Synapse Analytics |
| `classifier_name` | **sysname** | For requests utilizing resources, The name of the classifier used for assigning resources and importance. | |
| `resource_allocation_percentage` | **decimal(5,2)** | The percentage amount of resources allocated to the request.<br /><br />**Applies to**: Azure Synapse Analytics |
| `result_cache_hit` | **int** | Details whether a completed query used result set cache.<br /><br />**Applies to**: Azure Synapse Analytics | 1 = Result set cache hit<br />0 = Result set cache miss<br />Negative integer values = Reasons why result set caching wasn't used. See remarks section for details. |
| `client_correlation_id` | **nvarchar(255)** | Optional user-defined name for a client session. To set for a session, call `sp_set_session_context 'client_correlation_id', <CorrelationIDName>`. You can retrieve its value with `SELECT SESSION_CONTEXT (N'client_correlation_id')`. For more information, see [sp_set_session_context](../system-stored-procedures/sp-set-session-context-transact-sql.md). |
| `command2` | **nvarchar(max)** | Holds the full text of the request as submitted by the user. Holds queries that are longer than 4,000 characters. | Any valid query or request text. `NULL` = Queries that are 4000 characters long or less. For these queries the full text can be found under the `command` column. |

## Remarks

For information about the maximum rows retained by this view, see the Metadata section in the [Capacity limits](/azure/sql-data-warehouse/sql-data-warehouse-service-capacity-limits#metadata) article.

The negative integer value in the `result_cache_hit` column is a bitmap value of all applied reasons why a query's result set can't be cached. This column can be the [| (Bitwise OR)](../../t-sql/language-elements/bitwise-or-transact-sql.md) product of one or more of following values. For example, `-36` could be a **bitwise OR** of two reasons: `-0x04` (`-4`) and `-0x20` (`-32`).

| Value | Value in decimal | Description |
| --- | --- | --- |
| `1` | `1` | Result set cache hit |
| `0x00` | `0` | Result set cache miss |
| `-0x01` | `-1` | Result set caching is disabled on the database. |
| `-0x02` | `-2` | Result set caching is disabled on the session. |
| `-0x04` | `-4` | Result set caching is disabled due to no data sources for the query. |
| `-0x08` | `-8` | Result set caching is disabled due to row level security predicates. |
| `-0x10` | `-16` | Result set caching is disabled due to the use of system table, temporary table, or external table in the query. |
| `-0x20` | `-32` | Result set caching is disabled because the query contains runtime constants, user-defined functions, or nondeterministic functions. |
| `-0x40` | `-64` | Result set caching is disabled due to estimated result set size is greater than 10 GB. |
| `-0x80` | `-128` | Result set caching is disabled because the result set contains rows with large size (greater than 64 KB). |
| `-0x100` | `-256` | Result set caching is disabled because of the use of granular dynamic data masking. |

## Permissions

Requires `VIEW DATABASE STATE` permission.

## Security

`sys.dm_pdw_exec_requests` doesn't filter query results according to database-specific permissions. Logins with `VIEW DATABASE STATE` permission can obtain results query results for all databases.

> [!WARNING]  
> An attacker can use `sys.dm_pdw_exec_requests` to retrieve information about specific database objects by simply having `VIEW DATABASE STATE` permission and by not having database-specific permission.

## Related content

- [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)