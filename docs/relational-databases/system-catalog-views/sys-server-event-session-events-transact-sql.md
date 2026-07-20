---
title: "sys.server_event_session_events (Transact-SQL)"
description: The sys.server_event_session_events catalog view returns a row for each event in an event session.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/23/2025
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
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current "
---
# sys.server_event_session_events (Transact-SQL)

[!INCLUDE [sqlserver-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

  Returns a row for each event in an event session.    

|Column name|Data type|Description|    
|-----------------|---------------|-----------------|    
| `event_session_id` |**int**|The ID of the event session. Is not nullable.|    
| `event_id` |**int**|The ID of the event. This ID is unique within an event session object. Is not nullable.|    
| `name` |**sysname**|The name of the event. Is not nullable.|    
| `package` |**sysname**|The name of the event package that contains the event. Is not nullable.|    
| `module` |**sysname**|The name of the module that contains the event. Is not nullable.|    
| `predicate` |**nvarchar(3000)**|The predicate expression that is applied to the event. Is nullable.|    
| `predicate_xml` |**nvarchar(3000)**|The XML predicate expression that is applied to the event. Is nullable.|    

## Permissions

 [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

 [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

 This view has the following relationship cardinalities.    

| From | To | Relationship |
| ---- | ---- | ------------ |
| `sys.server_event_session_events.event_session_id` |`sys.server_event_sessions.event_session_id`|Many to one|    

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Extended Events Catalog Views (Transact-SQL)](extended-events-catalog-views-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
