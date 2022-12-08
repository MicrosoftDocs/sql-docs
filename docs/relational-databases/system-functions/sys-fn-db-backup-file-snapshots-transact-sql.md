---
description: "sys.fn_db_backup_file_snapshots (Transact-SQL)"
title: "sys.fn_db_backup_file_snapshots (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/03/2015"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 45010ff2-219f-4086-9ea4-016a6c17cddd
author: rwestMSFT
ms.author: randolphwest
---
# sys.fn_db_backup_file_snapshots (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Returns Azure snapshots associated with the database files. If the specified database is not found or if the database files are not stored in the Microsoft Azure Blob Storage, no rows are returned. Use this system function in conjunction with the **sys.sp_delete_backup_file_snapshot** system stored procedure to identify and delete orphaned backup snapshots. For more information, see [File-Snapshot Backups for Database Files in Azure](../../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sys.fn_db_backup_file_snapshots   
   [ ( database_name ) ]  
```  
  
## Arguments  
 *Database_name*  
 The name of the database being queried. If NULL, this function is executed in the current database scope.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|file_id|**int**|The File ID for the database. Is not nullable.|  
|snapshot_time|**nvarchar(260)**|The timestamp of the snapshot as it is returned by the REST API. Returns NULL if no snapshot exists.|  
|snapshot_url|**nvarchar(360)**|The full URL to the file snapshot. Returns NULL if no snapshot exist.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the database.  
  
## See Also  
 [sp_delete_backup_file_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md)   
 [sp_delete_backup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup.md)  
  
  
