---
title: "Display an actual execution plan"
description: Learn how to generate actual graphical execution plans by using SQL Server Management Studio. An actual graphical execution plan contains runtime information.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords:
  - "displaying execution plans"
  - "actual execution plans"
  - "viewing execution plans"
  - "execution plans [SQL Server], displaying"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Display an actual execution plan

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to generate actual graphical execution plans by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Actual execution plans are generated after the T-SQL queries or batches execute. Because of this, an actual execution plan contains runtime information, such as actual resource usage metrics and runtime warnings (if any). The execution plan that is generated displays the actual query execution plan that the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] used to execute the queries.

To use this feature, users must have the appropriate permissions to execute the [!INCLUDE [tsql](../../includes/tsql-md.md)] queries for which a graphical execution plan is being generated, and they must be granted the SHOWPLAN permission for all databases referenced by the query.

> [!NOTE]  
> To retrieve an actual execution plan for dedicated SQL pools (formerly SQL DW) and dedicated SQL pools in Azure Synapse Analytics, there are different commands. For more information, see [Monitor your Azure Synapse Analytics dedicated SQL pool workload using DMVs](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-manage-monitor#monitor-query-execution).

## Include an execution plan for a query during execution

1. On the [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] toolbar, select **Database Engine Query**. You can also open an existing query and display the estimated execution plan by selecting the **Open File** toolbar button and locating the existing query.

1. Enter the query for which you would like to display the actual execution plan.

1. On the **Query** menu, select **Include Actual Execution Plan** or select the **Include Actual Execution Plan** toolbar button.

   :::image type="content" source="media/display-an-actual-execution-plan/actual-execution-plan-toolbar.png" alt-text="Screenshot from SQL Server Management Studio showing the Actual Execution Plan button on the toolbar." lightbox="media/display-an-actual-execution-plan/actual-execution-plan-toolbar.png":::

1. Execute the query by selecting the **Execute** toolbar button. The plan used by the query optimizer is displayed on the **Execution Plan** tab in the results pane.

   :::image type="content" source="media/display-an-actual-execution-plan/actual-execution-plan.png" alt-text="Screenshot from SQL Server Management Studio showing a graphical Actual Execution Plan." lightbox="media/display-an-actual-execution-plan/actual-execution-plan.png":::

1. Pause the mouse over the logical and physical operators to view the description and properties of the operators in the displayed ToolTip, including properties of the overall execution plan, by selecting the root node operator (the SELECT node in the picture above).

   Alternatively, you can view operator properties in the **Properties** window. If **Properties** isn't visible, right-click an operator and select **Properties**. Select an operator to view its properties.

   :::image type="content" source="media/display-an-actual-execution-plan/plan-properties.png" alt-text="Screenshot from SQL Server Management Studio indicating where to right-click Properties in a plan operator.":::

1. You can alter the display of the execution plan by right-clicking the execution plan and selecting **Zoom In**, **Zoom Out**, **Custom Zoom**, or **Zoom to Fit**. **Zoom In** and **Zoom Out** allow you to zoom in or out on the execution plan, while **Custom Zoom** allows you to define your own zoom, such as zooming at 80 percent. **Zoom to Fit** magnifies the execution plan to fit the result pane. Alternatively, use a combination of the CTRL key and your mouse wheel to activate **dynamic zoom**.

1. To navigate the display of the execution plan, use the vertical and horizontal scroll bars, or **select and hold on any blank area** of the execution plan, and **drag your mouse**. Alternatively, select and hold the plus (+) sign in the right lower corner of the execution plan window, to display a miniature map of the entire execution plan.

> [!NOTE]  
> Alternatively, use [SET STATISTICS XML](../../t-sql/statements/set-statistics-xml-transact-sql.md) to return execution plan information for each statement after executing it. If used in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the *Results* tab will have a link to open the execution plan in graphical format.

For more information, see [Query Profiling Infrastructure](query-profiling-infrastructure.md).

## Related content

- [Execution plan overview](execution-plans.md)
- [Query processing architecture guide](../query-processing-architecture-guide.md)
- [Display the Estimated Execution Plan](display-the-estimated-execution-plan.md)
