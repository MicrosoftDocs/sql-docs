---
title: "Add Columns to Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "inserting columns"
  - "columns [SQL Server], adding"
  - "queries [SQL Server], columns"
  - "adding columns"
ms.assetid: 82f3ba72-3d72-4fb1-8179-2a953a782787
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Add Columns to Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
To use a column in a query, you must add it to the query. You might add a column to display it in query output, to use it for sorting, to search the contents of the column, or to summarize its contents. You can decide which of the columns you use in the query are included in the results pane when the query is run. For more information see [Remove Columns from Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/remove-columns-from-query-results-visual-database-tools.md).  
  
> [!NOTE]  
> To view the data type of a column in Query and View Designer; select the table or table-valued object in the **Diagram Pane** and in the properties window click Column List. Then click the **ellipses (...)** to open the **Column List** dialog box.  
  
Wherever you use a column in a query, you can also use an expression that can consist of any combination of columns, literals, operators, and functions.  
  
### To add an individual column  
  
-   In the **Diagram Pane**, select the check box next to the column that you want to include.  
  
    -or-  
  
-   In the **Criteria Pane**, move to the first blank grid row, click the field in the **Column** column, and select a column name from the drop-down list.  
  
### To add all columns for one table or table-valued object  
  
-   In the **Diagram Pane**, select the check box next to **&#42;(All Columns)**.  
  
### To add all columns for all tables and table-structured objects  
  
1.  Make sure no join lines in the **Table Operations Pane** are selected.  
  
2.  Right-click in the empty space of the Design window and choose **Properties** from the shortcut menu.  
  
3.  In the Properties window click **Output all columns** and choose **Yes** or **No** from the dropdown list.  
  
## See Also  
[Remove Columns from Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/remove-columns-from-query-results-visual-database-tools.md)  
[Remove Columns from Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/remove-columns-from-queries-visual-database-tools.md)  
[Specify Search Criteria &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
