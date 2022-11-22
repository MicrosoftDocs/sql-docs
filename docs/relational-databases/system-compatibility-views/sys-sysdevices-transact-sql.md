---
description: "sys.sysdevices (Transact-SQL)"
title: "sys.sysdevices (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysdevices"
  - "sysdevices_TSQL"
  - "sys.sysdevices"
  - "sys.sysdevices_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sysdevices compatibility view"
  - "sysdevices system table"
ms.assetid: ac5bcaf4-8fb6-4855-8856-d7643f469361
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysdevices (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each disk backup file, tape backup file, and database file.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**name**|**sysname**|Logical name of the backup file or database file.|  
|**size**|**int**|Size of the file in 2-kilobyte (KB) pages.|  
|**low**|**int**|Maintained for backward compatibility only.|  
|**high**|**int**|Maintained for backward compatibility only.|  
|**status**|**smallint**|Bitmap indicating the type of device:<br /><br /> 1 = Default disk<br /><br /> 2 = Physical disk<br /><br /> 4 = Logical disk<br /><br /> 8 = Skip header<br /><br /> 16 = Backup file<br /><br /> 32 = Serial writes<br /><br /> 4096 = Read-only|  
|**cntrltype**|**smallint**|Controller type:<br /><br /> 0 = Non-CD-ROM database file<br /><br /> 2 = Disk backup file<br /><br /> 3 - 4 = Diskette backup file<br /><br /> 5 = Tape backup file<br /><br /> 6 = Named-pipe file|  
|**phyname**|**nvarchar(260)**|Name of the physical file.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
