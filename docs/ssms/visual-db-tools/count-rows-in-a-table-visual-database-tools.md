---
title: "Count Rows in a Table (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "totals [SQL Server], row counts"
  - "row counts [SQL Server]"
  - "column values [SQL Server]"
  - "number of rows"
  - "number of values"
  - "counting rows"
ms.assetid: dda4296a-1d16-4e77-8d6f-e295f6dd4e87
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Count Rows in a Table (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can count rows in a table to determine:  
  
-   The total number of rows in a table, for example, a count of all the books in a `titles` table.  
  
-   The number of rows in a table that meet a specific condition, for example, the number of books by one publisher in a `titles` table.  
  
-   The number of values in a particular column.  
  
When you count values in a column, nulls are not included in the count. For example, you might count the number of books in a `titles` table that have values in the `advance` column. By default, the count includes all values, not just unique values.  
  
The procedures for all three types of counts are similar.  
  
### To count all the rows in a table  
  
1.  Be sure the table you want to summarize is already present in the Diagram pane.  
  
2.  Right-click the background of the Diagram pane, then choose **Add Group By** from the shortcut menu. The [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) adds a **Group By** column to the grid in the Criteria pane.  
  
3.  Select **&#42; (All Columns)** in the rectangle representing the table or table-valued object.  
  
    The Query and View Designer automatically fills the term **Count** into the **Group By** column in the Criteria pane and assigns a column alias to the column you are summarizing. You can replace this automatically generated alias with a more meaningful one. For more details, see [Create Column Aliases &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-column-aliases-visual-database-tools.md).  
  
4.  Run the query.  
  
### To count all the rows that meet a condition  
  
1.  Be sure the table you want to summarize is already present in the Diagram pane.  
  
2.  Right-click the background of the Diagram pane, then choose **Add Group By** from the shortcut menu. The Query and View Designer adds a **Group By** column to the grid in the Criteria pane.  
  
3.  Select **&#42;(All Columns)** in the rectangle representing the table or table-structured object.  
  
    The Query and View Designer automatically fills the term **Count** into the **Group By** column in the Criteria pane and assigns a column alias to the column you are summarizing. To create a more useful column heading in query output, see [Create Column Aliases &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-column-aliases-visual-database-tools.md).  
  
4.  Add the data column that you want to search, and then clear the check box in the **Output** column.  
  
    The Query and View Designer automatically fills the term **Group By** into the **Group By** column of the grid.  
  
5.  Change **Group By** in the **Group By** column to **Where**.  
  
6.  In the **Filter** column for the data column to search, enter the search condition.  
  
7.  Run the query.  
  
### To count the values in a column  
  
1.  Be sure the table you want to summarize is already present in the Diagram pane.  
  
2.  Right-click the background of the Diagram pane, then choose **Add Group By** from the shortcut menu. The Query and View Designer adds a **Group By** column to the grid in the Criteria pane.  
  
3.  Add the column that you want to count to the Criteria pane.  
  
    The Query and View Designer automatically fills the term **Group By** into the **Group By** column of the grid.  
  
4.  Change **Group By** in the **Group By** column to **Count**.  
  
    > [!NOTE]  
    > To count only unique values, choose **Count Distinct**.  
  
5.  Run the query.  
  
## See Also  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
  
