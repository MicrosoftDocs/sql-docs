---
title: "sp_delete_backup (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/03/2015"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 808e50ae-ff6e-4520-9ce2-530591d3d59b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# sp_delete_backup (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Deletes all snapshots and the backup file that comprise a snapshot backup set from the specified database. This system stored procedure is the only recommended method for managing snapshot backup sets. For more information, see [File-Snapshot Backups for Database Files in Azure](../../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_delete_backup   
    [ @backup_url = ] backup_metadata_file_url  
    ,[ [ @db_name = ] database_name | NULL ]  
```  
  
## Arguments  
 *[ @backup_url = ] backup_meta_file_url*  
 The URL of the backup to be deleted, which deletes all snapshots comprising the specified backup set including the backup file itself.  
  
 *[ @db_name = ] database_name*  
 The name of the database containing the snapshot to be deleted. When a database name is provided, the system verifies that the backup URL provided is a backup URL for the specified database and uses [sp_delete_backup_file_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md) to delete each snapshot. If no database name is provided, this database check is not performed.  
  
## Permissions  
 Requires ALTER ANY DATABASE permission or ALTER permission on the specified database.  
  
## See Also  
 [sys.fn_db_backup_file_snapshots &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md)   
 [sp_delete_backup_file_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md)  
  
  
