---
title: "Monitoring Replication | Microsoft Docs"
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
  - "monitoring performance [SQL Server replication], Replication Monitor"
  - "Replication Monitor, about Replication Monitor"
ms.assetid: 81f596d2-27a5-489d-bf8d-0f4361decd02
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Monitoring Replication Overview
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor is a graphical tool that allows you to monitor the overall health of a replication topology. Replication Monitor provides detailed information on the status and performance of publications and subscriptions, allowing you to answer common questions, such as:  
  
-   Is my replication system healthy?  
  
-   Which subscriptions are slow?  
  
-   How far behind is my transactional subscription?  
  
-   How long will it take a transaction committed now to reach a Subscriber in transactional replication?  
  
-   Why is my merge subscription slow?  
  
-   Why is an agent not running?  
  
 To monitor replication, a user must be a member of the **sysadmin** fixed server role at the Distributor or a member of the **replmonitor** fixed database role in the distribution database. A system administrator can add any user to the **replmonitor** role, which allows that user to view replication activity in Replication Monitor; however, the user cannot administer replication.  
  
## In This Section  
 The following topics provide information about Replication Monitor features.  
  
 [Overview of the Replication Monitor Interface](../../../relational-databases/replication/monitor/overview-of-the-replication-monitor-interface.md)  
 Describes each window and tab in Replication Monitor and provides information on how to answer the questions listed above.  
  
 [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md)  
 Describes how to start the Replication Monitor.  
  
 [Allow Non-Administrators to Use Replication Monitor](../../../relational-databases/replication/monitor/allow-non-administrators-to-use-replication-monitor.md)  
 Describes how to assign permissions to non-administrators so that they can use Replication Monitor.  
  
 [Add and Remove Publishers from Replication Monitor](../../../relational-databases/replication/monitor/add-and-remove-publishers-from-replication-monitor.md)  
 Describes how to add or remove Publishers from Replication Monitor.  
  
 [Refresh Data in Replication Monitor](../../../relational-databases/replication/monitor/refresh-data-in-replication-monitor.md)  
 Describes how to refresh data in Replication Monitor.  
  
 [Monitor Performance with Replication Monitor](../../../relational-databases/replication/monitor/monitor-performance-with-replication-monitor.md)  
 Describes how to monitor performance in Replication Monitor, including information on setting performance thresholds. Includes information on article-level statistics for merge replication, which provide a detailed view of processing.  
  
 [Measure Latency and Validate Connections for Transactional Replication](../../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md)  
 Describes tracer tokens, which allow you to measure the performance of a transactional replication topology.  
  
 [Monitor Replication Agents](../../../relational-databases/replication/monitor/monitor-replication-agents.md)  
 Describes how to find information about each replication agent.  
  
 [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md)  
 Describes the warnings, thresholds, and alerts you can set in Replication Monitor. It is recommended that you enable warnings for your topology, so that you are informed about status and performance in a timely manner.  
  
 [Caching, Refresh, and Replication Monitor Performance](../../../relational-databases/replication/monitor/caching-refresh-and-replication-monitor-performance.md)  
 Describes how Replication Monitor caches data and calculations to improve performance; explains how refresh of the user interface relates to refresh of the cache.  
  
 [View Publication and Subscription Status in Replication Monitor](../../../relational-databases/replication/monitor/view-publication-and-subscription-status-in-replication-monitor.md)  
 Describes how to view status information a Publication or Subscription by using the Replication Monitor.  
  
 [View Information and Perform Tasks for a Publisher &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-a-publisher-replication-monitor.md)  
 Describes how to view information and perform tasks for a Publisher by using the Replication Monitor.  
  
 [View Information and Perform Tasks for a Publication &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-a-publication-replication-monitor.md)  
 Describes how to view information and perform tasks for a Publication by using the Replication Monitor.  
  
 [View Information and Perform Tasks for the Agents Associated With a Publication &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-publication-agents.md)  
 Describes how to view information and perform tasks for the agents associated with a Publication by using the Replication Monitor.  
  
 [View Information and Perform Tasks for a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-a-subscription-replication-monitor.md)  
 Describes how to view information and perform tasks for a Subscription by using the Replication Monitor.  
  
 [View Information and Perform Tasks for the Agents Associated With a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-subscription-agents.md)  
 Describes how to view information and perform tasks for the agents associated with a Subscription by using the Replication Monitor.  
  
  