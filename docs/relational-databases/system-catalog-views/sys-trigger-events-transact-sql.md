---
title: "sys.trigger_events (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "trigger_events_TSQL"
  - "trigger_events"
  - "sys.trigger_events"
  - "sys.trigger_events_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.trigger_events catalog view"
ms.assetid: 92540447-131c-491c-b033-c064c7d950e1
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.trigger_events (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

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
  
  
