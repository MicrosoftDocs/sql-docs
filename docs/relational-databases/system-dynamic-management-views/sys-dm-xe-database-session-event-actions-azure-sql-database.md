---
description: "sys.dm_xe_database_session_event_actions (Azure SQL Database)"
title: "sys.dm_xe_database_session_event_actions"
titleSuffix: Azure SQL Database
ms.date: "03/30/2022"
ms.service: sql-database
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 48519fd9-c7c2-434b-848d-ccbf41133fdd
author: rwestMSFT
ms.author: randolphwest
monikerRange: "= azuresqldb-current"
ms.custom: seo-lt-2019
---
# sys.dm_xe_database_session_event_actions (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns information about event session actions. Actions are executed when events are fired. This management view aggregates statistics about the number of times an action has run, and the total run time of the action.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|action_name|**nvarchar(60)**|The name of the action. Is not nullable.|  
|action_package_guid|**uniqueidentifier**|The GUID for the package that contains the action. Is not nullable.|  
|event_name|**nvarchar(60)**|The name of the event that the action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package that contains the event. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_event_actions.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
|sys.dm_xe_database_session_event_actions.action_name<br /><br /> sys.dm_xe_session_event_actions.action_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_database_session_events.event_package_guid|Many-to-one|  
|sys.dm_xe_database_session_event_actions.event_name<br /><br /> sys.dm_xe_database_session_event_actions.event_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)