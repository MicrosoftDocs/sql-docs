---
title: "DROP VIEW (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/12/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_VIEW_TSQL"
  - "DROP VIEW"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dropping views"
  - "DROP VIEW statement"
  - "deleting views"
  - "indexed views [SQL Server], removing"
  - "views [SQL Server], removing"
  - "removing views"
ms.assetid: 03cea355-e39c-46e1-b7db-8832038669dd
caps.latest.revision: 42
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DROP VIEW (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes one or more views from the current database. DROP VIEW can be executed against indexed views.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database  
  
DROP VIEW [ IF EXISTS ] [ schema_name . ] view_name [ ...,n ] [ ; ]  
```  
  
```  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
DROP VIEW [ schema_name . ] view_name   
[;]  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658), [!INCLUDE[sssds](../../includes/sssds-md.md)]).|  
  
 Conditionally drops the view only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the view belongs.  
  
 *view_name*  
 Is the name of the view to remove.  
  
## Remarks  
 When you drop a view, the definition of the view and other information about the view is deleted from the system catalog. All permissions for the view are also deleted.  
  
 Any view on a table that is dropped by using DROP TABLE must be dropped explicitly by using DROP VIEW.  
  
 When executed against an indexed view, DROP VIEW automatically drops all indexes on a view. To display all indexes on a view, use [sp_helpindex](../../relational-databases/system-stored-procedures/sp-helpindex-transact-sql.md).  
  
 When querying through a view, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] checks to make sure that all the database objects referenced in the statement exist and that they are valid in the context of the statement, and that data modification statements do not violate any data integrity rules. A check that fails returns an error message. A successful check translates the action into an action against the underlying table or tables. If the underlying tables or views have changed since the view was originally created, it may be useful to drop and re-create the view.  
  
 For more information about determining dependencies for a specific view, see [sys.sql_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-dependencies-transact-sql.md).  
  
 For more information about viewing the text of the view, see [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md).  
  
## Permissions  
 Requires **CONTROL** permission on the view, **ALTER** permission on the schema containing the view, or membership in the **db_ddladmin** fixed server role.  
  
## Examples  
  
### A. Drop a view  
 The following example removes the view `Reorder`.  
  
```  
DROP VIEW dbo.Reorder ;  
GO  
```  
  
## See Also  
 [ALTER VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/alter-view-transact-sql.md)   
 [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [USE &#40;Transact-SQL&#41;](../../t-sql/language-elements/use-transact-sql.md)   
 [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md)  
 
