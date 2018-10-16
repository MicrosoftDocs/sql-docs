---
title: "Draw Reflexive Relationships (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "drawing reflexive relationships"
  - "reflexive relationships"
  - "database diagrams [SQL Server], relationships"
ms.assetid: e218363f-faec-46d5-9904-a537fbcc994d
author: "stevestein"
ms.author: "sstein"
manager: craigg

---
# Draw Reflexive Relationships (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
You create a reflexive relationship to link a column or columns in a table with another column or columns in the same table. For example, suppose the `employee` table has an `emp_id` column and a `mgr_id` column. Because each manager is also an employee, you relate these two columns by drawing a relationship line from the table to itself. This relationship ensures each manager ID that is added to the table matches an existing employee ID.  
  
Before you create a relationship, you must first define a primary key or unique constraint for your table. You then relate the primary key column to a matching column. Once you create the relationship, the matching column becomes a foreign key of the table.  
  
### To draw a reflexive relationship  
  
1.  In your database diagram, click the row selector for the database column that you want to relate to another column and drag the pointer outside the table until a line appears.  
  
2.  Drag the line back to the selected table.  
  
3.  Release the mouse button. The **Tables and Columns** dialog box appears.  
  
4.  Select the foreign key column and the primary key table and column with which you want form a relationship.  
  
5.  Choose **OK** twice to create the relationship.  
  
When you run queries against a table, you can use a reflexive relationship to create a self-join. For information about querying tables with joins, see [Query with Joins &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-joins-visual-database-tools.md).  
  
## See Also  
[Query with Joins &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-with-joins-visual-database-tools.md)  
  
