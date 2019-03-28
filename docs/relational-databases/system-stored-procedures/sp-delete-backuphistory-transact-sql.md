---
title: "sp_delete_backuphistory (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_delete_backuphistory"
  - "sp_delete_backuphistory_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_delete_backuphistory"
ms.assetid: bdb56834-616e-47e4-b942-e895d2325e97
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_delete_backuphistory (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Reduces the size of the backup and restore history tables by deleting the entries for backup sets older than the specified date. Additional rows are added to the backup and restore history tables after each backup or restore operation is performed; therefore, we recommend that you periodically execute **sp_delete_backuphistory**.  
  
> [!NOTE]  
>  The backup and restore history tables reside in the **msdb** database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_delete_backuphistory [ @oldest_date = ] 'oldest_date'   
```  
  
## Arguments  
`[ @oldest_date = ] 'oldest\_date'`
 Is the oldest date retained in the backup and restore history tables. *oldest_date* is **datetime**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_delete_backuphistory** must be run from the **msdb** database and affects the following tables:  
  
-   [backupfile](../../relational-databases/system-tables/backupfile-transact-sql.md)  
  
-   [backupfilegroup](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)  
  
-   [backupmediafamily](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)  
  
-   [backupmediaset](../../relational-databases/system-tables/backupmediaset-transact-sql.md)  
  
-   [backupset](../../relational-databases/system-tables/backupset-transact-sql.md)  
  
-   [restorefile](../../relational-databases/system-tables/restorefile-transact-sql.md)  
  
-   [restorefilegroup](../../relational-databases/system-tables/restorefilegroup-transact-sql.md)  
  
-   [restorehistory](../../relational-databases/system-tables/restorehistory-transact-sql.md)  
  
 The physical backup files are preserved, even if all the history is deleted.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role, but permissions can be granted to other users.  
  
## Examples  
 The following example deletes all entries that are older than January 14, 2010, 12:00 A.M. in the backup and restore history tables.  
  
```  
USE msdb;  
GO  
EXEC sp_delete_backuphistory @oldest_date = '01/14/2010';  
```  
  
## See Also  
 [sp_delete_database_backuphistory &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-delete-database-backuphistory-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
  
