---
title: "Monitoring (Replication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "monitoring performance [SQL Server replication], about monitoring replication"
  - "transactional replication, monitoring"
  - "monitoring [SQL Server replication]"
  - "merge replication monitoring [SQL Server replication]"
  - "snapshot replication [SQL Server], monitoring"
  - "replication [SQL Server], monitoring"
  - "administering replication, monitoring"
ms.assetid: f182f43a-6af8-45bc-a708-08d5f7a6984a
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Monitoring (Replication)
  Monitoring a replication topology is an important aspect of deploying replication. Because replication activity is distributed, it is essential to track activity and status across all computers involved in replication. The following tools can be used to monitor replication:  
  
-   [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor  
  
     Replication Monitor is the most important tool for monitoring replication, presenting a Publisher-focused view of all replication activity. For more information, see [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md).  
  
-   [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]  
  
     [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] provides access to Replication Monitor. It also allows you to view the current status and last message logged by the following agents and allows you start and stop each agent: Log Reader Agent, Snapshot Agent, Merge Agent, and Distribution Agent. For more information, see [Monitor Replication Agents](../../../relational-databases/replication/monitor/monitor-replication-agents.md).  
  
-   [!INCLUDE[tsql](../../../includes/tsql-md.md)] and Replication Management Objects (RMO)  
  
     Both interfaces allow you to monitor all types of replication from the Distributor. Merge replication also provides the ability to monitor replication from the Subscriber.  
  
-   Alerts for replication agent events  
  
     Replication provides a number of predefined alerts for replication agent events, and you can create additional alerts if necessary. Alerts can be used to trigger an automated response to an event and/or notify an administrator. For more information, see [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md).  
  
-   System Monitor  
  
     System Monitor can be useful for monitoring performance, providing a number of counters for replication. For more information, see [Monitoring Replication with System Monitor](../../../relational-databases/replication/monitor/monitoring-replication-with-system-monitor.md).  
  
## See Also  
 [Administration &#40;Replication&#41;](../../../relational-databases/replication/administration/administration-replication.md)   
 [Best Practices for Replication Administration](../../../relational-databases/replication/administration/best-practices-for-replication-administration.md)   
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  