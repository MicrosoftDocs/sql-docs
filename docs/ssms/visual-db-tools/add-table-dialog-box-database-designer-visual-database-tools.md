---
description: "Add Table Dialog Box (Database Designer) (Visual Database Tools)"
title: Add Table Dialog Box (Database Designer)
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdtsql.chm:65555"
  - "vdt.dlgbox.schema.addtable"
ms.assetid: 3c0b1b30-795c-4240-91d6-890b8348014a
author: markingmyname
ms.author: maghan
ms.reviewer: 
ms.custom: seo-lt-2019
ms.date: 01/19/2017
---

# Add Table Dialog Box (Database Designer) (Visual Database Tools)

[!INCLUDE[SQL Server](../../includes/applies-to-version/sqlserver.md)]

Lets you add tables in Database Designer.  
  
> [!NOTE]  
> If the table is published for replication, you must make schema changes using the Transact-SQL statement [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) or SQL Server Management Objects (SMO). When schema changes are made using the Table Designer or the Database Diagram Designer, it attempts to drop and recreate the table. You cannot drop published objects, therefore the schema change will fail.  
  
## UI element list  
**Refresh**  
Refreshed the list of tables to match the current state of the database.  
  
**Add**  
Adds the selected table or tables.  
  
> [!NOTE]  
> If you want to add several tables to the diagram, you can select them all before clicking **Add**. Alternatively, you can double-click each table you want to add, then click **Close** when you are finished.  
  
**Close**  
Closes the dialog box without adding further tables.  
  
## See Also  
[Add Tables to Diagrams &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/add-tables-to-diagrams-visual-database-tools.md)  
  
