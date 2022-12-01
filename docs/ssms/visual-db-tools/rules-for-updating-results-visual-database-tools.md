---
description: "Rules for Updating Results (Visual Database Tools)"
title: Rules for Updating Results
ms.custom: seo-lt-2019
ms.date: 01/19/2017
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "View Designer, Results pane"
  - "updating query results"
  - "Query Designer [SQL Server], Results pane"
  - "Results pane"
ms.assetid: de131ef0-ccbd-446f-9400-b93c7b8fa537
author: markingmyname
ms.author: maghan
ms.reviewer: 

---
# Rules for Updating Results (Visual Database Tools)
[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]
In many cases, you can update the result set displayed in the [Results Pane](../../ssms/visual-db-tools/results-pane-visual-database-tools.md). However, in some cases you cannot.  
  
In general, in order to update results, the [Query and View Designer](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) must have sufficient information to uniquely identify the row in the table. An example is if the query includes a primary key in the output list. In addition, you must have sufficient permission to update the database.  
  
If your query is based on a view, you might be able to update it. The same guidelines apply, except that they apply to the underlying tables in the view, not just to the view itself.  
  
> [!NOTE]  
> The Query and View Designer cannot determine in advance whether you can update a result set based on a view. Therefore, it displays all views, even though you might not be able to update them.  
  
The following table summarizes specific instances in which you might and might not be able to update query results in the Results pane. In many cases, the database you are using dictates whether you can update query results.  
  
|Query|Can results be updated?|  
|---------|---------------------------|  
|Query based on one table with primary key in the output list|Yes (except as listed below).|  
|Query based on a table with no unique index and without a primary key|Depends on query and database. Some databases allow updates if sufficient information is available to uniquely identify records.|  
|Query based on multiple tables which are not joined|No.|  
|Query based on data marked as read-only in the database|No.|  
|Query based on a view that involves one table with no constraints|Yes (except as listed below).|  
|Query based on tables joined with a one-to-one relationship|Yes (except as listed below).|  
|Query based on tables joined with a one-to-many relationship|Usually.|  
|Query based on three or more tables in which there is a many-to-many relationship|No.|  
|Query based on a table for which update permission is not granted|Can delete but not update.|  
|Query based on a table for which delete permission is not granted|Can update but not delete.|  
|Aggregate query|No.|  
|Query based on a subquery that contains totals or aggregate functions|No.|  
|Query that includes the DISTINCT keyword to exclude duplicate rows|No.|  
|Query whose FROM clause includes a user-defined function that returns a table and the user-defined function contains multiple select statements|No.|  
|Query whose FROM clause includes an inline user-defined function|Yes.|  
  
In addition, you might not be able to update specific columns in the query results. The following list summarizes specific types of columns that you cannot update in the Results pane.  
  
-   Columns based on expressions  
  
-   Columns based on scalar user-defined functions  
  
-   Rows or columns deleted by another user  
  
-   Rows or columns locked by another user (locked rows can usually be updated as soon as they are unlocked)  
  
-   Timestamp or BLOB columns  
  
## See Also  
[Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/design-queries-and-views-how-to-topics-visual-database-tools.md)  
  
