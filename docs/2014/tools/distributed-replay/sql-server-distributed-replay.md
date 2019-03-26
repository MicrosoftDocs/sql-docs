---
title: "SQL Server Distributed Replay | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
helpviewer_keywords: 
  - "Distributed Replay"
  - "SQL Server Distributed Replay"
ms.assetid: 58ef7016-b105-42c2-90a0-364f411849a4
author: stevestein
ms.author: sstein
manager: craigg
---
# SQL Server Distributed Replay
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributed Replay feature helps you assess the impact of future [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] upgrades. You can also use it to help assess the impact of hardware and operating system upgrades, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tuning.  
  
## Benefits of Distributed Replay  
 Similar to [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)], you can use Distributed Replay to replay a captured trace against an upgraded test environment. Unlike [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)], Distributed Replay is not limited to replaying the workload from a single computer.  
  
 Distributed Replay offers a more scalable solution than [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)]. With Distributed Replay, you can replay a workload from multiple computers and better simulate a mission-critical workload.  
  
 The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributed Replay feature can use multiple computers to replay trace data and simulate a mission-critical workload. Use Distributed Replay for application compatibility testing, performance testing, or capacity planning.  
  
## When to Use Distributed Replay  
 [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] and Distributed Replay provide some overlap in functionality.  
  
 You may use [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] to replay a captured trace against an upgraded test environment. You can also analyze the replay results to look for potential functional and performance incompatibilities. However, [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] can only replay a workload from a single computer. When replaying an intensive OLTP application that has many active concurrent connections or high throughput, [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)] can become a resource bottleneck.  
  
 Distributed Replay offers a more scalable solution than [!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)]. Use Distributed Replay to replay a workload from multiple computers and better simulate a mission-critical workload.  
  
 The following table describes when to use each tool.  
  
|Tool|Use When...|  
|----------|---------------|  
|[!INCLUDE[ssSqlProfiler](../../../includes/sssqlprofiler-md.md)]|You want to use the conventional replay mechanism on a single computer. In particular, you need line-by-line debugging capabilities, such as the **Step**, **Run to Cursor**, and **Toggle Breakpoint** commands.<br /><br /> You want to replay an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] trace.|  
|Distributed Replay|You want to evaluate application compatibility. For example, you want to test [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and operating system upgrade scenarios, hardware upgrades, or index tuning.<br /><br /> The concurrency in the captured trace is so high that a single replay client cannot sufficiently simulate it.|  
  
## Distributed Replay Concepts  
 The following components make up the Distributed Replay environment:  
  
-   **Distributed Replay administration tool**: A console application, `DReplay.exe`, used to communicate with the distributed replay controller. Use the administration tool to control the distributed replay.  
  
-   **Distributed Replay controller**: A computer running the Windows service named [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributed Replay controller. The Distributed Replay controller orchestrates the actions of the distributed replay clients. There can only be one controller instance in each Distributed Replay environment.  
  
-   **Distributed Replay clients**: One or more computers (physical or virtual) running the Windows service named [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributed Replay client. The Distributed Replay clients work together to simulate workloads against an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. There can be one or more clients in each Distributed Replay environment.  
  
-   **Target server**: An instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that the Distributed Replay clients can use to replay trace data. We recommend that the target server be located in a test environment.  
  
 The Distributed Replay administration tool, controller, and client can be installed on different computers or the same computer. There can be only one instance of the Distributed Replay controller or client service that is running on the same computer.  
  
 The following figure shows the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Distributed Replay physical architecture:  
  
 ![Distributed Replay Architecture](../../database-engine/media/distributedreplayarch.gif "Distributed Replay Architecture")  
  
## Distributed Replay Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how to configure Distributed Replay.|[Configure Distributed Replay](configure-distributed-replay.md)|  
|Describes how to prepare the input trace data.|[Prepare the Input Trace Data](prepare-the-input-trace-data.md)|  
|Describes how to replay trace data.|[Replay Trace Data](replay-trace-data.md)|  
|Describes how to review the Distributed Replay trace data results.|[Review the Replay Results](review-the-replay-results.md)|  
|Describes how to use the administration tool to initiate, monitor, and cancel operations on the controller.|[Administration Tool Command-line Options &#40;Distributed Replay Utility&#41;](administration-tool-command-line-options-distributed-replay-utility.md)|  
  
## See Also  
 [SQL Server Distributed Replay Forum](https://social.technet.microsoft.com/Forums/sl/sqldru/)   
 [Using Distributed Replay to Load Test Your SQL Server - Part 2](https://blogs.msdn.com/b/mspfe/archive/2012/11/14/using-distributed-replay-to-load-test-your-sql-server-part-2.aspx)   
 [Using Distributed Replay to Load Test Your SQL Server - Part 1](https://blogs.msdn.com/b/mspfe/archive/2012/11/08/using-distributed-replay-to-load-test-your-sql-server-part-1.aspx)  
  
  
