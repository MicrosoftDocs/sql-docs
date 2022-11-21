---
title: "sys.dm_xe_database_session_targets"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: "4/18/2022"
ms.service: sql-database
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
ms.assetid: 7f353e2a-f8fc-4366-97e4-aa1c49eadaf4
monikerRange: "=azuresqldb-current"
---
# sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)
[!INCLUDE[Azure SQL Database and Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns information about *active* database-scoped session targets. For information about targets for all database-scoped sessions, see [sys.database_event_session_targets](../system-catalog-views/sys-database-event-session-targets-azure-sql-database.md).

Azure SQL Database supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). Azure SQL Managed Instance supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for managed instances: learn more in [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Has a many-to-one relationship with sys.dm_xe_database_sessions.address. Is not nullable.|  
|target_name|**nvarchar(60)**|The name of the target within a session. Is not nullable.|  
|target_package_guid|**uniqueidentifier**|The GUID of the package that contains the target. Is not nullable.|  
|execution_count|**bigint**|The number of times the target has been executed for the session. Is not nullable.|  
|execution_duration_ms|**bigint**|The total amount of time, in milliseconds, that the target has been executing. Is not nullable.|  
|target_data|**nvarchar(max)**|The data that the target maintains, such as event aggregation information. Is nullable.| 
|bytes_written|**bigint**|Number of bytes written to target. Is not nullable. |
  
## Permissions  

Requires the VIEW DATABASE STATE permission.  
  
### Relationship cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_targets.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
  
## Next steps

Learn more about related concepts in the following articles:

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [sys.dm_xe_database_session_object_columns (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-object-columns-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
