---
title: "sp_dropdevice (Transact-SQL)"
description: "sp_dropdevice (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropdevice_TSQL"
  - "sp_dropdevice"
helpviewer_keywords:
  - "backup devices [SQL Server], deleting"
  - "sp_dropdevice"
dev_langs:
  - "TSQL"
---
# sp_dropdevice (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Drops a database device or backup device from an instance of the [!INCLUDE[ssdenoversion-md](../../includes/ssdenoversion-md.md)], deleting the entry from **master.dbo.sysdevices**.  
   
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dropdevice [ @logicalname = ] 'device'   
    [ , [ @delfile = ] 'delfile' ]  
```  
  
## Arguments  
`[ @logicalname = ] 'device'`
 Is the logical name of the database device or backup device as listed in **master.dbo.sysdevices.name**. *device* is **sysname**, with no default.  
  
`[ @delfile = ] 'delfile'`
 Specifies whether the physical backup device file should be deleted. *delfile* is **varchar(7)**. If specified as **DELFILE**, the physical backup device disk file is deleted.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 **sp_dropdevice** cannot be used inside a transaction.  
  
## Permissions  
 Requires membership in the **diskadmin** fixed server role.  
  
## Examples  
 The following example drops the `tapedump1` tape dump device from the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
```  
EXEC sp_dropdevice 'tapedump1';  
```  
  
## See also  
 [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 [Delete a Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/delete-a-backup-device-sql-server.md)   
 [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)   
 [sp_helpdb &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdb-transact-sql.md)   
 [sp_helpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helpdevice-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
