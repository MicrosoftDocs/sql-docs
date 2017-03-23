---
title: "sys.type_assembly_usages (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.type_assembly_usages"
  - "sys.type_assembly_usages_TSQL"
  - "type_assembly_usages_TSQL"
  - "type_assembly_usages"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.type_assembly_usages catalog view"
ms.assetid: 79b8bf25-6e4e-4a07-ae93-7a4e44f65171
caps.latest.revision: 19
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sys.type_assembly_usages (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains one row per type to assembly reference.  
  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**user_type_id**|**int**|ID of the type<br /><br /> To return the name of the type, join to the [sys.types](../../relational-databases/system-catalog-views/sys-types-transact-sql.md) catalog view on this column.|  
|**assembly_id**|**int**|ID of the assembly|  
  
## Permissions  
 Requires membership in the **public** role. For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Scalar Types Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/scalar-types-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
