---
description: "View a Saved Trace (Transact-SQL)"
title: "View a Saved Trace (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "traces [SQL Server], viewing"
  - "displaying traces"
  - "viewing traces"
ms.assetid: 3a95a816-aa89-4d5f-858c-968a9cb3ee87
author: "MashaMSFT"
ms.author: "mathoma"
---
# View a Saved Trace (Transact-SQL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to use built-in functions to view a saved trace.  
  
### To view a specific trace  
  
1.  Execute **fn_trace_getinfo** by specifying the ID of the trace about which information is needed. This function returns a table that lists the trace, trace property, and information about the property.  

     Invoke the function this way:  
  
    ```  
    SELECT *  
    FROM ::fn_trace_getinfo(trace_id)  
    ```  
  
### To view all existing traces  
  
1.  Execute **fn_trace_getinfo** by specifying `0` or `default`. This function returns a table that lists all the traces, their properties, and information about these properties.  
  
     Invoke the function as follows:  
  
    ```  
    SELECT *  
    FROM ::fn_trace_getinfo(default)  
    ```  
  
## .NET Framework Security  
 To run the built-in function **fn_trace_getinfo**, the user needs the following permission:  
  
 ALTER TRACE on the server.  
  
## See Also  
 [sys.fn_trace_getinfo &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-trace-getinfo-transact-sql.md)   
 [View and Analyze Traces with SQL Server Profiler](../../tools/sql-server-profiler/view-and-analyze-traces-with-sql-server-profiler.md)  
  
  
