---
title: "Remove Tables from Queries (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting tables"
  - "removing tables"
  - "dropping tables"
  - "queries [SQL Server], tables"
ms.assetid: 8fea0b4f-99b7-4050-8d6f-a97ffb839619
author: stevestein
ms.author: sstein
manager: craigg
---
# Remove Tables from Queries (Visual Database Tools)
  You can remove a table - or any table-valued object - from the query.  
  
> [!NOTE]  
>  Removing a table or table-valued object does not delete anything from the database; it only removes it from the current query. For details about removing a table from a database, see [Delete Tables &#40;Database Engine&#41;](../../relational-databases/tables/delete-tables-database-engine.md).  
  
### To remove a table or table-structured object  
  
-   In the **Diagram Pane**, select the table, view, user-defined function, synonym, or query, and then press DELETE, or right-click the object and then choose **Remove** in the resulting dialog box. You can select and remove multiple objects at one time.  
  
     -or-  
  
-   Remove all references to the object in the **SQL Pane.**  
  
 When you remove a table or table-valued object, the Query and View Designer automatically removes joins that involve that table or table-valued object and removes references to the object's columns in the **SQL Pane** and **Criteria Pane**. However, if the query contains complex expressions involving the object, the object is not automatically removed until all references to it are removed.  
  
## See Also  
 [Add Tables to Queries &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Create Table Aliases &#40;Visual Database Tools&#41;](create-table-aliases-visual-database-tools.md)   
 [Specify Search Criteria &#40;Visual Database Tools&#41;](specify-search-criteria-visual-database-tools.md)   
 [Summarize Query Results &#40;Visual Database Tools&#41;](summarize-query-results-visual-database-tools.md)   
 [Perform Basic Operations with Queries &#40;Visual Database Tools&#41;](perform-basic-operations-with-queries-visual-database-tools.md)  
  
  
