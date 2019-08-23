---
title: "MSSQLSERVER_2020 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2020 (Database Engine error)"
ms.assetid: 4a8bf90f-a083-4c53-84f0-d23c711c8081
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2020
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2020|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|The dependencies reported for entity "%.*ls" do not include references to columns. This is either because the entity references an object that does not exist or because of an error in one or more statements in the entity.  Before rerunning the query, ensure that there are no errors in the entity and that all objects referenced by the entity exist.|  
  
## Explanation  
 The **sys.dm_sql_referenced_entities** system function will report any column-level dependency for schema-bound references. For example, the function will report all column-level dependencies for an indexed view because an indexed view requires schema binding. However, when the referenced entity is not schema-bound, column dependencies are reported only when all statements in which the columns are referenced can be bound. Statements can be successfully bound only if all objects exist at the time the statements are parsed. If any statement defined in the entity fails to bind, column dependencies will not be reported and the **referenced_minor_id** column will return 0. When column dependencies cannot be resolved, error 2020 is raised. This error does not prevent the query from returning object-level dependencies.  
  
## User Action  
 Correct any errors identified in the message before error 2020. For example, in the following code example the view `Production.ApprovedDocuments` is defined on the columns `Title`, `ChangeNumber`, and `Status` in the `Production.Document` table. The **sys.dm_sql_referenced_entities** system function is queried for the objects and columns on which the `ApprovedDocuments` view depends. Because the view is not created using the WITH SCHEMA_BINDING clause, the columns referenced in the view can be modified in the referenced table. The example alters the column `ChangeNumber` in the `Production.Document` table by renaming it to `TrackingNumber`. The catalog view is queried again for the `ApprovedDocuments` view; however it cannot bind to all the columns defined in the view. Errors 207 and 2020 are returned identifying the problem. To resolve the problem, the view must be altered to reflect the new name of the column.  
  
 `USE AdventureWorks2012;`  
  
 `GO`  
  
 `CREATE VIEW Production.ApprovedDocuments`  
  
 `AS`  
  
 `SELECT Title, ChangeNumber, Status`  
  
 `FROM Production.Document`  
  
 `WHERE Status = 2;`  
  
 `GO`  
  
 `SELECT referenced_schema_name AS schema_name`  
  
 `,referenced_entity_name AS table_name`  
  
 `,referenced_minor_name AS referenced_column`  
  
 `FROM sys.dm_sql_referenced_entities ('Production.ApprovedDocuments', 'OBJECT');`  
  
 `GO`  
  
 `EXEC sp_rename 'Production.Document.ChangeNumber', 'TrackingNumber', 'COLUMN';`  
  
 `GO`  
  
 `SELECT referenced_schema_name AS schema_name`  
  
 `,referenced_entity_name AS table_name`  
  
 `,referenced_minor_name AS referenced_column`  
  
 `FROM sys.dm_sql_referenced_entities ('Production.ApprovedDocuments', 'OBJECT');`  
  
 `GO`  
  
 The query returns the following error messages.  
  
 `Msg 207, Level 16, State 1, Procedure ApprovedDocuments, Line 3`  
  
 `Invalid column name 'ChangeNumber'.`  
  
 `Msg 2020, Level 16, State 1, Line 1`  
  
 `The dependencies reported for entity`  
  
 `"Production.ApprovedDocuments" do not include references to`  
  
 `columns. This is either because the entity references an`  
  
 `object that does not exist or because of an error in one or`  
  
 `more statements in the entity. Before rerunning the query,`  
  
 `ensure that there are no errors in the entity and that all`  
  
 `objects referenced by the entity exist.`  
  
 The following example corrects the column name in the view.  
  
 `USE AdventureWorks2012;`  
  
 `GO`  
  
 `ALTER VIEW Production.ApprovedDocuments`  
  
 `AS`  
  
 `SELECT Title,TrackingNumber, Status`  
  
 `FROM Production.Document`  
  
 `WHERE Status = 2;`  
  
 `GO`  
  
## See Also  
 [sys.dm_sql_referenced_entities &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-sql-referenced-entities-transact-sql)  
  
  
