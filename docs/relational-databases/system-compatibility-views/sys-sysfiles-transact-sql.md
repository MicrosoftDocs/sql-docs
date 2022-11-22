---
description: "sys.sysfiles (Transact-SQL)"
title: "sys.sysfiles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysfiles"
  - "sys.sysfiles_TSQL"
  - "sys.sysfiles"
  - "sysfiles_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysfiles system table"
  - "sys.sysfiles compatibility view"
ms.assetid: 3b47f38d-1cff-404d-89d3-9342c451c802
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysfiles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each file in a database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**fileid**|**smallint**|File identification number unique for each database.|  
|**groupid**|**smallint**|File group identification number.|  
|**size**|**int**|Size of the file, in 8-KB pages.|  
|**maxsize**|**int**|Maximum file size, in 8-KB pages.<br /><br /> 0 = No growth.<br /><br /> -1 = File will grow until the disk is full.<br /><br /> 268435456 = Log file will grow to a maximum size of 2 TB.<br /><br /> Note: Databases that are upgraded with an unlimited log file size will report -1 for the maximum size of the log file.|  
|**growth**|**int**|Growth size of the database. Can be either the number of pages or the percentage of file size, depending on value of **status**.<br /><br /> 0 = No growth.|  
|**status**|**int**|Status bits for the **growth** value in either megabytes (MB) or kilobytes (KB).<br /><br /> 0x2 = Disk file.<br /><br /> 0x40 = Log file.<br /><br /> 0x100000 = Growth. This value is a percentage and not the number of pages.|  
|**perf**|**int**|Reserved.|  
|**name**|**sysname**|Logical name of the file.|  
|**filename**|**nvarchar(260)**|Name of the physical device. This includes the full path of the file.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
