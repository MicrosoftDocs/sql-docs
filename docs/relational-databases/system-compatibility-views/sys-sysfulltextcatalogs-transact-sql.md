---
description: "sys.sysfulltextcatalogs (Transact-SQL)"
title: "sys.sysfulltextcatalogs (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysfulltextcatalogs"
  - "sys.sysfulltextcatalogs_TSQL"
  - "sysfulltextcatalogs_TSQL"
  - "sys.sysfulltextcatalogs"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sysfulltextcatalogs compatibility view"
  - "sysfulltextcatalogs system table"
ms.assetid: 18ac6ad5-01e8-428f-8422-a9ca29626977
author: rwestMSFT
ms.author: randolphwest
monikerRange: ">=aps-pdw-2016||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.sysfulltextcatalogs (Transact-SQL)
[!INCLUDE [sql-asdbmi-pdw](../../includes/applies-to-version/sql-asdbmi-pdw.md)]

  Contains information about the full-text catalogs.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**ftcatid**|**smallint**|Identifier of the full-text catalog.|  
|**name**|**sysname**|Full-text catalog name specified by the user.|  
|**status**|**smallint**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**path**|**nvarchar(260)**|Root path specified by the user.<br /><br /> NULL = Path was not specified. The default (installation) path was used.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)  
  
  
