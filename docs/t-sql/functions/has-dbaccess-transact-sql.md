---
title: "HAS_DBACCESS (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/23/2017"
ms.prod: sql
ms.prod_service: "sql-data-warehouse, pdw, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "HAS_DBACCESS_TSQL"
  - "HAS_DBACCESS"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "permissions [SQL Server], verifying"
  - "permissions [SQL Server], user access status"
  - "HAS_DBACCESS"
  - "checking permission status"
  - "verifying permission status"
  - "users [SQL Server], access rights status"
  - "testing permissions"
  - "status information [SQL Server], user access"
ms.assetid: 99b43a72-0722-4a7b-a493-bdee1c74c7b9
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# HAS_DBACCESS (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Returns information about whether the user has access to the specified database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
HAS_DBACCESS ( 'database_name' )  
```  
  
## Arguments  
 '*database_name*'  
 The name of the database for which the user wants access information. *database_name* is **sysname**.  
  
## Return Types  
 **int**  
  
## Remarks  
 HAS_DBACCESS returns 1 if the user has access to the database, 0 if the user has no access to the database, and NULL if the database name is not valid.  
  
 HAS_DBACCESS returns 0 if the database is offline or suspect.  
  
 HAS_DBACCESS returns 0 if the database is in single-user mode and the database is in use by another user.  
  
## Permissions  
 Requires membership in the public role.  
  
## Examples  
 The following example tests whether current user has access to the `AdventureWorks2012` database.  
  
```  
SELECT HAS_DBACCESS('AdventureWorks2012');  
GO  
```  
  
## Examples: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]  
 The following example tests whether current user has access to the `AdventureWorksPDW2012` database.  
  
```  
SELECT HAS_DBACCESS('AdventureWorksPDW2012');  
GO  
```  
  
## See Also  
 [IS_MEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-member-transact-sql.md)   
 [IS_SRVROLEMEMBER &#40;Transact-SQL&#41;](../../t-sql/functions/is-srvrolemember-transact-sql.md)  
  
  

