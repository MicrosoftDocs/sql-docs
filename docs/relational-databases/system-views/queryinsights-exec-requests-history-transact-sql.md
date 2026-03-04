---
title: "queryinsights.exec_requests_history (Transact-SQL)"
description: "The queryinsights.exec_requests_history in Microsoft Fabric provides information about each complete SQL request."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mariyaali, randolphwest, emtehran
ms.date: 11/12/2025
ms.service: sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "queryinsights.exec_requests_history"
  - "queryinsights.exec_requests_history_TSQL"
helpviewer_keywords:
  - "queryinsights.exec_requests_history system view"
  - "queryinsights.exec_requests_history"
  - "query insights frequently_run_queries"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# queryinsights.exec_requests_history (Transact-SQL)

[!INCLUDE [Fabric SE DW](../../includes/applies-to-version/fabric-se-dw.md)]

The `queryinsights.exec_requests_history` in [!INCLUDE [fabric](../../includes/fabric.md)] Data Warehouse provides information about each completed SQL request.

| Column name | Data type | Description |
| --- | --- | --- |
| `distributed_statement_id` | **uniqueidentifier** | Unique ID for each query. |
| `database_name` | **varchar(200)** | Specifies the name of the item to which the SQL endpoint was connected at the time of query execution. | 
| `submit_time` | **datetime2** | Time at which the request was submitted for execution. |
| `start_time` | **datetime2** | Time when the query started running. |
| `end_time` | **datetime2** | Time when the query completed execution. |
| `is_distributed` | **int** | Specifies whether the query was executed in a distributed nature (`1`), or not (`0`). |
| `statement_type ` | **varchar(128)** | Identifies the type of command that was run. Common statement types include the following values: `SELECT`, `INSERT`, `UPDATE`, `DELETE` |
| `total_elapsed_time_ms` | **int** | Total time (in milliseconds) taken by the query to finish. |
| `login_name` | **varchar(128)** | Name of the user or system that sent the query. |
| `row_count` | **bigint** | Number of rows retrieved by the query. |
| `status` | **varchar(30)** | Query status: `Succeeded`, `Failed`, or `Canceled` |
| `session_id` | **smallint** | ID linking the query to a specific user session. |
| `connection_id` | **uniqueidentifier** | Identification number for the query's connection. Nullable. |
| `program_name` | **varchar(128)** | Name of client program that initiated the session. The value is `NULL` for internal sessions. Is nullable. |
| `batch_id` | **uniqueidentifier** | ID for grouped queries (if applicable). Nullable. |
| `root_batch_id` | **uniqueidentifier** | ID for the main group of queries (if nested). Nullable. |
| `query_hash` | **varchar(200)** | Binary hash value calculated on the query and used to identify queries with similar logic. You can use the query hash to correlate between Query Insight views. For more information, see [Query Insights - Aggregation](/fabric/data-warehouse/query-insights#similar-queries). |
| `label` | **varchar(8000)** | Optional label string associated with some `SELECT` query statements. |
| `result_cache_hit` | **int** | Shows the status of [result set caching](/fabric/data-warehouse/result-set-caching) for this query:<br /><br />`2` - query used result set cache (*cache hit*)<br />`1` - query created result set cache<br />`0` - query wasn't applicable for cache creation or usage |
| `sql_pool_name` | **varchar(128)** | Name of the SQL pool utilized for executing this request. Can be a default or a user defined pool.<br /><br />This value can be **NULL or empty** for queries do not require distributed execution in a SQL pool.<br /><br />Examples include lightweight or metadata-only queries where no distributed operators are invoked. |
| `allocated_cpu_time_ms` | **bigint** | Shows the total time of CPUs that was allocated for a query's execution. |
| `data_scanned_remote_storage_mb` | **decimal(18,3)** | Shows how much data was scanned/read from remote storage (One Lake). |
| `data_scanned_memory_mb` | **decimal(18,3)** | Shows how much data was scanned from local memory. Data scanned from disk and memory together indicates how much data was read from cache. |
| `data_scanned_disk_mb` | **decimal(18,3)** | Shows how much data was scanned/read from local disk. Data scanned from disk and memory together indicates how much data was read from cache. |
| `command` | **varchar(8000)** | Complete text of the executed query. |

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) or [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/data-warehousing#synapse-data-warehouse) within a [Premium capacity](/power-bi/enterprise/service-premium-what-is) workspace with Contributor or above permissions.

## Examples

### A. Find query performance on specific command text

You can the `queryinsights.exec_requets_history` view to find the history of query executions with commands on key words, such as a table, view, or column. For example, to look for queries on the `SalesInvoices` table:

```sql
SELECT *
FROM 
    queryinsights.exec_requests_history 
WHERE 
    command LIKE '%SalesInvoices%';
```

### B. Compare query with different labels

You can the `queryinsights.exec_requets_history` view to compare differences between the queries with different labels, for example, a query run with or without [data clustering](/fabric/data-warehouse/data-clustering). For a tutorial on using data clustering in Fabric Data Warehouse, see [Use data clustering in Fabric Data Warehouse](/fabric/data-warehouse/tutorial-data-clustering).

```sql
SELECT *
FROM 
    queryinsights.exec_requests_history 
WHERE 
    command LIKE '%NYTaxi%'
    AND label IN ('Regular','Clustered')
ORDER BY 
  submit_time DESC;
```

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Monitoring connections, sessions, and requests using DMVs in Fabric Data Warehouse](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-sessions-history-transact-sql.md)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)
