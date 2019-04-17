---
title: "sys.module_assembly_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "module_assembly_usages_TSQL"
  - "module_assembly_usages"
  - "sys.module_assembly_usages_TSQL"
  - "sys.module_assembly_usages"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.module_assembly_usages catalog view"
ms.assetid: b0f9ffab-6ac7-49d5-8369-477fa6b1c02b
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.module_assembly_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
