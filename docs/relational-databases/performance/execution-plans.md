---
title: "Execution Plans | Microsoft Docs"
description: Learn about execution plans or query plans, which the Query Optimizer creates for the SQL Server Database Engine to run queries. 
ms.custom: ""
ms.date: "03/01/2020"
ms.service: sql
ms.reviewer: wiassaf
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "query plans [SQL Server]"
  - "execution plans [SQL Server]"
  - "execution plan [SQL Server]"
  - "query plan [SQL Server]"
  - "query execution plans"
ms.assetid: 07f8f594-75b4-4591-8c29-d63811d7753f
author: rwestMSFT
ms.author: randolphwest
---
# Execution Plans
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

To be able to execute queries, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] must analyze the statement to determine the most efficient way to access the required data. This analysis is handled by a component called the Query Optimizer. The input to the Query Optimizer consists of the query, the database schema (table and index definitions), and the database statistics. The output of the Query Optimizer is a query execution plan, sometimes referred to as a query plan, or execution plan.   

A query execution plan is a definition of the following: 

- **The sequence in which the source tables are accessed.**  
  Typically, there are many sequences in which the database server can access the base tables to build the result set. For example, if a `SELECT` statement references three tables, the database server could first access `TableA`, use the data from `TableA` to extract matching rows from `TableB`, and then use the data from `TableB` to extract data from `TableC`. The other sequences in which the database server could access the tables are:  
  `TableC`, `TableB`, `TableA`, or  
  `TableB`, `TableA`, `TableC`, or  
  `TableB`, `TableC`, `TableA`, or  
  `TableC`, `TableA`, `TableB`  

- **The methods used to extract data from each table.**  
  Generally, there are different methods for accessing the data in each table. If only a few rows with specific key values are required, the database server can use an index. If all the rows in the table are required, the database server can ignore the indexes and perform a table scan. If all the rows in a table are required but there is an index whose key columns are in an `ORDER BY`, performing an index scan instead of a table scan may save a separate sort of the result set. If a table is very small, table scans may be the most efficient method for almost all access to the table.
  
- **The methods used to compute calculations, and how to filter, aggregate, and sort data from each table.**  
  As data is accessed from tables, there are different methods to perform calculations over data such as computing scalar values, and to aggregate and sort data as defined in the query text, for example when using a `GROUP BY` or `ORDER BY` clause, and how to filter data, for example when using a `WHERE` or `HAVING` clause.

> [!NOTE]
> [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] has three options to display execution plans:        
> -  The ***[Estimated Execution Plan](../../relational-databases/performance/display-the-estimated-execution-plan.md)*** is the compiled plan, as produced by the Query Optimizer based on estimations. This is the query plan that is stored in the plan cache.        
> -  The ***[Actual Execution Plan](../../relational-databases/performance/display-an-actual-execution-plan.md)*** is the compiled plan plus its [execution context](../../relational-databases/query-processing-architecture-guide.md#execution-plan-caching-and-reuse). It becomes available **after the query execution has completed**. This includes actual runtime information such as execution warnings, or in newer versions of the [!INCLUDE[ssde_md](../../includes/ssde_md.md)], the elapsed and CPU time used during execution.         
> -  The ***[Live Query Statistics](../../relational-databases/performance/live-query-statistics.md)*** is the compiled plan plus its execution context. It is available for **in-flight query executions**, and updated every second. This includes runtime information such as the actual number of rows flowing through the [operators](../../relational-databases/showplan-logical-and-physical-operators-reference.md), elapsed time, and the estimated query progress.

> [!TIP]
> For more information on query processing and query execution plans, see the sections [Optimizing SELECT statements](../../relational-databases/query-processing-architecture-guide.md#optimizing-select-statements) and [Execution Plan Caching and Reuse](../../relational-databases/query-processing-architecture-guide.md#execution-plan-caching-and-reuse) of the Query Processing Architecture Guide.

## In This Section  
[Query Profiling Infrastructure](../../relational-databases/performance/query-profiling-infrastructure.md)     
[Display and Save Execution Plans](../../relational-databases/performance/display-and-save-execution-plans.md)     
[Compare and Analyze Execution Plans](../../relational-databases/performance/compare-and-analyze-execution-plans.md)     
[Plan Guides](../../relational-databases/performance/plan-guides.md)     

## See Also  
[Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)     
[Performance Monitoring and Tuning Tools](../../relational-databases/performance/performance-monitoring-and-tuning-tools.md)     
[Query Processing Architecture Guide](../../relational-databases/query-processing-architecture-guide.md)    
[Live Query Statistics](../../relational-databases/performance/live-query-statistics.md)     
[Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md)     
[Monitoring Performance by Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)     
[sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md)     
[sys.dm_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md)     
[Trace flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)    
[Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)
