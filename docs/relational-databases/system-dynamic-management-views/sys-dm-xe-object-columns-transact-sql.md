---
title: "sys.dm_xe_object_columns (Transact-SQL)"
description: sys.dm_xe_object_columns (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "3/30/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_xe_object_columns"
  - "sys.dm_xe_object_columns_TSQL"
  - "dm_xe_object_columns_TSQL"
  - "dm_xe_object_columns"
helpviewer_keywords:
  - "sys.dm_xe_object_columns dynamic management view"
  - "extended events [SQL Server], views"
dev_langs:
  - "TSQL"
ms.assetid: d96a14f3-4284-45ff-b1fe-4858e540a013
---
# sys.dm_xe_object_columns (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL DB Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the schema information for all the objects.
  
> [!NOTE]  
>  Event objects expose fixed schemas for both read-only and read-write data.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|**nvarchar(256)**|The name of the column. name is unique within the object. Is not nullable.|  
|column_id|**int**|The identifier of the column. column_id is unique within the object when used with column_type. Is not nullable.|  
|object_name|**nvarchar(256)**|The name of the object to which this column belongs. There is a many-to-one relationship with sys.dm_xe_objects.id. Is not nullable.|  
|object_package_guid|**uniqueidentifier**|The GUID of the package that contains the object. Is not nullable.|  
|type_name|**nvarchar(256)**|The name of the type for this column. Is not nullable.|  
|type_package_guid|**uniqueidentifier**|The GUID of the package that contains the column data type. Is not nullable.|  
|column_type|**nvarchar(60)**|Indicates how this column is used. Is not nullable. column_type can be one of the following:<br /><br /> readonly. The column contains a static value that cannot be changed.<br /><br /> data. The column contains run-time data exposed by the object.<br /><br /> customizable. The column contains a value that can be changed.<br /><br /> Note: Changing this value can modify the behavior of the object.|  
|column_value|**nvarchar(256)**|Displays static values associated with the object column. Is nullable.|  
|capabilities|**int**|A bitmap describing the capabilities of the column. Is nullable.|  
|capabilities_desc|**nvarchar(256)**|A description of this object column's capabilities. This value can be one of the following:<br /><br /> Mandatory. The value must be set when binding the parent object to an event session.<br /><br /> Is nullable.|  
|description|**nvarchar(3072)**|The description of this object column. Is nullable.|  
  
## Permissions  

Requires VIEW SERVER STATE permission on the server.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_object_columns.object_name, sys.dm_xe_object_columns.object_package_guid|sys.dm_xe_objects.name,<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
|sys.dm_xe_object_columns.type_name<br /><br /> sys.dm_xe_object_columns.type_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
- [sys.dm_xe_sessions (Transact-SQL)](sys-dm-xe-sessions-transact-sql.md)
- [sys.dm_xe_session_events (Transact-SQL)](sys-dm-xe-session-events-transact-sql.md)
- [Extended events overview](../extended-events/extended-events.md)
- [Quickstart: Extended events](../extended-events/quick-start-extended-events-in-sql-server.md)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
