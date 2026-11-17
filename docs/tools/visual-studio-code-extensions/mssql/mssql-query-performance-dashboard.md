---
title: Query Performance Dashboard Overview (Preview)
titleSuffix: MSSQL Extension for Visual Studio Code
description: Use the Query Performance Dashboard in the MSSQL extension for Visual Studio Code to analyze Query Store data, compare performance, and review query plans.
author: cwebb-msft
ms.author: chweb
ms.reviewer: yoleichen
ms.date: 07/07/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: overview
ms.collection:
  - data-tools
ai-usage: ai-generated
---

# Query Performance Dashboard (preview)

The Query Performance Dashboard in the MSSQL extension for Visual Studio Code helps you investigate query performance without leaving your editor. It uses data from [Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md) to identify resource-intensive queries, explore execution history and wait statistics, compare performance over time, and review execution plans.

Query Store captures query text, execution plans, and runtime statistics for a database. The dashboard presents this historical data so you can move from a database-wide overview to an individual query and its plans. The available history depends on the data that Query Store captures and retains.

> [!NOTE]
> The Query Performance Dashboard is in public preview.

## Prerequisites

- [Connect to a database](mssql-database-connections.md) that supports Query Store.
- Enable Query Store in the database and allow it to capture workload data. For setup instructions, see [Enable Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md#enable-the-query-store).
- Use an account with permission to read Query Store data in the target database.

If Query Store is off, enable it before investigating query performance. If Query Store is read-only, you can review existing data, but it doesn't capture new activity. An empty time frame can also mean that Query Store hasn't captured or retained data for that period.

## Open the dashboard

1. In the **Connections** view, expand your connection and right-click the database you want to investigate.
1. Select **Open Query Performance Dashboard (Preview)**.

:::image type="content" source="media/mssql-query-performance-dashboard/query-performance-dashboard-context-menu.png" alt-text="Screenshot of the database context menu with Open Query Performance Dashboard (Preview) highlighted.":::

The dashboard opens for the selected database. Use the database selector at the top of the dashboard to switch databases.

## Explore the query overview

The overview lists queries for the selected database and time frame. Select a category to focus on a different aspect of the workload.

:::image type="content" source="media/mssql-query-performance-dashboard/query-performance-dashboard-overview.png" alt-text="Screenshot of the query overview with High CPU selected, Rank by set to Total, and queries ranked by CPU time for the past 24 hours." lightbox="media/mssql-query-performance-dashboard/query-performance-dashboard-overview.png":::

| Category | Use it to find |
| --- | --- |
| **High CPU** | Queries that consume the most CPU time. |
| **High memory** | Queries with the largest memory grants. |
| **Duration** | Queries with the longest execution times. |
| **Most frequent** | Queries with the highest execution counts. |
| **High reads** | Queries with the most logical reads. |

For resource and duration categories, use **Rank by** to choose **Total**, **Average**, **Maximum**, **Minimum**, or **Standard deviation**. For example, rank by **Total** to identify the largest overall CPU consumers, or by **Average** to find queries with a high CPU cost per execution. **Most frequent** ranks queries by execution count and doesn't use this selector.

Use the time-frame selector at the upper right to choose the past hour, 12 hours, 24 hours, seven days, or 30 days, or set a custom range. The default is **Past 24 hours**. Select **Refresh** to reload the data. Rankings and execution counts reflect the selected period, and the time frame stays selected as you move between the overview and query details.

To focus the list further, enter text in **Search queries**. Select the star beside a query to mark it as a favorite, and turn on **Favorites only** to show your favorites.

Select a **Query ID** to open the details for that query.

## Investigate a query

The query detail page brings together the query text, summary statistics, and four tabs: **Execution history**, **Waits**, **Compare**, and **Plans**. Use these views to investigate when a query became expensive, what it waited for, and whether a different execution plan coincided with the change.

:::image type="content" source="media/mssql-query-performance-dashboard/query-performance-dashboard-query-details.png" alt-text="Screenshot of query details showing execution counts, CPU statistics, the Execution history chart and interval table, and the query text panel." lightbox="media/mssql-query-performance-dashboard/query-performance-dashboard-query-details.png":::

The summary shows execution count, average, minimum, and maximum values for the selected metric, and the last execution time. The query text remains available beside the tabs. Select **Open in query editor** to open the SQL in an editor, or **Copy SQL** to copy it.

### Review execution history

Use **Execution history** to explore how the query performs over the selected time frame.

1. Choose a **Metric**, such as duration, CPU time, logical reads, or memory grants.
1. Use **View by** to choose **Total**, **Average**, **Maximum**, or **Minimum**.
1. Review the chart for changes or spikes, and use the plan indicators to see which plans ran during those intervals.
1. Review the interval table for execution counts, plan IDs, duration, CPU time, and logical reads.

For example, switch from total CPU time to average CPU time to distinguish an increase in execution frequency from an increase in the CPU cost of each execution.

### Review waits

Select **Waits** to understand where the query spends time waiting. The view shows wait time over the selected period and breaks it down by wait category, with total wait time and average wait time per execution.

Use this view alongside execution history to investigate whether a slowdown involves resource consumption or waiting, such as waits for locks or I/O. Wait statistics are available only when the database supports Query Store wait statistics and wait-statistics capture is enabled.

## Compare recent and baseline performance

Use the **Compare** tab to compare CPU time or memory grants between the selected period and an earlier baseline. For example, compare the past 24 hours with the preceding 30 days to investigate a change in a query's typical resource use.

1. Set the dashboard time frame to **Past 24 hours**, or choose another period to investigate.
1. Open a query and select **Compare**.
1. Select **CPU** or **Memory**.
1. Use **View by** to select **Average per execution** or **Total per day**. The first compares the cost of each execution; the second compares daily resource use across periods of different lengths.
1. Select a baseline: **Same length before**, **7 days before**, or **30 days before**. The baseline ends where the selected recent period starts, so the two periods don't overlap.
1. Review the recent value, baseline value, and percentage change. Use the per-plan table to see how individual plans contribute to the difference.

The comparison depends on Query Store data being available for both periods. **Memory** refers to query memory grants, not total database or server memory usage.

## Review and compare query plans

Select **Plans** to review the query's execution plans captured in Query Store. The list includes plan IDs, execution counts, average duration and CPU time, last execution times, and plan-forcing status. Runtime statistics reflect the selected time frame.

Select a plan ID to open that execution plan. To compare different plans for the same query:

1. Select the checkboxes beside two plans in the list.
1. Select **Compare plans** to open the plan comparison view.
1. Review the plans to investigate differences in how the query executes.

You can compare two plans at a time. Use execution history and the performance comparison together with the plan comparison to investigate whether a plan change coincides with higher CPU time, memory grants, or duration.

## Continue the investigation in chat

Use **Chat** to continue investigating a query in GitHub Copilot Chat. You can find it in two places:

- In the overview, open the actions menu (**...**) at the right of a query row, and select **Chat**.
- On the query detail page, select **Chat** at the upper right, above the query text. This button remains available as you switch between **Execution history**, **Waits**, **Compare**, and **Plans**.

:::image type="content" source="media/mssql-query-performance-dashboard/query-performance-dashboard-chat-menu.png" alt-text="Screenshot of a query row's actions menu with Copy and Chat options." lightbox="media/mssql-query-performance-dashboard/query-performance-dashboard-chat-menu.png":::

Selecting **Chat** opens GitHub Copilot Chat in agent mode and sends a prompt with the server, database, saved connection profile, query ID, and selected time range. The prompt explicitly references `#mssql_query_store_analyze_query`, which retrieves query statistics, execution history, waits, and summaries of the main plans for a brief performance assessment. It then asks what you want to investigate next and requires confirmation before applying changes.

Query Store diagnostic reads don't request confirmation or modify your database. Forcing or unforcing a plan is a separate operation that requires review and confirmation. For example prompts and the full workflow, see [Investigate Query Store performance with GitHub Copilot](../github-copilot/query-optimizer-assistant.md#investigate-query-store-performance-preview). To set up Copilot, see [GitHub Copilot for the MSSQL extension](../github-copilot/overview.md).

## Feedback and support

[!INCLUDE [feedback](../includes/feedback.md)]

## Related content

- [Monitor performance by using the Query Store](../../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Connect to a database with the MSSQL extension for Visual Studio Code](mssql-database-connections.md)
- [Query Profiler](mssql-query-profiler.md)
- [GitHub Copilot for the MSSQL extension](../github-copilot/overview.md)
