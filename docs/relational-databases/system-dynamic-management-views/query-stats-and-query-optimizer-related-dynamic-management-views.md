---
title: Usage statistics and performance of the views in a SQL Server database
description: Usage statistics and performance of the views in a SQL Server database
manager: craigg
author: craigg-msft
ms.author: craig
ms.date: 22/07/2018
---

# <a name="statistiche-di-utilizzo-e-performance-delle-viste-in-un-database-sql-server"></a>Usage statistics and performance of the views in a SQL Server database

<a name="introduzione"></a>Introduction
============

The performance of a database solution is often the subject of dispute between those who provide the solution and those who personalize. Write T-SQL code optimized, can scale to more data and users, isn't easy and when the complexity increases, code maintenance tasks become difficult to implement by the author.

In this article, I share the methodology of tuning and some scripts that I use to get information about the **performance of queries that use the views** in the database object of the analysis. The presence of nested views that contains non-optimized queries can become the subject of specific analysis. Scripts in this article intent to provide some indicators on the use and performance of the views of a DB.

<a name="alcuni-indicatori-sulle-performance-delle-viste-in-sql-server"></a>Some indicators on the performance of views in SQL Server
=============================================================

The first interesting fact was obtained by querying the sys.dm [_ _exec_query_optimizer_info DMV](https://docs.microsoft.com/it-it/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-optimizer-info-transact-sql) that exposes statistics about optimizations performed by the Query Optimizer SQL Server instance was started; the values are then cumulative.

The CTE that follows, based on the DMV [sys.dm _ _exec_query_optimizer_info](https://docs.microsoft.com/it-it/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-optimizer-info-transact-sql), provides information about the workload. The interesting fact that you get is the number (percentage) of queries that reference a view. I had the opportunity to examine cases where about 85% of the queries executed referenziava a view. Pure data, by itself, does not necessarily indicate a performance problem, but when combined with users ' complaints about the slowness of the system, suggests at least a feature article.

```SQL
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

The study can be performed by connecting the system view [sys. views](https://docs.microsoft.com/it-it/sql/relational-databases/system-catalog-views/sys-views-transact-sql) with the DMV that expose the query statistics, query text, and its execution plan in cache.

The following CTE provides detailed statistical information about queries in the cache using the views in the current database. The values for number of executions, total running time, memory pages read and other information dependent on the version of SQL Server. Provide a clear indication about queries to check or optimize; analysis supported by display of the execution plan in cache.

```SQL
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
      (CHARINDEX(vw.schemaname, st.text, 1) > 0)
      AND (st.dbid = DB_ID())
  ) AS t;
GO
```

The last query provides information about unused views, pay close attention to data mining, is based on the DMV [sys.dm _ _exec_cached_plans](https://docs.microsoft.com/it-it/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql) that is prone to fluctuate within the plan cache execution plans. If a view isn't in cache when executed the query, it's said that that view is to delete. If you're concerned that your cache is fairly representative of your workload, keep simply account. In the next controls, the view will always be present, you can evaluate to other investigations.

```SQL
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

<a name="risorse-correlate"></a>Related resources
=================

Some related resources:

1. [DMVs for Performance Tuning (Video - SQL Saturday Pordenone)](https://www.youtube.com/watch?v=9FQaFwpt3-k)

2. [DMVs for Performance Tuning (Slide e Demo - SQL Saturday Pordenone)](http://www.sqlsaturday.com/589/Sessions/Details.aspx?sid=57409)

3. [SQL Server Tuning in capsule form (movie-SQL Saturday Parma)](https://vimeo.com/200980883)

4. [SQL Server Tuning in a nutshell (slides and Demo-SQL Saturday Parma)](http://www.sqlsaturday.com/566/Sessions/Details.aspx?sid=53988)

5. [Performance Tuning With SQL Server Dynamic Management Views](https://www.red-gate.com/library/performance-tuning-with-sql-server-dynamic-management-views)

6. [The Most Prominent Wait Types of your SQL Server 2016](https://channel9.msdn.com/Blogs/MVP-Data-Platform/The-Most-Prominent-Wait-Types-of-your-SQL-Server-2016)
