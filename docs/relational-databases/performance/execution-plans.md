---
title: "Execution plan overview"
description: Learn about execution plans or query plans, which the Query Optimizer creates for the SQL Server Database Engine to run queries.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, vanto
ms.date: 09/17/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "query plans [SQL Server]"
  - "execution plans [SQL Server]"
  - "execution plan [SQL Server]"
  - "query plan [SQL Server]"
  - "query execution plans"
---
# Execution plan overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

To be able to execute queries, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] must analyze the statement to determine an efficient way to access the required data and process it. This analysis is handled by a component called the Query Optimizer. The input to the Query Optimizer consists of the query, the database schema (table and index definitions), and the database statistics. The Query Optimizer builds one or more *query execution plans*, sometimes referred to as *query plans* or *execution plans*. The Query Optimizer chooses a query plan using a set of heuristics to balance compilation time and plan optimality in order to find a good query plan.

> [!TIP]  
> For more information on query processing and query execution plans, see the sections [Optimizing SELECT statements](../query-processing-architecture-guide.md#optimizing-select-statements) and [Execution plan caching and reuse](../query-processing-architecture-guide.md#execution-plan-caching-and-reuse) of the Query processing architecture guide.
>
> For information on viewing execution plans in SQL Server Management Studio and Azure Data studio, see [Display and save execution plans](display-and-save-execution-plans.md).

A query execution plan is the definition of:

- **The sequence in which the source tables are accessed.**

  Typically, there are many sequences in which the database server can access the base tables to build the result set. For example, if a `SELECT` statement references three tables, the database server could first access `TableA`, use the data from `TableA` to extract matching rows from `TableB`, and then use the data from `TableB` to extract data from `TableC`. The other sequences in which the database server could access the tables are:  
  `TableC`, `TableB`, `TableA`, or  
  `TableB`, `TableA`, `TableC`, or  
  `TableB`, `TableC`, `TableA`, or  
  `TableC`, `TableA`, `TableB`

- **The methods used to extract data from each table.**

  Generally, there are different methods for accessing the data in each table. If only a few rows with specific key values are required, the database server can use an index. If all the rows in the table are required, the database server can ignore the indexes and perform a table scan. If all the rows in a table are required but there's an index whose key columns are in an `ORDER BY`, performing an index scan instead of a table scan might save a separate result set. If a table is small, table scans might be the most efficient method for almost all access to the table.

- **The methods used to compute calculations, and how to filter, aggregate, and sort data from each table.**

  As data is accessed from tables, there are different methods to perform calculations over data such as computing scalar values, and to aggregate and sort data as defined in the query text, for example when using a `GROUP BY` or `ORDER BY` clause, and how to filter data, for example when using a `WHERE` or `HAVING` clause.

## Related content

- [Monitor and Tune for Performance](monitor-and-tune-for-performance.md)
- [Performance monitoring and tuning tools](performance-monitoring-and-tuning-tools.md)
- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Live Query Statistics](live-query-statistics.md)
- [Activity Monitor](../performance-monitor/activity-monitor.md)
- [Monitor performance by using the Query Store](monitoring-performance-by-using-the-query-store.md)
- [sys.dm_exec_query_statistics_xml](../system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md)
- [sys.dm_exec_query_profiles](../system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md)
- [DBCC TRACEON - Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
- [Logical and physical showplan operator reference](../showplan-logical-and-physical-operators-reference.md)
- [Query Profiling Infrastructure](query-profiling-infrastructure.md)
- [Display and save execution plans](display-and-save-execution-plans.md)
- [Compare and Analyze Execution Plans](compare-and-analyze-execution-plans.md)
- [Plan Guides](plan-guides.md)
