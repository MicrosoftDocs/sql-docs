---
title: "sys.trigger_event_types (Transact-SQL)"
description: sys.trigger_event_types (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "trigger_event_types_TSQL"
  - "sys.trigger_event_types_TSQL"
  - "sys.trigger_event_types"
  - "trigger_event_types"
helpviewer_keywords:
  - "sys.trigger_event_types catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 054aed54-7151-4760-934a-149fa434f1ae
---
# sys.trigger_event_types (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
