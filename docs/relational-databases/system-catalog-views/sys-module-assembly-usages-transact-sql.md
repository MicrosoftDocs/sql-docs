---
title: "sys.module_assembly_usages (Transact-SQL)"
description: sys.module_assembly_usages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "module_assembly_usages_TSQL"
  - "module_assembly_usages"
  - "sys.module_assembly_usages_TSQL"
  - "sys.module_assembly_usages"
helpviewer_keywords:
  - "sys.module_assembly_usages catalog view"
dev_langs:
  - "TSQL"
ms.assetid: b0f9ffab-6ac7-49d5-8369-477fa6b1c02b
---
# sys.module_assembly_usages (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each module-to-assembly reference.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|Object identification number of the SQL object. Is unique within a database.|  
|**assembly_id**|**int**|ID of the assembly from which this module was created.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
