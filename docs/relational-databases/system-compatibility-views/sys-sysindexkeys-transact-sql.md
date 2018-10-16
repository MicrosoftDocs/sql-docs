---
title: "sys.sysindexkeys (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
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
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# sys.sysindexkeys (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
