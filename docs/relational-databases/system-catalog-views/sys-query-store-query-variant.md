---
title: "sys.query_store_query_variant (Transact-SQL)"
description: The sys.query_store_query_variant system catalog view returns Query Store variant information.
author: thesqlsith
ms.author: derekw
ms.date: 11/11/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "language-reference"
ms.custom: event-tier1-sqlpass-2022
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
monikerRange: "=azuresqldb-current||=azuresqldb-mi-current||>=sql-server-ver16||>=sql-server-linux-ver16"
---
# sys.query_store_query_variant (Transact-SQL)

[!INCLUDE [sqlserver2022](../../includes/applies-to-version/sqlserver2022.md)]

  Contains information about the parent-child relationships between the original parameterized queries (also known as parent queries), dispatcher plans, and their child query variants.  This catalog view offers the ability to view all query variants associated with a dispatcher as well as the original parameterized queries. Query variants will have the same query_hash value as viewed from within the sys.query_store_query catalog view, which when joined with the sys.query_store_query_variant and sys.query_store_runtime_stats catalog views, aggregate resource usage statistics can be obtained for queries that differ only by their input values.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**query_variant_query_id**|**bigint**|Primary key. ID of the parameterized sensitive query variant.|  
|**parent_query_id**|**bigint**|ID of the original parameterized query.|  
|**dispatcher_plan_id**|**bigint**|ID of the parameter sensitive plan optimization dispatcher plan.|  

## Remarks

Since more than one query variant can be associated with one dispatcher plan, there will be multiple plans that belong to query variants which will eventually add to the overall resource usage statistics of the parent query. The dispatcher plan for query variants does not produce any runtime statistics in the Query Store, which will cause existing Query Store queries to no longer be sufficient when gathering overall statistics unless an additional join to the **query_store_query_variant** view is included.
  
## Permissions  

 Requires the **VIEW DATABASE STATE** permission.  

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

## See Also  

- [sys.query_store_plan &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-plan-transact-sql.md)
- [sys.query_store_query &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-query-transact-sql.md)
- [sys.query_store_runtime_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql.md)
- [sys.query_store_wait_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-wait-stats-transact-sql.md)  
- [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql.md)
- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)
- [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)
