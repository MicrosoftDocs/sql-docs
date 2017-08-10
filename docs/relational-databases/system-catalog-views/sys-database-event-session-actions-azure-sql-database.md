---
title: "sys.database_event_session_actions (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "03/03/2017"
ms.prod: 
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "Azure SQL Database"
ms.assetid: 32494df1-7ab7-4b88-a858-6b1021d67433
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# sys.database_event_session_actions (Azure SQL Database)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Returns a row for each action on each event of an event session.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V12 and any later versions.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|event_id|**int**|The ID of the event. This ID is unique within the event session object. Is not nullable.|  
|name|**sysname**|The name of the action. Is nullable.|  
|package|**sysname**|The name of the event package that contains the event. Is nullable.|  
|module|**sysname**|The name of the module that contains the event. Is nullable.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
||||  
|-|-|-|  
|From|To|Relationship|  
|sys.database_event_session_actions.event_session_id|sys.sys.database_event_sessions.event_session_id|Many to one|  
|sys.database_event_session_actions.event_id<br /><br /> sys.database_event_session_actions.event_session_id|sys.database_event_session_events.event_session_id<br /><br /> sys.database_event_session_events.event_id|Many to one|  
  
  