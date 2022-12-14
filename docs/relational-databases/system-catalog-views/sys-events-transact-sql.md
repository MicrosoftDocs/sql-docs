---
title: "sys.events (Transact-SQL)"
description: sys.events (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.events_TSQL"
  - "sys.events"
  - "events_TSQL"
  - "events"
helpviewer_keywords:
  - "sys.events catalog view"
dev_langs:
  - "TSQL"
ms.assetid: f245a97a-80fc-43fb-a6e4-139420c9a47a
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.events (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Contains a row for each event for which a trigger or event notification fires. These events represent the event types that are specified when the trigger or event notification is created by using [CREATE TRIGGER](../../t-sql/statements/create-trigger-transact-sql.md) or [CREATE EVENT NOTIFICATION](../../t-sql/statements/create-event-notification-transact-sql.md).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the trigger or event notification. This value, together with **type**, uniquely identifies the row.|  
|**type**|**int**|Event that causes the trigger to fire.|  
|**type_desc**|**nvarchar(60)**|Description of the event that causes the trigger to fire.|  
|**is_trigger_event**|**bit**|1 = Trigger event.<br /><br /> 0 = Notification event.|  
|**event_group_type**|**int**|Event group on which the trigger or event notification is created, or null if not created on an event group.|  
|**event_group_type_desc**|**nvarchar(60)**|Description of the event group on which the trigger or event notification is created, or null if not created on an event group.|  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
