---
title: "DATABASE_PRINCIPAL_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
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
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# DATABASE_PRINCIPAL_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

This function returns the ID number of a principal in the current database. See [Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md) for more information about principals.
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)
  
## Syntax  
  
```sql
DATABASE_PRINCIPAL_ID ( 'principal_name' )  
```  
  
## Arguments  
*principal_name*  
An expression of type **sysname**, that represents the principal. When *principal_name* is omitted, `DATABASE_PRINCIPAL_ID` returns the ID of the current user. `DATABASE_PRINCIPAL_ID` requires the parentheses.
  
## Return types
**int**  
NULL if the database principal does not exist.
  
## Remarks  
Use `DATABASE_PRINCIPAL_ID` in a select list, a WHERE clause, or any place that allows an expression. See [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md) for more information.
  
## Examples  
  
### A. Retrieving the ID of the current user  
This example returns the database principal ID of the current user.
  
```sql
SELECT DATABASE_PRINCIPAL_ID();  
GO  
```  
  
### B. Retrieving the ID of a specified database principal  
This example returns the database principal ID for the database role `db_owner`.
  
```sql
SELECT DATABASE_PRINCIPAL_ID('db_owner');  
GO  
```  
  
## See also
[Principals &#40;Database Engine&#41;](../../relational-databases/security/authentication-access/principals-database-engine.md)  
[Permissions Hierarchy &#40;Database Engine&#41;](../../relational-databases/security/permissions-hierarchy-database-engine.md)  
[sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)
  
  
