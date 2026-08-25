---
title: "sys.dm_xe_map_values (Transact-SQL)"
description: sys.dm_xe_map_values (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/23/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_xe_map_values"
  - "dm_xe_map_values"
  - "dm_xe_map_values_TSQL"
  - "sys.dm_xe_map_values_TSQL"
helpviewer_keywords:
  - "sys.dm_xe_map_values dynamic management view"
  - "xe"
dev_langs:
  - "TSQL"
---
# sys.dm_xe_map_values (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL DB Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Returns a mapping of internal numeric keys to human-readable text.    

|Column name|Data type|Description|    
|-----------------|---------------|-----------------|    
| `name` |**nvarchar(256)**|The name of the map. name is unique across the local system. Is not nullable.|    
| `object_package_guid` |**uniqueidentifier**|The GUID of the package that contains the map. Is not nullable.|    
| `map_key` |**int**|The internal key value. Is not nullable.|    
| `map_value` |**nvarchar(3072)**|A description of the key value. Is not nullable.|    

## Permissions

Requires `VIEW SERVER STATE` permission on the server.    

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

### Relationship cardinalities

|From|To|Relationship|    
|----------|--------|------------------|    
| `dm_xe_map_values.object_package_guid<br /><br /> dm_xe_map_values.name` |sys.dm_xe_objects.package_guid<br /><br /> sys.dm_xe_objects.name|Many-to-one|  

## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
- [Quickstart: Extended Events](../extended-events/quick-start-extended-events-in-sql-server.md)
- [Extended Events in Azure SQL](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file)
