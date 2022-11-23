---
description: "sp_dbremove (Transact-SQL)"
title: "sp_dbremove (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_dbremove"
  - "sp_dbremove_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dbremove"
ms.assetid: a8513f4a-c025-49c8-99c3-4c83cb7f51ed
author: markingmyname
ms.author: maghan
---
# sp_dbremove (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Removes a database and all files associated with that database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbremove [ @dbname = ] 'database' [ , [ @dropdev = ] 'dropdev' ]   
```  
  
## Arguments  
`[ @dbname = ] 'database'`
 Is the name of the database to be removed. *database* is **sysname**, with a default value of NULL.  
  
`[ @dropdev = ] 'dropdev'`
 Is a flag provided for backward compatibility only and is currently ignored. *dropdev* has the value **dropdev**.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example removes a database named `sales` and all files associated with it.  
  
```  
EXEC sp_dbremove sales;  
```  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md)   
 [DBCC &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-transact-sql.md)   
 [sp_detach_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-detach-db-transact-sql.md)  
  
