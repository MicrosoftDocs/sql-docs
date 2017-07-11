---
title: "View Information and Perform Tasks for Subscription Agents | Microsoft Docs"
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
  - "agents [SQL Server replication], viewing information"
  - "viewing replication agent information"
  - "agents [SQL Server replication], tasks in Replication Monitor"
ms.assetid: fbb59d31-2424-4552-9195-0da8d83e755f
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# View Information and Perform Tasks for Subscription Agents
  Replication Monitor provides two tabs that allow access to information about the agent(s) associated with a subscription:  
  
-   **All Subscriptions**  
  
     This tab displays information about all subscriptions to the selected publication.  
  
-   **Subscription Watch List**  
  
     This tab is intended to display information about subscriptions from all publications available at the selected Publisher that have errors, warnings, or the poorest performance. This tab is not displayed for Distributors running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 For more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To view information and perform tasks for the agents associated with a subscription (All Subscriptions tab)  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **All Subscriptions** tab to view information about subscriptions. You can also access more detailed information and perform tasks on this tab:  
  
    -   To view detailed information about the agent associated with a subscription, right-click the subscription, and then click **View Details**. Detailed information includes: agent history and error messages; performance statistics for transactional replication; and article-level synchronization statistics for merge replication.  
  
         The tabs on the detail window that is launched depend on the type of subscription: for snapshot subscriptions, the tab is **Distributor to Subscriber History**; for transactional subscriptions, the tabs are **Publisher to Distributor History**, **Distributor to Subscriber History**, and **Undistributed Commands**; for merge subscriptions, the tab is **Synchronization History**.  
  
    -   To synchronize a push subscription, right-click the subscription, and then click **Start Synchronizing**.  
  
    -   To reinitialize a subscription, right-click the subscription, and then click **Reinitialize Subscription**.  
  
    -   To validate a single merge subscription, right-click the subscription, and then click **Validate Subscription**. To validate all subscriptions to a merge publication, right-click the publication, and then click **Validate All Subscriptions**; to validate all subscriptions for a transactional publication, right-click the publication, and then click **Validate Subscriptions**.  
  
    -   To manage profiles for the agent, right-click the agent, and then click **Agent Profile**. For more information, see [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md).  
  
### To view information and perform tasks for the agents associated with a subscription (Subscription Watch List tab)  
  
1.  Expand a Publisher group in the left pane, and then click a Publisher.  
  
2.  Click the **Subscription Watch List** tab to view information about subscriptions. You can also access more detailed information and perform tasks on this tab:  
  
    -   To view detailed information about the agent associated with a subscription, right-click the subscription, and then click **View Details**. Detailed information includes: agent history and error messages; performance statistics for transactional replication; and article-level synchronization statistics for merge replication.  
  
         The tabs on the detail window that is launched depend on the type of subscription: for snapshot subscriptions, the tab is **Distributor to Subscriber History**; for transactional subscriptions, the tabs are **Publisher to Distributor History**, **Distributor to Subscriber History**, and **Performance**; for merge subscriptions, the tab is **Synchronization History**.  
  
    -   To synchronize a push subscription, right-click the subscription, and then click **Start Synchronizing**.  
  
    -   To reinitialize a subscription, right-click the subscription, and then click **Reinitialize Subscription**.  
  
    -   To validate a single merge subscription, right-click the subscription, and then click **Validate Subscription**. To validate all subscriptions to a merge publication, right-click the publication, and then click **Validate All Subscriptions**; to validate all subscriptions for a transactional publication, right-click the publication, and then click **Validate Subscriptions**.  
  
    -   To manage profiles for the agent, right-click the agent, and then click **Agent Profile**. For more information, see [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md).  
  
## See Also  
 [View Information and Perform Tasks for a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-a-subscription-replication-monitor.md)   
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  