---
description: "sys.dm_xe_database_session_targets (Azure SQL Database)"
title: "sys.dm_xe_database_session_targets"
titleSuffix: Azure SQL Database
ms.date: "03/30/2022"
ms.service: sql-database
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "reference"
dev_langs: 
  - "TSQL"
ms.assetid: 7f353e2a-f8fc-4366-97e4-aa1c49eadaf4
author: rwestMSFT
ms.author: randolphwest
monikerRange: "= azuresqldb-current"
ms.custom: seo-dt-2019
---
# sys.dm_xe_database_session_targets (Azure SQL Database)
[!INCLUDE[Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Returns information about session targets.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Has a many-to-one relationship with sys.dm_xe_database_sessions.address. Is not nullable.|  
|target_name|**nvarchar(60)**|The name of the target within a session. Is not nullable.|  
|target_package_guid|**uniqueidentifier**|The GUID of the package that contains the target. Is not nullable.|  
|execution_count|**bigint**|The number of times the target has been executed for the session. Is not nullable.|  
|execution_duration_ms|**bigint**|The total amount of time, in milliseconds, that the target has been executing. Is not nullable.|  
|target_data|**nvarchar(max)**|The data that the target maintains, such as event aggregation information. Is nullable.|  
  
## Permissions  
 Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_targets.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [sys.database_event_sessions (Azure SQL Database)](sys-database-event-sessions-azure-sql-database.md)
- [sys.database_event_session_actions (Azure SQL Database)](sys-database-event-session-actions-azure-sql-database.md)
- [sys.database_event_session_events (Azure SQL Database)](sys-database-event-session-events-azure-sql-database.md)