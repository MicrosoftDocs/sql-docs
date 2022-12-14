---
title: "sys.database_event_session_actions"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.database_event_session_actions (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "04/18/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 32494df1-7ab7-4b88-a858-6b1021d67433
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_event_session_actions (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Returns a row for each action on each event of a database-scoped event session. For information on actions in *active* database-scoped event sessions, see [sys.dm_xe_database_session_event_actions](../system-dynamic-management-views/sys-dm-xe-database-session-event-actions-azure-sql-database.md).

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
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

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Monitoring Microsoft Azure SQL Database performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events overview (SQL Server and Azure SQL Managed Instance)](../extended-events/extended-events.md)
