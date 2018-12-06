---
title: "Enhance Transactional Replication Performance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publications [SQL Server replication], design and performance"
  - "performance [SQL Server replication], transactional replication"
  - "designing databases [SQL Server], replication performance"
  - "performance [SQL Server replication], snapshot replication"
  - "snapshot replication [SQL Server], performance"
  - "subscriptions [SQL Server replication], performance considerations"
  - "agents [SQL Server replication], performance"
  - "Distribution Agent, performance"
  - "transactional replication, performance"
  - "Log Reader Agent, performance"
ms.assetid: 67084a67-43ff-4065-987a-3b16d1841565
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Enhance Transactional Replication Performance
  After considering the general performance tips described in [Enhancing General Replication Performance](enhance-general-replication-performance.md), consider these additional areas specific to transactional replication.  
  
## Database Design  
  
-   Minimize transaction size in your application design.  
  
     By default, transactional replication propagates changes according to transaction boundaries. If transactions are smaller, it is less likely that the Distribution Agent will have to resend a transaction due to network issues. If the agent is required to resend a transaction, the amount of data sent is smaller.  
  
## Distributor Configuration  
  
-   Configure the Distributor on a dedicated server.  
  
     You can reduce processing overhead on the Publisher by configuring a remote Distributor. For more information, see [Configure Distribution](../configure-distribution.md).  
  
-   Size the distribution database appropriately.  
  
     Test replication with a typical load for your system to determine how much space is required to store commands. Ensure the database is large enough to store commands without having to auto-grow frequently. For more information about changing the size of a database, see [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql).  
  
## Publication Design  
  
-   Replicate stored procedure execution when making batch updates to published tables.  
  
     If you have batch updates that occasionally affect a large number of rows at the Subscriber, you should consider updating the published table using a stored procedure and publish the execution of the stored procedure. Instead of sending an update or delete for every row affected, the Distribution Agent executes the same procedure at the Subscriber with the same parameter values. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
-   Spread articles across multiple publications.  
  
     If you cannot use the **-SubscriptionStreams** parameter (described later in this topic), consider creating multiple publications. Spreading articles across these publications allows replication to apply changes in parallel to Subscribers.  
  
## Subscription Considerations  
  
-   Use independent agents rather than shared agents if you have multiple publications on the same Publisher (this is the default for the New Publication Wizard).  
  
-   Run agents continuously instead of on very frequent schedules.  
  
     Setting the agents to run continuously rather than creating frequent schedules (such as every minute) improves replication performance, because the agent does not have to start and stop. When you set the Distribution Agent to run continuously, changes are propagated with low latency to the other servers that are connected in the topology. For more information, see:  
  
    -   [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]: [Specify Synchronization Schedules](../specify-synchronization-schedules.md)  
  
## Distribution Agent and Log Reader Agent Parameters  
  
-   To resolve accidental, one-time bottlenecks use the **-MaxCmdsInTran** parameter for the Log Reader Agent.  
  
     The **-MaxCmdsInTran** parameter specifies the maximum number of statements grouped into a transaction as the Log Reader writes commands to the distribution database. Using this parameter allows the Log Reader Agent and Distribution Agent to divide large transactions (consisting of many commands) at the Publisher into several smaller transactions when applying commands at the Subscriber. Specifying this parameter can reduce contention at the Distributor and reduce latency between the Publisher and Subscriber. Because the original transaction is applied in smaller units, the Subscriber can access rows of a large logical Publisher transaction prior to the end of the original transaction, breaking strict transactional atomicity. The default is **0**, which preserves the transaction boundaries of the Publisher. This parameter does not apply to Oracle Publishers.  
  
    > [!WARNING]  
    >  `MaxCmdsInTran` was not designed to be always turned on. It exists to work around cases where someone accidentally performed a large number of DML operations in a single transaction (causing delay in distribution of commands until the entire transaction is in distribution database, locks being held, etc.). If you routinely fall into this situation, you should review your applications and find ways to reduce the transaction size.  
  
-   Use the **-SubscriptionStreams** parameter for the Distribution Agent.  
  
     The **-SubscriptionStreams** parameter can greatly improve aggregate replication throughput. It allows multiple connections to a Subscriber to apply batches of changes in parallel, while maintaining many of the transactional characteristics present when using a single thread. If one of the connections fails to execute or commit, all connections will abort the current batch, and the agent will use a single stream to retry the failed batches. Before this retry phase completes, there can be temporary transactional inconsistencies at the Subscriber. After the failed batches are successfully committed, the Subscriber is brought back to a state of transactional consistency.  
  
     A value for this agent parameter can be specified using the **@subscriptionstreams** of [sp_addsubscription &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addsubscription-transact-sql).  
  
-   Increase the value of the **-ReadBatchSize** parameter for the Log Reader Agent.  
  
     The Log Reader Agent and Distribution Agent support batch sizes for transaction read and commit operations. Batch sizes default to 500 transactions. The Log Reader Agent reads the specific number of transactions from the log, whether or not they are marked for replication. When a large number of transactions is written to a publication database, but only a small subset of those are marked for replication, you should use the **-ReadBatchSize** parameter to increase the read batch size of the Log Reader Agent. This parameter does not apply to Oracle Publishers.  
  
-   Increase the value of the **-CommitBatchSize** parameter for the Distribution Agent.  
  
     Committing a set of transactions has a fixed overhead; by committing a larger number of transactions less frequently, the overhead is spread across a larger volume of data. However, the benefit of increasing this parameter drops off as the cost of applying changes is gated by other factors, such as the maximum I/O of the disk that contains the log. Additionally, there is a trade off to be considered: any failure that causes the Distribution Agent to start over must rollback and reapply a larger number of transactions. For unreliable networks, a lower value can result in less failures and a smaller number of transactions to rollback and reapply if a failure occurs.  
  
-   Decrease the value of the **-PollingInterval** parameter for the Log Reader Agent.  
  
     The **-PollingInterval** parameter specifies how often the transaction log of a published database is queried for transactions to replicate. The default is 5 seconds. If you decrease this value, the log is polled more frequently, which can result in lower latency for the delivery of transactions from the publication database to the distribution database. However, you should balance the need for lower latency against the increased load on the server from polling more frequently.  
  
 Agent parameters can be specified in agent profiles and on the command line. For more information, see:  
  
-   [Work with Replication Agent Profiles](../agents/replication-agent-profiles.md)  
  
-   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
-   [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md)  
  
  
