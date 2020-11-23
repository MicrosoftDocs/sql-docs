---
description: "sys.dm_xe_database_session_events (Azure SQL Database)"
title: "sys.dm_xe_database_session_events"
titleSuffix: Azure SQL Database
ms.date: "06/10/2016"
ms.service: sql-database
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.topic: "language-reference"
ms.assetid: 9e985a19-f93f-4c56-b644-12c529298011
author: markingmyname
ms.author: maghan
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
ms.custom: seo-lt-2019
---
# sys.dm_xe_database_session_events (Azure SQL Database)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

  Returns information about session events. Events are discrete execution points. Predicates can be applied to events to stop them from firing if the event does not contain the required information.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V12 and any later versions.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Is not nullable.|  
|event_name|**nvarchar(60)**|The name of the event that an action is bound to. Is not nullable.|  
|event_package_guid|**uniqueidentifier**|The GUID for the package containing the event. Is not nullable.|  
|event_predicate|**nvarchar(2048)**|An XML representation of the predicate tree that is applied to the event. Is nullable.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission.  
  
### Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_database_session_events.event_session_address|sys.dm_xe_database_sessions.address|Many-to-one|  
|sys.dm_xe_database_session_events.event_package_guid, sys.dm_xe_database_session_events.event_name|sys.dm_xe_objects.name, sys.dm_xe_objects.package_guid|Many-to-one|  
  
  
