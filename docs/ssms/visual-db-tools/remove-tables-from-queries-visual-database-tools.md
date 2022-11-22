---
description: "Remove Tables from Queries (Visual Database Tools)"
title: Remove Tables from Queries
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "deleting tables"
  - "removing tables"
  - "dropping tables"
  - "queries [SQL Server], tables"
ms.assetid: 8fea0b4f-99b7-4050-8d6f-a97ffb839619
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Remove Tables from Queries (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
You can remove a table - or any table-valued object - from the query.  
  
> [!NOTE]  
> Removing a table or table-valued object does not delete anything from the database; it only removes it from the current query. For details about removing a table from a database, see [How to: Delete Tables from a Database](../../relational-databases/tables/delete-tables-database-engine.md).  
  
### To remove a table or table-structured object  
  
-   In the **Diagram Pane**, select the table, view, user-defined function, synonym, or query, and then press DELETE, or right-click the object and then choose **Remove** in the resulting dialog box. You can select and remove multiple objects at one time.  
  
    -or-  
  
-   Remove all references to the object in the **SQL Pane.**  
  
When you remove a table or table-valued object, the Query and View Designer automatically removes joins that involve that table or table-valued object and removes references to the object's columns in the **SQL Pane** and **Criteria Pane**. However, if the query contains complex expressions involving the object, the object is not automatically removed until all references to it are removed.  
  
## See Also  
[Add Tables to Queries](../../ssms/visual-db-tools/add-tables-to-queries-visual-database-tools.md)  
[Create Table Aliases](../../ssms/visual-db-tools/create-table-aliases-visual-database-tools.md)  
[Specify Search Criteria](../../ssms/visual-db-tools/specify-search-criteria-visual-database-tools.md)  
[Summarize Query Results](../../ssms/visual-db-tools/summarize-query-results-visual-database-tools.md)  
[Perform Basic Operations with Queries](../../ssms/visual-db-tools/perform-basic-operations-with-queries-visual-database-tools.md)  
