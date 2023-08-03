---
title: "sys.sysfilegroups (Transact-SQL)"
description: "sys.sysfilegroups (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysfilegroups_TSQL"
  - "sys.sysfilegroups"
  - "sysfilegroups"
  - "sys.sysfilegroups_TSQL"
helpviewer_keywords:
  - "sysfilegroups system table"
  - "sys.sysfilegroups compatibility view"
dev_langs:
  - "TSQL"
---
# sys.sysfilegroups (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each file group in a database. There is at least one entry in this table that is for the primary file group.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**groupid**|**smallint**|Group identification number unique for each database.|  
|**allocpolicy**|**smallint**|Reserved|  
|**status**|**int**|0x8 = Read-only<br /><br /> 0x10 = Default|  
|**groupname**|**sysname**|Name of the file group.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
