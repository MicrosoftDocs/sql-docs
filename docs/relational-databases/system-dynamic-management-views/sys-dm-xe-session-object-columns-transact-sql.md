---
title: "sys.dm_xe_session_object_columns (Transact-SQL)"
description: sys.dm_xe_session_object_columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/30/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_xe_session_object_columns_TSQL"
  - "sys.dm_xe_session_object_columns_TSQL"
  - "dm_xe_session_object_columns"
  - "sys.dm_xe_session_object_columns"
helpviewer_keywords:
  - "xe"
  - "sys.dm_xe_session_object_columns dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: e97f3307-2da6-4c54-b818-a474faec752e
---
# sys.dm_xe_session_object_columns (Transact-SQL)
[!INCLUDE [SQL Server SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Shows the configuration values for objects that are bound to an *active* server-scoped session.

Azure SQL Database supports only database-scoped sessions. See [sys.dm_xe_database_session_object_columns](sys-dm-xe-database-session-object-columns-azure-sql-database.md).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Has a many-to-one relationship with sys.dm_xe_sessions.address. Is not nullable.|  
|column_name|**nvarchar(256)**|The name of the configuration value. Is not nullable.|  
|column_id|**int**|The ID of the column. Is unique within the object. Is not nullable.|  
|column_value|**nvarchar(3072)**|The configured value of the column. Is nullable.|  
|object_type|**nvarchar(60)**|The type of the object. Is not nullable. object_type is one of:<br /><br /> event<br /><br /> target|  
|object_name|**nvarchar(256)**|The name of the object to which this column belongs. Is not nullable.|  
|object_package_guid|**uniqueidentifier**|The GUID of the package that contains the object. Is not nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|dm_xe_session_object_columns.object_name,<br /><br /> dm_xe_session_object_columns.object_package_guid|sys.dm_xe_objects.package_guid,<br /><br /> sys.dm_xe_objects.name|Many-to-one|  
|dm_xe_session_object_columns.column_name,<br /><br /> dm_xe_session_object_columns.column_id|sys.dm_xe_object_columns.name,<br /><br /> sys.dm_xe_object_columns.column_id|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
