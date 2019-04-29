---
title: "backupfile (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "backupfile"
  - "backupfile_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "file backups [SQL Server], backupfile system table"
  - "backupfile system table"
ms.assetid: f1a7fc0a-f4b4-47eb-9138-eebf930dc9ac
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# backupfile (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row for each data or log file of a database. The columns describes the file configuration at the time the backup was taken. Whether or not the file is included in the backup is determined by the **is_present** column. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**backup_set_id**|**int**|Unique identification number of the file containing the backup set. References **backupset(backup_set_id)**.|  
|**first_family_number**|**tinyint**|Family number of the first media containing this backup file. Can be NULL.|  
|**first_media_number**|**smallint**|Media number of the first media containing this backup file. Can be NULL.|  
|**filegroup_name**|**nvarchar(128)**|Name of the filegroup containing a backed up database file. Can be NULL.|  
|**page_size**|**int**|Size of the page, in bytes.|  
|**file_number**|**numeric(10,0)**|File identification number unique within a database (corresponds to **sys.database_files**.**file_id**).|  
|**backed_up_page_count**|**numeric(10,0)**|Number of pages backed up. Can be NULL.|  
|**file_type**|**char(1)**|File backed up, one of:<br /><br /> D = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data file.<br /><br /> L = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log file.<br /><br /> F = Full text catalog.<br /><br /> Can be NULL.|  
|**source_file_block_size**|**numeric(10,0)**|Device that the original data or log file resided on when it was backed up. Can be NULL.|  
|**file_size**|**numeric(20,0)**|Length of the file that is backed up, in bytes. Can be NULL.|  
|**logical_name**|**nvarchar(128)**|Logical name of the file that is backed up. Can be NULL.|  
|**physical_drive**|**nvarchar(260)**|Physical drive or partition name. Can be NULL.|  
|**physical_name**|**nvarchar(260)**|Remainder of the physical (operating system) file name. Can be NULL.|  
|**state**|**tinyint**|State of the file, one of:<br /><br /> 0 = ONLINE<br /><br /> 1 = RESTORING<br /><br /> 2 = RECOVERING<br /><br /> 3 = RECOVERY PENDING<br /><br /> 4 = SUSPECT<br /><br /> 6 = OFFLINE<br /><br /> 7 = DEFUNCT<br /><br /> 8 = DROPPED<br /><br /> Note: The value 5 is skipped so that these values correspond to the values for database states.|  
|**state_desc**|**nvarchar(64)**|Description of the file state, one of:<br /><br /> ONLINE RESTORING<br /><br /> RECOVERING<br /><br /> RECOVERY_PENDING<br /><br /> SUSPECT OFFLINE DEFUNCT|  
|**create_lsn**|**numeric(25,0)**|Log sequence number at which the file was created.|  
|**drop_lsn**|**numeric(25,0)**|Log sequence number at which the file was dropped. Can be NULL.<br /><br /> If the file has not been dropped, this value is NULL.|  
|**file_guid**|**uniqueidentifier**|Unique identifier of the file.|  
|**read_only_lsn**|**numeric(25,0)**|Log sequence number at which the filegroup containing the file changed from read-write to read-only (the most recent change). Can be NULL.|  
|**read_write_lsn**|**numeric(25,0)**|Log sequence number at which the filegroup containing the file changed from read-only to read-write (the most recent change). Can be NULL.|  
|**differential_base_lsn**|**numeric(25,0)**|Base LSN for differential backups. A differential backup includes only data extents having a log sequence number equal to or greater than **differential_base_lsn**.<br /><br /> For other backup types, the value is NULL.|  
|**differential_base_guid**|**uniqueidentifier**|For a differential backup, the unique identifier of the most recent data backup that forms the differential base of the file; if the value is NULL, the file was included in the differential backup, but was added after the base was created.<br /><br /> For other backup types, the value is NULL.|  
|**backup_size**|**numeric(20,0)**|Size of the backup for this file in bytes.|  
|**filegroup_guid**|**uniqueidentifier**|ID of the filegroup. To locate filegroup information in the backupfilegroup table, use **filegroup_guid** with **backup_set_id**.|  
|**is_readonly**|**bit**|1 = File is read-only.|  
|**is_present**|**bit**|1 = File is contained in the backup set.|  
  
## Remarks  
 RESTORE VERIFYONLY FROM *backup_device* WITH LOADHISTORY populates the columns of the **backupmediaset** table with the appropriate values from the media-set header.  
  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [backupfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)   
 [backupmediafamily &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)   
 [backupmediaset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediaset-transact-sql.md)   
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
