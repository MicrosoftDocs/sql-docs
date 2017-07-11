---
title: "DROP PROCEDURE (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/11/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DROP PROCEDURE"
  - "DROP_PROCEDURE_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "removing stored procedures"
  - "dropping procedure groups"
  - "deleting stored procedures"
  - "deleting procedure groups"
  - "DROP PROCEDURE statement"
  - "dropping stored procedures"
  - "stored procedures [SQL Server], removing"
  - "removing procedure groups"
ms.assetid: 1c2d7235-7b9b-4336-8f17-429e7d82c2c3
caps.latest.revision: 51
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DROP PROCEDURE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all_md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Removes one or more stored procedures or procedure groups from the current database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```tsql  
-- Syntax for SQL Server and Azure SQL Database  
  
DROP { PROC | PROCEDURE } [ IF EXISTS ] { [ schema_name. ] procedure } [ ,...n ]  
```  
  
```tsql  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
DROP { PROC | PROCEDURE } { [ schema_name. ] procedure_name }  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the procedure only if it already exists.  
  
 *schema_name*  
 The name of the schema to which the procedure belongs. A server name or database name cannot be specified.  
  
 *procedure*  
 The name of the stored procedure or stored procedure group to be removed. Individual procedures within a numbered procedure group cannot be dropped; the whole procedure group is dropped.  
  
## Best Practices  
 Before removing any stored procedure, check for dependent objects and modify these objects accordingly. Dropping a stored procedure can cause dependent objects and scripts to fail when these objects are not updated. For more information, see [View the Dependencies of a Stored Procedure](../../relational-databases/stored-procedures/view-the-dependencies-of-a-stored-procedure.md)  
  
## Metadata  
 To display a list of existing procedures, query the **sys.objects** catalog view. To display the procedure definition, query the **sys.sql_modules** catalog view.  
  
## Security  
  
### Permissions  
 Requires **CONTROL** permission on the procedure, or **ALTER** permission on the schema to which the procedure belongs, or membership in the **db_ddladmin** fixed server role.  
  
## Examples  
 The following example removes the `dbo.uspMyProc` stored procedure in the current database.  
  
```  
DROP PROCEDURE dbo.uspMyProc;  
GO  
```  
  
 The following example removes several stored procedures in the current database.  
  
```  
DROP PROCEDURE dbo.uspGetSalesbyMonth, dbo.uspUpdateSalesQuotes, dbo.uspGetSalesByYear;  
```  
  
 The following example removes the `dbo.uspMyProc` stored procedure if it exists but does not cause an error if the procedure does not exist. This syntax is new in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
  
```  
DROP PROCEDURE IF EXISTS dbo.uspMyProc;  
GO  
```  
  
  
## See Also  
 [ALTER PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-procedure-transact-sql.md)   
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md)   
 [Delete a Stored Procedure](../../relational-databases/stored-procedures/delete-a-stored-procedure.md)  
  
  


