---
title: "sys.dm_xe_database_session_object_columns"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_xe_database_session_object_columns (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "4/18/2022"
ms.service: sql-database
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
ms.assetid: 0e6adc54-4d97-4ef0-bf4f-b4538d69f136
monikerRange: "=azuresqldb-current"
---
# sys.dm_xe_database_session_object_columns (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database and Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Shows the configuration values for objects that are bound to an active database-scoped session.

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Has a many-to-one relationship with sys.dm_xe_database_sessions.address. Is not nullable.|  
|column_name|**nvarchar(60)**|The name of the configuration value. Is not nullable.|  
|column_id|**int**|The ID of the column. Is unique within the object. Is not nullable.|  
|column_value|**nvarchar(2048)**|The configured value of the column. Is nullable.|  
|object_type|**nvarchar(60)**|The type of the object.  Is not nullable.object_type is one of:<br /><br /> event<br /><br /> target|  
|object_name|**nvarchar(60)**|The name of the object to which this column belongs. Is not nullable.|  
|object_package_guid|**uniqueidentifier**|The GUID of the package that contains the object. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|dm_xe_database_session_object_columns.object_name<br /><br /> dm_xe_database_session_object_columns.object_package_guid|sys.dm_xe_objects.package_guid<br /><br /> sys.dm_xe_objects.name|Many-to-one|  
|dm_xe_database_session_object_columns.column_name<br /><br /> dm_xe_database_session_object_columns.column_id|sys.dm_xe_object_columns.name<br /><br /> sys.dm_xe_object_columns.column_id|Many-to-one|  
  
## Next steps

Learn more about Extended Events and related concepts in the following articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-targets-azure-sql-database.md)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
