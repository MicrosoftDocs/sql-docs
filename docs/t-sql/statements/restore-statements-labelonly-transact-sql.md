---
title: "RESTORE LABELONLY (Transact-SQL)"
description: RESTORE Statements - LABELONLY (Transact-SQL)
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/30/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "LABELONLY"
  - "RESTORE_LABELONLY_TSQL"
  - "LABELONLY_TSQL"
  - "RESTORE LABELONLY"
helpviewer_keywords:
  - "RESTORE LABELONLY statement"
  - "backup media [SQL Server], content information"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016||>=sql-server-linux-2017"
---
# RESTORE Statements - LABELONLY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdbmi-xxxx-xxx-md.md)]
  Returns a result set containing information about the backup media identified by the given backup device.  
  
> [!NOTE]  
>  For the descriptions of the arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
RESTORE LABELONLY   
FROM <backup_device>   
[ WITH   
 {  
--Media Set Options  
   MEDIANAME = { media_name | @media_name_variable }   
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
   | { DISK | TAPE | URL } = { 'physical_backup_device_name' |  
       @physical_backup_device_name_var }   
}  
  
```  
> [!NOTE] 
> URL is the format used to specify the location and the file name for  Microsoft Azure Blob Storage and is supported starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 CU2. Although Microsoft Azure storage is a service, the implementation is similar to disk and tape to allow for a consistent and seamless restore experience for all the three devices.
  
## Arguments  
 For descriptions of the RESTORE LABELONLY arguments, see [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md).  
  
## Result Sets  
 The result set from RESTORE LABELONLY consists of a single row with this information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**MediaName**|**nvarchar(128)**|Name of the media.|  
|**MediaSetId**|**uniqueidentifier**|Unique identification number of the media set.|  
|**FamilyCount**|**int**|Number of media families in the media set.|  
|**FamilySequenceNumber**|**int**|Sequence number of this family.|  
|**MediaFamilyId**|**uniqueidentifier**|Unique identification number for the media family.|  
|**MediaSequenceNumber**|**int**|Sequence number of this media in the media family.|  
|**MediaLabelPresent**|**tinyint**|Whether the media description contains:<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Tape Format media label<br /><br /> **0** = Media description|  
|**MediaDescription**|**nvarchar(255)**|Media description, in free-form text, or the Tape Format media label.|  
|**SoftwareName**|**nvarchar(128)**|Name of the backup software that wrote the label.|  
|**SoftwareVendorId**|**int**|Unique vendor identification number of the software vendor that wrote the backup.|  
|**MediaDate**|**datetime**|Date and time the label was written.|  
|**Mirror_Count**|**int**|Number of mirrors in the set (1-4).<br /><br /> Note: The labels written for different mirrors in a set are identical.|  
|**IsCompressed**|**bit**|Whether the backup is compressed:<br /><br /> 0 = not compressed<br /><br /> 1 =compressed|  
  
> [!NOTE]  
>  If passwords are defined for the media set, RESTORE LABELONLY returns information only if the correct media password is specified in the MEDIAPASSWORD option of the command.  
  
## General Remarks  
 Executing RESTORE LABELONLY is a quick way to find out what the backup media contains. Because RESTORE LABELONLY reads only the media header, this statement finishes quickly even when using high-capacity tape devices.  
  
## Security  
 A backup operation may optionally specify passwords for a media set. When a password has been defined on a media set, you must specify the correct password in the RESTORE statement. The password prevents unauthorized restore operations and unauthorized appends of backup sets to media using [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools. However, a password does not prevent overwrite of media using the BACKUP statement's FORMAT option.  
  
> [!IMPORTANT]  
>  The protection provided by this password is weak. It is intended to prevent an incorrect restore using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools by authorized or unauthorized users. It does not prevent the reading of the backup data by other means or the replacement of the password. [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]The best practice for protecting backups is to store backup tapes in a secure location or back up to disk files that are protected by adequate access control lists (ACLs). The ACLs should be set on the directory root under which backups are created.  
  
### Permissions  
 In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, obtaining information about a backup set or backup device requires CREATE DATABASE permission. For more information, see [GRANT Database Permissions &#40;Transact-SQL&#41;](../../t-sql/statements/grant-database-permissions-transact-sql.md).  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md)   
 [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
  
