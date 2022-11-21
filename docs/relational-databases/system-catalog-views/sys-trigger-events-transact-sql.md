---
title: "sys.trigger_events (Transact-SQL)"
description: sys.trigger_events (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "trigger_events_TSQL"
  - "trigger_events"
  - "sys.trigger_events"
  - "sys.trigger_events_TSQL"
helpviewer_keywords:
  - "sys.trigger_events catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 92540447-131c-491c-b033-c064c7d950e1
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.trigger_events (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Contains a row per event for which a trigger fires.  
  
> [!NOTE]  
>  **sys.trigger_events** does not apply to event notifications.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**\<Columns inherited from sys.events>**|Not applicable|Inherits the **object_id**, **type**, **type_desc** columns from [sys.events](../../relational-databases/system-catalog-views/sys-events-transact-sql.md).|  
|**is_first**|**bit**|Trigger is marked to be the first to fire for this event.|  
|**is_last**|**bit**|Trigger is marked to be the last to fire for this event.|  
|**event_group_type**|**int**|Event group on which the trigger is created, or null if not created on an event group.|  
|**event_group_type_desc**|**nvarchar(60)**|Description of the event group on which the trigger is created, or null if not created on an event group.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)  
  
  
