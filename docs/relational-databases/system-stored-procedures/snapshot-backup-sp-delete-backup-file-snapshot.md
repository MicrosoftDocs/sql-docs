---
title: "sp_delete_backup_file_snapshot (Transact-SQL)"
description: "sp_delete_backup_file_snapshot (Transact-SQL)"
author: MashaMSFT
ms.author: mathoma
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
---
# sp_delete_backup_file_snapshot (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Deletes a specified backup snapshot from the specified database. Use this system stored procedure in conjunction with the **sys.fn_db_backup_file_snapshots** system function to identify and delete orphaned backup snapshots. For more information, see [File-Snapshot Backups for Database Files in Azure](../../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  

  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.sp_delete_backup_file_snapshot  
    [ @db_name = ] N'<database_name>  
    , [ @snapshot_url = ] N'<snapshot_url>  
```  
  
## Arguments  
 *[ @db_name = ] database_name*  
 The name of the database containing the snapshot to be deleted, provided as a Unicode string.  
  
 *[ @snapshot_url = ] snapshot_url*  
 The URL of the snapshot to be deleted, provided as a Unicode string.  
  
## Permissions  
 Requires ALTER ANY DATABASE permission.  
  
## See Also  
 [sys.fn_db_backup_file_snapshots &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md)   
 [sp_delete_backup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup.md)  
  
  
