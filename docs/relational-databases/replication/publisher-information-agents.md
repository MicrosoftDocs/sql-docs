---
title: "Publisher Information, Agents | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
f1_keywords: 
  - "sql13.rep.monitor.publisherinfo.commonjobs.f1"
ms.assetid: 2346c00d-c269-45a1-af14-68e7fd7ebd7e
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Publisher Information, Agents
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **Agents** tab displays information about the agents and maintenance jobs that are associated with the Publisher:  
  
-   Snapshot Agent, which is displayed for all publications.  
  
-   Log Reader Agent, which is displayed for all transactional publications.  
  
-   Queue Reader Agent, which is displayed for those transactional publications that are enabled for queued updating subscriptions.  
  
-   Maintenance jobs, displayed for all publications:  
  
    -   Reinitialize subscriptions that have data validation failures  
  
    -   Agent history cleanup: distribution  
  
    -   Replication monitoring refresher for distribution  
  
    -   Replication agents checkup  
  
    -   Distribution cleanup: distribution  
  
    -   Expired subscription cleanup  
  
 For more information about these jobs, see [Replication Agent Administration](../../relational-databases/replication/agents/replication-agent-administration.md).  
  
## Options  
 To display information about an agent or job, select from the **Agent and Job Types** drop-down menu. For more detailed information and tasks that are related to an agent or job, right-click the row for that agent or job, and then click an option on the shortcut menu. To change the way that the grid displays data, right-click the grid, and then click one of the following options:  
  
-   **Sort**: Sort on one or more columns in the **Sort Columns** dialog box.  
  
-   **Choose Columns to Show**: Select which columns to display and the order in which to display them in the **Choose Columns** dialog box.  
  
-   **Filter**: Filter rows in the grid based on column values in the **Filter Settings** dialog box.  
  
-   **Clear Filter**: Clear any filter settings for the grid.  
  
 Filter settings are specific to each grid. Column selection and sorting are applied to all grids of the same type, such as the publications grid for each Publisher.  
  
 The following sections describe the data that is displayed on this tab for each agent or job.  
  
### Snapshot Agent  
 **Status**  
 The status of the agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retry  
  
-   Running  
  
-   Completed  
  
 **Publication**  
 The name of the publication with which the agent is associated.  
  
 **Last Start Time**  
 The last time at which the agent started.  
  
 **Duration**  
 The length of time the agent has run. The time represents elapsed time if the agent is currently running, and total time if the agent has run previously.  
  
 **Last Action**  
 The last action performed during the most recent run of the agent.  
  
 **Delivery Rate**  
 The rate, in commands per second, at which initialization commands are committed in the distribution database during the most recent run of the agent.  
  
 **#Trans**  
 The number of transactions committed in the distribution database during the most recent run of the agent.  
  
 **#Cmds**  
 The number of commands committed in the distribution database during the most recent run of the agent. A command is equivalent to a data change, such as an update.  
  
### Log Reader Agent  
 **Status**  
 The status of the agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retry  
  
-   Running  
  
-   Not running  
  
 **Publication Database**  
 The name of the publication with which the agent is associated.  
  
 **Last Start Time**  
 The last time at which the agent started.  
  
 **Duration**  
 The length of time the agent has run. The time represents elapsed time if the agent is currently running, and total time if the agent has run previously.  
  
 **Last Action**  
 The last action performed during the most recent run of the agent.  
  
 **Delivery Rate**  
 The rate, in commands per second, at which changes are committed in the distribution database.  
  
 **Latency**  
 The amount of time, in seconds, that has elapsed between the most recent change being committed in the publication database, and the corresponding command being committed in the distribution database.  
  
 **#Trans**  
 The number of transactions committed in the distribution database during the most recent run of the agent.  
  
 **#Cmds**  
 The number of commands committed in the distribution database during the most recent run of the agent. A command is equivalent to a data change, such as an update.  
  
 **Avg. #Cmds**  
 The average number of commands per transaction during the most recent run of the agent.  
  
### Queue Reader Agent  
 **Status**  
 The status of the agent. The following list shows the possible status values:  
  
-   Error  
  
-   Retry  
  
-   Running  
  
-   Not running  
  
 **Distribution Database**  
 The name of the distribution database with which the agent is associated.  
  
 **Last Start Time**  
 The last time at which the agent started.  
  
 **Duration**  
 The length of time the agent has run. The time represents elapsed time if the agent is currently running and total time if the agent has run previously.  
  
 **Last Action**  
 The last action performed during the most recent run of the agent.  
  
 **Delivery Rate**  
 The rate, in commands per second, at which changes are committed in the distribution database.  
  
 **Latency**  
 The amount of time, in seconds, that has elapsed between the most recent change being committed in a subscription database, and the corresponding command being committed in the publication database.  
  
 **#Trans**  
 The number of transactions committed in the publication database during the most recent run of the agent.  
  
 **#Cmds**  
 The number of commands committed in the publication database during the most recent run of the agent. A command is equivalent to a data change, such as an update.  
  
 **Avg. #Cmds**  
 The average number of commands per transaction during the most recent run of the agent.  
  
### Maintenance Jobs  
 **Status**  
 The status of each job. The following list shows the possible status values:  
  
-   Error  
  
-   Retry  
  
-   Running  
  
-   Not running  
  
 **Job**  
 The name of the job.  
  
 **Last Start Time**  
 The last time at which the job started.  
  
 **Duration**  
 The length of time the job has run. The time represents elapsed time if the job is currently running, and total time if the job has run previously.  
  
 **Last Action**  
 The last action performed during the most recent run of the job.  
  
## See Also  
 [Start the Replication Monitor](../../relational-databases/replication/monitor/start-the-replication-monitor.md)   
 [View information and perform tasks using Replication Monitor](../../relational-databases/replication/monitor/view-information-and-perform-tasks-replication-monitor.md)   
 [Monitoring Replication](../../relational-databases/replication/monitor/monitoring-replication.md)  
  
  
