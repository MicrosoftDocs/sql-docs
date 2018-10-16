---
title: "sys.dm_xe_session_targets (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_xe_session_targets"
  - "dm_xe_session_targets_TSQL"
  - "dm_xe_session_targets"
  - "sys.dm_xe_session_targets_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_xe_session_targets dynamic management view"
  - "extended events [SQL Server], views"
ms.assetid: 76fbc3e1-ad88-4a47-8bf1-471c3bee5ad8
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_xe_session_targets (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns information about session targets.  
  
  |Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|event_session_address|**varbinary(8)**|The memory address of the event session. Has a many-to-one relationship with sys.dm_xe_sessions.address. Is not nullable.|  
|target_name|**nvarchar(60)**|The name of the target within a session. Is not nullable.|  
|target_package_guid|**uniqueidentifier**|The GUID of the package that contains the target. Is not nullable.|  
|execution_count|**bigint**|The number of times the target has been executed for the session. Is not nullable.|  
|execution_duration_ms|**bigint**|The total amount of time, in milliseconds, that the target has been executing. Is not nullable.|  
|target_data|**nvarchar(max)**|The data that the target maintains, such as event aggregation information. Is nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
### Relationship Cardinalities  
  
|From|To|Relationship|  
|----------|--------|------------------|  
|sys.dm_xe_session_targets.event_session_address|sys.dm_xe_sessions.address|Many-to-one|  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Corrected the data type for the target_data column.|  
|Corrected the description for the target_data column to indicate that the value is nullable.|  
|Corrected the "Relationship Cardinalities" table.|  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  

