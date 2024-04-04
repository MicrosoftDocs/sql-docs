---
title: "queryinsights.exec_requests_history (Transact-SQL)"
description: "The queryinsights.exec_requests_history in Microsoft Fabric provides information about each complete SQL request."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mariyaali
ms.date: 10/30/2023
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

  The `queryinsights.exec_requests_history` in [!INCLUDE [fabric](../../includes/fabric.md)] provides information about each complete SQL request.

| Column name | Data type | Description |
| --- | --- | --- |
|distributed_statement_id | uniqueid | Unique ID for each query.|
|start_time | datetime2 | Time when the query started running.|
|command | varchar(8000) | Complete text of the executed query.|
|login_name | varchar(128) | Name of the user or system that sent the query.|
|row_count | bigint | Number of rows retrieved by the query.|
|total_elapsed_time_ms | int | Total time (ms) taken by the query to finish.|
|status | varchar(30) | Query status (Succeeded, Failed, or Canceled).|
|session_id | smallint | ID linking the query to a specific user session.|
|connection_id | uniqueid (nullable) | Identification number for the query's connection.|
|batch_id | uniqueid (nullable) | ID for grouped queries (if applicable).|
|root_batch_id | uniqueid (nullable) | ID for the main group of queries (if nested).|

## Permissions

You should have access to a [[!INCLUDE [fabric-se](../../includes/fabric-se.md)]](/fabric/data-warehouse/data-warehousing#sql-endpoint-of-the-lakehouse) or [[!INCLUDE [fabric-dw](../../includes/fabric-dw.md)]](/fabric/data-warehouse/data-warehousing#synapse-data-warehouse) within a [Premium capacity](/power-bi/enterprise/service-premium-what-is) workspace with Contributor or above permissions.

## Related content

- [Monitoring connections, sessions, and requests using DMVs in Microsoft Fabric](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)
