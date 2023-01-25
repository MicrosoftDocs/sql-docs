---
title: "sys.backup_devices (Transact-SQL)"
description: sys.backup_devices (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "backup_devices_TSQL"
  - "backup_devices"
  - "sys.backup_devices"
  - "sys.backup_devices_TSQL"
helpviewer_keywords:
  - "backup devices [SQL Server], viewing information"
  - "sys.backup_devices catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 457edaa4-aca1-4bd3-bf8d-734490b80fcd
---
# sys.backup_devices (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains a row for each backup-device registered by using **sp_addumpdevice** or created in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Name of the backup device. Is unique in the set.|  
|**type**|**tinyint**|Type of backup device:<br /><br /> 2 = Disk<br /><br /> 3 = Diskette (obsolete)<br /><br /> 5 = Tape<br /><br /> 6 = Pipe (obsolete)<br /><br /> 7 = Virtual device (for optional use by third-party backup vendors)<br /><br /> 9 = URL<br /><br />Typically, only disk (2) and URL (9) are used.|  
|**type_desc**|**nvarchar(60)**|Description of backup device type:<br /><br /> DISK<br /><br /> DISKETTE (obsolete)<br /><br /> TAPE<br /><br /> PIPE (obsolete)<br /><br /> VIRTUAL_DEVICE (for optional use by third party backup vendors)<br /><br /> URL <br /><br /> Typically, only DISK and URL are used.|  
|**physical_name**|**nvarchar(260)**|Physical file name or path of the backup device.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)   
 [Databases and Files Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/databases-and-files-catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.yml)  
  
  
