---
title: "RESTORE HEADERONLY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "HEADERONLY"
  - "RESTORE HEADERONLY"
  - "RESTORE_HEADERONLY_TSQL"
  - "HEADERONLY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backup sets [SQL Server], header information"
  - "headers [SQL Server]"
  - "RESTORE HEADERONLY statement"
  - "backup header information [SQL Server]"
ms.assetid: 4b88e98c-49c4-4388-ab0e-476cc956977c
author: mashamsft
ms.author: mathoma
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# RESTORE Statements - HEADERONLY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md )]

  Returns a result set containing all the backup header information for all backup sets on a particular backup device in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. 
 
> [!NOTE]  
>  For the descriptions of the arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
RESTORE HEADERONLY   
FROM <backup_device>   
[ WITH   
 {  
--Backup Set Options  
   FILE = { backup_set_file_number | @backup_set_file_number }   
 | PASSWORD = { password | @password_variable }   
  
--Media Set Options  
 | MEDIANAME = { media_name | @media_name_variable }   
 | MEDIAPASSWORD = { mediapassword | @mediapassword_variable }  
  
--Error Management Options  
 | { CHECKSUM | NO_CHECKSUM }   
 | { STOP_ON_ERROR | CONTINUE_AFTER_ERROR }  
  
--Tape Options  
 | { REWIND | NOREWIND }   
 | { UNLOAD | NOUNLOAD }    
 } [ ,...n ]  
]  
[;]  
  
<backup_device> ::=  
{   
   { logical_backup_device_name |  
      @logical_backup_device_name_var }  
   | { DISK | TAPE } = { 'physical_backup_device_name' |  
       @physical_backup_device_name_var }   
}  
  
