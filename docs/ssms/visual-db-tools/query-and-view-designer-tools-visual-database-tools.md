---
title: Query and View Designer Tools
description: "Query and View Designer Tools (Visual Database Tools)"
author: markingmyname
ms.author: maghan
ms.reviewer: vanto
ms.date: 09/17/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords:
  - "vdt.querydesigner"
  - "vdt.pane.diagram"
  - "vdt.pane.grid"
  - "vdt.dlgbox.querybuilder"
  - "vdt.pane.sql"
helpviewer_keywords:
  - "Query Designer [SQL Server], panes"
  - "View Designer, panes"
  - "Query Designer [SQL Server], components"
  - "View Designer, components"
---
# Query and View Designer Tools (Visual Database Tools)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When you design a query, view, in-line function, or single-statement stored procedure, the designer you use consists of four panes: the Diagram pane, the Criteria pane, the SQL pane, and the Results pane.

:::image type="content" source="media/query-and-view-designer-tools-visual-database-tools/vs-queryviewdsgpanes.gif" alt-text="Screenshot of Query Designer.":::

- The Diagram pane displays the tables and other table-valued objects that you're querying. Each rectangle represents a table or table-valued object and shows the available data columns. Joins are indicated by lines between the rectangles. For more information, see [Diagram Pane (Visual Database Tools)](diagram-pane-visual-database-tools.md).

- The Criteria pane contains a spreadsheet-like grid in which you specify options, such as which data columns to display, what rows to select, how to group rows, and so on. For more information, see [Criteria Pane (Visual Database Tools)](criteria-pane-visual-database-tools.md).

- The SQL pane displays the SQL statement for the query or view. You can edit the SQL statement created by the Designer or you can enter your own SQL statement. It's particularly useful for entering SQL statements that can't be created using the Diagram and Criteria panes, such as union queries. For more information, see [SQL Pane (Visual Database Tools)](sql-pane-visual-database-tools.md).

- The Results pane shows a grid with data retrieved by the query or view. In the Query and View Designer, the pane shows the results of the most recently executed SELECT query. You can modify the database by editing values in the cells of the grid, and you can add or delete rows. For more information, see [Results Pane (Visual Database Tools)](results-pane-visual-database-tools.md).

You can create a query or view by working in any of the panes: you can specify a column to display by choosing it in the Diagram pane, entering it into the Criteria pane, or making it part of the SQL statement in the SQL pane.

## Displaying and Hiding Panes

To hide a pane or display a pane that isn't visible, right-click the design surface, point to **Pane**, and then select the name of the pane. If the Query and View Designer is opened in Query Designer mode, the **Results** pane isn't available.

## Related content

- [Design Queries and Views How-to articles (Visual Database Tools)](design-queries-and-views-how-to-topics-visual-database-tools.md)
- [Open the Query and View Designer (Visual Database Tools)](open-the-query-and-view-designer-visual-database-tools.md)
- [SQL Editor (Visual Database Tools)](sql-editor-visual-database-tools.md)
