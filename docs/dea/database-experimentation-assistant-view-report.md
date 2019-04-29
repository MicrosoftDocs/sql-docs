---
title: View analysis reports in Database Experimentation Assistant for SQL Server upgrades
description: View analysis reports in Database Experimentation Assistant
ms.custom: ""
ms.date: 10/22/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: ajaykar
ms.reviewer: douglasl
manager: craigg
---

# View analysis reports in Database Experimentation Assistant

After you [create your analysis report](database-experimentation-assistant-create-report.md) in Database Experimentation Assistant (DEA), complete the steps described in this article to view the report and gain performance insights provided by your A/B test.

## Select a server

In DEA, select the menu icon. In the expanded menu, select **Analysis Reports** next to the checklist icon to open the Analysis Reports window.

Under **Analysis Reports**, enter the name of a computer running SQL Server that has an analysis database. Select **Connect**. 

![Connect to an existing report](./media/database-experimentation-assistant-view-report/dea-view-report-connect.png)

If you're missing any dependencies, the **Prerequisites** page prompts you with links to install them. Install the prerequisites, and then select **Try again**.

![Prerequisites page](./media/database-experimentation-assistant-view-report/dea-view-report-prereq.png)

## Select an analysis report to view

In the list of analysis reports, double-click a report to open it.

![View existing report](./media/database-experimentation-assistant-view-report/dea-view-report-view-existing.png)

You can get insights into how well your workload is represented, as shown in this example chart:

![Workload Rep Charts](./media/database-experimentation-assistant-view-report/dea-view-report-workload-compare.png)

## View and understand the analysis report

This section walks you through the analysis report.

### Query categories

Select different slices of the left pie chart to show only the queries that fall under that category.

![Report pie slices](./media/database-experimentation-assistant-view-report/dea-view-report-pie-slices.png)

- **Degraded queries**: Queries that performed better in A than in B.  
- **Errors**: Queries that show errors in instance B but not in instance A.  
- **Improved queries**: Queries that ran better in instance B than in instance A.  
- **Indeterminate queries**: Queries that had an indeterminate performance change.  
- **Same**: Queries in which performance stayed the same across instances A and B.

### Individual query drill-down

You can select the query template links to see more detailed information about specific queries.

![Query drill-down](./media/database-experimentation-assistant-view-report/dea-view-report-drilldown.png)

Select a specific query to open a comparison summary for the query.

![Comparison Summary](./media/database-experimentation-assistant-view-report/dea-view-report-comparison-summary.png)

You can see the A and B instances that the query ran on. You can also see a template of what the query might look like. A table displays query information that is specific to instances A and B.

### Error queries

The comparison summary report has expandable **Error Information** and **Query Plan Information** sections. The sections show the errors and plan information for both instances.

Select the error (red) pie to show these types of errors:
- **Existing errors**: Errors that were in A.
- **New errors**: Errors that were in B.
- **Resolved errors**: Errors that were in A but not in B.

![Error charts](./media/database-experimentation-assistant-view-report/dea-view-report-error-charts.png)

## Next steps

- To learn how to generate an analysis report at a command prompt, see [Run at command prompt](database-experimentation-assistant-run-command-prompt.md).

- For a 19-minute introduction to DEA and demonstration, watch the following video:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
