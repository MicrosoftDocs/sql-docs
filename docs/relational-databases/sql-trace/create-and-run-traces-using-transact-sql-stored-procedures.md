---
description: "Create and Run Traces Using Transact-SQL Stored Procedures"
title: "Create and Run Traces Using Transact-SQL Stored Procedures"
ms.custom: seo-dt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
ms.assetid: 80347417-338d-4bea-8885-91fae5181cfe
author: "MashaMSFT"
ms.author: "mathoma"
---
# Create and Run Traces Using Transact-SQL Stored Procedures
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
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
|[Optimize SQL Trace](../../relational-databases/sql-trace/optimize-sql-trace.md)|Contains information about ways you can reduce the effects of tracing on system performance.|  
|[Filter a Trace](../../relational-databases/sql-trace/filter-a-trace.md)|Contains information about using filters for tracing.|  
|[Limit Trace File and Table Sizes](../../relational-databases/sql-trace/limit-trace-file-and-table-sizes.md)|Contains information about how to limit the size of files and tables where trace data is written. Note that only [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can write trace information to tables.|  
|[Schedule Traces](../../relational-databases/sql-trace/schedule-traces.md)|Contains information about how to set the start time and the end time for tracing.|  
  
## See Also  
 [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setfilter &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setfilter-transact-sql.md)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)  
  
  
