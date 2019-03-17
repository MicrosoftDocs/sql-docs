---
title: "Create Table Aliases (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "table aliases [SQL Server]"
  - "aliases [SQL Server], tables"
ms.assetid: 49e61e85-8abf-4ca7-8c70-7e9f8f1078bd
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Create Table Aliases (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Aliases can make it easier to work with table names. Using aliases is helpful when:  
  
-   You want to make the statement in the [SQL Pane](../../ssms/visual-db-tools/sql-pane-visual-database-tools.md) shorter and easier to read.  
  
-   You refer to the table name often in your query - such as in qualifying column names - and want to be sure you stay within a specific character-length limit for your query. (Some databases impose a maximum length for queries.)  
  
-   You are working with multiple instances of the same table (such as in a self-join) and need a way to refer to one instance or the other.  
  
For example, you can create an alias `"e"` for a table name `employee_information`, and then refer to the table as `"e"` throughout the rest of the query.  
  
### To create an alias for a table or table-valued object  
  
1.  Add the table or table-valued object to your query.  
  
2.  In the **Diagram Pane**, right-click the object for which you want to create an alias, then select **Properties** from the shortcut menu.  
  
3.  In the **Properties** window, enter the alias in the **Alias** field.  
  
## See Also  
[Add Tables to Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-tables-to-queries-visual-database-tools.md)  
[Sort and Group Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/sort-and-group-query-results-visual-database-tools.md)  
[Summarize Query Results &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
  
