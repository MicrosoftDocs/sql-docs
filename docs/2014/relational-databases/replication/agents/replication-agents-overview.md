---
title: "Replication Agents Overview | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "Distribution Agent"
  - "agents [SQL Server replication]"
  - "Queue Reader Agent, about Queue Reader Agent"
  - "Queue Reader Agent"
  - "Merge Agent, about Merge Agent"
  - "Log Reader Agent, about Log Reader Agent"
  - "replication [SQL Server], agents and profiles"
  - "Log Reader Agent"
  - "Distribution Agent, about Distribution Agent"
  - "agents [SQL Server replication], about agents"
  - "Merge Agent"
  - "Snapshot Agent, about Snapshot Agent"
  - "Snapshot Agent"
ms.assetid: a35ecd7d-f130-483c-87e3-ddc8927bb91b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Replication Agents Overview
  Replication uses a number of standalone programs, called agents, to carry out the tasks associated with tracking changes and distributing data. By default, replication agents run as jobs scheduled under [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent, and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent must be running for the jobs to run. Replication agents can also be run from the command line and by applications that use Replication Management Objects (RMO). Replication agents can be administered from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication Monitor and [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].  
  
## SQL Server Agent  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent hosts and schedules the agents used in replication and provides an easy way to run replication agents. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent also controls and monitors operations outside of replication. For more information, see [Configure SQL Server Agent](../../../ssms/agent/sql-server-agent.md).  
  
> [!IMPORTANT]  
>  By default, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service is disabled when [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is installed unless you explicitly choose to autostart the service during installation. For more information about starting the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service, see [Start, Stop, or Pause the SQL Server Agent Service](../../../ssms/agent/start-stop-or-pause-the-sql-server-agent-service.md).  
  
## Snapshot Agent  
 The Snapshot Agent is typically used with all types of replication. It prepares schema and initial data files of published tables and other objects, stores the snapshot files, and records information about synchronization in the distribution database. The Snapshot Agent runs at the Distributor. For more information, see [Replication Snapshot Agent](replication-snapshot-agent.md).  
  
## Log Reader Agent  
 The Log Reader Agent is used with transactional replication. It moves transactions marked for replication from the transaction log on the Publisher to the distribution database. Each database published using transactional replication has its own Log Reader Agent that runs on the Distributor and connects to the Publisher (the Distributor can be on the same computer as the Publisher). For more information, see [Replication Log Reader Agent](replication-log-reader-agent.md).  
  
## Distribution Agent  
 The Distribution Agent is used with snapshot replication and transactional replication. It applies the initial snapshot to the Subscriber and moves transactions held in the distribution database to Subscribers. The Distribution Agent runs at either the Distributor for push subscriptions or at the Subscriber for pull subscriptions. For more information, see [Replication Distribution Agent](replication-distribution-agent.md).  
  
## Merge Agent  
 The Merge Agent is used with merge replication. It applies the initial snapshot to the Subscriber and moves and reconciles incremental data changes that occur. Each merge subscription has its own Merge Agent that connects to both the Publisher and the Subscriber and updates both. The Merge Agent runs at either the Distributor for push subscriptions or the Subscriber for pull subscriptions. By default, the Merge Agent uploads changes from the Subscriber to the Publisher and then downloads changes from the Publisher to the Subscriber. For more information, see [Replication Merge Agent](replication-merge-agent.md).  
  
## Queue Reader Agent  
 The Queue Reader Agent is used with transactional replication with the queued updating option. The agent runs at the Distributor and moves changes made at the Subscriber back to the Publisher. Unlike the Distribution Agent and the Merge Agent, only one instance of the Queue Reader Agent exists to service all Publishers and publications for a given distribution database. For more information about the Queue Reader Agent, see [Replication Queue Reader Agent](replication-queue-reader-agent.md). For more information about updatable subscriptions, see [Updatable Subscriptions for Transactional Replication](../transactional/updatable-subscriptions-for-transactional-replication.md).  
  
## Replication Maintenance Jobs  
 Replication has a number of maintenance jobs that perform scheduled and on-demand maintenance. For more information, see [Replication Agent Administration](replication-agent-administration.md).  
  
## See Also  
 [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](start-and-stop-a-replication-agent-sql-server-management-studio.md)   
 [Run Replication Maintenance Jobs &#40;SQL Server Management Studio&#41;](../administration/run-replication-maintenance-jobs-sql-server-management-studio.md)   
 [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md)   
 [Replication Agent Administration](replication-agent-administration.md)  
  
  
