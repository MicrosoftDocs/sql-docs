---
description: "sys.dm_xe_database_session_events (Azure SQL Database)"
title: "sys.dm_xe_database_session_events"
titleSuffix: Azure SQL Database
ms.date: "03/30/2022"
ms.service: sql-database
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 9e985a19-f93f-4c56-b644-12c529298011
author: rwestMSFT
ms.author: randolphwest
monikerRange: "= azuresqldb-current"
ms.custom: seo-lt-2019
---
# sys.dm_xe_database_session_events (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns information about session events. Events are discrete execution points. Predicates can be applied to events to stop them from firing if the event does not contain the required information.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|event_name|**nvarchar(60)**|The name of the event that an action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package containing the event. Is not nullable.|  
|event_predicate|**nvarchar(2048)**|An XML representation of the predicate tree that is applied to the event. Is nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_events.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
|sys.dm_xe_database_session_events.event_package_guid, sys.dm_xe_database_session_events.event_name|sys.dm_xe_objects.name, sys.dm_xe_objects.package_guid|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [sys.database_event_sessions (Azure SQL Database)](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_actions (Azure SQL Database)](sys-database-event-session-actions-azure-sql-database.md)
- [sys.database_event_session_targets (Azure SQL Database)](sys-database-event-session-targets-azure-sql-database.md)