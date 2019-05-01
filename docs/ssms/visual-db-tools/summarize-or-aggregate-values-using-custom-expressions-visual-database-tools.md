---
title: "Summarize or Aggregate Values Using Custom Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "summarizing query results"
  - "custom expressions to aggregate values [SQL Server]"
ms.assetid: 34130ac1-0106-4766-b324-acb0b7bb6f6e
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# Summarize or Aggregate Values Using Custom Expressions (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
In addition to using aggregate functions to aggregate data, you can create custom expressions to produce aggregate values. You can use custom expressions in place of aggregate functions anywhere in an aggregate query.  
  
For example, in the `titles` table you might want to create a query that shows not just the average price, but what the average price would be if it were discounted.  
  
You cannot include an expression that is based on calculations involving only individual rows in the table; the expression must be based on an aggregate value, because only the aggregate values are available at the time the expression is calculated.  
  
### To specify a custom expression for a summary value  
  
1.  Specify the groups for your query. For details, see [Group Rows in Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/group-rows-in-query-results-visual-database-tools.md).  
  
2.  Move to a blank row of the Criteria pane, and then type the expression in the **Columns** column.  
  
    The [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) automatically assigns a column alias to the expression to create a useful column heading in query output. For more details, see [Create Column Aliases &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/create-column-aliases-visual-database-tools.md).  
  
3.  In the **Group By** column for the expression, select **Expression**.  
  
4.  Run the query.  
  
## See Also  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
  
