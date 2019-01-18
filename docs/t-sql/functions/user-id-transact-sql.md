---
title: "USER_ID (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "USER_ID"
  - "USER_ID_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "USER_ID function"
  - "identification numbers [SQL Server]"
  - "IDs [SQL Server], databases"
  - "users [SQL Server], database ID numbers"
  - "database IDs [SQL Server]"
  - "identification numbers [SQL Server], databases"
ms.assetid: 67fd29bc-eda9-4d4d-b148-5d3659181a43
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# USER_ID (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the identification number for a database user.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [DATABASE_PRINCIPAL_ID](../../t-sql/functions/database-principal-id-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
USER_ID ( [ 'user' ] )  
```  
  
## Arguments  
 *user*  
 Is the username to be used. *user* is **nchar**. If a **char** value is specified, it is implicitly converted to **nchar**. The parentheses are required.  
  
## Return Types  
 **int**  
  
## Remarks  
 When *user* is omitted, the current user is assumed. If the parameter contains the word NULL will return NULL.When USER_ID is called after EXECUTE AS, USER_ID will return the ID of the impersonated context.  
  
 When a Windows principal that is not mapped to a specific database user accesses a database by way of membership in a group, USER_ID returns 0 (the ID of public). If such a principal creates an object without specifying a schema, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will create an implicit user and schema mapped to the Windows principal. The user created in such cases cannot be used to connect to the database. Calls to USER_ID by a Windows principal mapped to an implicit user will return the ID of the implicit user.  
  
 USER_ID can be used in a select list, in a WHERE clause, and anywhere an expression is allowed. For more information, see [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md).  
  
## Examples  
 The following example returns the identification number for the `AdventureWorks2012` user `Harold`.  
  
```  
USE AdventureWorks2012;  
SELECT USER_ID('Harold');  
GO  
```  
  
## See Also  
 [USER_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/user-name-transact-sql.md)   
 [sys.database_principals &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)   
 [DATABASE_PRINCIPAL_ID &#40;Transact-SQL&#41;](../../t-sql/functions/database-principal-id-transact-sql.md)   
 [Security Functions &#40;Transact-SQL&#41;](../../t-sql/functions/security-functions-transact-sql.md)  
  
  
