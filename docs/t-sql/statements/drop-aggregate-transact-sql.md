---
title: "DROP AGGREGATE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/10/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DROP_AGGREGATE_TSQL"
  - "DROP AGGREGATE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "aggregate functions [SQL Server], removing"
  - "removing user-defined functions"
  - "dropping user-defined functions"
  - "user-defined functions [CLR integration]"
  - "deleting user-defined functions"
  - "DROP AGGREGATE statement"
ms.assetid: 84ffc4e7-c451-4f1f-9a67-7fc3a120e53f
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DROP AGGREGATE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Removes a user-defined aggregate function from the current database. User-defined aggregate functions are created by using [CREATE AGGREGATE](../../t-sql/statements/create-aggregate-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
DROP AGGREGATE [ IF EXISTS ] [ schema_name . ] aggregate_name  
```  
  
## Arguments  
 *IF EXISTS*  
 **Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [current version](https://go.microsoft.com/fwlink/p/?LinkId=299658)).  
  
 Conditionally drops the aggregate only if it already exists.  
  
 *schema_name*  
 Is the name of the schema to which the user-defined aggregate function belongs.  
  
 *aggregate_name*  
 Is the name of the user-defined aggregate function you want to drop.  
  
## Remarks  
 DROP AGGREGATE does not execute if there are any views, functions, or stored procedures created with schema binding that reference the user-defined aggregate function you want to drop.  
  
## Permissions  
 To execute DROP AGGREGATE, at a minimum, a user must have ALTER permission on the schema to which the user-defined aggregate belongs, or CONTROL permission on the aggregate.  
  
## Examples  
 The following example drops the aggregate `Concatenate`.  
  
```  
DROP AGGREGATE dbo.Concatenate;  
```  
  
## See Also  
 [CREATE AGGREGATE &#40;Transact-SQL&#41;](../../t-sql/statements/create-aggregate-transact-sql.md)   
 [Create User-defined Aggregates](../../relational-databases/user-defined-functions/create-user-defined-aggregates.md)  
  
  
