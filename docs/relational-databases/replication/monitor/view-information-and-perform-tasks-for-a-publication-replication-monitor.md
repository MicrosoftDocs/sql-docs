---
title: "View Information and Perform Tasks for a Publication (Replication Monitor) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "viewing publication information"
  - "publications [SQL Server replication], viewing information"
  - "publications [SQL Server replication], Replication Monitor tasks"
ms.assetid: 92e28a07-d6a7-461b-a0b3-bd9bc6afcbe5
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# View Information and Perform Tasks for a Publication (Replication Monitor)
  Replication Monitor provides the following tabs that include information about the selected publication:  
  
-   **All Subscriptions**  
  
     This tab displays information about all subscriptions to the selected publication.  
  
-   **Agents**  
  
     This tab displays information about all agents used by a publication:  
  
    -   The Snapshot Agent, which is used by all publications.  
  
    -   The Log Reader Agent, which is used by all transactional publications.  
  
    -   The Queue Reader Agent, which is used by transactional publications that have queued updating subscriptions.  
  
-   **Warnings** (for Distributors running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later)  
  
    -   This tab allows you to specify warnings and alerts for agents.  
  
-   **Tracer Tokens** (transactional replication only, for Distributors running [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] and later)  
  
     This tab allows you to measure latency, the amount of time that elapses between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber.  
  
 For more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To view information and perform tasks for a publication  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  To view and modify publication properties, right-click the publication, and then click **Properties**.  
  
3.  To view information about subscriptions, click the **All Subscriptions** tab.  
  
     To view and modify subscription properties, right-click the subscription, and then click **Properties**. You can also access more detailed information and perform tasks on this tab. For more information, see [View Information and Perform Tasks for the Agents Associated With a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-subscription-agents.md).  
  
4.  To view information about agents, click the **Agents** tab. You can also access more detailed information and perform tasks on this tab. For more information, see [View Information and Perform Tasks for the Agents Associated With a Publication &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-publication-agents.md).  
  
5.  To view information about agent warnings and thresholds, click the **Warnings** tab. For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
6.  To view information about tracer tokens, click the **Tracer Tokens** tab. For more information about how to use tracer tokens, see [Measure Latency and Validate Connections for Transactional Replication](../../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md).  
  
## See Also  
 [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  