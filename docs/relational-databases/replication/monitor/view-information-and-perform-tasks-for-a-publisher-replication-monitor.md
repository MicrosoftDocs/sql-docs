---
title: "View Information and Perform Tasks for a Publisher (Replication Monitor) | Microsoft Docs"
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
  - "Publishers [SQL Server replication], Replication Monitor tasks"
  - "viewing Publisher information"
  - "Publishers [SQL Server replication], viewing information"
ms.assetid: 1e777e95-377a-4de3-b965-867464aadaaf
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# View Information and Perform Tasks for a Publisher (Replication Monitor)
  Replication Monitor provides the following tabs that display information about the selected Publisher:  
  
-   **Publications**  
  
     This tab displays information about all publications at the selected Publisher.  
  
-   **Subscription Watch List**  
  
     This tab is intended to display information about subscriptions from all publications available at the selected Publisher that have errors, warnings, or the poorest performance. This tab is not displayed for Distributors running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
-   **Agents** tab  
  
     This tab displays detailed information about the agents and jobs that are used by all types of replication. The tab also allows you to start and stop each agent and job.  
  
 To view more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To view information and perform tasks for a Publisher  
  
1.  Expand a Publisher group in the left pane, and then click a Publisher.  
  
2.  To view information about all publications, click the **Publications** tab.  
  
3.  To view information about subscriptions, click the **Subscription Watch List** tab. You can also access more detailed information and perform tasks from this tab:  
  
    -   To view detailed information about the agent associated with a subscription, right-click the subscription, and then click **View Details**.  
  
    -   To view the properties of a subscription, right-click the subscription, and then click **Properties**.  
  
    -   To synchronize a push subscription, right-click the subscription, and then click **Start Synchronizing**.  
  
    -   To reinitialize a subscription, right-click the subscription, and then click **Reinitialize Subscription**.  
  
4.  To view information about agents, click the **Agents** tab. You can also access more detailed information and perform tasks on this tab:  
  
    -   To view detailed information about an agent (such as informational messages and any error messages), right-click the agent, and then click **View Details**.  
  
    -   To view detailed information about the job that runs the agent (such as the schedule, job step details, and so on), right-click the agent, and then click **Properties**.  
  
    -   To manage profiles for the agent, right-click the agent, and then click **Agent Profile**. For more information, see [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md).  
  
    -   To start an agent that is not running, right-click the agent, and then click **Start Agent**.  
  
    -   To stop an agent that is running, right-click the agent, and then click **Stop Agent**.  
  
## See Also  
 [View and Modify Distributor and Publisher Properties](../../../relational-databases/replication/view-and-modify-distributor-and-publisher-properties.md)   
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  