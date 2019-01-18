---
title: "Performance Statistics Event Class | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "Performance Statistics event class"
ms.assetid: da9cd2c4-6fdd-4ada-b74f-105e3541393c
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Performance Statistics Event Class
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  The Performance Statistics event class can be used to monitor the performance of queries, stored procedures, and triggers that are executing. Each of the six event subclasses indicates an event in the lifetime of queries, stored procedures, and triggers within the system. Using the combination of these event subclasses and the associated sys.dm_exec_query_stats, sys.dm_exec_procedure_stats and sys.dm_exec_trigger_stats dynamic management views, you can reconstitute the performance history of any given query, stored procedure, or trigger.  
  
## Performance Statistics Event Class Data Columns  
 The following tables describe the event class data columns associated with each of the following event subclasses: EventSubClass 0, EventSubClass 1,EventSubClass 2,EventSubClass 3, EventSubClass 4, and EventSubClass 5.  
  
### EventSubClass 0  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|BigintData1|**bigint**|NULL|52|Yes|  
|BinaryData|**image**|NULL|2|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 0 = New batch SQL text that is not currently present in the cache.<br /><br /> The following EventSubClass types are generated in the trace for ad hoc batches.<br /><br /> For ad hoc batches with *n* number of queries:<br /><br /> 1 of type 0|21|Yes|  
|IntegerData2|**int**|NULL|55|Yes|  
|ObjectID|**int**|NULL|22|Yes|  
|Offset|**int**|NULL|61|Yes|  
|PlanHandle|**Image**|NULL|65|Yes|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**image**|SQL handle that can be used to obtain the batch SQL text using the sys.dm_exec_sql_text dynamic management view.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|SQL text of the batch.|1|Yes|  
  
### EventSubClass 1  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|BigintData1|**bigint**|The cumulative number of times this plan has been recompiled.|52|Yes|  
|BinaryData|**image**|The binary XML of the compiled plan.|2|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 1 = Queries within a stored procedure have been compiled.<br /><br /> The following EventSubClass types are generated in the trace for stored procedures.<br /><br /> For stored procedures with *n* number of queries:<br /><br /> *n* number of type 1|21|Yes|  
|IntegerData2|**int**|End of the statement within the stored procedure.<br /><br /> -1 for the end of the stored procedure.|55|Yes|  
|ObjectID|**int**|System-assigned ID of the object.|22|Yes|  
|Offset|**int**|Starting offset of the statement within the stored procedure or batch.|61|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**image**|SQL handle that can be used to obtain the SQL text of the stored procedure using the dm_exec_sql_text dynamic management view.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|NULL|1|Yes|  
|PlanHandle|**image**|The plan handle of the compiled plan for the stored procedure. This can be used to obtain the XML plan by using the sys.dm_exec_query_plan dynamic management view.|65|Yes|  
|ObjectType|**int**|A value that represents the type of object involved in the event.<br /><br /> 8272 = stored procedure|28|Yes|  
|BigintData2|**bigint**|Total memory, in kilobytes, used during compilation.|53|Yes|  
|CPU|**int**|Total CPU time, in milliseconds, spent during compilation.|18|Yes|  
|Duration|**int**|Total time, in microseconds, spent during compilation.|13|Yes|  
|IntegerData|**int**|The size, in kilobytes, of the compiled plan.|25|Yes|  
  
### EventSubClass 2  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|BigintData1|**bigint**|The cumulative number of times this plan has been recompiled.|52|Yes|  
|BinaryData|**image**|The binary XML of the compiled plan.|2|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 2 = Queries within an ad hoc SQL statement have been compiled.<br /><br /> The following EventSubClass types are generated in the trace for ad hoc batches.<br /><br /> For ad hoc batches with *n* number of queries:<br /><br /> *n* number of type 2|21|Yes|  
|IntegerData2|**int**|End of the statement within the batch.<br /><br /> -1 for the end of the batch.|55|Yes|  
|ObjectID|**int**|N/A|22|Yes|  
|Offset|**int**|Starting offset of the statement within the batch.<br /><br /> 0 for the beginning of the batch.|61|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**image**|SQL handle. This can be used to obtain the batch SQL text using the dm_exec_sql_text dynamic management view.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|NULL|1|Yes|  
|PlanHandle|**image**|The plan handle of the compiled plan for the batch. This can be used to obtain the batch XML plan using the dm_exec_query_plan dynamic management view.|65|Yes|  
|BigintData2|**bigint**|Total memory, in kilobytes, used during compilation.|53|Yes|  
|CPU|**int**|Total CPU time, in microseconds, spent during compilation.|18|Yes|  
|Duration|**int**|Total time, in milliseconds, spent during compilation.|13|Yes|  
|IntegerData|**int**|The size, in kilobytes, of the compiled plan.|25|Yes|  
  
