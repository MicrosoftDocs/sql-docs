---
title: "backupset (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "backupset"
  - "backupset_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backupset system table"
  - "backup media [SQL Server], backupset system table"
  - "backup sets [SQL Server]"
ms.assetid: 6ff79bbf-4acf-4f75-926f-38637ca8a943
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# backupset (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-pdw-md.md)]

  Contains a row for each backup set. A *backup set* contains the backup from a single, successful backup operation. RESTORE, RESTORE FILELISTONLY, RESTORE HEADERONLY, and RESTORE VERIFYONLY statements operate on a single backup set within the media set on the specified backup device or devices.  
  
 This table is stored in the **msdb** database.  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**backup_set_id**|**int**|Unique backup set identification number that identifies the backup set. Identity, primary key.|  
|**backup_set_uuid**|**uniqueidentifier**|Unique backup set identification number that identifies the backup set.|  
|**media_set_id**|**int**|Unique media set identification number that identifies the media set containing the backup set. References **backupmediaset(media_set_id)**.|  
|**first_family_number**|**tinyint**|Family number of the media where the backup set starts. Can be NULL.|  
|**first_media_number**|**smallint**|Media number of the media where the backup set starts. Can be NULL.|  
|**last_family_number**|**tinyint**|Family number of the media where the backup set ends. Can be NULL.|  
|**last_media_number**|**smallint**|Media number of the media where the backup set ends. Can be NULL.|  
|**catalog_family_number**|**tinyint**|Family number of the media containing the start of the backup set directory. Can be NULL.|  
|**catalog_media_number**|**smallint**|Media number of the media containing the start of the backup set directory. Can be NULL.|  
|**position**|**int**|Backup set position used in the restore operation to locate the appropriate backup set and files. Can be NULL. For more information, see FILE in [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).|  
|**expiration_date**|**datetime**|Date and time the backup set expires. Can be NULL.|  
|**software_vendor_id**|**int**|Identification number of the software vendor writing the backup media header. Can be NULL.|  
|**name**|**nvarchar(128)**|Name of the backup set. Can be NULL.|  
|**description**|**nvarchar(255)**|Description of the backup set. Can be NULL.|  
|**user_name**|**nvarchar(128)**|Name of the user performing the backup operation. Can be NULL.|  
|**software_major_version**|**tinyint**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] major version number. Can be NULL.|  
|**software_minor_version**|**tinyint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] minor version number. Can be NULL.|  
|**software_build_version**|**smallint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] build number. Can be NULL.|  
|**time_zone**|**smallint**|Difference between local time (where the backup operation is taking place) and Coordinated Universal Time (UTC) in 15-minute intervals. Values can be -48 through +48, inclusive. A value of 127 indicates unknown. For example, -20 is Eastern Standard Time (EST) or five hours after UTC. Can be NULL.|  
|**mtf_minor_version**|**tinyint**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Tape Format minor version number. Can be NULL.|  
|**first_lsn**|**numeric(25,0)**|Log sequence number of the first or oldest log record in the backup set. Can be NULL.|  
|**last_lsn**|**numeric(25,0)**|Log sequence number of the next log record after the backup set. Can be NULL.|  
|**checkpoint_lsn**|**numeric(25,0)**|Log sequence number of the log record where redo must start. Can be NULL.|  
|**database_backup_lsn**|**numeric(25,0)**|Log sequence number of the most recent full database backup. Can be NULL.<br /><br /> **database_backup_lsn** is the "begin of checkpoint" that is triggered when the backup starts. This LSN will coincide with **first_lsn** if the backup is taken when the database is idle and no replication is configured.|  
|**database_creation_date**|**datetime**|Date and time the database was originally created. Can be NULL.|  
|**backup_start_date**|**datetime**|Date and time the backup operation started. Can be NULL.|  
|**backup_finish_date**|**datetime**|Date and time the backup operation finished. Can be NULL.|  
|**type**|**char(1)**|Backup type. Can be:<br /><br /> D = Database<br /><br /> I = Differential database<br /><br /> L = Log<br /><br /> F = File or filegroup<br /><br /> G =Differential file<br /><br /> P = Partial<br /><br /> Q = Differential partial<br /><br /> Can be NULL.|  
|**sort_order**|**smallint**|Sort order of the server performing the backup operation. Can be NULL. For more information about sort orders and collations, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).|  
|**code_page**|**smallint**|Code page of the server performing the backup operation. Can be NULL. For more information about code pages, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).|  
|**compatibility_level**|**tinyint**|Compatibility level setting for the database. Can be:<br /><br /> 90 = [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]<br /><br /> 100 = [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]<br /><br /> 110 = [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]<br /><br /> 120 = [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]<br /><br /> Can be NULL.<br /><br /> For more information about compatibility levels, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).|  
|**database_version**|**int**|Database version number. Can be NULL.|  
|**backup_size**|**numeric(20,0)**|Size of the backup set, in bytes. Can be NULL. For VSS backups, backup_size is an estimated value.|  
|**database_name**|**nvarchar(128)**|Name of the database involved in the backup operation. Can be NULL.|  
|**server_name**|**nvarchar(128)**|Name of the server running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup operation. Can be NULL.|  
|**machine_name**|**nvarchar(128)**|Name of the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Can be NULL.|  
|**flags**|**int**|In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **flags** column has been deprecated and is being replaced with the following bit columns:<br /><br /> **has_bulk_logged_data** <br /> **is_snapshot** <br /> **is_readonly** <br /> **is_single_user** <br /> **has_backup_checksums** <br /> **is_damaged** <br /> **begins_log_chain** <br /> **has_incomplete_metadata** <br /> **is_force_offline** <br /> **is_copy_only**<br /><br /> Can be NULL.<br /><br /> In backup sets from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], flag bits:<br />1 = Backup contains minimally logged data. <br />2 = WITH SNAPSHOT was used. <br />4 = Database was read-only at the time of backup.<br />8 = Database was in single-user mode at the time of backup.|  
|**unicode_locale**|**int**|Unicode locale. Can be NULL.|  
|**unicode_compare_style**|**int**|Unicode compare style. Can be NULL.|  
|**collation_name**|**nvarchar(128)**|Collation name. Can be NULL.|  
|**Is_password_protected**|**bit**|Is the backup set<br /><br /> password protected:<br /><br /> 0 = Not protected<br /><br /> 1 = Protected|  
|**recovery_model**|**nvarchar(60)**|Recovery model for the database:<br /><br /> FULL<br /><br /> BULK-LOGGED<br /><br /> SIMPLE|  
|**has_bulk_logged_data**|**bit**|1 = Backup contains bulk-logged data.|  
|**is_snapshot**|**bit**|1 = Backup was taken using the SNAPSHOT option.|  
|**is_readonly**|**bit**|1 = Database was read-only at the time of backup.|  
|**is_single_user**|**bit**|1 = Database was single-user at the time of backup.|  
|**has_backup_checksums**|**bit**|1 = Backup contains backup checksums.|  
|**is_damaged**|**bit**|1 = Damage to the database was detected when this backup was created. The backup operation was requested to continue despite errors.|  
|**begins_log_chain**|**bit**|1 = This is the first in a continuous chain of log backups. A log chain begins with the first log backup taken after the database is created or when it is switched from the simple to the full or bulk-logged recovery model.|  
|**has_incomplete_metadata**|**bit**|1 = A tail log backup with incomplete metadata. For more information, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).|  
|**is_force_offline**|**bit**|1 = Database was taken offline using the NORECOVERY option when the backup was taken.|  
|**is_copy_only**|**bit**|1 = A copy-only backup. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/copy-only-backups-sql-server.md).|  
|**first_recovery_fork_guid**|**uniqueidentifier**|ID of the starting recovery fork. This corresponds to **FirstRecoveryForkID** of RESTORE HEADERONLY.<br /><br /> For data backups, **first_recovery_fork_guid** equals **last_recovery_fork_guid**.|  
|**last_recovery_fork_guid**|**uniqueidentifier**|ID of the ending recovery fork. This corresponds to **RecoveryForkID** of RESTORE HEADERONLY.<br /><br /> For data backups, **first_recovery_fork_guid** equals **last_recovery_fork_guid**.|  
|**fork_point_lsn**|**numeric(25,0)**|If **first_recovery_fork_guid** is not equal to **last_recovery_fork_guid**, this is the log sequence number of the fork point. Otherwise, the value is NULL.|  
|**database_guid**|**uniqueidentifier**|Unique ID for the database. This corresponds to **BindingID** of RESTORE HEADERONLY. When the database is restored, a new value is assigned.|  
|**family_guid**|**uniqueidentifier**|Unique ID of the original database at creation. This value remains the same when the database is restored, even to a different name.|  
|**differential_base_lsn**|**numeric(25,0)**|Base LSN for differential backups. For a single-based differential backup; changes with LSNs greater than or equal to **differential_base_lsn** are included in the differential backup.<br /><br /> For a multibased differential, the value is NULL, and the base LSN must be determined at the file level (see [backupfile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfile-transact-sql.md)).<br /><br /> For nondifferential backup types, the value is always NULL.|  
|**differential_base_guid**|**uniqueidentifier**|For a single-based differential backup, the value is the unique identifier of the differential base.<br /><br /> For multibased differentials, the value is NULL, and the differential base must be determined at the file level.<br /><br /> For nondifferential backup types, the value is NULL.|  
|**compressed_backup_size**|**Numeric(20,0)**|Total Byte count of the backup stored on disk.<br /><br /> To calculate the compression ratio, use **compressed_backup_size** and **backup_size**.<br /><br /> During an **msdb** upgrade, this value is set to NULL. which indicates an uncompressed backup.|  
|**key_algorithm**|**nvarchar(32)**|The encryption algorithm used to encrypt the backup. NO_Encryption value indicated that the backup was not encrypted.|  
|**encryptor_thumbprint**|**varbinary(20)**|The thumbprint of the encryptor which can be used to find certificate or the asymmetric key in the database. In the case where the backup was not encrypted, this value is NULL.|  
|**encryptor_type**|**nvarchar(32)**|The type of encryptor used: Certificate or Asymmetric Key. . In the case where the backup was not encrypted, this value is NULL.|  
  
## Remarks  
 RESTORE VERIFYONLY FROM *backup_device* WITH LOADHISTORY populates the column of the **backupmediaset** table with the appropriate values from the media-set header.  
  
 To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [backupfile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfile-transact-sql.md)   
 [backupfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)   
 [backupmediafamily &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)   
 [backupmediaset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediaset-transact-sql.md)   
 [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)   
 [RESTORE HEADERONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)   
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)  
  
  
