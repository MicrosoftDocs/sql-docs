---
title: "sys.server_trigger_events (Transact-SQL)"
description: sys.server_trigger_events (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.server_trigger_events_TSQL"
  - "server_trigger_events_TSQL"
  - "sys.server_trigger_events"
  - "server_trigger_events"
helpviewer_keywords:
  - "sys.server_trigger_events catalog view"
dev_langs:
  - "TSQL"
ms.assetid: be7d8a59-3c00-4f1b-b4b0-3dcd5572e002
---
# sys.server_trigger_events (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Contains one row for each event for which a server-level (synchronous) trigger fires.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**inherited columns**||Inherits all columns from [sys.server_events](../../relational-databases/system-catalog-views/sys-server-events-transact-sql.md).|  
|**is_first**|**bit**|Trigger is marked to be the first to fire for this event.|  
|**is_last**|**bit**|Trigger is marked to be the last to fire for this event.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
