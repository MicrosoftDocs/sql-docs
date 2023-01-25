---
description: "sys.sysindexkeys (Transact-SQL)"
title: "sys.sysindexkeys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sysindexkeys"
  - "sys.sysindexkeys_TSQL"
  - "sysindexkeys_TSQL"
  - "sys.sysindexkeys"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysindexkeys system table"
  - "sys.sysindexkeys compatibility view"
ms.assetid: 53a33c8d-e5f0-430d-a712-b65f43d64318
author: rwestMSFT
ms.author: randolphwest
---
# sys.sysindexkeys (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains information about the keys or columns in an index of the database.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssnoteCompView](../../includes/ssnotecompview-md.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|ID of the table.|  
|**indid**|**smallint**|ID of the index.|  
|**colid**|**smallint**|ID of the column.|  
|**keyno**|**smallint**|Position of the column in the index.|  
  
## See Also  
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)   
 [Compatibility Views &#40;Transact-SQL&#41;](~/relational-databases/system-compatibility-views/system-compatibility-views-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
