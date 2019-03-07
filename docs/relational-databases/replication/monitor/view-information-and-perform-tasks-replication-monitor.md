---
title: "View Information and Perform Tasks using Replication Monitor | Microsoft Docs"
ms.custom: ""
ms.date: "11/20/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "viewing publication information"
  - "publications [SQL Server replication], viewing information"
  - "publications [SQL Server replication], Replication Monitor tasks"
ms.assetid: 92e28a07-d6a7-461b-a0b3-bd9bc6afcbe5
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# View information and perform tasks using Replication Monitor
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Replication monitor provides a number of tabs and options to view information and perform various tasks. This article describes the different things that can be viewed and accomplished when using Replication Monitor. 


## For a Publication

### View information
Replication Monitor provides the following tabs that include information about the selected publication:  
  
-   **All Subscriptions** - This tab displays information about all subscriptions to the selected publication.  
  
-   **Agents** - This tab displays information about all agents used by a publication:   
    -   The Snapshot Agent, which is used by all publications.   
    -   The Log Reader Agent, which is used by all transactional publications.   
    -   The Queue Reader Agent, which is used by transactional publications that have queued updating subscriptions.  
  
-   **Warnings** - This tab allows you to specify warnings and alerts for agents.  
  
-   **Tracer Tokens** (transactional replication only) - This tab allows you to measure latency, the amount of time that elapses between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber.  
  
 For more information about the options on each tab, select the tab in the right pane, and then select **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### Perform tasks
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then select a publication.   
2.  To view and modify publication properties, right-click the publication, and then select **Properties**.    
3.  To view information about subscriptions, select the **All Subscriptions** tab, right-click the subscription, and then select **Properties**. You can also access more detailed information and perform tasks on this tab. 
4.  To view information about agents, select the **Agents** tab. You can also access more detailed information and perform tasks on this tab. 
5.  To view information about agent warnings and thresholds, select the **Warnings** tab. For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
6.  To view information about tracer tokens, select the **Tracer Tokens** tab. For more information about how to use tracer tokens, see [Measure Latency and Validate Connections for Transactional Replication](../../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md).  
  
## For a Publisher 

### View information
Replication Monitor provides the following tabs that display information about the selected Publisher:   
-   **Publications** - displays information about all publications at the selected Publisher.   
-   **Subscription Watch List** - display information about subscriptions from all publications available at the selected Publisher that have errors, warnings, or the poorest performance. This tab is not displayed for Distributors running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].    
-   **Agents** tab - displays detailed information about the agents and jobs that are used by all types of replication. The tab also allows you to start and stop each agent and job. To view more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### Perform tasks
  
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


## For a Subscription 

### View information
  Replication Monitor provides the following tabs that include information about subscriptions:    
-   **All Subscriptions** - displays information about all subscriptions to the selected publication.   
-   **Subscription Watch List** - displays information about subscriptions from all publications available at the selected Publisher that have errors, warnings, or the poorest performance. This tab is not displayed for Distributors running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. For more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### Perform tasks
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.   
2.  To view information about subscriptions, click the **All Subscriptions** tab. To view only those subscriptions in a given state, such as synchronizing, select an option from the **Show** drop-down list.    
3.  To view and modify subscription properties, right-click the subscription, and then click **Properties**. You can also access more detailed information and perform tasks on this tab. 
  
### To view information and perform tasks for subscriptions in the Subscription Watch List tab  
  
1.  Expand a Publisher group in the left pane, and then click a Publisher.    
2.  To view information about subscriptions, click the **Subscription Watch List** tab.    
3.  Select the type of subscription to display from the **Show \<SubscriptionType> Subscriptions** drop-down list. To view only those subscriptions in a given state, such as synchronizing, select an option from the **Show** drop-down list.    
4.  To view and modify subscription properties, right-click the subscription, and then click **Properties**. You can also access more detailed information and perform tasks on this tab. 
  
  
## For Publication Agents
Replication Monitor provides the **Agents** tab, which includes information about the agents associated with the selected publication. The Distribution Agent and Merge Agent are associated with subscriptions. 
  
