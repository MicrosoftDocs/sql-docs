---
title: "Distributor Information, Agents | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.rep.monitor.Distributor.commonjobs..f1"
ms.assetid: 5d601a64-6af0-42f9-81b1-cf0087f1c50d
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Distributor Information, Agents
  The **Agents** tab displays information about the agents and maintenance jobs that are associated with the Publisher and Subscriber.  
  
 The agents that are available in the **Agents** tab for a Distributor in Distributor view include all the agents that are available in the **Agents** tab for a Publisher. However, the **Agents** tab for a Distributor in Distributor view also includes a Distributor Agent and a Merge Agent.  
  
 For more information about the Snapshot, Log Reader, and Queue Reader agents, and maintenance jobs, see [Publisher Information, Agents](../../relational-databases/replication/publisher-information-agents.md). Notice that when you are viewing agent information on the **Agents** tab for a Distributor, Publisher information is present for the Snapshot and Log Reader agents. However, in the **Agents** tab for a Distributor in Distributor view, you can also select **Distributor Agent** and **Merge Agent**.  
  
## Options  
 The following sections describe the data that is displayed on this tab for the Distributor Agent and Merge Agent.  
  
### Distributor Agent  
 **Status**  
 The status of the agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retry  
  
-   Running  
  
-   Not Running  
  
-   Never started  
  
 **Publisher**  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance of the Publisher.  
  
 **Publication**  
 The name of the publication with which the agent is associated.  
  
 **Subscription**  
 The name of the subscription, in the form: [*SubscriberName*].[*Database*].  
  
 **Type**  
 Type of replication: push, pull, or Anonymous.  
  
 **Last Start Time**  
 The last time at which the agent started.  
  
 **Duration**  
 The length of time that the agent has run. The time represents elapsed time if the agent is currently running, and total time if the agent has run previously.  
  
 **Last Action**  
 The last action that was performed during the most recent run of the agent.  
  
 **Delivery Rate**  
 The rate, in commands per second, at which initialization commands are committed in the distribution database during the most recent run of the agent.  
  
 **Latency**  
 The time, in seconds, that has elapsed between the most recent change being committed in the publication database, and the corresponding command being committed in the distribution database.  
  
 **#Trans**  
 The number of transactions committed in the distribution database during the most recent run of the agent.  
  
 **#Cmds**  
 The number of commands committed in the distribution database during the most recent run of the agent. A command is the same as a data change, such as an update.  
  
 **Avg. #Cmds**  
 The average number of commands per transaction during the most recent run of the agent.  
  
### Merge Agent  
 **Status**  
 The status of the agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retry  
  
-   Running  
  
-   Not Running  
  
-   Never started  
  
 **Publisher**  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance of the Publisher.  
  
 **Publication**  
 The name of the publication with which the agent is associated.  
  
 **Subscription**  
 The name of the subscription, in the form: [*SubscriberName*].[*Database*].  
  
 **Type**  
 Type of replication: push, pull, or Anonymous.  
  
 **Last Start Time**  
 The last time at which the agent started.  
  
 **Duration**  
 The length of time that the agent has run. The time represents elapsed time if the agent is currently running, and total time if the agent has run previously.  
  
 **Last Action**  
 The last action that was performed during the most recent run of the agent.  
  
 **Delivery Rate**  
 The rate, in commands per second, at which changes are committed in the distribution database.  
  
 **Publisher Inserts**  
 Number of INSERT commands applied at the Publisher.  
  
 **Publisher Updates**  
 Number of UPDATE commands applied at the Publisher.  
  
 **Publisher Deletes**  
 Number of DELETE commands applied at the Publisher.  
  
 **Publisher Conflicts**  
 The number of conflicts occurring at the Publisher during the merge process.  
  
 **Subscriber Inserts**  
 Number of INSERT commands applied at the Subscriber.  
  
 **Subscriber Updates**  
 Number of UPDATE commands applied at the Subscriber.  
  
 **Subscriber Deletes**  
 Number of DELETE commands applied at the Subscriber.  
  
 **Subscriber Conflicts**  
 The number of conflicts occurring at the Subscriber during the merge process.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [View Information and Perform Tasks for a Publisher &#40;Replication Monitor&#41;](../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-a-publisher-replication-monitor.md)   
 [View Information and Perform Tasks for the Agents Associated With a Publication &#40;Replication Monitor&#41;](../../relational-databases/replication/monitor/view-information-and-perform-tasks-for-publication-agents.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication-overview.md)  
  
  