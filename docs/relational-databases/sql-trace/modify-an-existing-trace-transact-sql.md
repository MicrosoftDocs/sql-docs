---
title: "Modify an Existing Trace (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "traces [SQL Server], modifying"
  - "modifying traces"
ms.assetid: 8792b43f-2510-44e3-9239-e73ad8227b89
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Modify an Existing Trace (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to use stored procedures to modify an existing trace.  
  
### To modify an existing trace  
  
1.  If the trace is already running, execute **sp_trace_setstatus** by specifying **@status = 0** to stop the trace.  
  
2.  To modify trace events, execute **sp_trace_setevent** by specifying the changes through the parameters. Listed in order, the parameters are:  
  
    -   **@traceid** (Trace ID)  
  
    -   **@eventid** (Event ID)  
  
    -   **@columnid** (Column ID)  
  
    -   **@on** (ON)  
  
     When you modify the **@on** parameter, keep in mind its interaction with the **@columnid** parameter:  
  
    |ON|Column ID|Result|  
    |--------|---------------|------------|  
    |ON (**1**)|NULL|Event is turned on. All columns are cleared.|  
    ||NOT NULL|Column is turned on for the specified event.|  
    |OFF (**0**)|NULL|Event is turned off. All columns are cleared.|  
    ||NOT NULL|Column is turned off for the specified event.|  
  
> [!IMPORTANT]
>  Unlike regular stored procedures, parameters of all [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] stored procedures (<strong>sp_trace_*xx*</strong>) are strictly typed and do not support automatic data type conversion. If these parameters are not called with the correct input parameter data types, as specified in the argument description, the stored procedure returns an error.  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)   
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setstatus-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [SQL Server Profiler Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sql-server-profiler-stored-procedures-transact-sql.md)  
  
  
