---
title: Use Insight widgets to monitor servers and databases
description: Learn how to use Azure Data Studio insight widgets to turn queries that monitor servers and databases into insightful visualizations.
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.date: 05/14/2019
ms.service: azure-data-studio
ms.topic: how-to
ms.custom:
  - seodec18
  - sqlfreshmay19
  - seo-lt-2019
---

# Manage servers and databases with Insight widgets in Azure Data Studio

Insight widgets take the Transact-SQL (T-SQL) queries you use to monitor servers & databases and turns them into insightful visualizations.

Insights are customizable charts and graphs that you add to server and database monitoring dashboards. View at-a-glance insights of your servers and databases, then drill into more details, and launch management actions that you define.

You can build awesome server and database management dashboards similar to the following example:

![database dashboard](media/insight-widgets/database-dashboard.png)

To jump in and start creating different types of insight widgets, check out the following tutorials:

- [Build a custom insight widget](tutorial-build-custom-insight-sql-server.md)
- *Enable built-in insight widgets*
  - [Enable the performance monitoring insight](tutorial-qds-sql-server.md)
  - [Enable the table space usage insight](tutorial-table-space-sql-server.md)

## SQL Queries

Azure Data Studio tries to avoid introducing yet another language or heavy user interface so it tries to use T-SQL as much as possible with minimal JSON configuration. Configuring insight widgets with T-SQL leverages the countless number of existing sources of useful T-SQL queries that can be turned into insightful widgets.

Insight widgets are composed of one or two T-SQL queries:

- *Insight widget query* is mandatory, and is the query that returns the data that appears in the widget.*
- *Insight details query* is only required if you are creating an insight details page.*

An insight widget query defines a dataset that renders a count, chart, or graph. Insight details query is used to list relevant insight detail information in a tabular format in the insight details panel. 

Azure Data Studio executes insight widget queries and maps the query result set to a chart's dataset then renders it. When users open up an insight's details, it executes the insight details query and prints out the result in a grid view within the dialog.

The basic idea is to write a T-SQL query in a way so it can be used as a dataset of a count, chart, and graph widget. 

## Summary

The T-SQL query and its result set determine the insight widget behavior. Writing a query for a chart type or mapping a right chart type for existing query is the key consideration to build an effective insight widget.



## Additional resources
- [Query Editor](tutorial-sql-editor.md)

