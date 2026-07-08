---
title: "sys.dm_xe_database_session_object_columns"
description: The sys.dm_xe_database_session_object_columns dynamic management view (DMV) shows the configuration values for objects that are bound to an active database-scoped session.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2025
ms.service: azure-sql-database
ms.topic: "reference"
ms.custom:
  - ignite-2025
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_xe_database_session_object_columns 
[!INCLUDE [Azure SQL Database and Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/asdb-asmi-fabricsqldb.md)]

The `sys.dm_xe_database_session_object_columns` dynamic management view (DMV) shows the configuration values for objects that are bound to an active database-scoped session.

- Azure SQL Database and SQL database in Fabric support only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). 
- Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for SQL managed instances. For more information, see [CREATE EVENT SESSION code examples](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|`event_session_address`|**varbinary(8)**|The memory address of the event session. Has a many-to-one relationship with `sys.dm_xe_database_sessions.address`. Is not nullable.|  
|`column_name`|**nvarchar(60)**|The name of the configuration value. Is not nullable.|  
|`column_id`|**int**|The ID of the column. Is unique within the object. Is not nullable.|  
|`column_value`|**nvarchar(2048)**|The configured value of the column. Is nullable.|  
|`object_type`|**nvarchar(60)**|The type of the object. Is not nullable.<br /><br />`object_type` is one of:<br /><br /> event<br /><br /> target|  
|`object_name`|**nvarchar(60)**|The name of the object to which this column belongs. Is not nullable.|  
|`object_package_guid`|**uniqueidentifier**|The GUID of the package that contains the object. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|`sys.dm_xe_database_session_object_columns.object_name`<br /><br /> `sys.dm_xe_database_session_object_columns.object_package_guid`|`sys.dm_xe_objects.package_guid`<br /><br /> `sys.dm_xe_objects.name`|Many-to-one|  
|`sys.dm_xe_database_session_object_columns.column_name`<br /><br /> `sys.dm_xe_database_session_object_columns.column_id`|`sys.dm_xe_object_columns.name`<br /><br /> `sys.dm_xe_object_columns.column_id`|Many-to-one|  
  
## Related content

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-targets-azure-sql-database.md)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
