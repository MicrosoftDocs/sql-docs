---
title: "sys.query_store_query_variant (Transact-SQL)"
description: The sys.query_store_query_variant system catalog view returns Query Store variant information.
author: thesqlsith
ms.author: derekw
ms.date: 05/26/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SYS.QUERY_STORE_QUERY_VARIANT"
  - "QUERY_STORE_QUERY_VARIANT"
  - "SYS.QUERY_STORE_QUERY_VARIANT_TSQL"
  - "QUERY_STORE_QUERY_VARIANT_TSQL"
helpviewer_keywords:
  - "sys.query_store_query_variant catalog view"
  - "query_store_query_variant catalog view"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-ver16 || >=sql-server-linux-ver16 || =fabric-sqldb"
---
# sys.query_store_query_variant (Transact-SQL)

 [!INCLUDE [sqlserver2022-asdb-asdbmi-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asmi-fabricsqldb.md)]

Contains information about the parent-child relationships between the original parameterized queries (also known as parent queries), dispatcher plans, and their child query variants. This catalog view shows all query variants associated with a dispatcher and the original parameterized queries. Query variants share the same `query_hash` value as viewed from `sys.query_store_query`. When you join `sys.query_store_query_variant` with `sys.query_store_query` and `sys.query_store_runtime_stats`, you can obtain aggregate resource usage statistics for queries that differ only by their input values.  
  
|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`query_variant_query_id`|`bigint`|Primary key. ID of the parameterized sensitive query variant.|
|`parent_query_id`|`bigint`|ID of the original parameterized query.|
|`dispatcher_plan_id`|`bigint`|ID of the parameter sensitive plan optimization dispatcher plan.|

## Remarks

Because more than one query variant can associate with a single dispatcher plan, multiple plans belong to query variants and contribute to the overall resource usage statistics of the parent query. The dispatcher plan for query variants doesn't produce runtime statistics in the Query Store. As a result, existing Query Store queries aren't sufficient for gathering overall statistics unless you include an extra join to the `sys.query_store_query_variant` view.
  
## Permissions

Requires the `VIEW DATABASE STATE` permission.

### Permissions for SQL Server 2022 and later

Requires the `VIEW DATABASE PERFORMANCE STATE` permission on the database.

## Examples

### View Query Store variant information

```sql
SELECT 
    qspl.plan_type_desc AS query_plan_type, 
    qspl.plan_id as query_store_planid, 
    qspl.query_id as query_store_queryid, 
    qsqv.query_variant_query_id as query_store_variant_queryid,
    qsqv.parent_query_id as query_store_parent_queryid,
    qsqv.dispatcher_plan_id as query_store_dispatcher_planid,
    OBJECT_NAME(qsq.object_id) as module_name, 
    qsq.query_hash, 
    qsqtxt.query_sql_text,
    convert(xml,qspl.query_plan)as show_plan_xml,
    qsrs.last_execution_time as last_execution_time,
    qsrs.count_executions AS number_of_executions,
    qsq.count_compiles AS number_of_compiles 
FROM sys.query_store_runtime_stats AS qsrs
    JOIN sys.query_store_plan AS qspl 
        ON qsrs.plan_id = qspl.plan_id 
    JOIN sys.query_store_query_variant qsqv 
        ON qspl.query_id = qsqv.query_variant_query_id
    JOIN sys.query_store_query as qsq
        ON qsqv.parent_query_id = qsq.query_id
    JOIN sys.query_store_query_text AS qsqtxt  
        ON qsq.query_text_id = qsqtxt .query_text_id  
ORDER BY qspl.query_id, qsrs.last_execution_time;
GO
```

### View Query Store dispatcher and variant information

```sql
SELECT
    qspl.plan_type_desc AS query_plan_type, 
    qspl.plan_id as query_store_planid, 
    qspl.query_id as query_store_queryid, 
    qsqv.query_variant_query_id as query_store_variant_queryid,
    qsqv.parent_query_id as query_store_parent_queryid, 
    qsqv.dispatcher_plan_id as query_store_dispatcher_planid,
    qsq.query_hash, 
    qsqtxt.query_sql_text, 
    CONVERT(xml,qspl.query_plan)as show_plan_xml,
    qsq.count_compiles AS number_of_compiles,
    qsrs.last_execution_time as last_execution_time,
    qsrs.count_executions AS number_of_executions
FROM sys.query_store_query qsq
    LEFT JOIN sys.query_store_query_text qsqtxt
        ON qsq.query_text_id = qsqtxt.query_text_id
    LEFT JOIN sys.query_store_plan qspl
        ON qsq.query_id = qspl.query_id
    LEFT JOIN sys.query_store_query_variant qsqv
        ON qsq.query_id = qsqv.query_variant_query_id
    LEFT JOIN sys.query_store_runtime_stats qsrs
        ON qspl.plan_id = qsrs.plan_id
    LEFT JOIN sys.query_store_runtime_stats_interval qsrsi
        ON qsrs.runtime_stats_interval_id = qsrsi.runtime_stats_interval_id
WHERE qspl.plan_type = 1 or qspl.plan_type = 2
ORDER BY qspl.query_id, qsrs.last_execution_time;
GO
```

## Related content

- [sys.query_store_plan (Transact-SQL)](sys-query-store-plan-transact-sql.md)
- [sys.query_store_query (Transact-SQL)](sys-query-store-query-transact-sql.md)
- [sys.query_store_runtime_stats (Transact-SQL)](sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_wait_stats (Transact-SQL)](sys-query-store-wait-stats-transact-sql.md)
- [sys.query_store_runtime_stats_interval (Transact-SQL)](sys-query-store-runtime-stats-interval-transact-sql.md)
- [Monitor performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Query Store stored procedures (Transact-SQL)](../system-stored-procedures/query-store-stored-procedures-transact-sql.md)
