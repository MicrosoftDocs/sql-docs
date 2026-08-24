---
title: "backupmediafamily (Transact-SQL)"
description: Reference for backupmediafamily, which contains one row for each media family.
author: VanMSFT
ms.author: vanto
ms.date: "11/16/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "backupmediafamily"
  - "backupmediafamily_TSQL"
helpviewer_keywords:
  - "backupmediafamily system table"
  - "backup media [SQL Server], backupmediafamily system table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || =azuresqldb-mi-current"
---
# backupmediafamily (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Contains one row for each media family. If a media family resides in a mirrored media set, the family has a separate row for each mirror in the media set. This table is stored in the **msdb** database.  
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**media_set_id**|**int**|Unique identification number that identifies the media set of which this family is a member. References **backupmediaset(media_set_id)**|  
|**family_sequence_number**|**tinyint**|Position of this media family in the media set.|  
|**media_family_id**|**uniqueidentifier**|Unique identification number that identifies the media family. Can be NULL.|  
|**media_count**|**int**|Number of media in the media family. Can be NULL.|  
|**logical_device_name**|**nvarchar(128)**|Name of this backup device in **sys.backup_devices.name**. If this is a temporary backup device (as opposed to a permanent backup device that exists in **sys.backup_devices**), the value of **logical_device_name** is NULL.|  
|**physical_device_name**|**nvarchar(260)**|Physical name of the backup device. Can be NULL. This field is shared between backup and restore process. It may contain the original backup destination path or the original restore source path. Depending on whether backup or restore occurred first on a server for a database. Consecutive restores from the same backup file won't update the path regardless of its location at restore time. Because of this, **physical_device_name** field can't be used to see the restore path used.|  
|**device_type**|**tinyint**|Type of backup device:<br /><br /> 2 = Disk<br /><br /> 5 = Tape<br /><br /> 7 = Virtual device<br /><br /> 9 = Azure Storage<br /><br /> 105 = A permanent backup device.<br /><br /> Can be NULL.<br /><br /> All permanent device names and device numbers can be found in **sys.backup_devices**.|  
|**physical_block_size**|**int**|Physical block size used to write the media family. Can be NULL.|  
|**mirror**|**tinyint**|Mirror number (0-3).|  
  
## Remarks  
 RESTORE VERIFYONLY FROM *backup_device* WITH LOADHISTORY populates the columns of the **backupmediaset** table with the appropriate values from the media-set header.  
  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## Related content

- [Backup and Restore Tables (Transact-SQL)](backup-and-restore-tables-transact-sql.md)
- [backupfile (Transact-SQL)](backupfile-transact-sql.md)
- [backupfilegroup (Transact-SQL)](backupfilegroup-transact-sql.md)
- [backupmediaset (Transact-SQL)](backupmediaset-transact-sql.md)
- [backupset (Transact-SQL)](backupset-transact-sql.md)
- [System Tables (Transact-SQL)](system-tables-transact-sql.md)
