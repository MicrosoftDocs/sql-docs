---
title: "sys.server_event_session_fields (Transact-SQL)"
description: sys.server_event_session_fields (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_event_session_fields"
  - "server_event_session_fields_TSQL"
  - "sys.server_event_session_fields"
  - "sys.server_event_session_fields_TSQL"
helpviewer_keywords:
  - "sys.server_event_session_fields catalog view"
  - "xe"
dev_langs:
  - "TSQL"
ms.assetid: 7109f9fb-8a1f-432c-92d1-6f8af3e96af1
---
# sys.server_event_session_fields (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each customizable column that was explicitly set on events and targets.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|object_id|**int**|The ID of the object this field is associated with. Is not nullable.|  
|name|**sysname**|The name of the field. Is not nullable.|  
|value|**sql_variant**|The value of the field. Is not nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | -- | ------------ |
|sys.server_event_session_actions.event_session_id|sys.server_event_sessions.event_session_id|Many to one|  
|sys.server_event_session_actions.event_id<br /><br /> sys.server_event_session_actions.object_id<br /><br /> sys.server_event_session_actions.event_session_id|sys.server_event_session_events.event_session_id<br /><br /> sys.server_event_session_events.event_id|Many to one|  
|sys.server_event_session_actions.event_session_id<br /><br /> sys.server_event_session_actions.object_id|sys.server_event_session_targets.event_session_id<br /><br /> sys.server_event_session_targets.target_id|Many to one|  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Extended Events Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)   
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
