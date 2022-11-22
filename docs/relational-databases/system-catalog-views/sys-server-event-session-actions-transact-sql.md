---
title: "sys.server_event_session_actions (Transact-SQL)"
description: sys.server_event_session_actions (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.server_event_session_actions"
  - "server_event_session_actions_TSQL"
  - "server_event_session_actions"
  - "sys.server_event_session_actions_TSQL"
helpviewer_keywords:
  - "sys.server_event_session_actions catalog view"
  - "xe"
dev_langs:
  - "TSQL"
ms.assetid: 1d8c604e-4361-4846-8661-14cfd1c44f63
---
# sys.server_event_session_actions (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each action on each event of an event session.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|event_id|**int**|The ID of the event. This ID is unique within the event session object. Is not nullable.|  
|name|**sysname**|The name of the action. Is nullable.|  
|package|**sysname**|The name of the event package that contains the event. Is nullable.|  
|module|**sysname**|The name of the module that contains the event. Is nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | -- | ------------ |
|sys.server_event_session_actions.event_session_id|sys.sys.server_event_sessions.event_session_id|Many to one|  
|sys.server_event_session_actions.event_id<br /><br /> sys.server_event_session_actions.event_session_id|sys.server_event_session_events.event_session_id<br /><br /> sys.server_event_session_events.event_id|Many to one|  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Extended Events Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)   
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
