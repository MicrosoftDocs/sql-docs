---
title: "Monitor and Troubleshoot Managed Database Objects"
description: Information about the tools that can be used to monitor and troubleshoot managed database objects and assemblies (CLR).
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
ms.custom: "seo-lt-2019"
helpviewer_keywords:
  - "common language runtime [SQL Server], performance"
  - "monitoring [CLR integration]"
  - "performance [CLR integration]"
ms.assetid: a7b589ac-104d-4b68-b4aa-9f5fc192b13d
---
# Monitoring and Troubleshooting Managed Database Objects
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic provides information about the tools that can be used to monitor and troubleshoot managed database objects and assemblies running in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Profiler Trace Events  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides SQL Trace and event notifications to monitor events that occur in the Database Engine. By recording specified events, SQL Trace helps you troubleshoot performance, audit database activity, gather sample data for a test environment, debug [!INCLUDE[tsql](../../includes/tsql-md.md)] statements and stored procedures, and gather data for performance analysis tools. For more information, see [SQL Trace](../../relational-databases/sql-trace/sql-trace.md) and [Extended Events](../../relational-databases/extended-events/extended-events.md).  
  
|Event|Description|  
|-----------|-----------------|  
|[Assembly Load Event Class](../event-classes/sql-server-event-class-reference.md)|Used to monitor assembly load requests (success and failures).|  
|[SQL:BatchStarting Event Class](../../relational-databases/event-classes/sql-batchstarting-event-class.md), [SQL:BatchCompleted Event Class](../../relational-databases/event-classes/sql-batchcompleted-event-class.md)|Provides information about [!INCLUDE[tsql](../../includes/tsql-md.md)] batches that have started or completed.|  
|[SP:Starting Event Class](../../relational-databases/event-classes/sp-starting-event-class.md), [SP:Completed Event Class](../../relational-databases/event-classes/sp-completed-event-class.md)|Used to monitor the execution of [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures.|  
|[SQL:StmtStarting Event Class](../../relational-databases/event-classes/sql-stmtstarting-event-class.md), [SQL:StmtCompleted Event Class](../../relational-databases/event-classes/sql-stmtcompleted-event-class.md)|Used to monitor the execution of CLR and [!INCLUDE[tsql](../../includes/tsql-md.md)] routines.|  
  
## Performance Counters  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides objects and counters that can be used by System Monitor to monitor activity in computers running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. An object is any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] lock or a Windows process. Each object contains one or more counters that determine various aspects of the objects to monitor. For more information, see [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md).  
  
|Object|Description|  
|------------|-----------------|  
|[SQL Server, CLR Object](../../relational-databases/performance-monitor/sql-server-clr-object.md)|Total time spent in CLR execution.|  
  
## Windows System Monitor (PERFMON.EXE) Counters  
 The Windows System Monitor (PERFMON.EXE) tool has several performance counters that can be used to monitor CLR integration applications. The .NET CLR performance counters can be filtered by the "sqlservr" process name to track CLR integration applications that are currently running.  
  
|Performance Object|Description|  
|------------------------|-----------------|  
|SqlServer:CLR|Provides CPU statistics for the server.|  
|.NET CLR Exceptions|Tracks the number of exceptions per second.|  
|.NET CLR Loading|Provides information about the AppDomains and assemblies loaded in the server.|  
|.NET CLR Memory|Provides information about CLR memory usage. This object can be used to flag alerts if memory usage gets too large.|  
|.NET Data Provider for SQL Server|Tracks the number of connects and disconnects per second. This object can be used for monitoring the level of database activity.|  
  
## Catalog Views  
 Catalog views return information that is used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine. We recommend that you use catalog views because they are the most general interface to the catalog metadata and provide the most efficient way to obtain, transform, and present customized forms of this information. All user-available catalog metadata is exposed through catalog views. For more information, see [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md).  
  
|Catalog View|Description|  
|------------------|-----------------|  
|[sys.assemblies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md)|Returns information about the assemblies registered in a database.|  
|[sys.assembly_references &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-references-transact-sql.md)|Identifies assemblies that reference other assemblies.|  
|[sys.assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-modules-transact-sql.md)|Returns information about each function, stored procedure, and trigger defined in an assembly.|  
|[sys.assembly_files &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-files-transact-sql.md)|Returns information about the assembly files registered in the database.|  
|[sys.assembly_types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assembly-types-transact-sql.md)|Identifies the user-defined types (UDTs) defined by an assembly.|  
|[sys.module_assembly_usages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-module-assembly-usages-transact-sql.md)|Identifies the assemblies that CLR modules are defined in.|  
|[sys.parameter_type_usages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-parameter-type-usages-transact-sql.md)|Returns information about parameters that are user-defined types.|  
|[sys.server_assembly_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-assembly-modules-transact-sql.md)|Identifies the assembly that a CLR trigger is defined in.|  
|[sys.server_triggers &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-server-triggers-transact-sql.md)|Identifies the server-level DDL triggers on a server, including CLR triggers.|  
|[sys.type_assembly_usages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-type-assembly-usages-transact-sql.md)|Identifies the assemblies that user-defined types are defined in.|  
|[sys.types &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-types-transact-sql.md)|Returns the system and user-defined types registered in the database.|  
  
## Dynamic Management Views  
 Dynamic management views and functions return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance. For more information, see [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).  
  
|DMV|Description|  
|---------|-----------------|  
|[sys.dm_clr_appdomains &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-clr-appdomains-transact-sql.md)|Provides information about each application domain in the server.|  
|[sys.dm_clr_loaded_assemblies &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)|Identifies each managed assembly registered on the server.|  
|[sys.dm_clr_properties &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-clr-properties-transact-sql.md)|Returns information about the hosted CLR.|  
|[sys.dm_clr_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-clr-tasks-transact-sql.md)|Identifies all the CLR tasks that are currently running.|  
|[sys.dm_exec_cached_plans &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-cached-plans-transact-sql.md)|Returns information about the query execution plans that are cached by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for faster query execution.|  
|[sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)|Returns aggregate performance statistics for cached query plans.|  
|[sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)|Returns information about each request that is executing within [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[sys.dm_os_memory_clerks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md)|Returns all the memory clerks currently active in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, including CLR memory clerks.|  
  
## See Also  
 [Common Language Runtime &#40;CLR&#41; Integration Programming Concepts](../../relational-databases/clr-integration/common-language-runtime-clr-integration-programming-concepts.md)  
  
