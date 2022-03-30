---
description: "sys.database_event_session_actions (Azure SQL Database)"
title: "sys.database_event_session_actions"
titleSuffix: Azure SQL Database
ms.custom: ""
ms.date: "03/30/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 32494df1-7ab7-4b88-a858-6b1021d67433
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_event_session_actions (Azure SQL Database)
[!INCLUDE [sqlserver2016-asdb](../../includes/applies-to-version/sqlserver2016-asdb.md)]

Returns a row for each action on each event of an event session.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|event_id|**int**|The ID of the event. This ID is unique within the event session object. Is not nullable.|  
|name|**sysname**|The name of the action. Is nullable.|  
|package|**sysname**|The name of the event package that contains the event. Is nullable.|  
|module|**sysname**|The name of the module that contains the event. Is nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
## Remarks

This view has the following relationship cardinalities.  
  
| From | To | Relationship |
| ---- | -- | ------------ |
|sys.database_event_session_actions.event_session_id|sys.sys.database_event_sessions.event_session_id|Many to one|  
|sys.database_event_session_actions.event_id<br /><br /> sys.database_event_session_actions.event_session_id|sys.database_event_session_events.event_session_id<br /><br /> sys.database_event_session_events.event_id|Many to one|  

## Next steps

Learn more about related concepts in the following articles:

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [sys.database_event_sessions (Azure SQL Database)](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_events (Azure SQL Database)](sys-database-event-session-events-azure-sql-database.md)
- [sys.database_event_session_targets (Azure SQL Database)](sys-database-event-session-targets-azure-sql-database.md)