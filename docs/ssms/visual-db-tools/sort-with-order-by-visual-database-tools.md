---
description: "Sort with ORDER BY (Visual Database Tools)"
title: Sort with ORDER BY
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "ORDER BY clause [Visual Database Tools]"
ms.assetid: 459f5640-8058-4c24-97e7-7bbd6168bc39
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Sort with ORDER BY (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
You can sort query results by one or more of the columns in the returned rows by using an ORDER BY clause. You can define an ORDER BY clause by choosing options in the Criteria Details pane.  
  
### To sort a query using an ORDER BY clause  
  
1.  Open a query or create a new one.  
  
2.  In the [Criteria Pane](../../ssms/visual-db-tools/criteria-pane-visual-database-tools.md), click the **Sort Type** column for the row corresponding to the column that you want to use to sort your query results.  
  
3.  Choose *Ascending* or *Descending* from the drop-down list.  
  
> [!NOTE]  
> Clearing the **Sort Type** entry for a column removes that column from the ORDER BY clause.  
  
Notice that as you work in the Criteria pane, your query's UNION clause changes to match your most recent actions.  
  
> [!NOTE]  
> When sorting results by more than one column, specify the order in which columns are searched relative to each other by using the **Sort Order** column. For more information, see **How to: Sort Multiple Columns in Queries**.  
  
## See Also  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
