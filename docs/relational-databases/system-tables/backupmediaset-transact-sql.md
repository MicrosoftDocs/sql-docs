---
title: "backupmediaset (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "backupmediaset"
  - "backupmediaset_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "backup media [SQL Server], backupmediaset system table"
  - "backupmediaset system table"
ms.assetid: d9c18a93-cab9-4db8-ae09-c6bd8145ab8f
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# backupmediaset (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
## See Also  
 [Backup and Restore Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backup-and-restore-tables-transact-sql.md)   
 [backupfile &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfile-transact-sql.md)   
 [backupfilegroup &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupfilegroup-transact-sql.md)   
 [backupmediafamily &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupmediafamily-transact-sql.md)   
 [backupset &#40;Transact-SQL&#41;](../../relational-databases/system-tables/backupset-transact-sql.md)   
 [System Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/system-tables-transact-sql.md)  
  
  
