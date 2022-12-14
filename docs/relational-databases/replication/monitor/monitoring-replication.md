---
title: "Monitoring (Replication) | Microsoft Docs"
description: Learn about the monitoring tools used to track activity and status of replication in SQL Server replication topology.
ms.custom: ""
ms.date: "11/20/2018"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
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
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Monitoring (Replication)
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  Monitoring a replication topology is an important aspect of deploying replication. Because replication activity is distributed, it is essential to track activity and status across all computers involved in replication. With the use of various monitoring tools, you can answer such common questions as: 

-   Is my replication system healthy?
-   Which subscriptions are slow?
-   How far behind is my transactional subscription?
-   How long will it take a transaction committed now to reach a Subscriber in transactional replication?
-   Why is my merge subscription slow?
-   Why is an agent not running?  
  

The following tools can be used to monitor replication:  
  
-   **SQL Server Replication Monitor** -  the most important tool for monitoring replication, presenting a Publisher-focused view of all replication activity. For more information, see [Monitoring Replication](../../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md). 
-   **SQL Server Management Studio** - provides access to Replication Monitor. It also allows you to view the current status and last message logged by the following agents and allows you start and stop each agent: Log Reader Agent, Snapshot Agent, Merge Agent, and Distribution Agent. For more information, see [Monitor Replication Agents](../../../relational-databases/replication/monitor/monitor-replication-agents.md).  
  
-   **Transact-SQL (T-SQL) and Replication Management Objects (RMO)** - Both interfaces allow you to monitor all types of replication from the Distributor. Merge replication also provides the ability to monitor replication from the Subscriber.  
  
-   **Alerts for replication agent events** - Replication provides a number of predefined alerts for replication agent events, and you can create additional alerts if necessary. Alerts can be used to trigger an automated response to an event and/or notify an administrator. For more information, see [Use Alerts for Replication Agent Events](../../../relational-databases/replication/agents/use-alerts-for-replication-agent-events.md).  
  
-   **System Monitor** - can be useful for monitoring performance, providing a number of counters for replication. For more information, see [Monitoring Replication with System Monitor](../../../relational-databases/replication/monitor/monitoring-replication-with-system-monitor.md).  
  

## See Also  
 [Best Practices for Replication Administration](../../../relational-databases/replication/administration/best-practices-for-replication-administration.md)   

  
  
