---
title: "View Information and Perform Tasks for a Subscription (Replication Monitor) | Microsoft Docs"
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
  - "subscriptions [SQL Server replication], viewing information"
  - "subscriptions [SQL Server replication], Replication Monitor tasks"
  - "viewing subscription information"
ms.assetid: 54aac83b-6f29-40d7-8901-cf059749867f
caps.latest.revision: 34
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# View Information and Perform Tasks for a Subscription (Replication Monitor)
  Replication Monitor provides the following tabs that include information about subscriptions:  
  
-   **All Subscriptions**  
  
     This tab displays information about all subscriptions to the selected publication.  
  
-   **Subscription Watch List**  
  
     This tab is intended to display information about subscriptions from all publications available at the selected Publisher that have errors, warnings, or the poorest performance. This tab is not displayed for Distributors running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 For more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To view information and perform tasks for subscriptions in the All Subscriptions tab  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  To view information about subscriptions, click the **All Subscriptions** tab. To view only those subscriptions in a given state, such as synchronizing, select an option from the **Show** drop-down list.  
  
3.  To view and modify subscription properties, right-click the subscription, and then click **Properties**. You can also access more detailed information and perform tasks on this tab. For more information, see [View Information and Perform Tasks for the Agents Associated With a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-subscription-agents.md).  
  
### To view information and perform tasks for subscriptions in the Subscription Watch List tab  
  
1.  Expand a Publisher group in the left pane, and then click a Publisher.  
  
2.  To view information about subscriptions, click the **Subscription Watch List** tab.  
  
3.  Select the type of subscription to display from the **Show \<SubscriptionType> Subscriptions** drop-down list. To view only those subscriptions in a given state, such as synchronizing, select an option from the **Show** drop-down list.  
  
4.  To view and modify subscription properties, right-click the subscription, and then click **Properties**. You can also access more detailed information and perform tasks on this tab. For more information, see [View Information and Perform Tasks for the Agents Associated With a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-subscription-agents.md).  
  
## See Also  
 [View and Modify Push Subscription Properties](../../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
 [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)   
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  