---
title: "Display and save execution plans"
description: Learn how to display execution plans and how to save execution plans to a file in XML format by using SQL Server Management Studio.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 06/01/2023
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "Showplan results"
  - "execution plans [SQL Server]"
  - "queries [SQL Server], tuning"
  - "execution plans [SQL Server], how-to topics"
  - "SQL Server Management Studio [SQL Server], execution plans"
  - "tuning queries [SQL Server]"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Display and save execution plans

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This section explains how to display execution plans and how to save execution plans to a file in XML format by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS).

> [!NOTE]  
> For more information about viewing and saving plans in Azure Data Studio, see [Query Plan Viewer in Azure Data Studio](/azure-data-studio/query-plan-viewer).

Execution plans graphically display the data retrieval methods chosen by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer. Execution plans represent the execution cost of specific statements and queries in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using icons rather than the tabular representation produced by the [SET SHOWPLAN_ALL](../../t-sql/statements/set-showplan-all-transact-sql.md) or [SET SHOWPLAN_TEXT](../../t-sql/statements/set-showplan-text-transact-sql.md) statements. This graphical approach is useful for understanding the performance characteristics of a query.

While the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Query Optimizer produces only one execution plan, there is the concept of *estimated* execution plan, an *actual* execution plan, and *live query statistics*.

- An [estimated execution plan](display-the-estimated-execution-plan.md) returns the compiled plan as produced by the Query Optimizer, based on estimations. This is the query plan that is stored in the plan cache. Producing the estimated execution plan doesn't actually execute the query or batch, and therefore doesn't contain any runtime information, such as actual resource usage metrics or runtime warnings.

- An [actual execution plan](display-an-actual-execution-plan.md) returns the compiled plan plus its [execution context](../query-processing-architecture-guide.md#execution-plan-caching-and-reuse). It becomes available after the query execution has completed. This plan includes actual runtime information such as execution warnings, and in newer versions of the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)], the elapsed and CPU time used during execution.

- [Live query statistics](live-query-statistics.md) return the compiled plan plus its execution context. This plan is available for in-flight query executions, and is updated every second. This includes runtime information such as the actual number of rows flowing through the [operators](../showplan-logical-and-physical-operators-reference.md), elapsed time, and the estimated query progress. This option isn't available in Azure Data Studio.

For more information on query execution plans, see the [Query processing architecture guide](../query-processing-architecture-guide.md).

## Next steps

- [Display the estimated execution plan](display-the-estimated-execution-plan.md)
- [Display an actual execution plan](display-an-actual-execution-plan.md)
- [Save an execution plan in XML format](save-an-execution-plan-in-xml-format.md)
