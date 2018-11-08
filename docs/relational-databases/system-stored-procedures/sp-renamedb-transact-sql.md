---
title: "sp_renamedb (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_renamedb"
  - "sp_renamedb_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_renamedb"
ms.assetid: 7dd9d4ff-20e1-4857-9a8e-a5bff767cf76
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_renamedb (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes the name of a database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use ALTER DATABASE MODIFY NAME instead. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_renamedb [ @dbname = ] 'old_name' , [ @newname = ] 'new_name'  
```  
  
## Arguments  
 [ **@dbname=**] **'***old_name***'**  
 Is the current name of the database. *old_name* is **sysname**, with no default.  
  
 [ **@newname=**] **'***new_name***'**  
 Is the new name of the database. *new_name* must follow the rules for identifiers. *new_name* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or a nonzero number (failure)  
  
## Permissions  
 Requires membership in the **sysadmin** or **dbcreator** fixed server roles.  
  
## Examples  
 The following example creates the `Accounting` database and then changes the name of the database to `Financial`. The `sys.databases` catalog view is then queried to verify the new name of the database.  
  
```  
USE master;  
GO  
CREATE DATABASE Accounting;  
GO  
EXEC sp_renamedb N'Accounting', N'Financial';  
GO  
SELECT name, database_id, modified_date  
FROM sys.databases  
WHERE name = N'Financial';  
GO  
```  
  
## See Also  
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [sp_changedbowner &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md)   
 [sp_helpdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdb-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
