---
title: "backupfilegroup (Transact-SQL)"
description: backupfilegroup (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "backupfilegroup_TSQL"
  - "backupfilegroup"
helpviewer_keywords:
  - "filegroups [SQL Server], backupfilegroup system table"
  - "backupfilegroup system table"
dev_langs:
  - "TSQL"
ms.assetid: d26e8fbe-f5c5-4e10-b2bd-0d8e16ea21f9
---
# backupfilegroup (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each filegroup in a database at the time of backup. **backupfilegroup** is stored in the **msdb** database.  
  
> [!NOTE]  
>  The **backupfilegroup** table shows the filegroup configuration of the database, not of the backup set. To identify whether a file is included in the backup set, use the **is_present** column of the [backupfile](../../relational-databases/system-tables/backupfile-transact-sql.md) table.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**backup_set_id**|**int**|Backup set containing this filegroup.|  
|**name**|**sysname**|Name of the filegroup.|  
|**filegroup_id**|**int**|ID of the filegroup; unique within the database. Corresponds to **data_space_id** in **sys.filegroups**.|  
|**filegroup_guid**|**uniqueidentifier**|Globally unique identifier for the filegroup. Can be NULL.|  
|**type**|**char(2)**|Content type, one of:<br /><br /> FG = "Rows" Filegroup<br /><br /> SL = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Log filegroup|  
|**type_desc**|**nvarchar(60)**|Description of function type, one of:<br /><br /> ROWS_FILEGROUP<br /><br /> SQL_LOG_FILEGROUP|  
|**is_default**|**bit**|The default filegroup, used when no filegroup is specified in CREATE TABLE or CREATE INDEX.|  
|**is_readonly**|**bit**|1 = Filegroup is read-only.|  
|**log_filegroup_guid**|**uniqueidentifier**|Can be NULL.|  
  
## Remarks  
  
> [!IMPORTANT]  
>  The same filegroup name can appear in different databases; however, each filegroup has its own GUID. Therefore, **(backup_set_id,filegroup_guid)** is a unique key that identifies a filegroup in **backupfilegroup**.  
  
 RESTORE VERIFYONLY FROM *backup_device* WITH LOADHISTORY populates the columns of the **backupmediaset** table with the appropriate values from the media-set header.  
  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [backupfile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfile-transact-sql.md)   
 [backupmediafamily &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)   
 [backupmediaset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediaset-transact-sql.md)   
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
