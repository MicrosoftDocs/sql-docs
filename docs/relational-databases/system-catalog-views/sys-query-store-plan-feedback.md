---
title: "sys.query_store_plan_feedback (Transact-SQL)"
description: "The sys.query_store_plan_feedback system view contains information about Query Store tuning via memory grant, CE, and DOP feedback."
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: derekw
ms.date: 01/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "SYS.QUERY_STORE_PLAN_FEEDBACK_TSQL"
  - "QUERY_STORE_PLAN_FEEDBACK_TSQL"
  - "SYS.QUERY_STORE_PLAN_FEEDBACK"
  - "QUERY_STORE_PLAN_FEEDBACK"
helpviewer_keywords:
  - "query_store_plan_feedback catalog view"
  - "sys.query_store_plan_feedback catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16||=azuresqldb-mi-current||=azuresqldb-current"
---
# sys.query_store_plan_feedback (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

Contains information about Query Store tuning via query feedback features, including [memory grant feedback](../performance/intelligent-query-processing-memory-grant-feedback.md), [cardinality estimation (CE) feedback](../performance/intelligent-query-processing-cardinality-estimation-feedback.md), and [degree of parallelism (DOP) feedback](../performance/intelligent-query-processing-degree-parallelism-feedback.md).

| Column name | Data type | Description |
| --- | --- | --- |
| **plan_feedback_id** | bigint | Uniquely identifies the feedback change applied to a query. |
| **plan_id** | bigint | Foreign key. Joins to [sys.query_store_plan (Transact-SQL)](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md). |
| **feature_id** | tinyint | ID of the feature in use. |
| **feature_desc** | nvarchar(60) | 1 = CE feedback<br />2 = memory grant feedback<br />3 = DOP feedback |
| **feedback_data** | nvarchar(max) | For CE feedback, displays query hints in use.<br /><br />For memory grant feedback, displays JSON string containing operator-level grant values.<br />Format: `{"node_id": value}, {"node_id": value},….`<br />Example: `{"NodeId":"0","AdditionalMemoryKB":"1152"},{"NodeId":"18","AdditionalMemoryKB":"1856"}` |
| **state** | int | ID of the current feedback state. |
| **state_desc** | nvarchar(60) | 0. NO_FEEDBACK<br />1. NO_RECOMMENDATION<br />2. PENDING_VALIDATION<br />3. IN_VALIDATION<br />4. VERIFICATION_REGRESSED<br />5. VERIFICATION_PASSED<br />6. ROLLEDBACK_BY_APRC<br />7. FEEDBACK_VALID<br />8. FEEDBACK_INVALID |
| **create_time** | datetimeoffset(7) | When this row was created. |
| **last_updated_time** | datetimeoffset(7) | When this row was last updated. |

## Remarks

This catalog view will return the same row data on all replicas, if [Query Store for secondary replicas](../performance/query-store-for-secondary-replicas.md) is enabled.

## Permissions

Requires the `VIEW DATABASE STATE` permission.

## Related content

- [Memory grant feedback](../performance/intelligent-query-processing-memory-grant-feedback.md)
- [Cardinality estimation (CE) feedback](../performance/intelligent-query-processing-cardinality-estimation-feedback.md)
- [Degree of parallelism (DOP) feedback](../performance/intelligent-query-processing-degree-parallelism-feedback.md)
- [Intelligent query processing in SQL databases](../performance/intelligent-query-processing.md)
- [sys.database_query_store_internal_state (Transact-SQL)](sys-database-query-store-internal-state-transact-sql.md)
- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [Best practices for monitoring workloads with Query Store](../performance/best-practice-with-the-query-store.md)
