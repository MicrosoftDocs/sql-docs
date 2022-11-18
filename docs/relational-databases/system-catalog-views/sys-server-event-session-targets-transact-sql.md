---
title: "sys.server_event_session_targets (Transact-SQL)"
description: sys.server_event_session_targets (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_event_session_targets_TSQL"
  - "sys.server_event_session_targets_TSQL"
  - "sys.server_event_session_targets"
  - "server_event_session_targets"
helpviewer_keywords:
  - "sys.server_event_session_targets catalog view"
  - "xe"
dev_langs:
  - "TSQL"
ms.assetid: dda4879d-57ae-4267-b410-1ef5c37404c7
---
# sys.server_event_session_targets (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each event target for an event session.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|target_id|**int**|The ID of the target. ID is unique within the event session object. Is not nullable.|  
|name|**sysname**|The name of the event target. Is not nullable.|  
|package|**sysname**|The name of the event package that contains the event target. Is not nullable.|  
|module|**sysname**|The name of the module that contains the event target. Is not nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | -- | ------------ |
|sys.server_event_session_targets.event_session_id|sys.server_event_sessions.event_session_id|Many to one|  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Extended Events Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/extended-events-catalog-views-transact-sql.md)   
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
