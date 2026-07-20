---
title: "sys.server_event_session_fields (Transact-SQL)"
description: The sys.server_event_session_fields catalog view returns a row for each customizable column that was explicitly set on events and targets.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/23/2025
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
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current "
---
# sys.server_event_session_fields (Transact-SQL)

[!INCLUDE [sqlserver-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns a row for each customizable column that was explicitly set on events and targets.

|Column name|Data type|Description|    
|-----------------|---------------|-----------------|    
| `event_session_id` |**int**|The ID of the event session. Is not nullable.|    
| `object_id` |**int**|The ID of the object this field is associated with. Is not nullable.|    
| `name` |**sysname**|The name of the field. Is not nullable.|    
| `value` |**sql_variant**|The value of the field. Is not nullable.|    

## Permissions

 [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

 [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

 This view has the following relationship cardinalities.    

| From | To | Relationship |
| ---- | ---- | ------------ |
| `sys.server_event_session_actions.event_session_id` |`sys.server_event_sessions.event_session_id`|Many to one|    
| `sys.server_event_session_actions.event_id`<br /><br /> `sys.server_event_session_actions.object_id`<br /><br /> `sys.server_event_session_actions.event_session_id` |`sys.server_event_session_events.event_session_id`<br /><br /> `sys.server_event_session_events.event_id`|Many to one|    
| `sys.server_event_session_actions.event_session_id`<br /><br /> `sys.server_event_session_actions.object_id` |`sys.server_event_session_targets.event_session_id`<br /><br /> `sys.server_event_session_targets.target_id`|Many to one|    

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Extended Events Catalog Views (Transact-SQL)](extended-events-catalog-views-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
