---
title: "Replay a Trace Table (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "traces [SQL Server], replaying"
  - "replaying traces"
ms.assetid: 6a0ad817-3d8d-4495-889d-c66a7ef9e8bb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Replay a Trace Table (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Replay is the ability to open a saved trace and replay it again. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] features a multithreaded playback engine that can simulate user connections and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Replay is useful to troubleshoot an application or process problem. When you identify the problem and implement corrections, run the trace that found the potential problem against the corrected application or process. Then, replay the original trace and compare results.  
  
 In addition to any other event classes you want to monitor, specific event classes must be captured to enable replay. These events are captured by default if you use the **TSQL_Replay** trace template. For more information, see [Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md).  
  
### To replay a trace table  
  
1.  Open a trace table that contains the event classes necessary for replay.  
  
2.  On the **Replay** menu, click **Start**, and connect to the server instance where you want to replay the trace.  
  
3.  In the **Replay Configuration** dialog box, on the **Basic Replay Options** tab, specify **Replay server**. Click **Change** to change the server that is displayed in the **Replay server** box.  
  
4.  Optionally, select one of the following destinations in which to save the replay:  
  
    -   **Save to file,** which specifies a file in which to save the replay.  
  
    -   **Save to table**, which specifies a database table in which to save the replay.  
  
5.  Choose either **Replay the events in the order they were traced**or **Replay events using multiple threads**. The following table explains the difference between these settings.  
  
    |Option|Description|  
    |------------|-----------------|  
    |**Replay events in the order they were traced**|Replays events in the order they were recorded. This option enables debugging.|  
    |**Replay events using multiple threads**|This option uses multiple threads to replay each event regardless of the sequence. This option optimizes performance.|  
  
6.  Select **Display replay results** to view the replay as it occurs.  
  
7.  Optionally, click the **Advanced Replay Options**tab to specify the following options:  
  
    -   To replay all server process IDs (SPIDs), select **Replay system SPIDs**.  
  
    -   To limit the replay to processes belonging to a specific SPID, select **Replay one SPID only**. In the **SPID to replay**box, type the SPID.  
  
    -   To replay events that occurred during a specific time period, select **Limit replay by date and time**. Select a date and time for the **Start time**and **End time**to specify the time period to include in the replay.  
  
    -   To control how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] manages processes during replay, configure **Health Monitor Options**.  
  
## See Also  
 [Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md)   
 [Replay Traces](../../tools/sql-server-profiler/replay-traces.md)   
 [Open a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
