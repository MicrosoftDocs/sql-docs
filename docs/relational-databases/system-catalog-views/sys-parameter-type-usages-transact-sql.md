---
title: "sys.parameter_type_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.parameter_type_usages"
  - "sys.parameter_type_usages_TSQL"
  - "parameter_type_usages_TSQL"
  - "parameter_type_usages"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.parameter_type_usages catalog view"
ms.assetid: af0e167b-bffb-4525-84ec-3607f9268d3d
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.parameter_type_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns one row for each parameter that is of user-defined type.  
  
> [!NOTE]  
>  This view does not return rows for parameters of numbered procedures.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object to which this parameter belongs.|  
|**parameter_id**|**int**|ID of the parameter. Is unique within the object.|  
|**user_type_id**|**int**|ID of the user-defined type.<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Scalar Types Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/scalar-types-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
