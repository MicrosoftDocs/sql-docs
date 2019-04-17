---
title: "Create and Run Traces Using Transact-SQL Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
ms.assetid: 80347417-338d-4bea-8885-91fae5181cfe
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Create and Run Traces Using Transact-SQL Stored Procedures
  The process of tracing with SQL Trace varies depending on whether you create and run your trace by using Microsoft [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] or by using system stored procedures.  
  
 As an alternative to [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can use [!INCLUDE[tsql](../../includes/tsql-md.md)] system stored procedures to create and run traces. The process of tracing by using system stored procedures is as follows:  
  
1.  Create a trace by using **sp_trace_create**.  
  
2.  Add events with **sp_trace_setevent**.  
  
3.  (Optional) Set a filter with **sp_trace_setfilter**.  
  
4.  Start the trace with **sp_trace_setstatus**.  
  
5.  Stop the trace with **sp_trace_setstatus**.  
  
6.  Close the trace with **sp_trace_setstatus**.  
  
    > [!NOTE]  
    >  Using [!INCLUDE[tsql](../../includes/tsql-md.md)] system stored procedures creates a server-side trace, which guarantees that no events will be lost as long as there is space on the disk and no write errors occur. If the disk becomes full or the disk fails, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance continues to run, but tracing stops. If the **c2 audit mode** is set, and there is a write failure, tracing stops and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance shuts down. For more information about the **c2 audit mode** setting, see [c2 audit mode Server Configuration Option](../../database-engine/configure-windows/c2-audit-mode-server-configuration-option.md).  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Optimize SQL Trace](sql-trace.md)|Contains information about ways you can reduce the effects of tracing on system performance.|  
|[Filter a Trace](filter-a-trace.md)|Contains information about using filters for tracing.|  
|[Limit Trace File and Table Sizes](limit-trace-file-and-table-sizes.md)|Contains information about how to limit the size of files and tables where trace data is written. Note that only [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can write trace information to tables.|  
|[Schedule Traces](schedule-traces.md)|Contains information about how to set the start time and the end time for tracing.|  
  
## See Also  
 [sp_trace_create &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-create-transact-sql)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql)  
  
  
