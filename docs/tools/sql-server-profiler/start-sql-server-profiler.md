---
title: "Run SQL Server Profiler | Microsoft Docs"
ms.custom: ""
ms.date: "06/21/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Profiler [SQL Server Profiler], starting"
  - "SQL Server Profiler, starting"
  - "starting SQL Server Profiler"
  - "Profiler [SQL Server Profiler], running"
  - "SQL Server Profiler, running"
  - "running SQL Server Profiler"
ms.assetid: 22e57ffa-63b0-4de3-b92e-df297dda1226
caps.latest.revision: 25
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Run SQL Server Profiler
  You can run [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] in several different ways to support gathering trace output in a variety of scenarios. You can start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] from the Windows **Start** menu, from the **Tools** menu in [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor, and from several locations in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
 When you first start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and select **New Trace** from the **File** menu, the application displays a **Connect to Server** dialog box where you can specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to connect to.  
  
## To start SQL Server Profiler from the Windows Start menu  
  
-  On the **Start** menu, point to **All Programs**, point to [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], point to **Performance Tools**, and then click **SQL Server Profiler**.  
  
## To start SQL Server Profiler in Database Engine Tuning Advisor  
  
-  On the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor **Tools** menu, click **SQL Server Profiler**.  
  
## To start SQL Server Profiler in SQL Server Management Studio  
 You can start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] from several locations in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], as illustrated in the following procedures. When [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] starts it loads the connection context, trace template, and filter context of its launch point.  
  
 [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] starts each profiler session in its own instance, and continues to run if you shut down [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### To start SQL Server Profiler from the Tools menu  
  
-  In the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **Tools** menu, click **SQL Server Profiler**.  
  
### To start SQL Server Profiler from the Query Editor  
  
1.  On the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] menu bar, click **New Query**.  
  
2.  In Query Editor, right-click and then select **Trace Query in SQL Server Profiler**.  
  
    > [!NOTE]  
    >  The connection context is the editor connection, the trace template is TSQL_SPs, and the applied filter is SPID = query window SPID.  
  
### To start SQL Server Profiler from Activity Monitor  
  
1.  In Object Explorer, right-click an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and then click **Activity Monitor**.  
  
2.  Click the **Processes** pane, right-click the process that you want to profile, and then click **Trace Process in SQL Server Profiler**.  
  
    > [!NOTE]  
    >  When a process is selected, the connection context is the Object Explorer connection when Activity Monitor was opened. The trace template is the default based on the server type, and the SPID equals the SPID for the selected process.  
  
## .NET Framework Security  
 In Windows Authentication mode, the user account that runs [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must have permission to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 To perform tracing with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], users must also have the ALTER TRACE permission.  
  
## Next steps  
 [SQL Server Profiler overview](../../tools/sql-server-profiler/sql-server-profiler.md)   
 [Use SQL Server Management Studio](http://msdn.microsoft.com/library/f289e978-14ca-46ef-9e61-e1fe5fd593be)  
  
  