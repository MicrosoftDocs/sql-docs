---
title: "sys.trigger_event_types (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "trigger_event_types_TSQL"
  - "sys.trigger_event_types_TSQL"
  - "sys.trigger_event_types"
  - "trigger_event_types"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.trigger_event_types catalog view"
ms.assetid: 054aed54-7151-4760-934a-149fa434f1ae
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.trigger_event_types (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row for each event or event group on which a trigger can fire.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**type**|**int**|Type of event or event group that causes a trigger to fire.|  
|**type_name**|**nvarchar(64)**|Name of an event or event group. This can be specified in the FOR clause of a [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md) statement.|  
|**parent_type**|**int**|Type of event group that is the parent of the event or event group.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
