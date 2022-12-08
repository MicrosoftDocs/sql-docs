---
title: "sys.dm_xe_objects (Transact-SQL)"
description: sys.dm_xe_objects (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "3/30/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_xe_objects"
  - "sys.dm_xe_objects"
  - "sys.dm_xe_objects_TSQL"
  - "dm_xe_objects_TSQL"
helpviewer_keywords:
  - "sys.dm_xe_objects dynamic management view"
  - "extended events [SQL Server], views"
dev_langs:
  - "TSQL"
ms.assetid: 5d944b99-b097-491b-8cbd-b0e42b459ec0
---
# sys.dm_xe_objects (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL DB Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a row for each object that is exposed by an event package. Objects can be one of the following:  
  
-   Events. Events indicate points of interest in an execution path. All events contain information about a point of interest.  
  
-   Actions. Actions are run synchronously when events fire. An action can append run time data to an event.  
  
-   Targets. Targets consume events, either synchronously on the thread that fires the event or asynchronously on a system-provided thread.  
  
-   Predicates. Predicate sources retrieve values from event sources for use in comparison operations. Predicate comparisons compare specific data types and return a Boolean value.  
  
-   Types. Types encapsulate the length and characteristics of the byte collection, which is required in order to interpret the data.  

 |Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**nvarchar(60)**|The name of the object. name is unique within a package for a specific object type. Is not nullable.|  
|object_type|**nvarchar(60)**|The type of the object. object_type is one of the following:<br /><br /> event<br /><br /> action<br /><br /> target<br /><br /> pred_source<br /><br /> pred_compare<br /><br /> type<br /><br /> Is not nullable.|  
|package_guid|**uniqueidentifier**|The GUID for the package that exposes this action. There is a many-to-one relationship with sys.dm_xe_packages.package_id. Is not nullable.|  
|description|**nvarchar(256)**|A description of the action. description is set by the package author. Is not nullable.|  
|capabilities|**int**|A bitmap that describes the capabilities of the object. Is nullable.|  
|capabilities_desc|**nvarchar(256)**|Lists all the capabilities of the object. Is nullable.<br /><br /> **Capabilities that apply to all object types**<br /><br /> -<br />                                **Private**. The only object available for internal use, and that cannot be accessed via the CREATE/ALTER EVENT SESSION DDL. Audit events and targets fall into this category in addition to a small number of objects used internally.<br /><br /> ===============<br /><br /> **Event Capabilities**<br /><br /> -<br />                                **No_block**. The event is in a critical code path that cannot block for any reason. Events with this capability may not be added to any event session that specifies NO_EVENT_LOSS.<br /><br /> ===============<br /><br /> **Capabilities that apply to all object types**<br /><br /> -<br />                                **Process_whole_buffers**. The target consumes buffers of events at a time, rather than event by event.<br /><br /> -<br />                        **Singleton**. Only one instance of the target can exist in a process. Although multiple event sessions can reference the same singleton target there is really only one instance, and that instance will see each unique event only once. This is important if the target is added to multiple sessions that all collect the same event.<br /><br /> -<br />                                **Synchronous**. The target is executed on the thread that is producing the event, before control is returned to the calling code line.|  
|type_name|**nvarchar(60)**|The name for pred_source and pred_compare objects. Is nullable.|  
|type_package_guid|**uniqueidentifier**|The GUID for the package that exposes the type that this object operates on. Is nullable.|  
|type_size|**int**|The size, in bytes, of the data type. This is only for valid object types. Is nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_objects.package_guid|sys.dm_xe_packages.guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
