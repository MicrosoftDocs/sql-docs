---
title: "queryinsights.exec_requests_history (Transact-SQL)"
description: "The queryinsights.exec_requests_history in Microsoft Fabric provides information about each complete SQL request."
author: WilliamDAssafMSFT
ms.author: mariyaali
ms.reviewer: mariyaali
ms.date: 07/23/2024
ms.service: sql
ms.topic: "reference"
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
[!INCLUDE [fabric-se-and-dw](../../includes/applies-to-version/fabric-se-and-dw.md)]

  The `queryinsights.exec_requests_history` in [!INCLUDE [fabric](../../includes/fabric.md)] provides information about each completed SQL request.

| Column name | Data type | Description |
| --- | --- | --- |
| `distributed_statement_id` | **uniqueidentifier** | Unique ID for each query.|
| `start_time` | **datetime2** | Time when the query started running.|
| `command` | **varchar(8000)** | Complete text of the executed query.|
| `login_name` | **varchar(128)** | Name of the user or system that sent the query.|
| `row_count` | **bigint** | Number of rows retrieved by the query.|
| `total_elapsed_time_ms` | **int** | Total time (ms) taken by the query to finish.|
| `status` | **varchar(30)** | Query status: **Succeeded**, **Failed**, or **Cancelled**|
| `session_id` | **smallint** | ID linking the query to a specific user session.|
| `connection_id` | **uniqueidentifier** | Identification number for the query's connection. Nullable.|
| `batch_id` | **uniqueidentifier** | ID for grouped queries (if applicable). Nullable.|
| `root_batch_id` | **uniqueidentifier** | ID for the main group of queries (if nested). Nullable.|
| `query_hash` | **varchar(200)** | Binary hash value calculated on the query and used to identify queries with similar logic. You can use the query hash to correlate between Query Insight views. For more information, see [Query Insights - Aggregation](/fabric/data-warehouse/query-insights#similar-queries).|
| `label` | **varchar(8000)** | Optional label string associated with some SELECT query statements.|

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) or [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/data-warehousing#synapse-data-warehouse) within a [Premium capacity](/power-bi/enterprise/service-premium-what-is) workspace with Contributor or above permissions.

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Monitoring connections, sessions, and requests using DMVs in Microsoft Fabric](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-sessions-history-transact-sql.md)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)
