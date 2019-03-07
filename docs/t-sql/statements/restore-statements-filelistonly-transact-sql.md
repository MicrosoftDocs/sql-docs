---
title: "RESTORE FILELISTONLY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "RESTORE FILELISTONLY"
  - "RESTORE_FILELISTONLY_TSQL"
  - "FILELISTONLY"
  - "FILELISTONLY_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backups [SQL Server], file lists"
  - "RESTORE FILELISTONLY statement"
  - "listing backed up files"
ms.assetid: 0b4b4d11-eb9d-4f3e-9629-6c79cec7a81a
author: mashamsft
ms.author: mathoma
manager: craigg
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# RESTORE Statements - FILELISTONLY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md )]


  Returns a result set containing a list of the database and log files contained in the backup set in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

> [!NOTE]  
>  For the descriptions of the arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
RESTORE FILELISTONLY   
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
 For descriptions of the RESTORE FILELISTONLY arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
## Result Sets  
 A client can use RESTORE FILELISTONLY to obtain a list of the files contained in a backup set. This information is returned as a result set containing one row for each file.  
  
|Column name|Data type|Description|  
|-|-|-|  
|LogicalName|**nvarchar(128)**|Logical name of the file.|  
|PhysicalName|**nvarchar(260)**|Physical or operating-system name of the file.|  
|Type|**char(1)**|The type of file, one of:<br /><br /> **L** = Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] log file<br /><br /> **D** = [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data file<br /><br /> **F** = Full Text Catalog<br /><br /> **S** = FileStream, FileTable, or [!INCLUDE[hek_2](../../includes/hek-2-md.md)] container|  
|FileGroupName|**nvarchar(128)** NULL|Name of the filegroup that contains the file.|  
|Size|**numeric(20,0)**|Current size in bytes.|  
|MaxSize|**numeric(20,0)**|Maximum allowed size in bytes.|  
|FileID|**bigint**|File identifier, unique within the database.|  
|CreateLSN|**numeric(25,0)**|Log sequence number at which the file was created.|  
|DropLSN|**numeric(25,0)** NULL|The log sequence number at which the file was dropped. If the file has not been dropped, this value is NULL.|  
|UniqueID|**uniqueidentifier**|Globally unique identifier of the file.|  
|ReadOnlyLSN|**numeric(25,0) NULL**|Log sequence number at which the filegroup containing the file changed from read-write to read-only (the most recent change).|  
|ReadWriteLSN|**numeric(25,0)** NULL|Log sequence number at which the filegroup containing the file changed from read-only to read-write (the most recent change).|  
|BackupSizeInBytes|**bigint**|Size of the backup for this file in bytes.|  
|SourceBlockSize|**int**|Block size of the physical device containing the file in bytes (not the backup device).|  
|FileGroupID|**int**|ID of the filegroup.|  
|LogGroupGUID|**uniqueidentifier** NULL|NULL.|  
|DifferentialBaseLSN|**numeric(25,0)** NULL|For differential backups, changes with log sequence numbers greater than or equal to **DifferentialBaseLSN** are included in the differential.<br /><br /> For other backup types, the value is NULL.|  
|DifferentialBaseGUID|**uniqueidentifier** NULL|For differential backups, the unique identifier of the differential base.<br /><br /> For other backup types, the value is NULL.|  
|IsReadOnly|**bit**|**1** = The file is read-only.|  
|IsPresent|**bit**|**1** = The file is present in the backup.|  
|TDEThumbprint|**varbinary(32)** NULL|Shows the thumbprint of the Database Encryption Key. The encryptor thumbprint is a SHA-1 hash of the certificate with which the key is encrypted. For information about database encryption, see [Transparent Data Encryption &#40;TDE&#41;](../../relational-databases/security/encryption/transparent-data-encryption.md).|  
|SnapshotURL|**nvarchar(360)** NULL|The URL for the Azure snapshot of the database file contained in the FILE_SNAPSHOT backup. Returns NULL if no FILE_SNAPSHOT backup.|  
  
## Security  
 A backup operation may optionally specify passwords for a media set, a backup set, or both. When a password has been defined on a media set or backup set, you must specify the correct password or passwords in the RESTORE statement. These passwords prevent unauthorized restore operations and unauthorized appends of backup sets to media using [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools. However, a password does not prevent overwrite of media using the BACKUP statement's FORMAT option.  
  
> [!IMPORTANT]  
>  The protection provided by this password is weak. It is intended to prevent an incorrect restore using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools by authorized or unauthorized users. It does not prevent the reading of the backup data by other means or the replacement of the password. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] The best practice for protecting backups is to store backup tapes in a secure location or back up to disk files that are protected by adequate access control lists (ACLs). The ACLs should be set on the directory root under which backups are created.  
  
### Permissions  
 Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], obtaining information about a backup set or backup device requires CREATE DATABASE permission. For more information, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
  
## Examples  
 The following example returns the information from a backup device named `AdventureWorksBackups`. The example uses the `FILE` option to specify the second backup set on the device.  
  
```  
RESTORE FILELISTONLY FROM AdventureWorksBackups   
   WITH FILE=2;  
GO  
```  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
  
