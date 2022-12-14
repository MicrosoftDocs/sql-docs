---
description: "Overview of the Replication Monitor Interface"
title: "Overview of the Replication Monitor Interface | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Replication Monitor"
  - "Replication Monitor, about Replication Monitor"
ms.assetid: 078f0e34-7153-45c4-8725-778b5bef88da
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Overview of the Replication Monitor Interface
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor presents a Publisher-focused view or Distributor-focused view of all replication activity in a two pane format. You add a Publisher to the monitor in the left pane, and in the right pane the monitor displays information on the Publisher, its publications, the subscriptions to those publications, and the various replication agents. In addition to presenting information for the replication topology, Replication Monitor allows you to perform a number of tasks, such as starting and stopping agents, and validating data.  
  
## Viewing Information for the Entire Topology  
 The left pane of Replication Monitor displays  
  
-   Publisher groups, Publishers, and publications.  
  
-   Distributors, Publishers, and publications.  
  
 To view any information in Replication Monitor, you must first add a Publisher. For more information, see [Add and Remove Publishers from Replication Monitor](../../../relational-databases/replication/monitor/add-and-remove-publishers-from-replication-monitor.md).  
  
 The left pane helps answer the following questions:  
  
-   Is my replication system healthy?  
  
     The replication system is relatively healthy if there are no error icons on nodes in the left pane. To get a more complete view of system health, you should also check the **Subscription Watch List** tab, which displays information on subscriptions that might require attention.  
  
-   Why is an agent not running?  
  
     An agent is not running at a particular time either because it is not scheduled to run or because an error has occurred. If an error has occurred, an error icon is displayed on the appropriate nodes in the left pane. For example, if the Snapshot Agent for a publication stopped because of an error, an error icon is displayed on the Publisher group, Publisher, and publication nodes. Summary information for the Snapshot Agent is displayed on the **Agents** tab for the publication; double click the Snapshot Agent on this tab for detailed error information.  
  
## Viewing Information and Performing Tasks Related to Distributors  
 Replication Monitor displays information about Distributors on three tabs:  
  
-   **Publications** tab  
  
     This tab provides summary information for all publications of a Distributor.  
  
-   **Subscription Watch List** tab  
  
     This tab provides information about subscriptions for the selected Distributor. You can filter the list of subscriptions to see errors, warnings, and any poorly performing subscriptions. The tab also lets you to perform the following tasks: access subscription properties, access detailed information about the agent or agents associated with a subscription, reinitialize subscriptions, and validate subscriptions.  
  
     The **Subscription Watch List** tab helps answer the following questions:  
  
    -   Which subscriptions are slow?  
  
         Set options on this tab so that the grid displays subscriptions in order by their relative performance.  
  
    -   Is my replication system healthy?  
  
         The grid on this tab displays error and warning icons for any subscriptions that require your attention.  
  
     This tab is not available for Distributors that are running versions of [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] or earlier.  
  
-   **Agents** tab  
  
     This tab displays detailed information about the agents and jobs that are used by all types of replication. The tab also let you start and stop each agent and job.  
  
 Replication Monitor also provides a context menu for the Distributor node. Right-click a Distributor in the left pane to perform the following tasks:  
  
-   Add a Publisher to Replication Monitor.  
  
-   Edit Replication Monitor settings for the Distributor.  
  
-   Switch to the Publisher Group view.  
  
## Viewing Information and Performing Tasks Related to Publishers  
 Replication Monitor displays information about Publishers on three tabs:  
  
-   **Publications** tab  
  
     This tab provides summary information for all publications at a Publisher.  
  
-   **Subscription Watch List** tab  
  
     This tab is intended to display information on subscriptions from all publications available at the selected Publisher. You can filter the list of subscriptions to see errors, warnings, and any poorly performing subscriptions. The tab also allows you to: access subscription properties; access detailed information about the agent or agents associated with a subscription; reinitialize subscriptions; and validate subscriptions.  
  
     The **Subscription Watch List** tab helps answer the following questions:  
  
    -   Which subscriptions are slow?  
  
         Set options on this tab so that the grid displays subscriptions in order by their relative performance.  
  
    -   Is my replication system healthy?  
  
         The grid on this tab displays error and warning icons for any subscriptions that require your attention.  
  
     This tab is not displayed for Distributors running versions prior to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
