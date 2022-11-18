---
title: "sys.dm_xe_packages (Transact-SQL)"
description: sys.dm_xe_packages (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "3/30/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
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
ms.assetid: 2e5ecbe9-3ea8-45e6-a161-e31671a03e1d
---
# sys.dm_xe_packages (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL DB Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

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
  
## Remarks  

The packages registered with the extended events engine expose events, the actions that can be taken at the time of event firing, and targets for both synchronous and asynchronous processing of event data.  
  
These packages can be dynamically loaded into a process address space. At the time the package is loaded, it registers all the objects it exposes with the extended events engine.  
  
## Relationship cardinalities  
  
| From | To | Relationship |
| ---- | -- | ------------ |  
|sys.dm_xe_packages.module_address|sys.dm_os_loaded_modules.base_address|Many to one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
