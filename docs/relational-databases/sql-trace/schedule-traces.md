---
description: "Schedule Traces"
title: "Schedule Traces | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "filters [SQL Server], events"
  - "traces [SQL Server]"
  - "traces [SQL Server], stopping"
  - "events [SQL Server], filters"
  - "scheduling traces [SQL Server]"
  - "traces [SQL Server], scheduling"
  - "stopping traces"
ms.assetid: 620b79db-924b-4502-8af3-39efcfca245d
author: "MashaMSFT"
ms.author: "mathoma"
---
# Schedule Traces
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  There are two ways to schedule tracing in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can:  
  
-   Enable a trace stop time.  
  
-   Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to schedule a trace.  
  
## Specifying a Stop Time  
 You can specify a trace stop time if you use [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures or if you use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. The stop time must be set when the trace is originally configured.  
  
## Scheduling Traces by Using SQL Server Agent  
 The best way to schedule traces is to use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent to start the trace and then specify a trace stop time by using the [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure **sp_trace_setstatus**, or [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
 **To set an end time filter for a trace**  
  
 [Filter Events Based on the Event End Time &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-based-on-the-event-end-time-sql-server-profiler.md)  
  
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)  
  
## See Also  
 [Automated Administration Tasks &#40;SQL Server Agent&#41;](../../ssms/agent/automated-administration-tasks-sql-server-agent.md)  
  
