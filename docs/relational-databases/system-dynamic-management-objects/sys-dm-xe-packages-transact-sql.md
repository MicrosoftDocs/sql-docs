---
title: "sys.dm_xe_packages (Transact-SQL)"
description: sys.dm_xe_packages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/27/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "dm_xe_packages_TSQL"
  - "sys.dm_xe_packages_TSQL"
  - "dm_xe_packages"
  - "sys.dm_xe_packages"
helpviewer_keywords:
  - "sys.dm_xe_packages dynamic management view"
  - "extended events [SQL Server], views"
dev_langs:
  - "TSQL"
---
# sys.dm_xe_packages (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL DB Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Lists all the packages registered with the extended events engine.  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**nvarchar(256)**|The name of package. The description is exposed from the package itself. Is not nullable.|  
|guid|**uniqueidentifier**|The GUID that identifies the package. Is not nullable.|  
|description|**nvarchar(3072)**|The package description. description is set by the package author and is not nullable.|  
|capabilities|**int**|Bitmap describing the capabilities of this package. Is nullable.|  
|capabilities_desc|**nvarchar(256)**|A list of all the capabilities possible for this package. Is nullable.|  
|module_guid|**nvarchar(60)**|The GUID of the module that exposes this package. Is not nullable.|  
|module_address|**varbinary(8)**|The base address where the module containing the package is loaded. A single module may expose several packages. Is not nullable.|  
  
## Permissions  
Requires VIEW SERVER STATE permission on the server.  
  
### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Remarks  

The packages registered with the extended events engine expose events, the actions that can be taken at the time of event firing, and targets for both synchronous and asynchronous processing of event data.  
  
These packages can be dynamically loaded into a process address space. At the time the package is loaded, it registers all the objects it exposes with the extended events engine.  
  
## Relationship cardinalities  
  
| From | To | Relationship |
| ---- | -- | ------------ |  
|sys.dm_xe_packages.module_address|sys.dm_os_loaded_modules.base_address|Many to one|  
  
## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
- [Quickstart: Extended Events](../extended-events/quick-start-extended-events-in-sql-server.md)
- [Extended Events in Azure SQL](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file)