```  
  
## Arguments  
 For descriptions of the RESTORE HEADERONLY arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
## Result Sets  
 For each backup on a given device, the server sends a row of header information with the following columns:  
  
> [!NOTE]
>  RESTORE HEADERONLY looks at all backup sets on the media. Therefore, producing this result set when using high-capacity tape drives can take some time. To get a quick look at the media without getting information about every backup set, use RESTORE LABELONLY or specify FILE **=** _backup_set_file_number_.  
> 
> [!NOTE]
>  Due to the nature of [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tape Format, it is possible for backup sets from other software programs to occupy space on the same media as [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup sets. The result set returned by RESTORE HEADERONLY includes a row for each of these other backup sets.  
  
|Column name|Data type|Description for SQL Server backup sets|  
|-----------------|---------------|--------------------------------------------|  
|**BackupName**|**nvarchar(128)**|Backup set name.|  
|**BackupDescription**|**nvarchar(255)**|Backup set description.|  
|**BackupType**|**smallint**|Backup type:<br /><br /> **1** = Database<br /><br /> **2** = Transaction log<br /><br /> **4** = File<br /><br /> **5** = Differential database<br /><br /> **6** = Differential file<br /><br /> **7** = Partial<br /><br /> **8** = Differential partial|  
|**ExpirationDate**|**datetime**|Expiration date for the backup set.|  
|**Compressed**|**BYTE(1)**|Whether the backup set is compressed using software-based compression:<br /><br /> **0** = No<br /><br /> **1** = Yes|  
|**Position**|**smallint**|Position of the backup set in the volume (for use with the FILE = option).|  
|**DeviceType**|**tinyint**|Number corresponding to the device used for the backup operation.<br /><br /> Disk:<br /><br /> **2** = Logical<br /><br /> **102** = Physical<br /><br /> Tape:<br /><br /> **5** = Logical<br /><br /> **105** = Physical<br /><br /> Virtual Device:<br /><br /> **7** = Logical<br /><br /> **107** = Physical<br /><br /> Logical device names and device numbers are in **sys.backup_devices**; for more information, see [sys.backup_devices &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md).|  
|**UserName**|**nvarchar(128)**|User name that performed the backup operation.|  
|**ServerName**|**nvarchar(128)**|Name of the server that wrote the backup set.|  
|**DatabaseName**|**nvarchar(128)**|Name of the database that was backed up.|  
|**DatabaseVersion**|**int**|Version of the database from which the backup was created.|  
|**DatabaseCreationDate**|**datetime**|Date and time the database was created.|  
|**BackupSize**|**numeric(20,0)**|Size of the backup, in bytes.|  
|**FirstLSN**|**numeric(25,0)**|Log sequence number of the first log record in the backup set.|  
|**LastLSN**|**numeric(25,0)**|Log sequence number of the next log record after the backup set.|  
|**CheckpointLSN**|**numeric(25,0)**|Log sequence number of the most recent checkpoint at the time the backup was created.|  
|**DatabaseBackupLSN**|**numeric(25,0)**|Log sequence number of the most recent full database backup.<br /><br /> **DatabaseBackupLSN**  is the "begin of checkpoint" that is triggered when the backup starts. This LSN will coincide with **FirstLSN** if the backup is taken when the database is idle and no replication is configured.|  
|**BackupStartDate**|**datetime**|Date and time that the backup operation began.|  
|**BackupFinishDate**|**datetime**|Date and time that the backup operation finished.|  
|**SortOrder**|**smallint**|Server sort order. This column is valid for database backups only. Provided for backward compatibility.|  
|**CodePage**|**smallint**|Server code page or character set used by the server.|  
|**UnicodeLocaleId**|**int**|Server Unicode locale ID configuration option used for Unicode character data sorting. Provided for backward compatibility.|  
|**UnicodeComparisonStyle**|**int**|Server Unicode comparison style configuration option, which provides additional control over the sorting of Unicode data. Provided for backward compatibility.|  
|**CompatibilityLevel**|**tinyint**|Compatibility level setting of the database from which the backup was created.|  
|**SoftwareVendorId**|**int**|Software vendor identification number. For SQL Server, this number is **4608** (or hexadecimal **0x1200**).|  
|**SoftwareVersionMajor**|**int**|Major version number of the server that created the backup set.|  
|**SoftwareVersionMinor**|**int**|Minor version number of the server that created the backup set.|  
|**SoftwareVersionBuild**|**int**|Build number of the server that created the backup set.|  
|**MachineName**|**nvarchar(128)**|Name of the computer that performed the backup operation.|  
|**Flags**|**int**|Individual flags bit meanings if set to **1**:<br /><br /> **1** = Log backup contains bulk-logged operations.<br /><br /> **2** = Snapshot backup.<br /><br /> **4** = Database was read-only when backed up.<br /><br /> **8** = Database was in single-user mode when backed up.<br /><br /> **16** = Backup contains backup checksums.<br /><br /> **32** = Database was damaged when backed up, but the backup operation was requested to continue despite errors.<br /><br /> **64** = Tail log backup.<br /><br /> **128** = Tail log backup with incomplete metadata.<br /><br /> **256** = Tail log backup with NORECOVERY.<br /><br /> **Important:** We recommend that instead of **Flags** you use the individual Boolean columns (listed below starting with **HasBulkLoggedData** and ending with **IsCopyOnly**).|  
|**BindingID**|**uniqueidentifier**|Binding ID for the database. This corresponds to **sys.database_recovery_status database_guid**. When a database is restored, a new value is assigned. Also see **FamilyGUID** (below).|  
|**RecoveryForkID**|**uniqueidentifier**|ID for the ending recovery fork. This column corresponds to **last_recovery_fork_guid** in the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) table.<br /><br /> For data backups, **RecoveryForkID** equals **FirstRecoveryForkID**.|  
|**Collation**|**nvarchar(128)**|Collation used by the database.|  
|**FamilyGUID**|**uniqueidentifier**|ID of the original database when created. This value stays the same when the database is restored.|  
|**HasBulkLoggedData**|**bit**|**1** = Log backup containing bulk-logged operations.|  
|**IsSnapshot**|**bit**|**1** = Snapshot backup.|  
|**IsReadOnly**|**bit**|**1** = Database was read-only when backed up.|  
|**IsSingleUser**|**bit**|**1** = Database was single-user when backed up.|  
|**HasBackupChecksums**|**bit**|**1** = Backup contains backup checksums.|  
|**IsDamaged**|**bit**|**1** = Database was damaged when backed up, but the backup operation was requested to continue despite errors.|  
|**BeginsLogChain**|**bit**|**1** = This is the first in a continuous chain of log backups. A log chain begins with the first log backup taken after the database is created or when it is switched from the Simple to the Full or Bulk-Logged Recovery Model.|  
|**HasIncompleteMetaData**|**bit**|**1** = A tail-log backup with incomplete meta-data.<br /><br /> For information about tail-log backups with incomplete backup metadata, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).|  
|**IsForceOffline**|**bit**|**1** = Backup taken with NORECOVERY; the database was taken offline by backup.|  
|**IsCopyOnly**|**bit**|**1** = A copy-only backup.<br /><br /> A copy-only backup does not impact the overall backup and restore procedures for the database. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/copy-only-backups-sql-server.md).|  
|**FirstRecoveryForkID**|**uniqueidentifier**|ID for the starting recovery fork. This column corresponds to **first_recovery_fork_guid** in the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) table.<br /><br /> For data backups, **FirstRecoveryForkID** equals **RecoveryForkID**.|  
|**ForkPointLSN**|**numeric(25,0)** NULL|If **FirstRecoveryForkID** is not equal to **RecoveryForkID**, this is the log sequence number of the fork point. Otherwise, this value is NULL.|  
|**RecoveryModel**|**nvarchar(60)**|Recovery model for the Database, one of:<br /><br /> FULL<br /><br /> BULK-LOGGED<br /><br /> SIMPLE|  
|**DifferentialBaseLSN**|**numeric(25,0)** NULL|For a single-based differential backup, the value equals the **FirstLSN** of the differential base; changes with LSNs greater than or equal to **DifferentialBaseLSN** are included in the differential.<br /><br /> For a multi-based differential, the value is NULL, and the base LSN must be determined at the file level. For more information, see [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).<br /><br /> For non-differential backup types, the value is always NULL.<br /><br /> For more information, see [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md).|  
|**DifferentialBaseGUID**|**uniqueidentifier**|For a single-based differential backup, the value is the unique identifier of the differential base.<br /><br /> For multi-based differentials, the value is NULL, and the differential base must be determined per file.<br /><br /> For non-differential backup types, the value is NULL.|  
|**BackupTypeDescription**|**nvarchar(60)**|Backup type as string, one of:<br /><br /> DATABASE<br /><br /> TRANSACTION LOG<br /><br /> FILE OR FILEGROUP<br /><br /> DATABASE DIFFERENTIAL<br /><br /> FILE DIFFERENTIAL PARTIAL<br /><br /> PARTIAL DIFFERENTIAL|  
|**BackupSetGUID**|**uniqueidentifier** NULL|Unique identification number of the backup set, by which it is identified on the media.|  
|**CompressedBackupSize**|**bigint**|Byte count of the backup set. For uncompressed backups, this value is the same as **BackupSize**.<br /><br /> To calculate the compression ratio, use **CompressedBackupSize** and **BackupSize**.<br /><br /> During an **msdb** upgrade, this value is set to match the value of the **BackupSize** column.|  
|**containment**|**tinyint** not NULL|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Indicates the containment status of the database.<br /><br /> 0 = database containment is off<br /><br /> 1 = database is in partial containment|  
|**KeyAlgorithm**|**nvarchar(32)**|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (CU1) through current version.<br /><br /> The encryption algorithm used to encrypt the backup. NO_Encryption indicates that the backup was not encrypted. When the correct value cannot be determined the value should be NULL.|  
|**EncryptorThumbprint**|**varbinary(20)**|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (CU1) through current version.<br /><br /> The thumbprint of the encryptor which can be used to find certificate or the asymmetric key in the database. When the backup was not encrypted, this value is NULL.|  
|**EncryptorType**|**nvarchar(32)**|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] (CU1) through current version.<br /><br /> The type of encryptor used: Certificate or Asymmetric Key. When the backup was not encrypted, this value is NULL.|  
  
> [!NOTE]  
>  If passwords are defined for the backup sets, RESTORE HEADERONLY shows complete information for only the backup set whose password matches the specified PASSWORD option of the command. RESTORE HEADERONLY also shows complete information for unprotected backup sets. The **BackupName** column for the other password-protected backup sets on the media is set to '**_Password Protected_**', and all other columns are NULL.  
  
## General Remarks  
 A client can use RESTORE HEADERONLY to retrieve all the backup header information for all backups on a particular backup device. For each backup on the backup device, the server sends the header information as a row.  
  
## Security  
 A backup operation may optionally specify passwords for a media set, a backup set, or both. When a password has been defined on a media set or backup set, you must specify the correct password or passwords in the RESTORE statement. These passwords prevent unauthorized restore operations and unauthorized appends of backup sets to media using [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools. However, a password does not prevent overwrite of media using the BACKUP statement's FORMAT option.  
  
> [!IMPORTANT]  
>  The protection provided by this password is weak. It is intended to prevent an incorrect restore using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools by authorized or unauthorized users. It does not prevent the reading of the backup data by other means or the replacement of the password. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]The best practice for protecting backups is to store backup tapes in a secure location or back up to disk files that are protected by adequate access control lists (ACLs). The ACLs should be set on the directory root under which backups are created.  
  
### Permissions  
 Obtaining information about a backup set or backup device requires CREATE DATABASE permission. For more information, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
  
## Examples  
 The following example returns the information in the header for the disk file `C:\AdventureWorks-FullBackup.bak`.  
  
```  
RESTORE HEADERONLY   
FROM DISK = N'C:\AdventureWorks-FullBackup.bak'   
WITH NOUNLOAD;  
GO  
```  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)   
 [Enable or Disable Backup Checksums During Backup or Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/enable-or-disable-backup-checksums-during-backup-or-restore-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)  
  
  
