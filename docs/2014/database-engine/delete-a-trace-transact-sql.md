---
title: "Delete a Trace (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "traces [SQL Server], deleting"
  - "removing traces"
  - "deleting traces"
ms.assetid: a5502814-b281-42dd-b885-5c9368025ae6
caps.latest.revision: 21
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Delete a Trace (Transact-SQL)
  This topic describes how to use stored procedures to delete a trace.  
  
 For an example of using trace stored procedures, see [Create a Trace &#40;Transact-SQL&#41;](../../2014/database-engine/create-a-trace-transact-sql.md).  
  
### To delete a trace  
  
1.  Execute **sp_trace_setstatus** by specifying **@status = 0** to stop the trace.  
  
2.  Execute **sp_trace_setstatus** by specifying **@status = 2** to close the trace and delete its information from the server.  
  
> [!NOTE]  
>  A trace must be stopped first before it can be closed.  
  
## See Also  
 [sp_trace_setstatus &#40;Transact-SQL&#41;](../Topic/sp_trace_setstatus%20\(Transact-SQL\).md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../Topic/System%20Stored%20Procedures%20\(Transact-SQL\).md)   
 [SQL Server Profiler Stored Procedures &#40;Transact-SQL&#41;](../Topic/SQL%20Server%20Profiler%20Stored%20Procedures%20\(Transact-SQL\).md)  
  
  