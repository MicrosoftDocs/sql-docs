---
title: "RESTORE VERIFYONLY (Transact-SQL)"
description: RESTORE Statements - VERIFYONLY (Transact-SQL)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/30/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "VERIFYONLY"
  - "RESTORE VERIFYONLY"
  - "VERIFYONLY_TSQL"
  - "RESTORE_VERIFYONLY_TSQL"
helpviewer_keywords:
  - "RESTORE VERIFYONLY statement"
  - "backups [SQL Server], verifying"
  - "verifying backups"
  - "checking backups"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# RESTORE Statements - VERIFYONLY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]

  Verifies the backup but does not restore it, and checks to see that the backup set is complete and the entire backup is readable. However, RESTORE VERIFYONLY does not attempt to verify the structure of the data contained in the backup volumes. In [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], RESTORE VERIFYONLY has been enhanced to do additional checking on the data to increase the probability of detecting errors. The goal is to be as close to an actual restore operation as practical. For more information, see the Remarks.  

 If the backup is valid, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] returns a success message.  
  
> [!NOTE]  
>  For the descriptions of the arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
RESTORE VERIFYONLY  
FROM <backup_device> [ ,...n ]  
[ WITH    
 {  
   LOADHISTORY   
  
--Restore Operation Option  
 | MOVE 'logical_file_name_in_backup' TO 'operating_system_file_name'   
          [ ,...n ]   
  
--Backup Set Options  
 | FILE = { backup_set_file_number | @backup_set_file_number }   
 | PASSWORD = { password | @password_variable }   
  
--Media Set Options  
 | MEDIANAME = { media_name | @media_name_variable }   
 | MEDIAPASSWORD = { mediapassword | @mediapassword_variable }  
  
--Error Management Options  
 | { CHECKSUM | NO_CHECKSUM }   
 | { STOP_ON_ERROR | CONTINUE_AFTER_ERROR }  
  
--Monitoring Options  
 | STATS [ = percentage ]   
  
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
   | { DISK | TAPE | URL } = { 'physical_backup_device_name' |  
       @physical_backup_device_name_var }   
}  
  
```  
 > [!NOTE] 
> URL is the format used to specify the location and the file name for  Microsoft Azure Blob Storage and is supported starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2. Although Microsoft Azure storage is a service, the implementation is similar to disk and tape to allow for a consistent and seamless restore experience for all the three devices.
 
## Arguments  
 For descriptions of the RESTORE VERIFYONLY arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
## General Remarks  
 The media set or the backup set must contain minimal correct information to enable it to be interpreted as Microsoft Tape Format. If not, RESTORE VERIFYONLY stops and indicates that the format of the backup is invalid.  
  
 Checks performed by RESTORE VERIFYONLY include:  
  
-   That the backup set is complete and all volumes are readable.  
  
-   Some header fields of database pages, such as the page ID (as if it were about to write the data).  
  
-   Checksum (if present on the media).  
  
-   Checking for sufficient space on destination devices.  
  
> [!NOTE]  
>  RESTORE VERIFYONLY does not work on a database snapshot. To verify a database snapshot before a revert operation, you can run DBCC CHECKDB.  
  
> [!NOTE]  
>  With snapshot backups, RESTORE VERIFYONLY confirms the existence of the snapshots in the locations specified in the backup file. Snapshot backups are a new feature in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]. For more information about Snapshot Backups, see [File-Snapshot Backups for Database Files in Azure](../../relational-databases/backup-restore/file-snapshot-backups-for-database-files-in-azure.md).  
  
## Security  
 A backup operation may optionally specify passwords for a media set, a backup set, or both. When a password has been defined on a media set or backup set, you must specify the correct password or passwords in the RESTORE statement. These passwords prevent unauthorized restore operations and unauthorized appends of backup sets to media using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools. However, a password does not prevent overwrite of media using the BACKUP statement's FORMAT option.  
  
> [!IMPORTANT]  
>  The protection provided by this password is weak. It is intended to prevent an incorrect restore using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools by authorized or unauthorized users. It does not prevent the reading of the backup data by other means or the replacement of the password. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]The best practice for protecting backups is to store backup tapes in a secure location or back up to disk files that are protected by adequate access control lists (ACLs). The ACLs should be set on the directory root under which backups are created.  
  
### Permissions  
 Beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], obtaining information about a backup set or backup device requires CREATE DATABASE permission. For more information, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
 
## Examples  
 The following example verifies the backup from disk.
  
```sql  
RESTORE VERIFYONLY FROM DISK = 'D:\AdventureWorks.bak';
GO
```  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
  
