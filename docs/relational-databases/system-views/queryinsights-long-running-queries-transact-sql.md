---
title: "queryinsights.long_running_queries (Transact-SQL)"
description: "The queryinsights.long_running_queries in Fabric Data Warehouse provides information about SQL query execution times."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mariyaali, marakiketema
ms.date: 08/03/2026
ms.service: sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "queryinsights.long_running_queries"
  - "queryinsights.long_running_queries_TSQL"
helpviewer_keywords:
  - "queryinsights.long_running_queries system view"
  - "queryinsights.long_running_queries"
  - "query insights long_running_queries"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# queryinsights.long_running_queries (Transact-SQL)

[!INCLUDE [Fabric SE DW](../../includes/applies-to-version/fabric-se-dw.md)]

  The `queryinsights.long_running_queries` view in [!INCLUDE [fabric](../../includes/fabric.md)] Data Warehouse provides information about SQL query execution times.

| Column name | Data type | Description |
| --- | --- | --- |
| `last_run_start_time` | **datetime2** | Time of the most recent query execution.|
| `last_run_command` | **varchar(8000)** |  Text of the last query execution.|
| `median_total_elapsed_time_ms` | **int** | Median query execution time (ms) across runs.|
| `number_of_runs` | **int** | Total number of times the query was executed.|
| `number_of_accelerated_runs` | **int** | Total number of times the query was [accelerated](/fabric/data-warehouse/query-acceleration).|
| `last_run_total_elapsed_time_ms` | **int** | Time taken by the last execution (ms).|
| `last_dist_statement_id` | **uniqueidentifier** | ID linking the query to `queryinsights.exec_requests_history`.|
| `last_run_session_id` | **smallint** | User session ID for the last execution.|
| `query_hash` | **varchar(200)** | Binary hash value calculated on the query and used to identify semantically similar queries. For example, queries that are identical but have different parameterized values share a `query_hash`. Use the query hash to correlate between Query Insight views. For more information, see [Query Insights - Aggregation](/fabric/data-warehouse/query-insights#similar-queries). |

## Permissions

You need access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) or [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/data-warehousing#synapse-data-warehouse) within a [Premium capacity](/power-bi/enterprise/service-premium-what-is) workspace with Contributor or higher permissions.

## Example

You can find the performance of long-running queries, for example:

```sql
SELECT * 
FROM queryinsights.long_running_queries
WHERE last_run_command LIKE '%SALES DASHBOARD%'
ORDER BY median_total_elapsed_time_ms DESC;
```

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Monitor connections, sessions, and requests using DMVs](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-sessions-history-transact-sql.md)
- [queryinsights.exec_requests_history (Transact-SQL)](queryinsights-exec-requests-history-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)
- [queryinsights.sql_pool_insights (Transact-SQL)](queryinsights-sql-pool-insights-transact-sql.md)
