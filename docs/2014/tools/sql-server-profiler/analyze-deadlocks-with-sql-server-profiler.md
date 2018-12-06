---
title: "Analyze Deadlocks with SQL Server Profiler | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "process nodes [SQL Server Profiler]"
  - "Profiler [SQL Server Profiler], deadlocks"
  - "deadlocks [SQL Server], identifying cause"
  - "resource nodes [SQL Server Profiler]"
  - "graphs [SQL Server Profiler]"
  - "SQL Server Profiler, deadlocks"
  - "events [SQL Server], deadlocks"
  - "edges [SQL Server Profiler]"
ms.assetid: 72d6718f-501b-4ea6-b344-c0e653f19561
author: stevestein
ms.author: sstein
manager: craigg
---
# Analyze Deadlocks with SQL Server Profiler
  Use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to identify the cause of a deadlock. A deadlock occurs when there is a cyclic dependency between two or more threads, or processes, for some set of resources within SQL Server. Using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can create a trace that records, replays, and displays deadlock events for analysis.  
  
 To trace deadlock events, add the **Deadlock graph** event class to a trace. This event class populates the **TextData** data column in the trace with XML data about the process and objects that are involved in the deadlock. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can extract the XML document to a deadlock XML (.xdl) file which you can view later in SQL Server Management Studio. You can configure [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to extract **Deadlock graph** events to a single file that contains all **Deadlock graph** events, or to separate files. This extraction can be done in any of the following ways:  
  
-   At trace configuration time, using the **Events Extraction Settings** tab. Note that this tab does not appear until you select the **Deadlock graph** event on the **Events Selection** tab.  
  
-   Using the **Extract SQL Server Events** option on the **File** menu.  
  
-   Individual events can also be extracted and saved by right-clicking a specific event and choosing **Extract Event Data**.  
  
## Deadlock Graphs  
 [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] and [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] use a deadlock wait-for graph to describe a deadlock. The deadlock wait-for graph contains process nodes, resource nodes, and edges representing the relationships between the processes and the resources. The components of wait-for graphs are defined in the following table:  
  
 Process node  
 A thread that performs a task; for example, INSERT, UPDATE, or DELETE.  
  
 Resource node  
 A database object; for example, a table, index, or row.  
  
 Edge  
 A relationship between a process and a resource. A `request` edge occurs when a process waits for a resource. An `owner` edge occurs when a resource waits for a process. The lock mode is included in the edge description. For example, **Mode: X**.  
  
## Deadlock Process Node  
 In a wait-for graph, the process node contains information about the process. The following table explains the components of a process.  
  
|Component|Definition|  
|---------------|----------------|  
|Server process Id|Server process identifier (SPID), a server assigned identifier for the process owning the lock.|  
|Server batch Id|Server batch identifier (SBID).|  
|Execution context Id|Execution context identifier (ECID). The execution context ID of a given thread associated with a specific SPID.<br /><br /> ECID = {0,1,2,3, *...n*}, where 0 always represents the main or parent thread, and {1,2,3, *...n*} represent the subthreads.|  
|Deadlock priority|Deadlock priority for the process. For more information about possible values, see [SET DEADLOCK_PRIORITY &#40;Transact-SQL&#41;](/sql/t-sql/statements/set-deadlock-priority-transact-sql).|  
|Log Used|Amount of log space used by the process.|  
|Owner Id|Transaction ID for the processes which are using transactions and currently waiting on a lock.|  
|Transaction descriptor|Pointer to the transaction descriptor that describes the state of the transaction.|  
|Input buffer|Input buffer of the current process, defines the type of event and the statement being executed. Possible values include:<br /><br /> **Language**<br /><br /> **RPC**<br /><br /> **None**|  
|Statement|Type of statement. Possible values are:<br /><br /> **NOP**<br /><br /> **SELECT**<br /><br /> **UPDATE**<br /><br /> **INSERT**<br /><br /> **DELETE**<br /><br /> **Unknown**|  
  
## Deadlock Resource Node  
 In a deadlock, two processes are each waiting for a resource held by the other process. In a deadlock graph, the resources are displayed as resource nodes.  
  
  
