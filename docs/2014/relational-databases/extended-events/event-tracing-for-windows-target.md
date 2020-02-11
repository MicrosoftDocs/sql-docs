---
title: "Event Tracing for Windows Target | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
helpviewer_keywords: 
  - "event tracing for windows target"
  - "ETW target"
  - "targets [SQL Server extended events], event tracing for windows target"
ms.assetid: ca2bb295-b7f6-49c3-91ed-0ad4c39f89d5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Event Tracing for Windows Target
  Before you use Event Tracing for Windows (ETW) as a target, we recommend that you have a working knowledge of ETW. ETW tracing is either used together with Extended Events or as an Extended Events event consumer. The following external links provide a starting point for obtaining background information about ETW:  
  
-   [Windows Events](https://go.microsoft.com/fwlink/?LinkId=92380)  
  
-   [Improve Debugging And Performance Tuning With ETW](https://go.microsoft.com/fwlink/?LinkId=92381)  
  
 The ETW target is a singleton target, although the target can be added to many sessions. If an event is raised on many sessions, the event will only be propagated to the ETW target one time per event occurrence. The Extended Events engine is limited to a single instance per process.  
  
> [!IMPORTANT]  
>  For the ETW target to work, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service startup account must be a member of the Performance Log Users group.  
  
 The configuration of the events present in an ETW session is controlled by the process that hosts the Extended Events engine. The engine controls which events to raise and what conditions must be met for an event to occur.  
  
 After binding to an Extended Events session, which attaches the ETW target for the first time during the lifetime of a process, the ETW target opens a single ETW session on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provider. If an ETW session already exists, the ETW target obtains a reference to the existing session. This ETW session is shared across all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances on a given computer. This ETW session receives all the events from sessions that have the ETW target.  
  
 Because ETW needs providers to be enabled to consume events and flow them down to the ETW, all Extended Events packages are enabled on the session. When an event is fired, the ETW target sends the event to the session on which the provider for the event is enabled.  
  
 The ETW target supports synchronous publishing of events on the thread that raises the event. However, the ETW target does not support asynchronous event publishing.  
  
 The ETW target does not support control from external ETW controllers such as Logman.exe. To produce ETW traces, an event session must be created with the ETW target. For more information, see [CREATE EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-event-session-transact-sql).  
  
> [!NOTE]  
>  Enabling the ETW target creates an ETW session that is named XE_DEFAULT_ETW_SESSION. If a session with the name XE_DEFAULT_ETW_SESSION already exists, it is used without modifying any properties of the existing session. The XE_DEFAULT_ETW_SESSION is shared between all instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. After you start the XE_DEFAULT_ETW_SESSION, you must stop it by using an ETW controller, such as the Logman tool. For example, you can run the following command at the command prompt: `logman stop XE_DEFAULT_ETW_SESSION -ets`.  
  
 The following table describes the available options for configuring the ETW target.  
  
|Option|Allowed values|Description|  
|------------|--------------------|-----------------|  
|default_xe_session_name|Any string up to 256 characters. This value is optional.|The Extended Events session name. By default, this is XE_DEFAULT_ETW_SESSION.|  
|default_etw_session_logfile_path|Any string up to 256 characters. This value is optional.|The path of the log file for the Extended Events session. By default, this is %TEMP%\ XEEtw.etl.|  
|default_etw_session_logfile_size_mb|Any unsigned integer. This value is optional.|The log file size, in megabytes (MB), for the Extended Events session. The default is 20 MB.|  
|default_etw_session_buffer_size_kb|Any unsigned integer. This value is optional.|The in-memory buffer size, in kilobytes (KB), for the Extended Events session. The default is 128 KB.|  
|retries|Any unsigned integer.|The number of times to retry publishing the event to the ETW subsystem before dropping the event. The default is 0.|  
  
 Configuring these settings is optional. The ETW target uses default values for these settings.  
  
 The ETW target is responsible for:  
  
-   Creating the default ETW session.  
  
-   Registering all Extended Events packages with ETW. This ensures that events are not dropped by ETW.  
  
-   Managing the flow of events to ETW. The ETW target creates an ETW event with Extended Events data and sends it to the appropriate ETW session. If the event is larger than the buffer size, or data cannot fit in one ETW event, ETW splits the event into fragments.  
  
-   Keeping Extended Events packages enabled at all times.  
  
 The following default file locations are used by ETW:  
  
-   The ETW output file is in %TEMP%\XEEtw.etl.  
  
    > [!IMPORTANT]  
    >  The file path cannot be changed after the first session starts.  
  
-   Managed Object Format (MOF) files are in *\<your install path>*\Microsoft SQL Server\Shared. For more information, see [Managed Object Format](https://go.microsoft.com/fwlink/?LinkId=92851) on MSDN.  
  
## Adding the Target to a Session  
 To add the ETW target to an Extended Events session, you must include the following statement when you create or alter an event session:  
  
```  
ADD TARGET package0.etw_classic_sync_target  
```  
  
 For more information about a full example that shows how to use the ETW target, including how to view the data, see [Monitor System Activity Using Extended Events](monitor-system-activity-using-extended-events.md).  
  
## See Also  
 [SQL Server Extended Events Targets](../../database-engine/sql-server-extended-events-targets.md)   
 [sys.dm_xe_session_targets &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql)   
 [CREATE EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-event-session-transact-sql)   
 [ALTER EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-event-session-transact-sql)  
  
  
