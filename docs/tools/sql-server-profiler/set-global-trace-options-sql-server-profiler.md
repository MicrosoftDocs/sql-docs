---
title: "Set Global Trace Options (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "global trace options [SQL Server]"
ms.assetid: 2854608a-c3c7-4eb8-b567-034bfec4b1a9
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Set Global Trace Options (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to set options that apply to all traces that are created with a specific instance of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To set global trace options  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  In the **General Options**dialog box, click **Choose Font**to modify the display options, and then click **OK**.  
  
3.  Optionally, select **Start tracing immediately after making connection**.  
  
4.  Optionally, select **Update trace definition when provider version changes**. This option is recommended and is selected by default. When this option is selected, the trace definition is automatically updated to the current version of the server where tracing is performed.  
  
5.  Optionally, specify how the server should manage rollover files:  
  
    -   Select **Load all rollover files in sequence without prompting** to automatically load rollover files during replay.  
  
    -   Select **Prompt before loading rollover files** to control rollover files during replay.  
  
    -   Select **Never load subsequent rollover files** to replay only one file at a time.  
  
6.  Optionally, set replay options:  
  
    -   **Default number of replay threads** controls the number of processor threads to use during replay. A higher number of threads causes replay to complete faster, but causes server performance to degrade during replay. The recommended setting is **4**. The following table lists the available options:  
  
        |Value|Description|  
        |-----------|-----------------|  
        |**2**|Minimum value. Use two threads to replay.|  
        |**4**|Default value.|  
        |**255**|Maximum value. Setting a maximum value will impede performance for other processes.|  
  
    -   **Default health monitor wait interval (sec)** sets the maximum amount of time that a replay thread can block another process in seconds. The following table explains the values.  
  
        |Value|Description|  
        |-----------|-----------------|  
        |**0**|Minimum value. A setting of **0** means that [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] will never stop a blocking process.|  
        |**3600**|Default value. Allow blocking processes that do not exceed **3600** seconds, or one hour.|  
        |**86400**|Maximum value. Allow blocking processes that do not exceed **86400** seconds, or one day.|  
  
    -   **Default health monitor poll interval (sec)** sets the frequency to poll replay threads for blocking processes. The following table explains the values.  
  
        |Value|Description|  
        |-----------|-----------------|  
        |**1**|Minimum value. A setting of **1** means [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] will poll for blocking processes once per second.|  
        |**60**|Default value. Poll for blocking processes once per minute.|  
        |**86400**|Maximum value. Poll for blocking processes once per **86400** seconds, or one day.|  
  
## See Also  
 [Set Trace Display Defaults &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/set-trace-display-defaults-sql-server-profiler.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
