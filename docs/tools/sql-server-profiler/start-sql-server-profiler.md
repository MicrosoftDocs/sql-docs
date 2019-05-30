---
title: "Run SQL Server Profiler | Microsoft Docs"
ms.custom: ""
ms.date: "07/07/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "Profiler [SQL Server Profiler], starting"
  - "SQL Server Profiler, starting"
  - "starting SQL Server Profiler"
  - "Profiler [SQL Server Profiler], running"
  - "SQL Server Profiler, running"
  - "running SQL Server Profiler"
ms.assetid: 22e57ffa-63b0-4de3-b92e-df297dda1226
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Run SQL Server Profiler
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can run [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] in several different ways, to support gathering trace output in a variety of scenarios. You can start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] from the Windows 10 **Start** menu, from the **Tools** menu in [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor, and from several locations in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
When you first start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and select **New Trace** from the **File** menu, the application displays a **Connect to Server** dialog box where you can specify a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to connect to.  
## To start SQL Server Profiler from the Windows 10 Start menu  
-  Click the Windows **Start** icon or press the Windows key and start to type "SQL Server Profiler 17". When the **SQL Server Profiler 17** tile appears, click it.   

## To start SQL Server Profiler in Database Engine Tuning Advisor  
-  On the [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor **Tools** menu, click **SQL Server Profiler**.  

## To start SQL Server Profiler in SQL Server Management Studio  
 You can start [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] from several locations in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. When [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] starts, it loads the connection context, trace template, and filter context of its launch point. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] starts each SQL Server Profiler session in its own instance, and Profiler continues to run if you shut down [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
### To start SQL Server Profiler from the Tools menu  
-  In the [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] **Tools** menu, click **SQL Server Profiler**.  

### To start SQL Server Profiler from the Query Editor  
- In Query Editor, right-click and then select **Trace Query in SQL Server Profiler**.  

  > [!NOTE]  
  >  The connection context is the editor connection, the trace template is TSQL_SPs, and the applied filter is SPID = query window SPID.  
    
### To start SQL Server Profiler from Activity Monitor  
- In Activity Monitor, click the **Processes** pane, right-click the process that you want to profile, and then click **Trace Process in SQL Server Profiler**.  

    > [!NOTE]  
    >  When a process is selected, the connection context is the Object Explorer connection when Activity Monitor was opened. The trace template is the default based on the server type, and the SPID equals the SPID for the selected process.  
    
## .NET Framework Security  
- In Windows Authentication mode, the user account that runs [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must have permission to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
- To perform tracing with [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], users must also have the ALTER TRACE permission.  

## Next steps  
 [SQL Server Profiler overview](../../tools/sql-server-profiler/sql-server-profiler.md)   
 [Use SQL Server Management Studio](https://msdn.microsoft.com/library/f289e978-14ca-46ef-9e61-e1fe5fd593be)  
