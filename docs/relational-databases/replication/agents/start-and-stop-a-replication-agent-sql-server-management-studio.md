---
title: "Start & stop a Replication Agent (SSMS)"
description: Learn how to start an stop a Replication Agent in SQL Server Management Studio and Replication Monitor.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "agents [SQL Server replication], stopping"
  - "agents [SQL Server replication], starting"
monikerRange: ">=sql-server-2016"
---
# Start and Stop a Replication Agent (SQL Server Management Studio)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  Start and stop agents from the **Jobs** folder and the **Replication** folder in [!INCLUDE [msCoName](../../../includes/msconame-md.md)] [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] and from Replication Monitor. Start and stop the following agents and jobs:  
  
-   The Snapshot Agent, which is used by all publications.  
  
-   The Log Reader Agent, which is used by all transactional publications.  
  
-   The Queue Reader Agent, which is used by transactional publications enabled for queued updating subscriptions.  
  
-   The Distribution Agent, which synchronizes subscriptions to transactional and snapshot publications.  
  
-   The Merge Agent, which synchronizes subscriptions to merge publications.  
  
-   Replication maintenance jobs.  
  
 For more information about starting the Merge Agent and the Distribution Agent, see [Synchronize a Push Subscription](../../../relational-databases/replication/synchronize-a-push-subscription.md) and [Synchronize a Pull Subscription](../../../relational-databases/replication/synchronize-a-pull-subscription.md). For more information about maintenance jobs, see [Run Replication Maintenance Jobs (SQL Server Management Studio)](../../../relational-databases/replication/administration/run-replication-maintenance-jobs-sql-server-management-studio.md).  
  
 For information about starting Replication Monitor, see [Start the Replication Monitor](../../../relational-databases/replication/monitor/start-the-replication-monitor.md).  
  
### <a id="to-start-and-stop-a-snapshot-agent-or-log-reader-agent-from-management-studio"></a> Start and stop a Snapshot Agent or Log Reader Agent from Management Studio
  
1. Connect to the Publisher in [!INCLUDE [ssManStudio](../../../includes/ssmanstudio-md.md)], and then expand the server node and the **Replication** folder.  
  
1. Expand the **Local Publications** folder, and then right-click a publication.  
  
1. Select **View Snapshot Agent Status** or **View Log Reader Agent Status**.  
  
1. Select **Start** or **Stop**.  
  
### <a id="to-start-and-stop-a-queue-reader-agent-from-management-studio"></a> Start and stop a Queue Reader Agent from Management Studio
  
1. Connect to the Distributor in [!INCLUDE [ssManStudio](../../../includes/ssmanstudio-md.md)], and then expand the server node.  
  
1. Expand the **SQL Server Agent** folder, and then expand the **Jobs** folder.  
  
1. Right-click the job for the agent, and then select **Start Job** or **Stop Job**. The name of the job for the Queue Reader Agent is in the form **[\<Distributor>].\<integer>**.  
  
### <a id="to-start-and-stop-a-snapshot-agent-log-reader-agent-or-queue-reader-agent-from-replication-monitor"></a> Start and stop a Snapshot Agent, Log Reader Agent, or Queue Reader Agent from Replication Monitor
  
1. Expand a Publisher group in the left pane, expand a Publisher, and then select a publication.  
  
1. Select the **Agents** tab.  
  
1. Right-click an agent, and then select **Start Agent** or **Stop Agent**.  
  
## Related content

- [Monitoring Replication](../../../relational-databases/replication/monitor/monitoring-replication.md)
- [Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)
- [Replication Agents Overview](../../../relational-databases/replication/agents/replication-agents-overview.md)
