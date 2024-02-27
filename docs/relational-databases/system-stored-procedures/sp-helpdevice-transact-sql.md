---
title: "sp_helpdevice (Transact-SQL)"
description: "sp_helpdevice (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpdevice"
  - "sp_helpdevice_TSQL"
helpviewer_keywords:
  - "sp_helpdevice"
dev_langs:
  - "TSQL"
---
# sp_helpdevice (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Reports information about [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backup devices.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] We recommend that you use the [sys.backup_devices](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md) catalog view instead  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_helpdevice [ [ @devname = ] 'name' ]  
```  
  
## Arguments  
`[ @devname = ] 'name'`
 Is the name of the backup device for which information is reported. The value of *name* is always **sysname**.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**device_name**|**sysname**|Logical device name.|  
|**physical_name**|**nvarchar(260)**|Physical file name.|  
|**description**|**nvarchar(255)**|Description of the device.|  
|**status**|**int**|A number that corresponds to the status description in the **description** column.|  
|**cntrltype**|**smallint**|Controller type of the device:<br /><br /> 2 = Disk device<br /><br /> 5 = Tape device|  
|**size**|**int**|Device size in 2-KB pages.|  
  
## Remarks  
 If *name* is specified, **sp_helpdevice** displays information about the specified dump device. If *name* is not specified, **sp_helpdevice** displays information about all dump devices in the **sys.backup_devices** catalog view.  
  
 Dump devices are added to the system by using **sp_addumpdevice**.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## Examples  
 The following example reports information about all dump devices on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
```  
EXEC sp_helpdevice;  
```  
  
## See Also  
 [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)   
 [sp_dropdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdevice-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
