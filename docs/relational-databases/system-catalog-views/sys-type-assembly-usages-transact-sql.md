---
title: "sys.type_assembly_usages (Transact-SQL)"
description: sys.type_assembly_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.type_assembly_usages"
  - "sys.type_assembly_usages_TSQL"
  - "type_assembly_usages_TSQL"
  - "type_assembly_usages"
helpviewer_keywords:
  - "sys.type_assembly_usages catalog view"
dev_langs:
  - "TSQL"
---
# sys.type_assembly_usages (Transact-SQL)
[!INCLUDE[SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

  Contains one row per type to assembly reference.  
  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**user_type_id**|**int**|ID of the type<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|**assembly_id**|**int**|ID of the assembly|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## Related content

- [Scalar Types Catalog Views (Transact-SQL)](scalar-types-catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
