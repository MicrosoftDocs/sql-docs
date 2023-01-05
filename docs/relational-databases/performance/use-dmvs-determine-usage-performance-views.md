---
title: DMVs - usage statistics and performance of views
description: Learn how to use the Dynamic Management Views (DMVs) sys.dm_exec_query_optimizer_info, sys.views, and sys.dmv_exec_cached_plans to get SQL query performance statistics.
ms.custom: seo-dt-2019
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/27/2018
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
---

# Use DMVs to Determine Usage Statistics and Performance of Views
This article covers methodology and scripts used to get information about the **performance of queries that use Views**. The intention of these scripts is to provide indicators of use and performance of various Views found in a database. 

## sys.dm_exec_query_optimizer_info
The DMV [sys.dm_exec_query_optimizer_info](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-optimizer-info-transact-sql.md) exposes statistics about the optimizations performed by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] query optimizer. These values are cumulative and begin recording when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] starts. For more information on the query optimizer, see the [Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md).   

The below common_table_expression (CTE) uses this DMV to provide information about the workload, such as the percentage of queries that reference a view. The results returned by this query do not indicate a performance problem by themselves, but can expose underlying issues when combined with users' complaints of slow-performing queries. 

```sql
WITH CTE_QO AS
(
  SELECT
    occurrence
  FROM
    sys.dm_exec_query_optimizer_info
  WHERE
    ([counter] = 'optimizations')
),
QOInfo AS
(
  SELECT
    [counter]
    ,[%] = CAST((occurrence * 100.00)/(SELECT occurrence FROM CTE_QO) AS DECIMAL(5, 2))
  FROM
    sys.dm_exec_query_optimizer_info
  WHERE
    [counter] IN ('optimizations'
                  ,'trivial plan'
                  ,'no plan'
                  ,'search 0'
                  ,'search 1'
                  ,'search 2'
                  ,'timeout'
                  ,'memory limit exceeded'
                  ,'insert stmt'
                  ,'delete stmt'
                  ,'update stmt'
                  ,'merge stmt'
                  ,'contains subquery'
                  ,'view reference'
                  ,'remote query'
                  ,'dynamic cursor request'
                  ,'fast forward cursor request'
             )
)
SELECT
  [optimizations] AS [optimizations %]
  ,[trivial plan] AS [trivial plan %]
  ,[no plan] AS [no plan %]
  ,[search 0] AS [search 0 %]
  ,[search 1] AS [search 1 %]
  ,[search 2] AS [search 2 %]
  ,[timeout] AS [timeout %]
  ,[memory limit exceeded] AS [memory limit exceeded %]
  ,[insert stmt] AS [insert stmt %]
  ,[delete stmt] AS [delete stmt]
  ,[update stmt] AS [update stmt]
  ,[merge stmt] AS [merge stmt]
  ,[contains subquery] AS [contains subquery %]
  ,[view reference] AS [view reference %]
  ,[remote query] AS [remote query %]
  ,[dynamic cursor request] AS [dynamic cursor request %]
  ,[fast forward cursor request] AS [fast forward cursor request %]
FROM
  QOInfo
PIVOT (MAX([%]) FOR [counter] 
  IN ([optimizations]
      ,[trivial plan]
      ,[no plan]
      ,[search 0]
      ,[search 1]
      ,[search 2]
      ,[timeout]
      ,[memory limit exceeded]
      ,[insert stmt]
      ,[delete stmt]
      ,[update stmt]
      ,[merge stmt]
      ,[contains subquery]
      ,[view reference]
      ,[remote query]
      ,[dynamic cursor request]
      ,[fast forward cursor request])) AS p;
GO
```

Combine the results of this query with the results of the system view [sys.views](../../relational-databases/system-catalog-views/sys-views-transact-sql.md) to identify query statistics, query text, and the cached execution plan. 

## sys.views
The below CTE provides information about the number of executions, total run time, and pages read from memory. The results can be used to identify queries that may be candidates for optimization. 
  
> [!NOTE]
> The results of this query can vary depending on the version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  


```sql
WITH CTE_VW_STATS AS
(
  SELECT
    SCHEMA_NAME(vw.schema_id) AS schemaname
    ,vw.name AS viewname
    ,vw.object_id AS viewid
  FROM
    sys.views AS vw
  WHERE
    (vw.is_ms_shipped = 0)
  INTERSECT
  SELECT
    SCHEMA_NAME(o.schema_id) AS schemaname
    ,o.Name AS name
    ,st.objectid AS viewid
  FROM
    sys.dm_exec_cached_plans cp
  CROSS APPLY
    sys.dm_exec_sql_text(cp.plan_handle) st
  INNER JOIN
    sys.objects o ON st.[objectid] = o.[object_id]
  WHERE
    st.dbid = DB_ID()
)
SELECT
  vw.schemaname
  ,vw.viewname
  ,vw.viewid
  ,DB_NAME(t.databaseid) AS databasename
  ,t.databaseid
  ,t.*
FROM
  CTE_VW_STATS AS vw
CROSS APPLY
  (
    SELECT
      st.dbid AS databaseid
      ,st.text
      ,qp.query_plan
      ,qs.*
    FROM
      sys.dm_exec_query_stats AS qs
    CROSS APPLY
      sys.dm_exec_sql_text(qs.plan_handle) AS st
    CROSS APPLY
      sys.dm_exec_query_plan(qs.plan_handle) AS qp
    WHERE
      (CHARINDEX(vw.viewname, st.text, 1) > 0)
      AND (st.dbid = DB_ID())
  ) AS t;
GO
```

## sys.dmv_exec_cached_plans
The final query provides information about unused views by using the DMV [sys.dmv_exec_cached_plans](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md). However, the execution plan cache is dynamic, and results can vary. As such, use this query over time to determine whether or not a view is actually being used or not. 

```sql
SELECT
  SCHEMA_NAME(vw.schema_id) AS schemaname
  ,vw.name AS name
  ,vw.object_id AS viewid
FROM
  sys.views AS vw
WHERE
  (vw.is_ms_shipped = 0)
EXCEPT
SELECT
  SCHEMA_NAME(o.schema_id) AS schemaname
  ,o.name AS name
  ,st.objectid AS viewid
FROM
  sys.dm_exec_cached_plans cp
CROSS APPLY
  sys.dm_exec_sql_text(cp.plan_handle) st
INNER JOIN
  sys.objects o ON st.[objectid] = o.[object_id]
WHERE
  st.dbid = DB_ID();
GO
```

## See also
[Dynamic management views and functions](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md) 
