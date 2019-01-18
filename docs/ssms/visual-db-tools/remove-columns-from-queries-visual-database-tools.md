---
title: "Remove Columns from Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "removing columns"
  - "queries [SQL Server], columns"
  - "deleting columns"
  - "dropping columns"
ms.assetid: 6d9819b8-ee2f-4838-9713-c5e3ad37ab46
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Remove Columns from Queries (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
If you no longer want to use a column in a query, you can remove it. If you do, the Query and View Designer removes references to the column in the select list, the sort specification, the search criteria, **SQL Pane**, and any grouping specifications.  
  
> [!NOTE]  
> If you want to remove a column from just the output of a Select query, you can do so without removing it from the query altogether. For details, see [Remove Columns from Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/remove-columns-from-query-results-visual-database-tools.md).  
  
### To remove a column from the query  
  
-   In the **Criteria Pane**, select the grid row containing the column you want to remove and then press DELETE.  
  
    -or-  
  
-   Remove all references to the column in the [SQL Pane](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md).  
  
## See Also  
[Add Columns to Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-columns-to-queries-visual-database-tools.md)  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
