---
title: "Add Table Dialog Box (Query and View Designers) (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.query.addtable"
  - "vdtsql.chm:65565"
ms.assetid: fce7adcc-4cf5-4a52-9203-11c13d1ecf08
author: stevestein
ms.author: sstein
manager: craigg
---
# Add Table Dialog Box (Query and View Designers) (Visual Database Tools)
  This dialog box lets you add tables, views, user-defined functions, or synonyms to a query or view.  
  
> [!NOTE]  
>  If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](/sql/t-sql/statements/alter-table-transact-sql) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## Options  
 **Tables**  
 Lists the tables you can add to the **Diagram** pane. To add a table, select it and click **Add**. To add several tables at once, select them and click **Add**.  
  
 **Views**  
 Lists the views you can add to the **Diagram** pane. To add a view, select it and click **Add**. To add several views at once, select them and click **Add**.  
  
 **Functions**  
 Lists the user-defined functions you can add to the **Diagram** pane. To add a function, select it and click **Add**. To add several functions at once, select them and click **Add**.  
  
 **Synonyms**  
 Lists the synonyms you can add to the **Diagram** pane. To add a synonym, select it and click **Add**. To add several synonyms at once, select them and click **Add**.  
  
 **Refresh**  
 Update the list to include any changes made to the database since the list was last retrieved.  
  
 **Add**  
 Add the selected item or items.  
  
## See Also  
 [Design Queries and Views How-to Topics &#40;Visual Database Tools&#41;](visual-database-tools.md)  
  
  
