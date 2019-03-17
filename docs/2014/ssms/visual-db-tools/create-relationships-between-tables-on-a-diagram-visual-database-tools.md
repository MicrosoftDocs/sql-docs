---
title: "Create Relationships Between Tables on a Diagram (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "diagrams [SQL Server], designing"
ms.assetid: 28e9630c-dff4-46cc-bb0e-fe77998b6ac2
author: stevestein
ms.author: sstein
manager: craigg
---
# Create Relationships Between Tables on a Diagram (Visual Database Tools)
  You can create relationships between columns in different tables in the Diagram Designer by dragging columns between tables.  
  
### To create a relationship graphically  
  
1.  In Database Designer, click the row selector for one or more database columns that you want to relate to a column in another table.  
  
2.  Drag the selected column(s) to the related table.  
  
3.  Two dialog boxes appear: **Foreign Key Relationship** and **Tables and Columns**, with the latter appearing in the foreground.  
  
4.  **Relationship name** has a system-provided name in the format FK_*localtable*_*foreigntable*. You may change this value.  
  
5.  Verify that **Primary key table** specifies the correct table.  
  
6.  The grid lists the local columns and their matching foreign columns. You can add or remove table columns or change mappings.  
  
7.  Choose **OK**.  
  
     The **Foreign Key Relationship** dialog box appears. **Selected Relationship** shows the relationship you have created.  
  
8.  Change properties for the relationship in the grid.  
  
9. Choose **OK** to create the relationship.  
  
     Database Designer shows a relationship between the columns you chose.  
  
## See Also  
 [Tables and Columns Dialog Box &#40;Visual Database Tools&#41;](visual-database-tools.md)   
 [Unique Constraints and Check Constraints](../../relational-databases/tables/unique-constraints-and-check-constraints.md)   
 [Work with Tables in Database Diagram &#40;Visual Database Tools&#41;](work-with-tables-in-database-diagram-visual-database-tools.md)  
  
  
