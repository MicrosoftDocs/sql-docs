---
title: "sys.database_event_session_events (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
ms.assetid: f4c9eb0a-173c-4c66-8dd8-6f7176b2657f
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.database_event_session_events (Azure SQL Database)
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  Returns a row for each event in an event session.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] V12 and any later versions.|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_id|**int**|The ID of the event session. Is not nullable.|  
|event_id|**int**|The ID of the event. This ID is unique within an event session object. Is not nullable.|  
|name|**sysname**|The name of the event. Is not nullable.|  
|package|**sysname**|The name of the event package that contains the event. Is not nullable.|  
|module|**sysname**|The name of the module that contains the event. Is not nullable.|  
|predicate|**nvarchar(3000)**|The predicate expression that is applied to the event. Is nullable.|  
|predicate_xml|**nvarchar(3000)**|The XML predicate expression that is applied to the event. Is nullable.|  
  
## Permissions  
 Requires VIEW DATABASE STATE permission on the server.  
  
## Remarks  
 This view has the following relationship cardinalities.  
  
||||  
|-|-|-|  
|From|To|Relationship|  
|sys.database_event_session_events.event_session_id|sys.database_event_sessions.event_session_id|Many to one|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