-   **Agents** tab  
  
     This tab displays detailed information about the agents and jobs used by all types of replication. The tab also allows you to start and stop each agent and job.  
  
 For more information, see [View Information and Perform Tasks for a Publisher &#40;Replication Monitor&#41;](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
 Replication Monitor also provides a context menu for the Publisher node. Right-click a Publisher in the left pane to:  
  
-   Edit Replication Monitor settings for the Publisher  
  
-   Remove the Publisher from Replication Monitor  
  
-   View and edit agent profiles  
  
-   Connect to or disconnect from the Distributor that stores information about the Publisher  
  
## Viewing Information and Performing Tasks Related to Publications  
 Replication Monitor displays information about publications on three tabs and a number of detail windows:  
  
-   **All Subscriptions** tab  
  
     This tab displays information about all subscriptions to the selected publication. By default, this tab is sorted in priority order: errors, then warnings, then in increasing order of performance, with any poorly performing subscriptions at the top.  
  
     The **All Subscriptions** tab helps answer the following questions:  
  
    -   Which subscriptions are slow?  
  
         Set options on this tab so that the grid displays subscriptions in order by their relative performance.  
  
    -   Is my replication system healthy?  
  
         The grid on this tab displays error and warning icons for any subscriptions that require your attention.  
  
-   **Agents** tab  
  
     This tab displays information about the agents that are used by replication. This tab displays information about the following agents:  
  
    -   The Snapshot Agent, which is used by all publications.  
  
    -   The Log Reader Agent, which is used by all transactional publications.  
  
    -   The Queue Reader Agent, which is used by transactional publications enabled for queued updating subscriptions.  
  
     The tab also allows for you to perform the following tasks: access detailed information about each agent, and start and stop each agent. For information about agents that are associated with subscriptions (the Distribution Agent and Merge Agent), see the section "Viewing Information and Performing Tasks Related to Subscriptions" in this topic.  
  
-   **Warnings** tab  
  
     This tab enables you to specify warnings and alerts for agents. For more information, see [Set Thresholds and Warnings in Replication Monitor](../../../relational-databases/replication/monitor/set-thresholds-and-warnings-in-replication-monitor.md).  
  
-   **Tracer Tokens** tab (transactional replication only)  
  
     This tab allows you to measure latency, the amount of time that elapses between a transaction being committed at the Publisher and the corresponding transaction being committed at the Subscriber.  
  
     This tab helps answers the following question:  
  
    -   How long will it take a transaction committed now to reach a Subscriber in transactional replication?  
  
         View the total time for a transaction to travel through the system and also compare it to previous times.  
  
     This tab is not displayed for Distributors running [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] or earlier. For more information on tracer tokens, see [Measure Latency and Validate Connections for Transactional Replication](../../../relational-databases/replication/monitor/measure-latency-and-validate-connections-for-transactional-replication.md).  
  
-   Detail windows for the agents associated with a publication. The following agents are associated with publications:  
  
    -   The Snapshot Agent, which is used by all publications.  
  
    -   The Log Reader Agent, which is used by all transactional publications.  
  
    -   The Queue Reader Agent, which is used by transactional publications enabled for queued updating subscriptions.  
  
     Double-click an agent in Replication Monitor to access information in a detail window. In addition to the agents listed above, there are agents associated with subscriptions: the Distribution Agent for subscriptions to snapshot and transactional publications; and the Merge Agent for subscriptions to merge publications. For more information, see the section "Viewing Information and Performing Tasks Related to Subscriptions" in this topic.  
  
     The agent detail windows provide information about agent sessions, including start time, end time, duration, and the actions performed in a session. They help to answer the following question:  
  
    -   Why is an agent not running?  
  
         The error messages available provide detailed information on why an agent is not running and provide a starting point for troubleshooting issues with the agents associated with a publication.  
  
 For more information, see [View Information and Perform Tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
 Replication Monitor also provides a context menu for the publications node. Right-click a publication in the left pane to:  
  
-   Reinitialize all subscriptions to a publication  
  
-   Validate all subscriptions to a publication  
  
-   Generate a snapshot for a publication  
  
-   View and edit publication properties  
  
## Viewing Information and Performing Tasks Related to Subscriptions  
 Replication Monitor displays information about subscriptions on a number of different tabs. Double-click a subscription in Replication Monitor to access these tabs in a detail window. All of the tabs are useful in answering the question "Why is an agent not running?" The error messages available provide detailed information on why an agent is not running and provide a starting point for troubleshooting issues with the agents associated with a subscription.  
  
-   **All Subscriptions tab** and **Subscription Watch List tab.**  
  
     These tabs are described earlier in this topic.  
  
-   **Publisher to Distributor History** tab (transactional replication only)  
  
     This tab displays information on the Log Reader Agent for a publication (the tab is identical to the Log Reader Agent details window).  
  
-   **Distributor to Subscriber History** tab (snapshot replication and transactional replication)  
  
     This tab displays information on the Distribution Agent for a subscription.  
  
-   **Undistributed Commands** tab (transactional replication only)  
  
     This tab displays information about the number of commands in the distribution database that have not been delivered to the selected Subscriber, and the estimated time to deliver those commands. The tab helps answer the question, "How far behind is my subscription?" This tab is not displayed for Distributors running versions prior to SQL Server 2005.  
  
-   **Synchronization History** tab (merge replication only)  
  
     This tab displays information on the Merge Agent for a subscription. This tab helps answer the following question:  
  
    -   Why is my merge subscription slow?  
  
         This tab provides detailed statistics for each article processed during synchronization, including the amount of time spent in each processing phase (uploading changes, downloading changes, and so on). It can help pinpoint specific tables that are causing slowdowns and is the best place to troubleshoot performance issues with merge subscriptions.  
  
 For more information, see [View information and perform tasks using Replication Monitor](../../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md).
  
## Viewing Information and Performing Tasks Related to Agent Profiles  
 Replication Monitor includes a number of dialog boxes for managing agent profiles. Agent profiles are sets of parameters for an agent that determine agent behavior. For more information, see [Replication Agent Profiles](../../../relational-databases/replication/agents/replication-agent-profiles.md). The dialog boxes are:  
  
-   **Agent Profiles**  
  
     This dialog box allows you to: change the properties of profiles; create and delete profiles; specify a default profile; and specify that all agents of a specific type (such as Snapshot Agents) should use a given profile.  
  
-   **\<AgentProfileName> Properties**  
  
     This dialog box allows you to view and edit the parameter settings in a profile.  
  
-   **New Agent Profile**  
  
     This dialog box allows you to create a new profile, optionally including the values from an existing profile.  
  
## See Also  
 [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
