---
title: "queryinsights.external_api_call_stats (Transact-SQL)"
description: The queryinsights.external_api_call_stats view provides call, latency, payload, retry, and row outcome details for external functions.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jovanpop, wiassaf
ms.date: 08/24/2026
ms.service: sql
ms.topic: reference
f1_keywords:
  - "queryinsights.external_api_call_stats"
  - "queryinsights.external_api_call_stats_TSQL"
helpviewer_keywords:
  - "queryinsights.external_api_call_stats system view"
  - "queryinsights.external_api_call_stats"
  - "query insights external API calls"
  - "AI function query insights"
dev_langs:
  - TSQL
monikerRange: "=fabric"
---

# queryinsights.external_api_call_stats (Transact-SQL)

[!INCLUDE [Fabric SE DW](../../includes/applies-to-version/fabric-se-dw.md)]

The `queryinsights.external_api_call_stats` view in [!INCLUDE [fabric](../../includes/fabric.md)] Data Warehouse provides function-level diagnostics for queries that call external APIs through AI functions. Use this view to investigate external service latency, retries, throttling, payload size, execution mode, and failed rows.

The view contains one row for each distinct external function used by a query. Join it to `queryinsights.exec_requests_history` by using `distributed_statement_id` to combine function-level statistics with query text, status, and elapsed time.

| Column name | Data type | Description |
| --- | --- | --- |
| `distributed_statement_id` | **uniqueidentifier** | Identifies the query that invoked the function. Join this column to `queryinsights.exec_requests_history.distributed_statement_id`. Together with `function_name`, it identifies one function within a query. |
| `database_name` | **varchar(200)** | Name of the database in which the query ran. |
| `function_name` | **varchar(256)** | Name of the external function, such as `ai_analyze_sentiment` or `MyFunctionSet.ScoreModel`. |
| `function_type` | **varchar(32)** | Type of external function. The value is `AI_FUNCTION`. |
| `execution_mode` | **varchar(16)** | Call mode used by the function. The value is `batch`, `row`, or `mixed`. Batch mode can process up to 60 rows per request. Row mode processes one row per request. |
| `call_count` | **int** | Total number of HTTP calls sent to the external endpoint, including HTTP-layer retries. |
| `batch_call_count` | **int** | Number of HTTP calls made in batch mode. The value is `0` when `execution_mode` is `row`. |
| `row_call_count` | **int** | Number of HTTP calls made in row mode. The value is `0` when `execution_mode` is `batch`. |
| `call_retry_count` | **int** | Number of full call or batch retries after the initial call. This value doesn't include HTTP-layer retries, which are included in `call_count`. |
| `external_service_wait_time_ms` | **bigint** | Total time, in milliseconds, spent waiting for the external endpoint. |
| `data_sent_bytes` | **bigint** | Total request payload size, in bytes, sent to the external endpoint. |
| `data_received_bytes` | **bigint** | Total response payload size, in bytes, received from the external endpoint. |
| `rows_total` | **int** | Total number of rows processed by the function. |
| `rows_succeeded` | **int** | Number of rows that returned valid results. |
| `rows_failed` | **int** | Number of rows that returned `NULL` because of processing errors. This value equals `rows_total - rows_succeeded`. |
| `rows_throttled` | **int** | Number of rows that failed with HTTP status code 429 after retries were exhausted. |
| `rows_content_filtered` | **int** | Number of rows blocked by content safety filters. |
| `rows_text_too_long` | **int** | Number of rows rejected because the input exceeded the maximum text length. |
| `rows_service_error` | **int** | Number of rows that failed after HTTP 500, 502, 503, or 504 responses and retries, plus rows affected by non-retriable service errors that weren't caused by user input. |

## Remarks

- Queries that don't use external functions don't produce rows in `queryinsights.external_api_call_stats`.
- A query that invokes multiple distinct external functions produces one row for each function.
- Use the `is_using_external_api` column in `queryinsights.exec_requests_history` to identify queries that can have corresponding rows in this view.
- `call_count` includes HTTP-layer retries. `call_retry_count` tracks retries of a complete call or batch and doesn't include HTTP-layer retries.
- Compare `batch_call_count` and `row_call_count` to determine whether the function processed rows in batches, individually, or in both modes.
- Payload counters contain sizes only. Query Insights doesn't store external request or response content.
- Failure counters help classify common causes, but they don't automatically remediate throttling, content filtering, invalid input length, or service failures.
- The `queryinsights.external_api_call_stats` and all `queryinsights` views are only available in Fabric Data Warehouse and SQL analytics endpoint.

## Permissions

You should have access to a SQL analytics endpoint or warehouse within a Fabric Capacity workspace with Contributor or above permissions, or Viewer permissions with Monitor access.

## Examples

Use this view to identify queries that call external functions and investigate function-level performance and failures. Some examples follow.

### A. Find recent queries that called external APIs

Return the 100 most recent queries that invoked an AI function:

