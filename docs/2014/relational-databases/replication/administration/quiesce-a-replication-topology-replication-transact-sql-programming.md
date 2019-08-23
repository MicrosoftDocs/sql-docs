---
title: "Quiesce a Replication Topology (Replication Transact-SQL Programming) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "administering replication, quiescing"
  - "quiesce [SQL Server replication]"
  - "transactional replication, backup and restore"
ms.assetid: 7626d575-9994-47be-b772-5b6f1b7ef7ca
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Quiesce a Replication Topology (Replication Transact-SQL Programming)
  *Quiescing* a system involves stopping activity on published tables at all nodes and ensuring that each node has received all changes from all other nodes. This topic explains how to quiesce a replication topology, which is required for a number of administrative tasks, and how to ensure that a node has received all changes from other nodes.  
  
### To quiesce a transactional replication topology with read-only subscriptions  
  
1.  Stop activity on all published tables at the Publisher.  
  
2.  At the Publisher on the publication database, execute [sp_posttracertoken &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-posttracertoken-transact-sql).  
  
3.  At the Publisher on the publication database, execute [sp_helptracertokenhistory](/sql/relational-databases/system-stored-procedures/sp-helptracertokenhistory-transact-sql).  
  
4.  Ensure that each Subscriber has received the tracer token.  
  
### To quiesce a transactional replication topology with updatable subscriptions  
  
1.  Stop activity on all published tables at the Publisher and all Subscribers.  
  
2.  If any Subscribers use queued updating subscriptions:  
  
    1.  If the Queue Reader Agent is not running in continuous mode, run the agent. For more information about running agents, see [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md) or [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
    2.  To verify that the queue is empty, execute [sp_replqueuemonitor](/sql/relational-databases/system-stored-procedures/sp-replqueuemonitor-transact-sql) at each Subscriber.  
  
3.  At the Publisher on the publication database, execute [sp_posttracertoken](/sql/relational-databases/system-stored-procedures/sp-posttracertoken-transact-sql).  
  
4.  At the Publisher on the publication database, execute [sp_helptracertokenhistory](/sql/relational-databases/system-stored-procedures/sp-helptracertokenhistory-transact-sql).  
  
5.  Ensure that each Subscriber has received the tracer token.  
  
### To quiesce a peer-to-peer transactional replication topology  
  
1.  Stop activity on all published tables at all nodes.  
  
2.  Execute [sp_requestpeerresponse](/sql/relational-databases/system-stored-procedures/sp-requestpeerresponse-transact-sql) on each publication database in the topology.  
  
3.  If the Log Reader Agent or Distribution Agent is not running in continuous mode, run the agent. The Log Reader Agent must be started before the Distribution Agent. For more information about running agents, see [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md) or [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
4.  Execute [sp_helppeerresponses](/sql/relational-databases/system-stored-procedures/sp-helppeerresponses-transact-sql) on each publication database in the topology. Ensure that the result set contains responses from each of the other nodes.  
  
### To ensure a peer-to-peer node has received all prior changes  
  
1.  Execute [sp_requestpeerresponse](/sql/relational-databases/system-stored-procedures/sp-requestpeerresponse-transact-sql) on the publication database at the node you are checking.  
  
2.  If the Log Reader Agent or Distribution Agent is not running in continuous mode, run the agent. The Log Reader Agent must be started before the Distribution Agent. For more information about running agents, see [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md) or [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
3.  Execute [sp_helppeerresponses](/sql/relational-databases/system-stored-procedures/sp-helppeerresponses-transact-sql) on the publication database at the node you are checking. Ensure that the result set contains responses from each of the other nodes.  
  
### To quiesce a merge replication topology  
  
1.  Stop activity on all published tables at the Publisher and at all Subscribers.  
  
2.  Run the Merge Agent for each subscription two times: synchronize all subscriptions once and then synchronize each subscription a second time. This ensures that all changes are replicated to all nodes. For more information about running agents, see [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md) or [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../agents/start-and-stop-a-replication-agent-sql-server-management-studio.md).  
  
    > [!NOTE]  
    >  If conflicts occur during synchronization, it is possible that changes required by conflict resolution will not be propagated to all nodes after running the Merge Agent two times.  
  
## See Also  
 [Administer a Peer-to-Peer Topology &#40;Replication Transact-SQL Programming&#41;](administer-a-peer-to-peer-topology-replication-transact-sql-programming.md)   
 [Measure Latency and Validate Connections for Transactional Replication](../monitor/measure-latency-and-validate-connections-for-transactional-replication.md)  
  
  
