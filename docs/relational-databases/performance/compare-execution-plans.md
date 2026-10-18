---
title: "Compare Execution Plans"
description: Learn how to compare similarities and differences between actual graphical execution plans by using SQL Server Management Studio Plan Comparison feature.
author: thesqlsith
ms.author: derekw
ms.reviewer: wiassaf
ms.date: 09/07/2025
ms.service: sql
ms.subservice: performance
ms.topic: how-to
helpviewer_keywords:
  - "comparing execution plans"
  - "compare execution plans"
  - "plan comparison"
  - "execution plans [SQL Server], comparing"
  - "Query Store, comparing plans"
---
# Compare execution plans

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article describes how to compare similarities and differences between actual graphical execution plans by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] Plan Comparison feature. This feature is available starting with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] v16.

> [!NOTE]  
> Actual execution plans are generated after the [!INCLUDE [tsql](../../includes/tsql-md.md)] queries or batches execute. Because of this, an actual execution plan contains runtime information, such as actual number of rows, resource usage metrics, and runtime warnings (if any). For more information, see [Display an actual execution plan](display-an-actual-execution-plan.md).

The ability to compare plans is something that database professionals might have to do for troubleshooting reasons:

- Find why a query or batch suddenly slowed down.
- Understand the effect of a query rewrite.
- Observe how a specific performance-enhancing change introduced to the schema design (like a new index) has effectively changed the execution plan.

The **Plan Comparison** menu option allows side-by-side comparison of two different execution plans, for easier identification of similarities and changes that explain the different behaviors for all the reasons stated previously. This option can compare between:

- Two previously saved execution plan files (*.sqlplan* extension).
- One active execution plan and one previously saved query execution plan.
- Two selected query plans in [Query Store](monitoring-performance-by-using-the-query-store.md).

> [!TIP]  
> Plan Comparison works with any *.sqlplan* files, even from older versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Also, this option enables an offline compare, so there's no need to be connected to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance.

When two execution plans are compared, regions of the plan that **do essentially the same** are highlighted in the same color and pattern. Selecting a colored region in one plan will center the other plan on the matching node in that plan. You can still compare unmatched operators and nodes of the execution plans, but in that case you must manually select the operators to compare.

> [!IMPORTANT]  
> Only nodes considered to change the shape of the plan are used to check for similarities. Therefore, there might be a node that isn't colored, in the middle of two nodes that are in the same subsection of the plan. The lack of color in this case implies that the nodes weren't considered when checking if the sections are equal.

## Compare execution plans

1. Open a previously saved query execution plan file (`.sqlplan`) using the **File** menu and selecting on **Open File**, or drag a plan file to [!INCLUDE [ssManStudio](../../includes/ssManStudio-md.md)] window. Alternatively, if you just executed a query and chose to display its execution plan, move to the **Execution Plan** tab in the results pane.

1. Right-click in a blank area of the execution plan and select **Compare Showplan**.

   :::image type="content" source="media/plancomparisonmenuoption.png" alt-text="Screenshot of Right-click Compare Showplan.":::

1. Choose the second query plan file that you would like to compare with. The second file opens so that you can compare the plans.

1. The compared plans open a new window, by default with one on top and one on the bottom. The default selection is the first occurrence of an operator or node that is common in the compared plans, but showing differences between plans. All highlighted operators and nodes exist in both compared plans. Selecting a highlighted operator in the top or left plans automatically selects the corresponding operator in the bottom or right plans. Selecting the root node operator in any of the compared plans (the `SELECT` node in the following image) also selects the respective root node operator in the other compared plan.

   :::image type="content" source="media/plancomparison-plans.png" alt-text="Screenshot of Plan comparison of two saved plan files." lightbox="media/plancomparison-plans.png":::

   > [!TIP]  
   > You can toggle the display of the execution plan comparison to side-by-side, by right-clicking a blank area of the execution plan and selecting **Toggle Splitter Orientation**.

   All zooming and navigation options available for execution plans work in plan comparison mode. For more information, see [Display an actual execution plan](display-an-actual-execution-plan.md).

1. A dual properties window also opens on the right side, in the scope of the default selection. Properties that exist in both compared operators but have differences, are preceded by the *not equal* sign (&ne;) for easier identification.

   :::image type="content" source="media/plancomparison-properties.png" alt-text="Screenshot of Dual properties window.":::

1. The **Showplan Analysis** comparison navigation window also opens on the bottom. Three tabs are available:

   1. In the **Statement Options** tab, the default selection is *Highlight similar operations* and the same highlighted operator or node in compared plans share the same color and line pattern. Navigate between similar areas in compared plans by selecting on a lime pattern. You can also choose to highlight differences in plans rather similarities, by selecting *Highlight operations not matching similar segments*.

      > [!NOTE]  
      > By default, database names are ignored when comparing plans to allow comparison of plans captured for databases that have different names, but share the same schema. For example, when comparing plans from databases `ProdDB` and `TestDB`. This behavior can be changed with the *Ignore database name when comparing operators* option.

      :::image type="content" source="media/plancomparison-analysis.png" alt-text="Screenshot of Showplan Analysis window." lightbox="media/plancomparison-analysis.png":::

   1. The **Multi Statement** tab is useful when comparing plans with multiple statements, by allowing the right statement pair to be compared.

      :::image type="content" source="media/plancomparison-multiple.png" alt-text="Screenshot of Multiple statements in compared plan." lightbox="media/plancomparison-multiple.png":::

   1. In the **Scenarios** tab, you can find an automated analysis of some of the most relevant aspects to look at in what relates to [Cardinality Estimation (SQL Server)](cardinality-estimation-sql-server.md) differences in compared plans. For each listed operator on the left pane, the right pane shows details about the scenario in the *Select here for more information about this scenario* link, and possible reasons to explain that scenario are listed.

      :::image type="content" source="media/plancomparison-scenarios.png" alt-text="Screenshot of Different estimated rows." lightbox="media/plancomparison-scenarios.png":::

   If this window is closed, right-click on a blank area of a compared plan, and select **Showplan Compare Options** to reopen.

   :::image type="content" source="media/plancomparison-options.png" alt-text="Screenshot of Plan compare options.":::

## Compare execution plans in Query Store

1. In Query Store, identify a query that has more than one execution plan. For more information on Query Store scenarios, see [Query Store Usage Scenarios](../../relational-databases/performance/query-store-usage-scenarios.md#identify-and-tune-top-resource-consuming-queries).

1. Use a combination of the **Shift** key and your mouse to select two plans for the same query.

   :::image type="content" source="media/plancomparison-querystore.png" alt-text="Screenshot of Select two plans in Query Store.":::

1. Use the button **Compare the plans for the select query in a separate window** to start plan comparison. Then steps 4 through 6 of *To compare execution plans* are applicable.

   :::image type="content" source="media/plancomparison-querystoreoption.png" alt-text="Screenshot of Compare Showplan in Query Store.":::
