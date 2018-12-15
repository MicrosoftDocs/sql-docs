---
title: "Monitoring Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "monitoring performance [SQL Server replication], about monitoring replication"
  - "transactional replication, monitoring"
  - "monitoring [SQL Server replication]"
  - "merge replication monitoring [SQL Server replication]"
  - "snapshot replication [SQL Server], monitoring"
  - "replication [SQL Server], monitoring"
  - "administering replication, monitoring"
ms.assetid: f182f43a-6af8-45bc-a708-08d5f7a6984a
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Monitoring (Replication)
  Monitoring a replication topology is an important aspect of deploying replication. Because replication activity is distributed, it is essential to track activity and status across all computers involved in replication. The following tools can be used to monitor replication:  
  
-   [!INCLUDE[msCoName](../../includes/msCoName-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssNoVersion-md.md)] Replication Monitor  
  
     Replication Monitor is the most important tool for monitoring replication, presenting a Publisher-focused view of all replication activity. For more information, see [Monitor performance with Replication Monitor](monitor/monitor-performance-with-replication-monitor.md).  
  
-   [!INCLUDE[msCoName](../../includes/msCoName-md.md)] [!INCLUDE[ssManStudioFull](../../includes/ssManStudioFull-md.md)]  
  
     [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] provides access to Replication Monitor. It also allows you to view the current status and last message logged by the following agents and allows you start and stop each agent: Log Reader Agent, Snapshot Agent, Merge Agent, and Distribution Agent. For more information, see [Monitor Replication Agents](monitor/monitor-replication-agents.md).  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] and Replication Management Objects (RMO)  
  
     Both interfaces allow you to monitor all types of replication from the Distributor. Merge replication also provides the ability to monitor replication from the Subscriber.  
  
-   Alerts for replication agent events  
  
     Replication provides a number of predefined alerts for replication agent events, and you can create additional alerts if necessary. Alerts can be used to trigger an automated response to an event and/or notify an administrator. For more information, see [Use Alerts for Replication Agent Events](agents/use-alerts-for-replication-agent-events.md).  
  
-   System Monitor  
  
     System Monitor can be useful for monitoring performance, providing a number of counters for replication. For more information, see [Monitoring Replication with System Monitor](monitor/monitoring-replication-with-system-monitor.md).  
  
## See Also  
 [Replication Administration FAQ](administration/frequently-asked-questions-for-replication-administrators.md)   
 [Best Practices for Replication Administration](administration/best-practices-for-replication-administration.md)   

  
  