### View information
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
  
## For Subscription Agents

### View information
-   **All Subscriptions** - displays information about all subscriptions to the selected publication.  
  
-   **Subscription Watch List** - intended to display information about subscriptions from all publications available at the selected Publisher that have errors, warnings, or the poorest performance. This tab is not displayed for Distributors running versions prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. For more information about the options on each tab, click the tab in the right pane, and then click **Help** on the menu bar. For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### Perform tasks
  
1.  Expand a Publisher group in the left pane, expand a Publisher, and then click a publication.    
2.  Click the **All Subscriptions** tab to view information about subscriptions. You can also access more detailed information and perform tasks on this tab:   
    -   To view detailed information about the agent associated with a subscription, right-click the subscription, and then click **View Details**. Detailed information includes: agent history and error messages; performance statistics for transactional replication; and article-level synchronization statistics for merge replication.  
  
         The tabs on the detail window that is launched depend on the type of subscription: for snapshot subscriptions, the tab is **Distributor to Subscriber History**; for transactional subscriptions, the tabs are **Publisher to Distributor History**, **Distributor to Subscriber History**, and **Undistributed Commands**; for merge subscriptions, the tab is **Synchronization History**.  
  
    -   To synchronize a push subscription, right-click the subscription, and then click **Start Synchronizing**.    
    -   To reinitialize a subscription, right-click the subscription, and then click **Reinitialize Subscription**.    
    -   To validate a single merge subscription, right-click the subscription, and then click **Validate Subscription**. To validate all subscriptions to a merge publication, right-click the publication, and then click **Validate All Subscriptions**; to validate all subscriptions for a transactional publication, right-click the publication, and then click **Validate Subscriptions**.    
    -   To manage profiles for the agent, right-click the agent, and then click **Agent Profile**. For more information, see [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md).  
  
### Subscription Watch List tab 
  
1.  Expand a Publisher group in the left pane, and then click a Publisher.    
2.  Click the **Subscription Watch List** tab to view information about subscriptions. You can also access more detailed information and perform tasks on this tab:   
    -   To view detailed information about the agent associated with a subscription, right-click the subscription, and then click **View Details**. Detailed information includes: agent history and error messages; performance statistics for transactional replication; and article-level synchronization statistics for merge replication.    
         The tabs on the detail window that is launched depend on the type of subscription: for snapshot subscriptions, the tab is **Distributor to Subscriber History**; for transactional subscriptions, the tabs are **Publisher to Distributor History**, **Distributor to Subscriber History**, and **Performance**; for merge subscriptions, the tab is **Synchronization History**.  
  
    -   To synchronize a push subscription, right-click the subscription, and then click **Start Synchronizing**.    
    -   To reinitialize a subscription, right-click the subscription, and then click **Reinitialize Subscription**.    
    -   To validate a single merge subscription, right-click the subscription, and then click **Validate Subscription**. To validate all subscriptions to a merge publication, right-click the publication, and then click **Validate All Subscriptions**; to validate all subscriptions for a transactional publication, right-click the publication, and then click **Validate Subscriptions**.    
    -   To manage profiles for the agent, right-click the agent, and then click **Agent Profile**. For more information, see [Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md).  

    


## See Also  
 [View and Modify Publication Properties](../../../relational-databases/replication/publish/view-and-modify-publication-properties.md)   
 [View and Modify Push Subscription Properties](../../../relational-databases/replication/view-and-modify-push-subscription-properties.md)   
 [View and Modify Pull Subscription Properties](../../../relational-databases/replication/view-and-modify-pull-subscription-properties.md)  
 [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md)   
 [Replication Agent Administration](../../../relational-databases/replication/agents/replication-agent-administration.md)    
  
  
