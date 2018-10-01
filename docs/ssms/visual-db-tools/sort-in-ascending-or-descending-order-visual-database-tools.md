---
title: "Sort in Ascending or Descending Order (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "descending sorts"
  - "ascending sorts"
ms.assetid: d61cc55b-9ee8-4ecf-a32f-6459ae43910b
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Sort in Ascending or Descending Order (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You can sort query results in ascending or descending order on one or more of the columns in the result set by using the **ASC** or **DESC** keywords with the **ORDER BY** clause.  
  
> [!NOTE]  
> The sort order is determined in part by the column's collation sequence. You can change the collation sequence in the [Collation Dialog Box](../../ssms/visual-db-tools/collation-dialog-box-visual-database-tools.md).  
  
The following procedure assumes that you have a query open in Query and View Designer that uses the ORDER BY clause to sort one or more columns.  
  
### To specify or change the order in which results are sorted  
  
1.  In the [Criteria pane](../../ssms/visual-db-tools/criteria-pane-visual-database-tools.md), click the **Sort Type** field for the column that you want to reorder.  
  
2.  Choose **Ascending** or **Descending** to specify the sort order for the column.  
  
Notice that as you work in the Criteria pane, your query's UNION clause changes to match your most recent actions.  
  
> [!NOTE]  
> When sorting results by more than one column, specify the order in which columns are searched relative to each other by using the Sort Order column. For more information, see [Sort Multiple Columns in Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-multiple-columns-in-queries-visual-database-tools.md).  
  
## See Also  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
