---
title: "sys.server_event_session_events (Transact-SQL)"
description: sys.server_event_session_events (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_event_session_events"
  - "server_event_session_events_TSQL"
  - "sys.server_event_session_events"
  - "sys.server_event_session_events_TSQL"
helpviewer_keywords:
  - "sys.server_event_session_events catalog view"
  - "xe"
dev_langs:
  - "TSQL"
ms.assetid: 75986e91-1fc7-4f14-98ac-4e90154a74db
---
# sys.server_event_session_events (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each event in an event session.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|event_id|**int**|The ID of the event. This ID is unique within an event session object. Is not nullable.|  
|name|**sysname**|The name of the event. Is not nullable.|  
|package|**sysname**|The name of the event package that contains the event. Is not nullable.|  
|module|**sysname**|The name of the module that contains the event. Is not nullable.|  
|predicate|**nvarchar(3000)**|The predicate expression that is applied to the event. Is nullable.|  
|predicate_xml|**nvarchar(3000)**|The XML predicate expression that is applied to the event. Is nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | -- | ------------ |
|sys.server_event_session_events.event_session_id|sys.server_event_sessions.event_session_id|Many to one|  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Extended Events Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)   
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
