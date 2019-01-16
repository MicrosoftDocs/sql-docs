---
title: "High Availability (Reporting Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "high availability [SQL Server], Reporting Services"
  - "high availability [Reporting Services]"
  - "Reporting Services, high availability"
ms.assetid: 50e0813f-f591-4688-9cd1-e6389a3808e5
author: markingmyname
ms.author: maghan
manager: craigg
---
# High Availability (Reporting Services)
  A [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server is a stateless server that stores application data, content, properties, and session information in two [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] relational databases. As such, the best way to ensure the availability of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] functionality is to do the following:  
  
-   Use the high availability features of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssDE](../includes/ssde-md.md)] to maximize the uptime of the report server databases. If you configure a [!INCLUDE[ssDE](../includes/ssde-md.md)] instance to run in a failover cluster, you can select that instance when you create a report server database.  
  
-   Use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssHADR](../includes/sshadr-md.md)] with the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] databases and for data sources, as possible. For more information, see [Reporting Services with AlwaysOn Availability Groups &#40;SQL Server&#41;](../database-engine/availability-groups/windows/reporting-services-with-always-on-availability-groups-sql-server.md).  
  
-   Configure multiple report servers to run in a scale-out deployment, where all the servers share a single report server database. Deploying multiple report server instances, preferably on different servers, in a scale-out deployment can help provide uninterrupted service in the event one of the report server instances goes down.  
  
 A scale-out deployment provides a way to share a database. If one report server goes down, other servers in the same deployment will continue to work.  
  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] is not cluster-aware. By itself, a scale-out deployment does not provide load balancing; it does not detect the processing loads on a report server and route new processing requests to the least busy server. It does not re-route processing requests that failed before completion. To get load balancing features, you must configure load balancing for the Web servers that host the report servers, and then configure the report servers in a scale-out deployment so that they share the same report server database.  
  
 The Report Server Web service and Windows service are tightly integrated and run together as a single report server instance. You cannot configure availability for one service separately from the other.  
  
## See Also  
 [High Availability Solutions &#40;SQL Server&#41;](../database-engine/sql-server-business-continuity-dr.md)   
 [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](install-windows/configure-a-native-mode-report-server-scale-out-deployment.md)  
  
  
