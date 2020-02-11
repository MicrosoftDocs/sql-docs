---
title: "Pause a Trace (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "pausing traces"
  - "temporarily stopping traces"
  - "traces [SQL Server], pausing"
  - "stopping traces"
ms.assetid: 432b9b0c-b5e7-47f3-a71b-310fb3bf2445
author: stevestein
ms.author: sstein
manager: craigg
---
# Pause a Trace (SQL Server Profiler)
  Pausing a trace prevents further event data from being captured until the trace is restarted.  
  
 When you pause a trace, you prevent event data from being captured until the trace is restarted. Restarting a trace lets trace operations resume. No previously captured data is lost after a restart. When the trace is restarted, data capturing resumes from that point onward. While a trace is paused, you can change the name, events, columns, and filters. However, you cannot change the destinations to which you are sending the trace data, nor change the server connection.  
  
### To pause a trace  
  
1.  Select a window that contains a running trace.  
  
2.  On the **File** menu, click **Pause Trace**.  
  
## See Also  
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
