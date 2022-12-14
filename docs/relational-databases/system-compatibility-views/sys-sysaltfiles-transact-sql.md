---
description: "sys.sysaltfiles (Transact-SQL)"
title: "sys.sysaltfiles (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sys.sysaltfiles_TSQL"
  - "sys.sysaltfiles"
  - "sysaltfiles_TSQL"
  - "sysaltfiles"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysaltfiles system table"
  - "sys.sysaltfiles compatibility view"
ms.assetid: 698dec23-5336-4108-87a5-f8e407f8da09
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysaltfiles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Under special circumstances, contains rows corresponding to the files in a database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**fileid**|**smallint**|File identification number. This is unique for each database.|  
|**groupid**|**smallint**|File group identification number.|  
|**size**|**int**|File size, in 8-kilobyte (KB) pages.|  
|**maxsize**|**int**|Maximum file size, in 8-KB pages.<br /><br /> 0 = No growth.<br /><br /> -1 = File will grow until the disk is full.<br /><br /> 268435456 = Log file will grow to a maximum size of 2 TB.<br /><br /> Note: Databases that are upgraded with an unlimited log file size will report -1 for the maximum size of the log file.|  
|**growth**|**int**|Growth size of the database.<br /><br /> 0 = No growth. Can be either the number of pages or the percentage of file size, depending on the value of status. If **status** is 0x100000, **growth** is the percentage of file size; otherwise, it is the number of pages.|  
|**status**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**perf**|**int**|Reserved.|  
|**dbid**|**smallint**|Database identification number of the database to which this file belongs.|  
|**name**|**sysname**|Logical name of the file.|  
|**filename**|**nvarchar(260)**|Name of the physical device. This includes the full path of the file.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
