---
title: "View Information and Perform Tasks for Publication Agents | Microsoft Docs"
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
  - "agents [SQL Server replication], viewing information"
  - "viewing replication agent information"
  - "agents [SQL Server replication], tasks in Replication Monitor"
ms.assetid: 2a420da2-66f4-4650-9bdd-1992221ed3fd
caps.latest.revision: 39
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# View Information and Perform Tasks for Publication Agents
  Replication Monitor provides the **Agents** tab, which includes information about the agents associated with the selected publication. The Distribution Agent and Merge Agent are associated with subscriptions; for more information, see [View Information and Perform Tasks for the Agents Associated With a Subscription &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-subscription-agents.md).  
  
 This tab displays information about the following agents:  
  
-   The Snapshot Agent, which is used by all publications.  
  
-   The Log Reader Agent, which is used by all transactional publications.  
  
-   The Queue Reader Agent, which is used by transactional publications enabled for queued updating subscriptions.  
  
 To view more information about the options on this tab, click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### To view information and perform tasks for the agents associated with a publication  
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.  
  
2.  Click the **Agents** tab to view information about agents. You can also access more detailed information and perform tasks on this tab:  
  
    -   To view detailed information about an agent (such as informational messages and any error messages), right-click the agent, and then click **View Details**.  
  
    -   To view detailed information about the job that runs the agent (such as the schedule, job step details, and so on), right-click the agent, and then click **Properties**.  
  
    -   To manage profiles for the agent, right-click the agent, and then click **Agent Profile**. For more information, see [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md).  
  
    -   To start an agent that is not running, right-click the agent, and then click **Start Agent**.  
  
    -   To stop an agent that is running, right-click the agent, and then click **Stop Agent**.  
  
## See Also  
 [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md)   
 [View Information and Perform Tasks for a Publication &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-a-publication-replication-monitor.md)   
 [Replication Agent Administration](../../../relational-databases/replication/agents/replication-agent-administration.md)   
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  