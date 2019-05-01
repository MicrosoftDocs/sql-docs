---
title: "Create Insert Results Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [SQL Server], types"
  - "result sets [SQL Server], queries"
  - "results [SQL Server], query"
  - "Insert Results query"
  - "queries [SQL Server], results"
ms.assetid: 8770d630-09cc-47ec-a0e9-e9de2d7bbc89
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Create Insert Results Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can copy rows from one table to another or within a table using an Insert Results query. For example, in a `titles` table, you can use an Insert Results query to copy information about all the titles for one publisher to a second table that you can make available to that publisher. An Insert Results query is similar to Make Table Queries, but copies rows into an existing table.  
  
> [!TIP]  
> You can also copy rows from one table to another using cut and paste. Create a query for each table and run the queries. Copy the rows you want from one results grid to the other.  
  
When you create an Insert Results query, you specify:  
  
-   The database table to copy rows to (the destination table).  
  
-   The table or tables to copy rows from (the source table). The source table or tables become part of a subquery. If you are copying within a table, the source table is the same as the destination table.  
  
-   The columns in the source table whose contents you want to copy.  
  
-   The target columns in the destination table to copy the data to.  
  
-   Search conditions to define the rows you want to copy.  
  
-   Sort order, if you want to copy the rows in a particular order.  
  
-   Group By options, if you want to copy only summary information.  
  
For example, the following query copies title information from the `titles` table to an archive table called `archivetitles`. The query copies the contents of four columns for all titles belonging to a particular publisher:  
  
```  
INSERT INTO archivetitles   
   (title_id, title, type, pub_id)  
SELECT title_id, title, type, pub_id  
FROM titles  
WHERE (pub_id = '0766')  
```  
  
> [!NOTE]  
> To insert values into a new row, use an Insert Values query.  
  
You can copy the contents of selected columns or of all columns in a row. In either case, the data you are copying must be compatible with the columns in the rows you are copying to. For example, if you copy the contents of a column such as `price`, the column in the row you are copying to must accept numeric data with decimal places. If you are copying an entire row, the destination table must have compatible columns in the same physical position as the source table.  
  
When you create an Insert Results query, the Criteria pane changes to reflect options available for copying data. An Append column is added to allow you to specify the columns into which data should be copied.  
  
> [!CAUTION]  
> You cannot undo the action of executing an Insert Results query. As a precaution, back up your data before executing the query.  
  
### To create an Insert Results query  
  
1.  Create a new query and add the table from which you want to copy rows (the source table). If you are copying rows within a table, you can add the source table as a destination table.  
  
2.  From the **Query Designer** menu, point to **Change Type**, and then click **Insert Results**.  
  
3.  In the [Choose Target Table for Insert Results Dialog Box](../../ssms/visual-db-tools/choose-target-table-for-insert-results-dialog-box-visual-database-tools.md), select the table to copy rows to (the destination table).  
  
    > [!NOTE]  
    > The Query and View Designer cannot determine in advance which tables and views you can update. Therefore, the **Table Name** list in the **Choose Table for Insert From Query** dialog box shows all available tables and views in the data connection you are querying, even those that you might not be able to copy rows to.  
  
4.  In the rectangle representing the table or table-valued object, choose the names of the columns whose contents you want to copy. To copy entire rows, choose **&#42; (All Columns)**.  
  
    The Query and View Designer adds the columns you choose to the **Column** column of the Criteriapane.  
  
5.  In the **Append** column of the Criteria pane, select a target column in the destination table for each column you are copying. Choose *tablename.&#42;* if you are copying entire rows. The columns in the destination table must have the same (or compatible) data types as the columns in the source table.  
  
6.  If you want to copy rows in a particular order, specify a sort order. For details, see [Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md).  
  
7.  Specify the rows to copy by entering search conditions in the **Filter** column. For details, see [Specify Search Criteria &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md).  
  
    If you do not specify a search condition, all rows from the source table will be copied to the destination table.  
  
    > [!NOTE]  
    > When you add a column to search to the Criteria pane, the Query and View Designer also adds it to the list of columns to copy. If you want to use a column for searching but not copy it, clear the check box next to the column name in the rectangle representing the table or table-valued object.  
  
8.  If you want to copy summary information, specify Group By options. For details, see [Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md).  
  
When you execute an Insert Results query, no results are reported in the [Results Pane](../../ssms/visual-db-tools/results-pane-visual-database-tools.md). Instead, a message appears indicating how many rows were copied.  
  
## See Also  
[Types of Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/types-of-queries-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
