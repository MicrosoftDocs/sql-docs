---
title: "Update Table Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.updatetable"
  - "vdtsql.chm:69643"
ms.assetid: 174c7275-5b15-42a9-b172-5ff30de575a1
author: stevestein
ms.author: sstein
manager: craigg
---
# Update Table Dialog Box (Visual Database Tools)
  This dialog box allows you to specify the table to be updated.  
  
 This dialog box appears if more than one table is displayed in the Diagram pane when you change a query's type to be an Update query.  
  
 Select the table to update, and then choose **OK**.\  
  
> [!NOTE]  
>  If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](/sql/t-sql/statements/alter-table-transact-sql) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## See Also  
 [Create Update Queries &#40;Visual Database Tools&#41;](visual-database-tools.md)  
  
  
