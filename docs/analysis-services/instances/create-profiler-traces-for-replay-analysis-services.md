---
title: "Create Profiler Traces for Replay (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQL Server Profiler, Analysis Services"
  - "monitoring Analysis Services [SQL Server]"
  - "replaying queries"
  - "replaying discovers"
  - "events [Analysis Services]"
  - "replaying commands"
  - "Profiler [SQL Server Profiler], Analysis Services"
  - "performance [Analysis Services], replays"
  - "traces [Analysis Services]"
ms.assetid: 93b2fc46-7cfb-4ab5-abeb-1475a7d6f0f2
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Create Profiler Traces for Replay (Analysis Services)
  To replay queries, discovers, and commands that are submitted by users to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must gather the required events. In order to initiate collection of these events, appropriate event classes must be selected in the **Event Selection** tab of the **Trace Properties** dialog box. For example if the Query Begin event class is selected, events that contain queries are collected and used for replay. Also, the trace file contains sufficient information to support replaying server transactions in a distributed environment in the original sequence of transactions.  
  
## Replay for Queries  
 To replay queries, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must capture the following events:  
  
-   Audit Login event class with all its data columns. This event class provides information about which user logged in and about the session settings. The server process ID (SPID) provides the reference to the user session. For more information, see [Security Audit Data Columns](../../analysis-services/trace-events/security-audit-data-columns.md).  
  
-   Query Begin event class with all its data columns. This event class provides information about the query that was submitted to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The Event Subclass column provides information about the type of query. The TextData column provides the actual text of the query. The RequestParameters column provides the parameters for parameterized queries, and the RequestProperties column provides the properties of an XML for Analysis (XMLA) request. For more information, see [Queries Events Data Columns](../../analysis-services/trace-events/queries-events-data-columns.md).  
  
-   Query End event class with all its data columns. This event class verifies the status of the query execution. For more information, see [Queries Events Data Columns](../../analysis-services/trace-events/queries-events-data-columns.md).  
  
## Replay for Discovers  
 To replay discovers, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must capture the following events:  
  
-   Audit Login event class with all its data columns. This event class provides information about which user logged in and about the session settings. The SPID provides the reference to the user session. For more information, see [Security Audit Data Columns](../../analysis-services/trace-events/security-audit-data-columns.md).  
  
-   Discover Begin event class with all its data columns. The TextData column provides the \<RequestType> portion of the discover request, and the RequestProperties column provides the \<Properties> portion of the discover request. The EventSubclass column provides the discover type. For more information, see [Discover Events Data Columns](../../analysis-services/trace-events/discover-events-data-columns.md).  
  
-   Discover End event class with all its data columns. This event class verifies the status of the discover request. For more information, see [Discover Events Data Columns](../../analysis-services/trace-events/discover-events-data-columns.md).  
  
## Replay for Commands  
 To replay commands, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must capture the following events:  
  
-   Command Begin event class with all its data columns. The TextData column provides the command details, such as the process type, database ID, and cube ID. The RequestParameters column provides the parameters for parameterized command, and the RequestProperties column provides the properties of an XMLA request. For more information, see [Command Events Data Columns](../../analysis-services/trace-events/command-events-data-columns.md).  
  
-   Command End event class with all its data columns. This event class verifies the status of the command. For more information, see [Command Events Data Columns](../../analysis-services/trace-events/command-events-data-columns.md).  
  
## See Also  
 [Analysis Services Trace Events](../../analysis-services/trace-events/analysis-services-trace-events.md)   
 [Introduction to Monitoring Analysis Services with SQL Server Profiler](../../analysis-services/instances/introduction-to-monitoring-analysis-services-with-sql-server-profiler.md)  
  
  