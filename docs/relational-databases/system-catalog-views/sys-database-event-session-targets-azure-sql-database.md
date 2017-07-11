---
title: "sys.database_event_session_targets (Azure SQL Database) | Microsoft Docs"
ms.custom: 
  - "MSDN content"
  - "MSDN - SQL DB"
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.service: "sql-database"
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
applies_to: 
  - "Azure SQL Database"
ms.assetid: 38d775ee-1fe1-4820-88c6-02b2f875a66b
caps.latest.revision: 6
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# sys.database_event_session_targets (Azure SQL Database)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Returns a row for each event target for an event session.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V12 and any later versions.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|target_id|**int**|The ID of the target. ID is unique within the event session object. Is not nullable.|  
|name|**sysname**|The name of the event target. Is not nullable.|  
|package|**sysname**|The name of the event package that contains the event target. Is not nullable.|  
|module|**sysname**|The name of the module that contains the event target. Is not nullable.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
||||  
|-|-|-|  
|From|To|Relationship|  
|sys.database_event_session_targets.event_session_id|sys.database_event_sessions.event_session_id|Many to one|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  