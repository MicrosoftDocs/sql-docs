---
title: "Add Table Dialog Box (Database Designer) (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:65555"
  - "vdt.dlgbox.schema.addtable"
ms.assetid: 3c0b1b30-795c-4240-91d6-890b8348014a
author: stevestein
ms.author: sstein
manager: craigg
---
# Add Table Dialog Box (Database Designer) (Visual Database Tools)
  Lets you add tables in Database Designer.  
  
> [!NOTE]  
>  If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](/sql/t-sql/statements/alter-table-transact-sql) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## UIElement List  
 **Refresh**  
 Refreshed the list of tables to match the current state of the database.  
  
 **Add**  
 Adds the selected table or tables.  
  
> [!NOTE]  
>  If you want to add several tables to the diagram, you can select them all before clicking **Add**. Alternatively, you can double-click each table you want to add, then click **Close** when you are finished.  
  
 **Close**  
 Closes the dialog box without adding further tables.  
  
## See Also  
 [Add Tables to Diagrams &#40;Visual Database Tools&#41;](visual-database-tools.md)  
  
  
