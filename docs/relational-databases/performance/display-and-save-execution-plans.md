---
title: "Display and save Execution Plans"
description: Learn how to display execution plans and how to save execution plans to a file in XML format by using SQL Server Management Studio.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 01/26/2026
ms.service: sql
ms.subservice: performance
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Showplan results"
  - "execution plans [SQL Server]"
  - "queries [SQL Server], tuning"
  - "execution plans [SQL Server], how-to topics"
  - "SQL Server Management Studio [SQL Server], execution plans"
  - "tuning queries [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Display and save execution plans

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

This section explains how to display execution plans and how to save execution plans to a file in XML format by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS).

Execution plans graphically display the data retrieval methods chosen by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer. Execution plans represent the execution cost of specific statements and queries in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using icons rather than the tabular representation produced by the [SET SHOWPLAN_ALL](../../t-sql/statements/set-showplan-all-transact-sql.md) or [SET SHOWPLAN_TEXT](../../t-sql/statements/set-showplan-text-transact-sql.md) statements. This graphical approach is useful for understanding the performance characteristics of a query.

While the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer produces only one execution plan, there's the concept of *estimated* execution plan, an *actual* execution plan, and *live query statistics*.

- An [estimated execution plan](display-the-estimated-execution-plan.md) returns the compiled plan as produced by the Query Optimizer, based on estimations. This is the query plan that is stored in the plan cache. Producing the estimated execution plan doesn't actually execute the query or batch, and therefore doesn't contain any runtime information, such as actual resource usage metrics or runtime warnings.

- An [actual execution plan](display-an-actual-execution-plan.md) returns the compiled plan plus its [execution context](../query-processing-architecture-guide.md#execution-plan-caching-and-reuse). It becomes available after the query execution has completed. This plan includes actual runtime information such as execution warnings, and in newer versions of the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)], the elapsed and CPU time used during execution.

- [Live query statistics](live-query-statistics.md) return the compiled plan plus its execution context. This plan is available for in-flight query executions, and is updated every second. This includes runtime information such as the actual number of rows flowing through the [operators](../showplan-logical-and-physical-operators-reference.md), elapsed time, and the estimated query progress.

For more information on query execution plans, see the [Query processing architecture guide](../query-processing-architecture-guide.md).

## Related content

- [Display the Estimated Execution Plan](display-the-estimated-execution-plan.md)
- [Display an actual execution plan](display-an-actual-execution-plan.md)
- [Save an Execution Plan in XML Format](save-an-execution-plan-in-xml-format.md)