### EventSubClass 3  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|BigintData1|**bigint**|The cumulative number of times this plan has been recompiled.|52|Yes|  
|BinaryData|**image**|NULL|2|Yes|  
|DatabaseID|**int**|ID of the database specified by the USE *database* statement or the default database if no USE *database* statement has been issued for a given instance. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the name of the database if the ServerName data column is captured in the trace and the server is available. Determine the value for a database by using the DB_ID function.|3|Yes|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 3 = A cached query has been destroyed and the historical performance data associated with the plan is about to be destroyed.<br /><br /> The following EventSubClass types are generated in the trace.<br /><br /> For ad hoc batches with *n* number of queries:<br /><br /> 1 of type 3 when the query is flushed from the cache<br /><br /> For stored procedures with *n* number of queries:<br /><br /> 1 of type 3 when the query is flushed from the cache.|21|Yes|  
|IntegerData2|**int**|End of the statement within the stored procedure or batch.<br /><br /> -1 for the end of the stored procedure or batch.|55|Yes|  
|ObjectID|**int**|NULL|22|Yes|  
|Offset|**int**|Starting offset of the statement within the stored procedure or batch.<br /><br /> 0 for the beginning of the stored procedure or batch.|61|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**image**|SQL handle that can be used to obtain the stored procedure or batch SQL text using the dm_exec_sql_text dynamic management view.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|QueryExecutionStats|1|Yes|  
|PlanHandle|**image**|The plan handle of the compiled plan for the stored procedure or batch. This can be used to obtain the XML plan using the dm_exec_query_plan dynamic management view.|65|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
  
### EventSubClass 4  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|BigintData1|**bigint**|NULL|52|Yes|  
|BinaryData|**image**|NULL|2|Yes|  
|DatabaseID|**int**|ID of the database in which the given stored procedure resides.|3|Yes|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 4 = A cached stored procedure has been removed from the cache and the historical performance data associated with it is about to be destroyed.|21|Yes|  
|IntegerData2|**int**|NULL|55|Yes|  
|ObjectID|**int**|ID of the stored procedure. This is same as the object_id column in sys.procedures.|22|Yes|  
|Offset|**int**|NULL|61|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**image**|SQL handle that can be used to obtain the stored procedure SQL text that was executed using the dm_exec_sql_text dynamic management view.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|ProcedureExecutionStats|1|Yes|  
|PlanHandle|**image**|The plan handle of the compiled plan for the stored procedure. This can be used to obtain the XML plan using the dm_exec_query_plan dynamic management view.|65|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
  
### EventSubClass 5  
  
|Data column name|Data type|Description|Column ID|Filterable|  
|----------------------|---------------|-----------------|---------------|----------------|  
|BigintData1|**bigint**|NULL|52|Yes|  
|BinaryData|**image**|NULL|2|Yes|  
|DatabaseID|**int**|ID of the database in which the given trigger resides.|3|Yes|  
|EventSequence|**int**|Sequence of a given event within the request.|51|No|  
|SessionLoginName|**nvarchar**|Login name of the user who originated the session. For example, if you connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Login1 and execute a statement as Login2, SessionLoginName shows Login1 and LoginName shows Login2. This column displays both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows logins.|64|Yes|  
|EventSubClass|**int**|Type of event subclass.<br /><br /> 5 = A cached trigger has been removed from the cache and the historical performance data associated with it is about to be destroyed.|21|Yes|  
|IntegerData2|**int**|NULL|55|Yes|  
|ObjectID|**int**|ID of the trigger. This is same as the object_id column in sys.triggers/sys.server_triggers catalog views.|22|Yes|  
|Offset|**int**|NULL|61|Yes|  
|SPID|**int**|ID of the session on which the event occurred.|12|Yes|  
|SqlHandle|**image**|SQL handle that can be used to obtain the trigger's SQL text using the dm_exec_sql_text dynamic management view.|63|Yes|  
|StartTime|**datetime**|Time at which the event started, if available.|14|Yes|  
|TextData|**ntext**|TriggerExecutionStats|1|Yes|  
|PlanHandle|**image**|The plan handle of the compiled plan for the trigger. This can be used to obtain the XML plan using the dm_exec_query_plan dynamic management view.|65|Yes|  
|GroupID|**int**|ID of the workload group where the SQL Trace event fires.|66|Yes|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [Showplan XML for Query Compile Event Class](../../relational-databases/event-classes/showplan-xml-for-query-compile-event-class.md)   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)  
  
  
