---
title: "DATABASE_PRINCIPAL_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DATABASE_PRINCIPAL_ID_TSQL"
  - "DATABASE_PRINCIPAL_ID"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "identification numbers [SQL Server], principals"
  - "principal ID numbers [SQL Server]"
  - "DATABASE_PRINCIPAL_ID function"
  - "IDs [SQL Server], principals"
ms.assetid: 908c7dd8-c10b-4658-92f6-0224f9835dd9
caps.latest.revision: 28
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# DATABASE_PRINCIPAL_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the ID number of a principal in the current database. For more information about principals, see [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
DATABASE_PRINCIPAL_ID ( 'principal_name' )  
```  
  
## Arguments  
 *principal_name*  
 Is an expression of type **sysname** that represents the principal.  
  
 When *principal_name* is omitted, the ID of the current user is returned. The parentheses are required.  
  
## Return Types  
 **int**  
  
 NULL when the database principal does not exist  
  
## Remarks  
 DATABASE_PRINCIPAL_ID can be used in a select list, a WHERE clause, or anywhere an expression is allowed. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
## Examples  
  
### A. Retrieving the ID of the current user  
 The following example returns the database principal ID of the current user.  
  
```  
SELECT DATABASE_PRINCIPAL_ID();  
GO  
```  
  
### B. Retrieving the ID of a specified database principal  
 The following example returns the database principal ID for the database role `db_owner`.  
  
```  
SELECT DATABASE_PRINCIPAL_ID('db_owner');  
GO  
```  
  
## See Also  
 [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)   
 [Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)  
  
  