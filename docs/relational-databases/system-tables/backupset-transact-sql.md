---
title: "backupset (Transact-SQL)"
description: "Contains a row for each backup set. A backup set contains the backup from a single, successful backup operation."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 09/09/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom: event-tier1-build-2022
f1_keywords:
  - "backupset"
  - "backupset_TSQL"
helpviewer_keywords:
  - "backupset system table"
  - "backup media [SQL Server], backupset system table"
  - "backup sets [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# backupset (Transact-SQL)

[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Contains a row for each backup set. A *backup set* contains the backup from a single, successful backup operation. RESTORE, RESTORE FILELISTONLY, RESTORE HEADERONLY, and RESTORE VERIFYONLY statements operate on a single backup set within the media set on the specified backup device or devices.

 This table is stored in the `msdb` database.

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
|**position**|**int**|Backup set position used in the restore operation to locate the appropriate backup set and files. Can be NULL. For more information, see FILE in [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md).|
|**expiration_date**|**datetime**|Date and time the backup set expires. Can be NULL.|
|**software_vendor_id**|**int**|Identification number of the software vendor writing the backup media header. Can be NULL.|
|**name**|**nvarchar(128)**|Name of the backup set. Can be NULL.|
|**description**|**nvarchar(255)**|Description of the backup set. Can be NULL.|
|**user_name**|**nvarchar(128)**|Name of the user performing the backup operation. Can be NULL.|
|**software_major_version**|**tinyint**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] major version number. Can be NULL.|
|**software_minor_version**|**tinyint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] minor version number. Can be NULL.|
|**software_build_version**|**smallint**|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] build number. Can be NULL.|
|**time_zone**|**smallint**|Difference between local time (where the backup operation is taking place) and Coordinated Universal Time (UTC) in 15-minute intervals using the time zone information at the time the backup operation started. Values can be -48 through +48, inclusive. A value of 127 indicates unknown. For example, -20 is Eastern Standard Time (EST) or five hours after UTC. Can be NULL.|
|**mtf_minor_version**|**tinyint**|[!INCLUDE[msCoName](../../includes/msconame-md.md)] Tape Format minor version number. Can be NULL.|
|**first_lsn**|**numeric(25,0)**|Log sequence number of the first or oldest log record in the backup set. Can be NULL.|
|**last_lsn**|**numeric(25,0)**|Log sequence number of the next log record after the backup set. Can be NULL.|
|**checkpoint_lsn**|**numeric(25,0)**|Log sequence number of the log record where redo must start. Can be NULL.|
|**database_backup_lsn**|**numeric(25,0)**|Log sequence number of the most recent full database backup. Can be NULL.<br /><br />**database_backup_lsn** is the "begin of checkpoint" that is triggered when the backup starts. This LSN will coincide with **first_lsn** if the backup is taken when the database is idle and no replication is configured.|
|**database_creation_date**|**datetime**|Date and time the database was originally created. Can be NULL.|
|**backup_start_date**|**datetime**|Date and time the backup operation started. Can be NULL.|
|**backup_finish_date**|**datetime**|Date and time the backup operation finished. Can be NULL.|
|**type**|**char(1)**|Backup type. Can be:<br /><br />D = Database<br />I = Differential database<br />L = Log<br />F = File or filegroup<br />G =Differential file<br />P = Partial<br />Q = Differential partial<br /><br />Can be NULL.|
|**sort_order**|**smallint**|Sort order of the server performing the backup operation. Can be NULL. For more information about sort orders and collations, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).|
|**code_page**|**smallint**|Code page of the server performing the backup operation. Can be NULL. For more information about code pages, see [Collation and Unicode Support](../../relational-databases/collations/collation-and-unicode-support.md).|
|**compatibility_level**|**tinyint**|Compatibility level setting for the database. Can be:<br /><br />90 = [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]<br />100 = [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)]<br />110 = [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]<br />120 = [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]<br />130 = [!INCLUDE[ssSQL16](../../includes/sssql16-md.md)]<br />140 = [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]<br />150 = [!INCLUDE[ssSQL19](../../includes/sssql19-md.md)]<br />160 = [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)]<br /><br />Can be NULL.<br /><br />For more information about compatibility levels, see [ALTER DATABASE Compatibility Level (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).|
|**database_version**|**int**|Database version number. Can be NULL.|
|**backup_size**|**numeric(20,0)**|Size of the backup set, in bytes. Can be NULL. For VSS backups, backup_size is an estimated value.|
|**database_name**|**nvarchar(128)**|Name of the database involved in the backup operation. Can be NULL.|
|**server_name**|**nvarchar(128)**|Name of the server running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup operation. Can be NULL.|
|**machine_name**|**nvarchar(128)**|Name of the computer running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Can be NULL.|
|**flags**|**int**|In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **flags** column has been deprecated and is being replaced with the following bit columns:<br /><br />**has_bulk_logged_data**<br />**is_snapshot**<br />**is_readonly**<br />**is_single_user**<br />**has_backup_checksums**<br />**is_damaged**<br />**begins_log_chain**<br />**has_incomplete_metadata**<br />**is_force_offline**<br />**is_copy_only**<br /><br />Can be NULL.<br /><br />In backup sets from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], flag bits:<br />1 = Backup contains minimally logged data.<br />2 = WITH SNAPSHOT was used.<br />4 = Database was read-only at the time of backup.<br />8 = Database was in single-user mode at the time of backup.|
|**unicode_locale**|**int**|Unicode locale. Can be NULL.|
|**unicode_compare_style**|**int**|Unicode compare style. Can be NULL.|
|**collation_name**|**nvarchar(128)**|Collation name. Can be NULL.|
|**Is_password_protected**|**bit**|Is the backup set<br /><br />password protected:<br /><br />0 = Not protected<br /><br />1 = Protected|
|**recovery_model**|**nvarchar(60)**|Recovery model for the database:<br /><br />FULL<br /><br />BULK-LOGGED<br /><br />SIMPLE|
|**has_bulk_logged_data**|**bit**|1 = Backup contains bulk-logged data.|
|**is_snapshot**|**bit**|1 = Backup was taken using the SNAPSHOT option.|
|**is_readonly**|**bit**|1 = Database was read-only at the time of backup.|
|**is_single_user**|**bit**|1 = Database was single-user at the time of backup.|
|**has_backup_checksums**|**bit**|1 = Backup contains backup checksums.|
|**is_damaged**|**bit**|1 = Damage to the database was detected when this backup was created. The backup operation was requested to continue despite errors.|
|**begins_log_chain**|**bit**|1 = This is the first in a continuous chain of log backups. A log chain begins with the first log backup taken after the database is created or when it is switched from the simple to the full or bulk-logged recovery model.|
|**has_incomplete_metadata**|**bit**|1 = A tail log backup with incomplete metadata. For more information, see [Tail-Log Backups (SQL Server)](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).|
|**is_force_offline**|**bit**|1 = Database was taken offline using the NORECOVERY option when the backup was taken.|
|**is_copy_only**|**bit**|1 = A copy-only backup. For more information, see [Copy-Only Backups (SQL Server)](../../relational-databases/backup-restore/copy-only-backups-sql-server.md).|
|**first_recovery_fork_guid**|**uniqueidentifier**|ID of the starting recovery fork. This corresponds to **FirstRecoveryForkID** of RESTORE HEADERONLY.<br /><br />For data backups, **first_recovery_fork_guid** equals **last_recovery_fork_guid**.|
|**last_recovery_fork_guid**|**uniqueidentifier**|ID of the ending recovery fork. This corresponds to **RecoveryForkID** of RESTORE HEADERONLY.<br /><br />For data backups, **first_recovery_fork_guid** equals **last_recovery_fork_guid**.|
|**fork_point_lsn**|**numeric(25,0)**|If **first_recovery_fork_guid** is not equal to **last_recovery_fork_guid**, this is the log sequence number of the fork point. Otherwise, the value is NULL.|
|**database_guid**|**uniqueidentifier**|Unique ID for the database. This corresponds to **BindingID** of RESTORE HEADERONLY. When the database is restored, a new value is assigned.|
|**family_guid**|**uniqueidentifier**|Unique ID of the original database at creation. This value remains the same when the database is restored, even to a different name.|
|**differential_base_lsn**|**numeric(25,0)**|Base LSN for differential backups. For a single-based differential backup; changes with LSNs greater than or equal to **differential_base_lsn** are included in the differential backup.<br /><br />For a multibased differential, the value is NULL, and the base LSN must be determined at the file level (see [backupfile (Transact-SQL)](../../relational-databases/system-tables/backupfile-transact-sql.md)).<br /><br />For nondifferential backup types, the value is always NULL.|
|**differential_base_guid**|**uniqueidentifier**|For a single-based differential backup, the value is the unique identifier of the differential base.<br /><br />For multibased differentials, the value is NULL, and the differential base must be determined at the file level.<br /><br />For nondifferential backup types, the value is NULL.|
|**compressed_backup_size**|**Numeric(20,0)**|Total Byte count of the backup stored on disk.<br /><br />To calculate the compression ratio, use **compressed_backup_size** and **backup_size**.<br /><br />During an `msdb` upgrade, this value is set to NULL. which indicates an uncompressed backup.|
|**key_algorithm**|**nvarchar(32)**|The encryption algorithm used to encrypt the backup. NO_Encryption value indicated that the backup was not encrypted.|
|**encryptor_thumbprint**|**varbinary(20)**|The thumbprint of the encryptor which can be used to find certificate or the asymmetric key in the database. In the case where the backup was not encrypted, this value is NULL.|
|**encryptor_type**|**nvarchar(32)**|The type of encryptor used: Certificate or Asymmetric Key. In the case where the backup was not encrypted, this value is NULL.|
|**encryptor_type**|**nvarchar(32)**|The type of encryptor used: Certificate or asymmetric key. In the case where the backup was not encrypted, this value is NULL.|
|**last_valid_restore_time**|**datetime**|The latest point in time to which the backup can be restored. Introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. |
|**compression_algorithm**|**nvarchar(32)**|The compression algorithm that was used when creating the SQL Server backup. Introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. Default is `MS_XPRESS`. For more information, see [BACKUP COMPRESSION](../../t-sql/statements/backup-transact-sql.md#compression) and [Integrated acceleration and offloading](../integrated-acceleration/overview.md).|

## Remarks

- `RESTORE VERIFYONLY FROM <backup_device> WITH LOADHISTORY` populates the column of the `backupmediaset` table with the appropriate values from the media-set header.  
- To reduce the number of rows in this table and in other backup and history tables, execute the [sp_delete_backuphistory](../../relational-databases/system-stored-procedures/sp-delete-backuphistory-transact-sql.md) stored procedure.  
- For SQL Managed Instance, see [backup transparency](/azure/azure-sql/managed-instance/backup-transparency) and how to [monitor backups](/azure/azure-sql/managed-instance/backup-activity-monitor).

## Examples

### Query backup history

The following query returns successful backup information from the past two months.

```sql
SELECT bs.database_name,
    backuptype = CASE 
        WHEN bs.type = 'D' AND bs.is_copy_only = 0 THEN 'Full Database'
        WHEN bs.type = 'D' AND bs.is_copy_only = 1 THEN 'Full Copy-Only Database'
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
    backup_size_mb = CONVERT(DECIMAL(10, 2), bs.backup_size / 1024. / 1024.),
    compressed_backup_size_mb = CONVERT(DECIMAL(10, 2), bs.compressed_backup_size / 1024. / 1024.),
    database_backup_lsn, -- For tlog and differential backups, this is the checkpoint_lsn of the FULL backup it is based on.
    checkpoint_lsn,
    begins_log_chain,
    bms.is_password_protected
FROM msdb.dbo.backupset bs
LEFT JOIN msdb.dbo.backupmediafamily bf
    ON bs.[media_set_id] = bf.[media_set_id]
INNER JOIN msdb.dbo.backupmediaset bms
    ON bs.[media_set_id] = bms.[media_set_id]
WHERE bs.backup_start_date > DATEADD(MONTH, - 2, sysdatetime()) --only look at last two months
ORDER BY bs.database_name ASC,
    bs.Backup_Start_Date DESC;
```

## Next steps

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Backup and Restore Tables (Transact-SQL)](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)
- [backupfile (Transact-SQL)](../../relational-databases/system-tables/backupfile-transact-sql.md)
- [backupfilegroup (Transact-SQL)](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)
- [backupmediafamily (Transact-SQL)](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)
- [backupmediaset (Transact-SQL)](../../relational-databases/system-tables/backupmediaset-transact-sql.md)
- [Possible Media Errors During Backup and Restore (SQL Server)](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md)
- [Media Sets, Media Families, and Backup Sets (SQL Server)](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)
- [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
- [RESTORE HEADERONLY (Transact-SQL)](../../t-sql/statements/restore-statements-headeronly-transact-sql.md)
- [Backup and Restore Tables (Transact-SQL)](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)
