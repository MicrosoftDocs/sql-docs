---
title: "queryinsights.frequently_run_queries (Transact-SQL)"
description: "The queryinsights.frequently_run_queries provides information about frequently run queries in Fabric Data Warehousing."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mariyaali
ms.date: 07/17/2024
ms.service: sql
ms.topic: "reference"
f1_keywords:
  - "queryinsights.frequently_run_queries"
  - "queryinsights.frequently_run_queries_TSQL"
helpviewer_keywords:
  - "queryinsights.frequently_run_queries system view"
  - "queryinsights.frequently_run_queries"
  - "query insights frequently_run_queries"
dev_langs:
  - "TSQL"
monikerRange: "=fabric"
---
# queryinsights.frequently_run_queries (Transact-SQL)
[!INCLUDE [fabric-se-and-dw](../../includes/applies-to-version/fabric-se-and-dw.md)]

  The `queryinsights.frequently_run_queries` in [!INCLUDE [fabric](../../includes/fabric.md)] provides information about frequently run queries in Fabric Data Warehousing.

| Column name | Data type | Description |
| --- | --- | --- |
| `last_run_start_time` | **datetime2** | Time of the most recent query execution.|
| `last_run_command` | **varchar(8000)** |  Text of the last query execution.|
| `number_of_runs` | **int** | Total number of times the query was executed.|
| `avg_total_elapsed_time_ms` | **int** | Average query execution time (ms) across all runs.|
| `last_run_total_elapsed_time_ms` | **int** | Time taken by the last execution (ms).|
| `last_dist_statement_id` | **uniqueidentifier** | ID linking the query to `queryinsights.exec_requests_history`.|
| `last_run_session_id` | **smallint** | User session ID for the last execution.|
| `min_run_total_elapsed_time_ms` | **int** |Shortest query execution time (ms).|
| `max_run_total_elapsed_time_ms` | **int** |Longest query execution time (ms).|
| `number_of_successful_runs` | **int** |Number of successful query executions.|
| `number_of_failed_runs` | **int** |Number of failed query executions.|
| `number_of_cancelled_runs` |**int** |Number of canceled query executions.|

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) or [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/data-warehousing#synapse-data-warehouse) within a [Premium capacity](/power-bi/enterprise/service-premium-what-is) workspace with Contributor or above permissions.

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Monitoring connections, sessions, and requests using DMVs in Microsoft Fabric](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-sessions-history-transact-sql.md)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.exec_requests_history (Transact-SQL)](queryinsights-exec-requests-history-transact-sql.md)
