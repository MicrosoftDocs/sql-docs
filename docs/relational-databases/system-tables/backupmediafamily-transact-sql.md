---
title: "backupmediafamily (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "backupmediafamily"
  - "backupmediafamily_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backupmediafamily system table"
  - "backup media [SQL Server], backupmediafamily system table"
ms.assetid: ee16de24-3d95-4b2e-a094-78df2514d18a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# backupmediafamily (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each media family. If a media family resides in a mirrored media set, the family has a separate row for each mirror in the media set. This table is stored in the **msdb** database.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**media_set_id**|**int**|Unique identification number that identifies the media set of which this family is a member. References **backupmediaset(media_set_id)**|  
|**family_sequence_number**|**tinyint**|Position of this media family in the media set.|  
|**media_family_id**|**uniqueidentifier**|Unique identification number that identifies the media family. Can be NULL.|  
|**media_count**|**int**|Number of media in the media family. Can be NULL.|  
|**logical_device_name**|**nvarchar(128)**|Name of this backup device in **sys.backup_devices.name**. If this is a temporary backup device (as opposed to a permanent backup device that exists in **sys.backup_devices**), the value of **logical_device_name** is NULL.|  
|**physical_device_name**|**nvarchar(260)**|Physical name of the backup device. Can be NULL. This field is shared between backup and restore process. It may contain the original backup destination path or the original restore source path. Depending on whether backup or restore occurred first on a server for a database. Note that consecutive restores from the same backup file will not update the path regardless of it's location at restore time. Because of this, **physical_device_name** field cannot be used to see the restore path used.|  
|**device_type**|**tinyint**|Type of backup device:<br /><br /> 2 = Disk<br /><br /> 5 = Tape<br /><br /> 7 = Virtual device<br /><br /> 9 = Azure Storage<br /><br /> 105 = A permanent backup device.<br /><br /> Can be NULL.<br /><br /> All permanent device names and device numbers can be found in **sys.backup_devices**.|  
|**physical_block_size**|**int**|Physical block size used to write the media family. Can be NULL.|  
|**mirror**|**tinyint**|Mirror number (0-3).|  
  
## Remarks  
 RESTORE VERIFYONLY FROM *backup_device* WITH LOADHISTORY populates the columns of the **backupmediaset** table with the appropriate values from the media-set header.  
  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [backupfile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfile-transact-sql.md)   
 [backupfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)   
 [backupmediaset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediaset-transact-sql.md)   
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
