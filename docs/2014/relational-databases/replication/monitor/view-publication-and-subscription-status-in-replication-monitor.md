---
title: "View Publication and Subscription Status in Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Log Reader Agent, monitoring"
  - "Merge Agent, monitoring"
  - "Queue Reader Agent, monitoring"
  - "publications [SQL Server replication], viewing information"
  - "Snapshot Agent, monitoring"
  - "Distribution Agent, monitoring"
  - "monitoring performance [SQL Server replication], publication status"
  - "monitoring performance [SQL Server replication], subscription status"
  - "subscriptions [SQL Server replication], viewing status"
  - "Replication Monitor, publication and subscription status"
ms.assetid: 16590771-9867-463e-a973-36a5c145ac16
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View Publication and Subscription Status in Replication Monitor
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor displays status information for publications and subscriptions:  
  
-   The status of a publication is determined by the highest priority status of its subscriptions. For example, if one subscription to a publication has an error and another has a performance issue, a status of error is displayed for the publication.  
  
-   The status of a subscription is determined by the status of the agents that service the subscription. For merge replication, this is the Merge Agent. For transactional replication, this is either the Log Reader Agent or the Distribution Agent (the higher priority status is displayed; the status can also be determined by the Queue Reader Agent if queued updating subscriptions are used). For snapshot replication, this is the Snapshot Agent or the Distribution Agent (the higher priority status is displayed).  
  
 Tables in the following sections list the possible status values for publications and subscriptions. Three of the status values are displayed only if a threshold is met or exceeded:  
  
-   Expiring subscription  
  
     This status value applies to all types of replication. For more information, see [Set Thresholds and Warnings in Replication Monitor](set-thresholds-and-warnings-in-replication-monitor.md).  
  
-   Performance critical  
  
     This status value applies to transactional replication and merge replication. For more information, see [Monitor Performance with Replication Monitor](monitor-performance-with-replication-monitor.md).  
  
-   Long-running merge  
  
     This status value applies to merge replication. For more information, see [Monitor Performance with Replication Monitor](monitor-performance-with-replication-monitor.md).  
  
 In addition to publication and subscription status, merge replication provides article-level statistics, which give detailed information about: how much longer a merge phase will take to complete; how much time was spent processing a given article; the type of connection a Subscriber is using; and other important information. The statistics are displayed in the Merge Agent window in Replication Monitor. Snapshot and transactional replication provide detailed information on Distribution Agent processing.  
  
 **To view publication and subscription status**  
  
-   Replication Monitor: [View Information and Perform Tasks using Replication Monitor](view-information-and-perform-tasks-replication-monitor.md).
  
  
## Publication Status Values  
 The following table shows publication status values and their corresponding icons in priority order.  
  
|Status|Icon|  
|------------|----------|  
|Error|![UI icon: error](../media/repl-icon-error.gif "UI icon: error")|  
|Performance critical|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Retrying failed command|![UI icon: replication agent retry](../media/repl-icon-retry.gif "UI icon: replication agent retry")|  
|OK|none|  
  
## Subscription Status Values  
 The following tables show subscription status values and their corresponding icons in priority order. It is possible for a subscription to be in two states at the same time, such as **Expiring soon/Expired** and **Retrying failed command**; the highest priority status is displayed.  
  
 The status values **Performance critical**, **Expiring soon/Expired**, and **Uninitialized** are warnings. When a warning is displayed, Replication Monitor also displays whether an agent is running. For example, the status could be **Running, Performance critical**.  
  
### Transactional subscriptions  
  
|Status|Icon|  
|------------|----------|  
|Error|![UI icon: error](../media/repl-icon-error.gif "UI icon: error")|  
|Performance critical|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Expiring soon/Expired|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Uninitialized subscription|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Retrying failed command|![UI icon: replication agent retry](../media/repl-icon-retry.gif "UI icon: replication agent retry")|  
|Not running|![UI icon: replication agent stopped](../media/repl-icon-stopped.gif "UI icon: replication agent stopped")|  
|Running|![UI icon: replication agent running](../media/repl-icon-running.gif "UI icon: replication agent running")|  
  
### Merge subscriptions  
  
|Status|Icon|  
|------------|----------|  
|Error|![UI icon: error](../media/repl-icon-error.gif "UI icon: error")|  
|Performance critical|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Long-running merge|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Expiring soon/Expired|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Uninitialized subscription|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Retrying failed command|![UI icon: replication agent retry](../media/repl-icon-retry.gif "UI icon: replication agent retry")|  
|Synchronizing|![UI icon: replication agent running](../media/repl-icon-running.gif "UI icon: replication agent running")|  
|Not Synchronizing|![UI icon: replication agent stopped](../media/repl-icon-stopped.gif "UI icon: replication agent stopped")|  
  
### Snapshot subscriptions  
  
|Status|Icon|  
|------------|----------|  
|Error|![UI icon: error](../media/repl-icon-error.gif "UI icon: error")|  
|Expiring soon/Expired|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Uninitialized subscription|![UI icon: warning](../media/repl-icon-warn.gif "UI icon: warning")|  
|Retrying failed command|![UI icon: replication agent retry](../media/repl-icon-retry.gif "UI icon: replication agent retry")|  
|Synchronizing|![UI icon: replication agent running](../media/repl-icon-running.gif "UI icon: replication agent running")|  
|Not Synchronizing|![UI icon: replication agent stopped](../media/repl-icon-stopped.gif "UI icon: replication agent stopped")|  
  
## See Also  
 [Monitoring Replication](../monitoring-replication.md)  
  
  