```sql
SELECT TOP 100
       distributed_statement_id,
       submit_time,
       status,
       total_elapsed_time_ms,
       command
FROM queryinsights.exec_requests_history
WHERE is_using_external_api = 1
ORDER BY submit_time DESC;
```

### B. Calculate external wait time per call

Join the views and rank external functions by their average external service wait per HTTP call:

```sql
SELECT TOP 100
       h.distributed_statement_id,
       h.submit_time,
       f.function_name,
       f.function_type,
       f.call_count,
       f.external_service_wait_time_ms,
       f.external_service_wait_time_ms
           / NULLIF(f.call_count, 0) AS wait_time_ms_per_call
FROM queryinsights.exec_requests_history AS h
INNER JOIN queryinsights.external_api_call_stats AS f
    ON h.distributed_statement_id = f.distributed_statement_id
WHERE h.is_using_external_api = 1
ORDER BY wait_time_ms_per_call DESC;
```

### C. Find retry and failure hotspots

Return queries with at least three complete-call retries or more than 5 percent failed rows:

```sql
SELECT h.distributed_statement_id,
       h.submit_time,
       h.status,
       SUM(f.call_retry_count) AS retry_count,
       SUM(f.rows_total) AS rows_total,
       SUM(f.rows_failed) AS rows_failed,
       SUM(f.rows_throttled) AS rows_throttled,
       SUM(f.rows_service_error) AS rows_service_error
FROM queryinsights.exec_requests_history AS h
INNER JOIN queryinsights.external_api_call_stats AS f
    ON h.distributed_statement_id = f.distributed_statement_id
WHERE h.is_using_external_api = 1
GROUP BY h.distributed_statement_id,
         h.submit_time,
         h.status
HAVING SUM(f.call_retry_count) >= 3
    OR SUM(f.rows_failed) > SUM(f.rows_total) * 0.05
ORDER BY retry_count DESC, rows_failed DESC;
```

### D. Find queries dominated by external wait

Return queries for which external service wait exceeds half of the total query elapsed time:

```sql
SELECT h.distributed_statement_id,
       h.submit_time,
       h.total_elapsed_time_ms,
       SUM(f.external_service_wait_time_ms) AS external_wait_time_ms,
       100.0 * SUM(f.external_service_wait_time_ms)
           / NULLIF(h.total_elapsed_time_ms, 0) AS external_wait_pct
FROM queryinsights.exec_requests_history AS h
INNER JOIN queryinsights.external_api_call_stats AS f
    ON h.distributed_statement_id = f.distributed_statement_id
WHERE h.is_using_external_api = 1
GROUP BY h.distributed_statement_id,
         h.submit_time,
         h.total_elapsed_time_ms
HAVING SUM(f.external_service_wait_time_ms)
       > h.total_elapsed_time_ms * 0.50
ORDER BY external_wait_pct DESC;
```

### E. Find row-mode-only queries

Return queries whose external functions used row mode without any batch calls:

```sql
SELECT h.distributed_statement_id,
       h.submit_time,
       SUM(f.row_call_count) AS row_call_count,
       SUM(f.call_count) AS total_call_count,
       SUM(f.rows_total) AS rows_total
FROM queryinsights.exec_requests_history AS h
INNER JOIN queryinsights.external_api_call_stats AS f
    ON h.distributed_statement_id = f.distributed_statement_id
WHERE h.is_using_external_api = 1
GROUP BY h.distributed_statement_id,
         h.submit_time
HAVING SUM(f.row_call_count) > 0
   AND SUM(f.batch_call_count) = 0
ORDER BY row_call_count DESC;
```

### F. Find large payloads per row

Return external function calls whose average request payload exceeds 32 KB per processed row:

```sql
SELECT h.distributed_statement_id,
       h.submit_time,
       f.function_name,
       f.rows_total,
       f.data_sent_bytes,
       f.data_sent_bytes
           / NULLIF(f.rows_total, 0) AS bytes_sent_per_row
FROM queryinsights.exec_requests_history AS h
INNER JOIN queryinsights.external_api_call_stats AS f
    ON h.distributed_statement_id = f.distributed_statement_id
WHERE h.is_using_external_api = 1
  AND f.data_sent_bytes / NULLIF(f.rows_total, 0) > 32768
ORDER BY bytes_sent_per_row DESC;
```

## Next step

> [!div class="nextstepaction"]
> [Query insights in Microsoft Fabric](/fabric/data-warehouse/query-insights)

## Related content

- [Query insights in Fabric Data Warehouse](/fabric/data-warehouse/query-insights)
- [Monitor connections, sessions, and requests using DMVs](/fabric/data-warehouse/monitor-using-dmv)
- [queryinsights.exec_requests_history (Transact-SQL)](queryinsights-exec-requests-history-transact-sql.md)
- [queryinsights.exec_sessions_history (Transact-SQL)](queryinsights-exec-sessions-history-transact-sql.md)
- [queryinsights.long_running_queries (Transact-SQL)](queryinsights-long-running-queries-transact-sql.md)
- [queryinsights.frequently_run_queries (Transact-SQL)](queryinsights-frequently-run-queries-transact-sql.md)