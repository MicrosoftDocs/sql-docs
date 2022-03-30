---
description: "sys.dm_xe_database_session_object_columns (Azure SQL Database)"
title: "sys.dm_xe_database_session_object_columns"
titleSuffix: Azure SQL Database
ms.date: "03/30/2022"
ms.service: sql-database
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 0e6adc54-4d97-4ef0-bf4f-b4538d69f136
author: rwestMSFT
ms.author: randolphwest
monikerRange: "= azuresqldb-current"
ms.custom: seo-dt-2019
---
# sys.dm_xe_database_session_object_columns (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Shows the configuration values for objects that are bound to a session.  
  
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

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [sys.database_event_sessions (Azure SQL Database)](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_actions (Azure SQL Database)](sys-database-event-session-actions-azure-sql-database.md)
- [sys.database_event_session_events (Azure SQL Database)](sys-database-event-session-events-azure-sql-database.md)