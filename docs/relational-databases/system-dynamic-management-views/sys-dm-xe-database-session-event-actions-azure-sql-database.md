---
description: "sys.dm_xe_database_session_event_actions (Azure SQL Database)"
title: "sys.dm_xe_database_session_event_actions"
titleSuffix: Azure SQL Database
ms.date: "06/10/2016"
ms.service: sql-database
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "language-reference"
ms.assetid: 48519fd9-c7c2-434b-848d-ccbf41133fdd
author: markingmyname
ms.author: maghan
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
ms.custom: seo-lt-2019
---
# sys.dm_xe_database_session_event_actions (Azure SQL Database)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

  Returns information about event session actions. Actions are executed when events are fired. This management view aggregates statistics about the number of times an action has run, and the total run time of the action.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V12 and any future versions.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|action_name|**nvarchar(60)**|The name of the action. Is not nullable.|  
|action_package_guid|**uniqueidentifier**|The GUID for the package that contains the action. Is not nullable.|  
|event_name|**nvarchar(60)**|The name of the event that the action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package that contains the event. Is not nullable.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission.  
  
### Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_event_actions.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
|sys.dm_xe_database_session_event_actions.action_name<br /><br /> sys.dm_xe_session_event_actions.action_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_database_session_events.event_package_guid|Many-to-one|  
|sys.dm_xe_database_session_event_actions.event_name<br /><br /> sys.dm_xe_database_session_event_actions.event_package_guid|sys.dm_xe_objects.name<br /><br /> sys.dm_xe_objects.package_guid|Many-to-one|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
