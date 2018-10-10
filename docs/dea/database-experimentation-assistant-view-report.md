---
title: View Analysis Reports with Database Experimentation Assistant for SQL Server upgrades
description: View Analysis Reports with Database Experimentation Assistant
ms.custom: ""
ms.date: 10/12/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: douglasl
manager: craigg
---

# View analysis reports with Database Experimentation Assistant
After you've [created your analysis report](database-experimentation-assistant-create-report.md), follow the steps in this article to view the report and gain performance insights provided by your A/B test.

## Select a server

Open the tool and select the menu icon on the left side of the screen. Choose **Analysis Reports** next to the checklist icon to open the Analysis Reports window.

From the **Analysis Reports** window, enter a SQL Server name that has an analysis database and select **Connect**. 

![Existing Report Connect](./media/database-experimentation-assistant-view-report/dea-view-report-connect.png)

If you're missing any dependencies, the Prerequisites screen prompts you with links to install them. Install the requirements and select **Try again**.

![PreReq Screen](./media/database-experimentation-assistant-view-report/dea-view-report-prereq.png)

## Select an analysis report to view

From the list of analysis reports, double-click a report name to open it.

![View existing Report](./media/database-experimentation-assistant-view-report/dea-view-report-view-existing.png)

You can get insights into how well your workload is represented, as shown in this example chart:

![Workload Rep Charts](./media/database-experimentation-assistant-view-report/dea-view-report-workload-compare.png)

## View and understand the analysis report

This section walks you through viewing and understanding the report.

### Query categories
Select different slices of the left pie chart to show only the queries that fall under that category.

![Report Pie Slices](./media/database-experimentation-assistant-view-report/dea-view-report-pie-slices.png)

- **Degraded queries**: queries whose performance was better in A than in B.  
- **Errors**: queries that show errors in instance B but not in instance A.  
- **Improved queries**: queries that ran better in instance B than in instance A.  
- **Indeterminate queries**: queries whose performance change was uncertain.  
- **Same**: queries whose performance stayed the same across instances A and B.

### Individual query drilldown
You can select the query template links to see more detailed information about specific queries.

![Query Drilldown](./media/database-experimentation-assistant-view-report/dea-view-report-drilldown.png)

Select a specific query to open that query’s comparison summary.

![Comparison Summary](./media/database-experimentation-assistant-view-report/dea-view-report-comparison-summary.png)

You can also see the A and B instance the specific query ran on, as well as a template of what the query might look like. Also, a table displays query information specific to instances A and B.

### Error queries
The comparison summary report has an expandable section for **Error Information** and **Query Plan Information**, which shows the errors and plan information for both instances.

Select the error (red) pie to show these types of errors:
- **Existing errors**: errors that were in A
- **New errors**: errors that were shown in B
- **Resolved errors**: errors that were in A but not in B

![Error Charts](./media/database-experimentation-assistant-view-report/dea-view-report-error-charts.png)

## Next steps

- [Run at command prompt](database-experimentation-assistant-run-command-prompt.md) shows you how you can generate an analysis report from the command prompt.

- For a 19-minute introduction and demonstration of DEA, watch the following video:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
