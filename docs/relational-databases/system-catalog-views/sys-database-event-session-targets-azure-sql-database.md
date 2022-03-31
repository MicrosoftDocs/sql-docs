---
description: "sys.database_event_session_targets (Azure SQL Database and Azure SQL Managed Instance)"
title: "sys.database_event_session_targets"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
ms.custom: ""
ms.date: "03/30/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 38d775ee-1fe1-4820-88c6-02b2f875a66b
author: rwestMSFT
ms.author: randolphwest
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_event_session_targets (Azure SQL Database and Azure SQL Managed Instance)

[!INCLUDE [sqlserver2016-asdb](../../includes/applies-to-version/sqlserver2016-asdb.md)]

Returns a row for each event target for a database-scoped event session.

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and more capable [server-scoped sessions](../extended-events/extended-events.md).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|target_id|**int**|The ID of the target. ID is unique within the event session object. Is not nullable.|  
|name|**sysname**|The name of the event target. Is not nullable.|  
|package|**sysname**|The name of the event package that contains the event target. Is not nullable.|  
|module|**sysname**|The name of the module that contains the event target. Is not nullable.|  
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
## Remarks  

This view has the following relationship cardinalities.  
  
|From|To|Relationship|  
|-|-|-|  
|sys.database_event_session_targets.event_session_id|sys.database_event_sessions.event_session_id|Many to one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [sys.database_event_sessions (Azure SQL Database)](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_actions (Azure SQL Database)](sys-database-event-session-actions-azure-sql-database.md)
- [sys.database_event_session_events (Azure SQL Database)](sys-database-event-session-events-azure-sql-database.md)