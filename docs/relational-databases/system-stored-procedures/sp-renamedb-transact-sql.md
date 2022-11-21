---
description: "sp_renamedb (Transact-SQL)"
title: "sp_renamedb (Transact-SQL)"
ms.custom: ""
ms.date: "04/06/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_renamedb"
  - "sp_renamedb_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_renamedb"
author: markingmyname
ms.author: maghan
---
# sp_renamedb (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the name of a database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use ALTER DATABASE MODIFY NAME instead. For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
sp_renamedb [ @dbname = ] 'old_name' , [ @newname = ] 'new_name'  
```  
  
## Arguments  
`[ @dbname = ] 'old_name'`
 Is the current name of the database. *old_name* is **sysname**, with no default.  
  
`[ @newname = ] 'new_name'`
 Is the new name of the database. *new_name* must follow the rules for identifiers. *new_name* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or a nonzero number (failure)  
 
## Remarks

It's not possible to rename an Azure SQL database configured in an [active geo-replication](/azure/azure-sql/database/active-geo-replication-overview) relationship.

  
## Permissions  
 Requires membership in the **sysadmin** or **dbcreator** fixed server roles.  
  
## Examples  
 The following example creates the `Accounting` database and then changes the name of the database to `Financial`. The `sys.databases` catalog view is then queried to verify the new name of the database.  
  
```sql  
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
  
## Next steps

- [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
- [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
- [sp_changedbowner &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md)   
- [sp_helpdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdb-transact-sql.md)   
- [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
- [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
