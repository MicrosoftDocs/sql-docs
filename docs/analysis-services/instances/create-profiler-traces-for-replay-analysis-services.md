---
title: "Create Profiler Traces for Replay (Analysis Services) | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom:
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Create Profiler Traces for Replay (Analysis Services)
[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  To replay queries, discovers, and commands that are submitted by users to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must gather the required events. In order to initiate collection of these events, appropriate event classes must be selected in the **Event Selection** tab of the **Trace Properties** dialog box. For example if the Query Begin event class is selected, events that contain queries are collected and used for replay. Also, the trace file contains sufficient information to support replaying server transactions in a distributed environment in the original sequence of transactions.  
  
## Replay for Queries  
 To replay queries, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must capture the following events:  
  
-   Audit Login event class with all its data columns. This event class provides information about which user logged in and about the session settings. The server process ID (SPID) provides the reference to the user session. For more information, see [Security Audit Data Columns](https://docs.microsoft.com/bi-reference/trace-events/security-audit-data-columns).  
  
-   Query Begin event class with all its data columns. This event class provides information about the query that was submitted to [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. The Event Subclass column provides information about the type of query. The TextData column provides the actual text of the query. The RequestParameters column provides the parameters for parameterized queries, and the RequestProperties column provides the properties of an XML for Analysis (XMLA) request. For more information, see [Queries Events Data Columns](https://docs.microsoft.com/bi-reference/trace-events/queries-events-data-columns).  
  
-   Query End event class with all its data columns. This event class verifies the status of the query execution. For more information, see [Queries Events Data Columns](https://docs.microsoft.com/bi-reference/trace-events/queries-events-data-columns).  
  
## Replay for Discovers  
 To replay discovers, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must capture the following events:  
  
-   Audit Login event class with all its data columns. This event class provides information about which user logged in and about the session settings. The SPID provides the reference to the user session. For more information, see [Security Audit Data Columns](https://docs.microsoft.com/bi-reference/trace-events/security-audit-data-columns).  
  
-   Discover Begin event class with all its data columns. The TextData column provides the \<RequestType> portion of the discover request, and the RequestProperties column provides the \<Properties> portion of the discover request. The EventSubclass column provides the discover type. For more information, see [Discover Events Data Columns](https://docs.microsoft.com/bi-reference/trace-events/discover-events-data-columns).  
  
-   Discover End event class with all its data columns. This event class verifies the status of the discover request. For more information, see [Discover Events Data Columns](https://docs.microsoft.com/bi-reference/trace-events/discover-events-data-columns).  
  
## Replay for Commands  
 To replay commands, [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] must capture the following events:  
  
-   Command Begin event class with all its data columns. The TextData column provides the command details, such as the process type, database ID, and cube ID. The RequestParameters column provides the parameters for parameterized command, and the RequestProperties column provides the properties of an XMLA request. For more information, see [Command Events Data Columns](https://docs.microsoft.com/bi-reference/trace-events/command-events-data-columns).  
  
-   Command End event class with all its data columns. This event class verifies the status of the command. For more information, see [Command Events Data Columns](https://docs.microsoft.com/bi-reference/trace-events/command-events-data-columns).  
  
## See Also  
 [Analysis Services Trace Events](https://docs.microsoft.com/bi-reference/trace-events/analysis-services-trace-events)   
 [Introduction to Monitoring Analysis Services with SQL Server Profiler](../../analysis-services/instances/introduction-to-monitoring-analysis-services-with-sql-server-profiler.md)  
  
  
