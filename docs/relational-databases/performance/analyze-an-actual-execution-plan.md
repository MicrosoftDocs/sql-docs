---
title: "Analyze an actual execution plan"
description: Learn how to analyze actual graphical execution plans, which contain runtime information, by using SQL Server Management Studio Plan Analysis feature.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf
ms.date: 07/08/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "analyzing execution plans"
  - "analyzing actual execution plans"
  - "execution plans [SQL Server], analyzing"
---
# Analyze an actual execution plan

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how you can analyze actual graphical execution plans by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Plan Analysis feature. This feature is available starting with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] v17.4. We generally recommend that you [install the latest version of SSMS](../../ssms/download-sql-server-management-studio-ssms.md).

## Remarks

Actual execution plans are generated after the [!INCLUDE [tsql](../../includes/tsql-md.md)] queries or batches execute. Because of this, an actual execution plan contains runtime information, such as actual number of rows, resource usage metrics and runtime warnings (if any). For more information, see [Display an actual execution plan](display-an-actual-execution-plan.md).

Query performance troubleshooting requires significant expertise in understanding query processing and execution plans, to actually find and fix root causes. For more information, see [Logical and physical showplan operator reference](../showplan-logical-and-physical-operators-reference.md), and [Query processing architecture guide](../query-processing-architecture-guide.md)

[!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] includes functionality that implements some degree of automation in the task of actual execution plan analysis, especially for large and complex plans. The goal is to make it easier to find scenarios of inaccurate [Cardinality Estimation](cardinality-estimation-sql-server.md) and get recommendations on which possible mitigations might be available.

> [!IMPORTANT]  
> Ensure proper testing of proposed mitigations before applying them on production environments.

## Analyze an execution plan for a query

1. Open a previously saved query execution plan file (`.sqlplan`) using the **File** menu and selecting on **Open File**, or drag a plan file to [!INCLUDE [ssManStudio](../../includes/ssManStudio-md.md)] window. Alternatively, if you just executed a query and chose to display its execution plan, move to the **Execution Plan** tab in the results pane.

1. Right-click in a blank area of the execution plan and select **Analyze Actual Execution Plan**.

   :::image type="content" source="media/analyze-an-actual-execution-plan/plan-analysis-menu-option.png" alt-text="Screenshot showing right-click analyze actual execution plan.":::

1. The **Showplan Analysis** window opens on the bottom. The **Multi Statement** tab is useful when analyzing plans with multiple statements, by allowing the right statement to be analyzed.

1. Select the Scenarios tab to see details on the issues found for the actual execution plan. For each listed operator on the left pane, the right pane shows details about the scenario in the *Select here for more information about this scenario* link, and possible reasons to explain that scenario are listed.

:::image type="content" source="media/plananalysis-scenarios.png" alt-text="Screenshot of execution plan analysis results.":::

## Related content

- [Query Profiling Infrastructure](query-profiling-infrastructure.md)
- [Execution plan overview](execution-plans.md)
- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Display an actual execution plan](display-an-actual-execution-plan.md)
