---
title: Pause a Trace
titleSuffix: SQL Server Profiler
description: Find out how to pause a trace so that SQL Server Profiler stops capturing event data, and see which properties you can change while the trace is paused.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: 432b9b0c-b5e7-47f3-a71b-310fb3bf2445
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Pause a Trace (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
Pausing a trace prevents further event data from being captured until the trace is restarted.  
  
 When you pause a trace, you prevent event data from being captured until the trace is restarted. Restarting a trace lets trace operations resume. No previously captured data is lost after a restart. When the trace is restarted, data capturing resumes from that point onward. While a trace is paused, you can change the name, events, columns, and filters. However, you cannot change the destinations to which you are sending the trace data, nor change the server connection.  
  
### To pause a trace  
  
1.  Select a window that contains a running trace.  
  
2.  On the **File** menu, click **Pause Trace**.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
