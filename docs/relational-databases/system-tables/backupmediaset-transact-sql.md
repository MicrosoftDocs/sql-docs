---
title: "backupmediaset (Transact-SQL)"
description: Reference for backupmediaset, which contains one row for each backup media set.
author: VanMSFT
ms.author: vanto
ms.date: "11/16/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "backupmediaset"
  - "backupmediaset_TSQL"
helpviewer_keywords:
  - "backup media [SQL Server], backupmediaset system table"
  - "backupmediaset system table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2016||=azuresqldb-mi-current"
---
# backupmediaset (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Contains one row for each backup media set. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**media_set_id**|**int**|Unique media set identification number. Identity, primary key.|  
|**media_uuid**|**uniqueidentifier**|The UUID of the media set. All [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] media sets have a UUID.<br /><br /> For earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], however, if a media set contains only one media family, the **media_uuid** column might be NULL (**media_family_count** is 1).|  
|**media_family_count**|**tinyint**|Number of media families in the media set. Can be NULL.|  
|**name**|**nvarchar(128)**|Name of the media set. Can be NULL.<br /><br /> For more information, see MEDIANAME and MEDIADESCRIPTION in [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).|  
|**description**|**nvarchar(255)**|Textual description of the media set. Can be NULL.<br /><br /> For more information, see MEDIANAME and MEDIADESCRIPTION in [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).|  
|**software_name**|**nvarchar(128)**|Name of the backup software that wrote the media label. Can be NULL.|  
|**software_vendor_id**|**int**|Identification number of the software vendor that wrote the backup media label. Can be NULL.<br /><br /> The value for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is hexadecimal 0x1200.|  
|**MTF_major_version**|**tinyint**|Major version number of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tape Format used to generate this media set. Can be NULL.|  
|**mirror_count**|**tinyint**|Number of mirrors in the media set.|  
|**is_password_protected**|**bit**|Is the media set password protected:<br /><br /> 0 = Not protected<br /><br /> 1 = Protected|  
|**is_compressed**|**bit**|Whether the backup is compressed:<br /><br /> 0 = Not compressed<br /><br /> 1 = Compressed<br /><br /> During an **msdb** upgrade, this value is set to NULL. which indicates an uncompressed backup.|  
|**is_encrypted**|**Bit**|Whether the backup is encrypted:<br /><br /> 0 = Not encrypted<br /><br /> 1 = Encrypted|  
  
## Remarks  
 RESTORE VERIFYONLY FROM *backup_device* WITH LOADHISTORY populates the columns of the **backupmediaset** table with the appropriate values from the media-set header.  
  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  


## Examples
  
### Query backup history

The following query returns successful backup information from the past 2 months.


  
```sql
SELECT bs.database_name,
	backuptype = CASE
			WHEN bs.type = 'D'
			AND bs.is_copy_only = 0 THEN 'Full Database'
			WHEN bs.type = 'D'
			AND bs.is_copy_only = 1 THEN 'Full Copy-Only Database'
			WHEN bs.type = 'I' THEN 'Differential database backup'
			WHEN bs.type = 'L' THEN 'Transaction Log'
			WHEN bs.type = 'F' THEN 'File or filegroup'
			WHEN bs.type = 'G' THEN 'Differential file'
			WHEN bs.type = 'P' THEN 'Partial'
			WHEN bs.type = 'Q' THEN 'Differential partial'
		END + ' Backup',
	CASE bf.device_type
			WHEN 2 THEN 'Disk'
			WHEN 5 THEN 'Tape'
			WHEN 7 THEN 'Virtual device'
			WHEN 9 THEN 'Azure Storage'
			WHEN 105 THEN 'A permanent backup device'
			ELSE 'Other Device'
		END AS DeviceType,
	bms.software_name AS backup_software,
	bs.recovery_model,
	bs.compatibility_level,
	BackupStartDate = bs.Backup_Start_Date,
	BackupFinishDate = bs.Backup_Finish_Date,
	LatestBackupLocation = bf.physical_device_name,
	backup_size_mb = CONVERT(decimal(10, 2), bs.backup_size/1024./1024.),
	compressed_backup_size_mb = CONVERT(decimal(10, 2), bs.compressed_backup_size/1024./1024.),
	database_backup_lsn, -- For tlog and differential backups, this is the checkpoint_lsn of the FULL backup it is based on.
	checkpoint_lsn,
	begins_log_chain,
	bms.is_password_protected
FROM msdb.dbo.backupset bs
LEFT OUTER JOIN msdb.dbo.backupmediafamily bf ON bs.[media_set_id] = bf.[media_set_id]
INNER JOIN msdb.dbo.backupmediaset bms ON bs.[media_set_id] = bms.[media_set_id]
WHERE bs.backup_start_date > DATEADD(MONTH, -2, sysdatetime()) --only look at last two months
ORDER BY bs.database_name ASC, bs.Backup_Start_Date DESC;
```   


## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [backupfile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfile-transact-sql.md)   
 [backupfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)   
 [backupmediafamily &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)   
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
