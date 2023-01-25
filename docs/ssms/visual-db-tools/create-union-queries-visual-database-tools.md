---
description: "Create UNION Queries (Visual Database Tools)"
title: Create UNION Queries
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "queries [SQL Server], types"
  - "UNION queries"
  - "Select query"
  - "combining query results"
  - "merged SELECT query [SQL Server]"
ms.assetid: b5aafb1d-e4ed-4922-b790-56abc5ec551a
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Create UNION Queries (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
The UNION keyword enables you to include the results of two SELECT statements in one resulting table. All rows returned from either SELECT statement are combined into the result of the UNION expression. For examples, see [SELECT Examples (Transact-SQL)](../../t-sql/queries/select-examples-transact-sql.md).  
  
> [!NOTE]  
> The Diagram pane can only display one SELECT clause. Therefore, when you are working with a UNION query, Query Designer hides the Table Operations pane.  
  
### To create a Merged SELECT query  
  
1.  Open a query or create a new one.  
  
2.  In the SQL pane, type a valid UNION expression.  
  
    The following example is a valid UNION expression.  
  
    ```  
    SELECT ProductModelID, Name  
    FROM Production.ProductModel  
    UNION  
    SELECT ProductModelID, Name   
    FROM dbo.Gloves;  
    ```  
  
3.  On the **Query Designer** menu, click **Execute SQL** to run the query.  
  
    Your UNION query is now formatted by Query Designer.  
  
## See Also  
[Supported Query Types](../../ssms/visual-db-tools/supported-query-types-visual-database-tools.md)  
[Design Queries and Views How-to Topics](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
[Perform Basic Operations with Queries](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
[UNION (Transact-SQL)](../../t-sql/language-elements/set-operators-union-transact-sql.md)