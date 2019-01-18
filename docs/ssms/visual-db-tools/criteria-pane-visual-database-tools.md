---
title: "Criteria Pane (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Designer [SQL Server], Criteria pane"
  - "View Designer, Criteria pane"
  - "entering query options into grid [SQL Server]"
  - "Criteria pane"
  - "inserting query options into grid"
  - "grid showing query options [SQL Server]"
  - "adding query options into grid"
ms.assetid: 6291affe-580e-482f-a7ff-45ce3837956a
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Criteria Pane (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
The Criteria pane allows you to specify query options - such as which data columns to display, how to order the results, and what rows to select - by entering your choices into a spreadsheet-like grid. In the Criteria pane you can specify the following:  
  
-   Columns to display and column name aliases.  
  
-   The table that a column belongs to.  
  
-   Expressions for calculated columns.  
  
-   The sort order for the query.  
  
-   Search conditions.  
  
-   Grouping criteria, including aggregate functions to use for summary reports.  
  
-   New values for UPDATE or INSERT INTO queries.  
  
-   Target column names for INSERT FROM queries.  
  
Changes you make in the Criteria pane are automatically reflected in the Diagram pane and SQL pane. Similarly, the Criteria pane is updated automatically to reflect changes made in the other panes.  
  
## About the Criteria Pane  
The rows in the Criteria pane display the data columns used in your query; columns in the Criteria pane display query options.  
  
The specific information that appears in the Criteria pane depends on the type of query you are creating.  
  
If the Criteria pane is not visible, right-click the designer, point to **Pane**, and then click **Criteria**.  
  
## Options  
  
|**Column**|**Query type**|**Description**|  
|--------------|------------------|-------------------|  
|Column|All|Displays either the name of a data column used for the query or the expression for a computed column. This column is locked so that it is always visible as you scroll horizontally.|  
|Alias|SELECT, INSERT FROM, UPDATE, MAKE TABLE|Specifies either an alternative name for a column or the name you can use for a computed column.|  
|Table|SELECT, INSERT FROM, UPDATE, MAKE TABLE|Specifies the name of the table or table-structured object for the associated data column. This column is blank for computed columns.|  
|Output|SELECT, INSERT FROM, MAKE TABLE|Specifies whether a data column appears in the query output.<br /><br />Note: If the database allows, you can use a data column for sort or search clauses without displaying it in the result set.|  
|Sort Type|SELECT, INSERT FROM|Specifies that the associated data column is used to sort the query results and whether the sort is ascending or descending.|  
|Sort Order|SELECT, INSERT FROM|Specifies the sort priority for data columns used to sort the result set. When you change the sort order for a data column, the sort order for all other columns is updated accordingly.|  
|Group By|SELECT, INSERT FROM, MAKE TABLE|Specifies that the associated data column is being used to create an aggregate query. This grid column appears only if you have chosen **Group By** from the **Tools** menu or have added a GROUP BY clause to the SQL pane.<br /><br />By default, the value of this column is set to **Group By**, and the column becomes part of the GROUP BY clause.<br /><br />When you move to a cell in this column and select an aggregate function to apply to the associated data column, by default the resulting expression is added as an output column for the result set.|  
|Criteria|All|Specifies a search condition (filter) for the associated data column. Enter an operator (the default is "=") and the value to search for. Enclose text values in single quotation marks.<br /><br />If the associated data column is part of a GROUP BY clause, the expression you enter is used for a HAVING clause.<br /><br />If you enter values for more than one cell in the **Criteria** grid column, the resulting search conditions are automatically linked with a logical AND.<br /><br />To specify multiple search condition expressions for a single database column, for example, (fname > 'A') AND (fname < 'M'), add the data column to the Criteria pane twice and enter separate values in the **Criteria** grid column for each instance of the data column.|  
|Or...|All|Specifies an additional search condition expression for the data column, linked to previous expressions with a logical OR. You can add more **Or...** grid columns by pressing the TAB key in the rightmost **Or...** column.|  
|Append|INSERT FROM|Specifies the name of the target data column for the associated data column. When you create an Insert From query, the Query and View Designer attempts to match the source to an appropriate target data column. If the Query and View Designer cannot choose a match, you must provide the column name.|  
|New Value|UPDATE, INSERT INTO|Specifies the value to place into the associated column. Enter a literal value or an expression.|  
  
## See Also  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Diagram Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/diagram-pane-visual-database-tools.md)  
[Rules for Entering Search Values &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/rules-for-entering-search-values-visual-database-tools.md)  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Results Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/results-pane-visual-database-tools.md)  
[SQL Pane &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md)  
  
