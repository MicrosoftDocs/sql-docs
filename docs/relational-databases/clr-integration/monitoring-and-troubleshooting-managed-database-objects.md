---
title: "Monitor and Troubleshoot Managed Database Objects"
description: Information about the tools that can be used to monitor and troubleshoot managed database objects and assemblies (CLR).
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "common language runtime [SQL Server], performance"
  - "monitoring [CLR integration]"
  - "performance [CLR integration]"
---
# Monitor and troubleshoot managed database objects

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article provides information about the tools that can be used to monitor and troubleshoot managed database objects and assemblies running in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Profiler trace events

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides SQL Trace and event notifications to monitor events that occur in the Database Engine. By recording specified events, SQL Trace helps you troubleshoot performance, audit database activity, gather sample data for a test environment, debug [!INCLUDE [tsql](../../includes/tsql-md.md)] statements and stored procedures, and gather data for performance analysis tools. For more information, see [SQL Trace](../sql-trace/sql-trace.md) and [Extended Events overview](../extended-events/extended-events.md).

| Event | Description |
| --- | --- |
| [SQL Server Event Class Reference](../event-classes/sql-server-event-class-reference.md) | Used to monitor assembly load requests (success and failures). |
| [SQL:BatchStarting Event Class](../event-classes/sql-batchstarting-event-class.md), [SQL:BatchCompleted Event Class](../event-classes/sql-batchcompleted-event-class.md) | Provides information about [!INCLUDE [tsql](../../includes/tsql-md.md)] batches that have started or completed. |
| [SP:Starting Event Class](../event-classes/sp-starting-event-class.md), [SP:Completed Event Class](../event-classes/sp-completed-event-class.md) | Used to monitor the execution of [!INCLUDE [tsql](../../includes/tsql-md.md)] stored procedures. |
| [SQL:StmtStarting Event Class](../event-classes/sql-stmtstarting-event-class.md), [SQL:StmtCompleted Event Class](../event-classes/sql-stmtcompleted-event-class.md) | Used to monitor the execution of CLR and [!INCLUDE [tsql](../../includes/tsql-md.md)] routines. |

## Performance counters

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides objects and counters that can be used by Performance Monitor to monitor activity in computers running an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. An object is any [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource, such as a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] lock or a Windows process. Each object contains one or more counters that determine various aspects of the objects to monitor. For more information, see [Use SQL Server Objects](../performance-monitor/use-sql-server-objects.md).

| Object | Description |
| --- | --- |
| [SQL Server, CLR object](../performance-monitor/sql-server-clr-object.md) | Total time spent in CLR execution. |

## Windows Performance Monitor (perfmon.exe) counters

The Windows Performance Monitor (`perfmon.exe`) tool has several performance counters that can be used to monitor CLR integration applications. The .NET CLR performance counters can be filtered by the `sqlservr` process name to track CLR integration applications that are currently running.

| Performance object | Description |
| --- | --- |
| `SqlServer:CLR` | Provides CPU statistics for the server. |
| `.NET CLR Exceptions` | Tracks the number of exceptions per second. |
| `.NET CLR Loading` | Provides information about the AppDomains and assemblies loaded in the server. |
| `.NET CLR Memory` | Provides information about CLR memory usage. This object can be used to flag alerts if memory usage gets too large. |
| `.NET Data Provider for SQL Server` | Tracks the number of connects and disconnects per second. This object can be used for monitoring the level of database activity. |

## Catalog views

Catalog views return information that is used by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Database Engine. You should use catalog views because they're the most general interface to the catalog metadata, and provide the most efficient way to obtain, transform, and present customized forms of this information. All user-available catalog metadata is exposed through catalog views. For more information, see [System catalog views](../system-catalog-views/catalog-views-transact-sql.md).

| Catalog view | Description |
| --- | --- |
| [sys.assemblies](../system-catalog-views/sys-assemblies-transact-sql.md) | Returns information about the assemblies registered in a database. |
| [sys.assembly_references](../system-catalog-views/sys-assembly-references-transact-sql.md) | Identifies assemblies that reference other assemblies. |
| [sys.assembly_modules](../system-catalog-views/sys-assembly-modules-transact-sql.md) | Returns information about each function, stored procedure, and trigger defined in an assembly. |
| [sys.assembly_files](../system-catalog-views/sys-assembly-files-transact-sql.md) | Returns information about the assembly files registered in the database. |
| [sys.assembly_types](../system-catalog-views/sys-assembly-types-transact-sql.md) | Identifies the user-defined types (UDTs) defined by an assembly. |
| [sys.module_assembly_usages](../system-catalog-views/sys-module-assembly-usages-transact-sql.md) | Identifies the assemblies that CLR modules are defined in. |
| [sys.parameter_type_usages](../system-catalog-views/sys-parameter-type-usages-transact-sql.md) | Returns information about parameters that are user-defined types. |
| [sys.server_assembly_modules](../system-catalog-views/sys-server-assembly-modules-transact-sql.md) | Identifies the assembly that a CLR trigger is defined in. |
| [sys.server_triggers](../system-catalog-views/sys-server-triggers-transact-sql.md) | Identifies the server-level DDL triggers on a server, including CLR triggers. |
| [sys.type_assembly_usages](../system-catalog-views/sys-type-assembly-usages-transact-sql.md) | Identifies the assemblies that user-defined types are defined in. |
| [sys.types](../system-catalog-views/sys-types-transact-sql.md) | Returns the system and user-defined types registered in the database. |

## Dynamic management views

Dynamic management views and functions return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance. For more information, see [System dynamic management views](../system-dynamic-management-objects/system-dynamic-management-objects.md).

| DMV | Description |
| --- | --- |
| [sys.dm_clr_appdomains](../system-dynamic-management-objects/sys-dm-clr-appdomains-transact-sql.md) | Provides information about each application domain in the server. |
| [sys.dm_clr_loaded_assemblies](../system-dynamic-management-objects/sys-dm-clr-loaded-assemblies-transact-sql.md) | Identifies each managed assembly registered on the server. |
| [sys.dm_clr_properties](../system-dynamic-management-objects/sys-dm-clr-properties-transact-sql.md) | Returns information about the hosted CLR. |
| [sys.dm_clr_tasks](../system-dynamic-management-objects/sys-dm-clr-tasks-transact-sql.md) | Identifies all the CLR tasks that are currently running. |
| [sys.dm_exec_cached_plans](../system-dynamic-management-objects/sys-dm-exec-cached-plans-transact-sql.md) | Returns information about the query execution plans that are cached by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] for faster query execution. |
| [sys.dm_exec_query_stats](../system-dynamic-management-objects/sys-dm-exec-query-stats-transact-sql.md) | Returns aggregate performance statistics for cached query plans. |
| [sys.dm_exec_requests](../system-dynamic-management-objects/sys-dm-exec-requests-transact-sql.md) | Returns information about each request that is executing within [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [sys.dm_os_memory_clerks](../system-dynamic-management-objects/sys-dm-os-memory-clerks-transact-sql.md) | Returns all the memory clerks currently active in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance, including CLR memory clerks. |

## Related content

- [Common language runtime (CLR) integration programming concepts](common-language-runtime-clr-integration-programming-concepts.md)
