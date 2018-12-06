---
title: "Extended Events Tools | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
helpviewer_keywords: 
  - "extended events [SQL Server], using"
  - "extended events [SQL Server], options for using"
ms.assetid: d312a9ff-50ba-4721-baef-50bfd3169d38
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Extended Events Tools
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  You can use the following tools to create and manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events sessions:  
  
-   Data Definition Language (DDL) statements. These enable you to create and modify an Extended Events session.  
  
-   Dynamic management views, catalog views and system tables. These enable you to obtain session data and metadata by using [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. The system tables help you determine the existing Extended Events equivalents for SQL Trace event classes and columns.  
  
-   The **Extended Events** node of Object Explorer. This enables you to start, stop or delete a session, or to import and export session templates.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell provider. This is a powerful tool that you can use to create, alter, and manage Extended Events sessions. For more information, see [Use the PowerShell Provider for Extended Events](../../relational-databases/extended-events/use-the-powershell-provider-for-extended-events.md).  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. This enables you to create and execute the code samples that are provided in the Extended Events topics. For more information, see [Object Explorer](../../ssms/object/object-explorer.md).  
  
 In addition to sessions that you create, a default system health session exists on the server. The session collects system data that you can use to help troubleshoot performance issues. For more information, see [Use the system_health Session](../../relational-databases/extended-events/use-the-system-health-session.md).  
  
## DDL Statements  
 Use the following DDL statements to create, change, and drop an Extended Events session.  
  
|Name|Description|  
|----------|-----------------|  
|[CREATE EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/create-event-session-transact-sql.md)|Creates an Extended Event session object that identifies the source of the events, the event session targets, and the event session parameters.|  
|[ALTER EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-event-session-transact-sql.md)|Starts or stops an event session or changes an event session configuration.|  
|[DROP EVENT SESSION &#40;Transact-SQL&#41;](../../t-sql/statements/drop-event-session-transact-sql.md)|Drops an event session.|  
  
## Catalog Views  
 Use the following catalog views to obtain the metadata that is created when you create an event session.  
  
|Name|Description|  
|----------|-----------------|  
|[sys.server_event_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)|Lists all event session definitions.|  
|[sys.server_event_session_actions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-session-actions-transact-sql.md)|Returns a row for each action on each event of an event session.|  
|[sys.server_event_session_events &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-session-events-transact-sql.md)|Returns a row for each event in an event session.|  
|[sys.server_event_session_fields &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-session-fields-transact-sql.md)|Returns a row for each customizable column that was explicitly set on events and targets.|  
|[sys.server_event_session_targets &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-event-session-targets-transact-sql.md)|Returns a row for each event target for an event session.|  
  
## Dynamic Management Views  
 Use the following dynamic management views to obtain session metadata and session data. The metadata is obtained from the catalog views, and the session data is created when you start and run an event session.  
  
> [!NOTE]  
>  These views do not contain session data until a session starts.  
  
|Name|Description|  
|----------|-----------------|  
|[sys.dm_os_dispatcher_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-dispatcher-pools-transact-sql.md)|Returns information about session dispatcher pools.|  
|[sys.dm_xe_objects &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql.md)|Returns a row for each object that is exposed by an event package.|  
|[sys.dm_xe_object_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-object-columns-transact-sql.md)|Returns the schema information for all the objects.|  
|[sys.dm_xe_packages &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-packages-transact-sql.md)|Lists all the packages registered with the Extended Events engine.|  
|[sys.dm_xe_sessions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-sessions-transact-sql.md)|Returns information about an active Extended Events session.|  
|[sys.dm_xe_session_targets &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql.md)|Returns information about session targets.|  
|[sys.dm_xe_session_events &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-session-events-transact-sql.md)|Returns information about session events.|  
|[sys.dm_xe_session_event_actions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-session-event-actions-transact-sql.md)|Returns information about event session actions.|  
|[sys.dm_xe_map_values &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-map-values-transact-sql.md)|Provides a mapping of internal numeric keys to human-readable text.|  
|[sys.dm_xe_session_object_columns &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-xe-session-object-columns-transact-sql.md)|Shows the configuration values for objects that are bound to a session.|  
  
## System Tables  
 Use the following system tables to obtain information about the Extended Events equivalents for SQL Trace event classes and columns.  
  
|Name|Description|  
|----------|-----------------|  
|[trace_xe_event_map &#40;Transact-SQL&#41;](../../relational-databases/system-tables/extended-events-tables-trace-xe-event-map.md)|Contains one row for each Extended Events event that is mapped to a SQL Trace event class.|  
|[trace_xe_action_map &#40;Transact-SQL&#41;](../../relational-databases/system-tables/extended-events-tables-trace-xe-action-map.md)|Contains one row for each Extended Events action that is mapped to a SQL Trace column ID.|  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [SQL Server Extended Events Tables &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/6d52ff03-f5aa-4f0f-8c98-9b49dc76f94e)   
 [Use the system_health Session](../../relational-databases/extended-events/use-the-system-health-session.md)   
 [Use the PowerShell Provider for Extended Events](../../relational-databases/extended-events/use-the-powershell-provider-for-extended-events.md)  
  
  
