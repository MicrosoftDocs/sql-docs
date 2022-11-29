---
title: "Enhance Transactional Replication Performance | Microsoft Docs"
description: In addition to general performance tips to enhance replication performance in SQL Server, learn about additional techniques for transactional replication.
ms.custom: ""
ms.date: "03/07/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
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
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Enhance Transactional Replication Performance
[!INCLUDE[sql-asdbmi](../../../includes/applies-to-version/sql-asdbmi.md)]
  After considering the general performance tips described in [Enhancing General Replication Performance](../../../relational-databases/replication/administration/enhance-general-replication-performance.md), consider these additional areas specific to transactional replication.  
  
## Database Design  
  
-   Minimize transaction size in your application design.  
  
     By default, transactional replication propagates changes according to transaction boundaries. If transactions are smaller, the Distribution agent is less likely to resend a transaction due to network issues. If the agent is required to resend a transaction, the amount of data sent is smaller. 

  
## Distributor Configuration  
  
-   Configure the Distributor on a dedicated server.  
  
     You can reduce processing overhead on the Publisher by configuring a remote Distributor. For more information, see [Configure Distribution](../../../relational-databases/replication/configure-distribution.md).  
  
-   Size the distribution database appropriately.  
  
     Test replication with a typical load for your system to determine how much space is required to store commands. Ensure the database is large enough to store commands without having to auto-grow frequently. For more information about changing the size of a database, see [ALTER DATABASE &#40;Transact-SQL&#41;](../../../t-sql/statements/alter-database-transact-sql.md).  
  
## Publication Design  
  
-   Replicate stored procedure execution when making batch updates to published tables.  
  
     If you have batch updates that occasionally affect a large number of rows at the Subscriber, you should consider updating the published table using a stored procedure and publish the execution of the stored procedure. Instead of sending an update or delete for every row affected, the Distribution Agent executes the same procedure at the Subscriber with the same parameter values. For more information, see [Publishing Stored Procedure Execution in Transactional Replication](../../../relational-databases/replication/transactional/publishing-stored-procedure-execution-in-transactional-replication.md).  
  
-   Spread articles across multiple publications.  
  
     If you cannot use the [**-SubscriptionStreams** parameter](#subscriptionstreams), consider creating multiple publications. Spreading articles across these publications allows replication to apply changes in parallel to Subscribers.  
  
## Subscription Considerations  
  
-   Use independent agents rather than shared agents if you have multiple publications on the same Publisher (this is the default for the New Publication Wizard).  
  
-   Run agents continuously instead of on frequent schedules.  
  
     Setting the agents to run continuously rather than creating frequent schedules (such as every minute) improves replication performance, because the agent does not have to start and stop. When you set the Distribution Agent to run continuously, changes are propagated with low latency to the other servers that are connected in the topology. For more information, see:  
  
    -   [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]: [Specify Synchronization Schedules](../../../relational-databases/replication/specify-synchronization-schedules.md)  
  
## Distribution Agent and Log Reader Agent Parameters  
Agent profile parameters are often adjusted to increase throughput of the Log Reader and Distribution Agent with high traffic OLTP systems. 

Testing was conducted to determine the best values to improve performance for the Log Reader and Distribution Agent. This testing concluded that workload was a determining factor for which values worked in which situation, and as such, there isn't a single value adjustment that improves performance for every situation. 

The findings: 
- For a *Log Reader Agent* with workloads of smaller transactions (fewer than 500 commands), a higher value of **ReadBatchSize** may benefit throughput. However, for workloads with large transactions, changing this value will not improve performance. 
    - When there are multiple Log Reader Agents and multiple Distribution Agents running in parallel on the same server, a higher value of **ReadBatchSize** causes contention on the distribution database. 
- For the *Distribution Agent*
    - Increasing **CommitBatchSize** can improve throughput. The trade-off is that, if a failure occurs, the Distribution Agent must roll back and start over to reapply a larger number of transactions. 
    - Increasing **SubscriptionStreams** value does help in the overall throughput of the Distribution Agent, since multiple connections to the Subscriber apply batches of changes in parallel. However, depending on the number of processors and other metadata conditions (such as primary key, foreign keys, unique constraints, and indexes) the higher value of SubscriptionStreams might actually have an adverse effect. Additionally, if a stream fails to execute or commit, the Distribution Agent falls back to using a single stream to retry the failed batches.


For more information about this testing, see the blog [Optimizing replication agent profile parameters for better performance](/archive/blogs/sql_server_team/optimizing-replication-agent-profile-parameters-for-better-performance).


### Log Reader Agent

#### ReadBatchSize
- Increase the value of the **-ReadBatchSize** parameter for the Log Reader Agent.  
  
The Log Reader Agent and Distribution Agent support batch sizes for transaction read and commit operations. Batch sizes default to 500 transactions. The Log Reader Agent reads the specific number of transactions from the log, whether or not they are marked for replication. When a large number of transactions are written to a publication database, but only a small subset of those are marked for replication, you should use the **-ReadBatchSize** parameter to increase the read batch size of the Log Reader Agent. This parameter does not apply to Oracle Publishers.  

   - Workloads of smaller transactions (fewer than 500 commands) see an increase in how many commands are processed per second when **ReadBatchSize** is increased up to 5000. 
   - For larger workloads (transactions with 500 to 1000 commands), increasing **ReadBatchSize** has little performance improvement. Increasing **ReadBatchSize** results in a greater number of transactions written to the distribution database in one roundtrip. This increases the time transactions and commands are visible to the Distribution Agent and introduces latency to the replication process.  

#### PollingInterval
- Decrease the value of the **-PollingInterval** parameter for the Log Reader Agent.  
  
The **-PollingInterval** parameter specifies how often the transaction log of a published database is queried for transactions to replicate. The default is 5 seconds. If you decrease this value, the log is polled more frequently, which can result in lower latency for the delivery of transactions from the publication database to the distribution database. However, you should balance the need for lower latency against the increased load on the server from polling more frequently.   
  
#### MaxCmdsInTran
- To resolve accidental, one-time bottlenecks use the **–MaxCmdsInTran** parameter for the Log Reader Agent.  
  
The **–MaxCmdsInTran** parameter specifies the maximum number of statements grouped into a transaction as the Log Reader writes commands to the distribution database. Using this parameter allows the Log Reader Agent and Distribution Agent to divide large transactions (consisting of many commands) at the Publisher into several smaller transactions when applying commands at the Subscriber. Specifying this parameter can reduce contention at the Distributor and reduce latency between the Publisher and Subscriber. Because the original transaction is applied in smaller units, the Subscriber can access rows of a large logical Publisher transaction prior to the end of the original transaction, breaking strict transactional atomicity. The default is **0**, which preserves the transaction boundaries of the Publisher. This parameter does not apply to Oracle Publishers.  
  
   > [!WARNING]  
   >  **MaxCmdsInTran** was not designed to be always turned on. It exists to work around cases where someone accidentally performed a large number of DML operations in a single transaction (causing delay in distribution of commands until the entire transaction is in distribution database, locks being held, etc.). If you routinely fall into this situation,review your applications and find ways to reduce the transaction size.  
  
### Distribution Agent

#### SubscriptionStreams
- Increase the **–SubscriptionStreams** parameter for the Distribution Agent.  
  
The **–SubscriptionStreams** parameter can greatly improve aggregate replication throughput. It allows multiple connections to a Subscriber to apply batches of changes in parallel, while maintaining many of the transactional characteristics present when using a single thread. If one of the connections fails to execute or commit, all connections will abort the current batch, and the agent will use a single stream to retry the failed batches. Before this retry phase completes, there can be temporary transactional inconsistencies at the Subscriber. After the failed batches are successfully committed, the Subscriber is brought back to a state of transactional consistency.  
  
A value for this agent parameter can be specified using the `@subscriptionstreams` of [sp_addsubscription &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-addsubscription-transact-sql.md).  

For more information on implementing subscription streams, see [Navigating SQL replication subscriptionStream setting](https://blogs.msdn.microsoft.com/repltalk/2010/03/01/navigating-sql-replication-subscriptionstreams-setting).
  
### Blocking Monitor Thread

Distribution Agent maintains a blocking monitor thread that detects blocking between sessions. If the blocking monitor thread detects blocking between the sessions, Distribution Agent switches to use one session to reapply the current batch of commands that could not be applied previously.

The blocking monitor thread can detect blocking between Distribution Agent sessions. However, the blocking monitor thread cannot detect blocking in the following situations:
- One of the sessions where blocking occurs is not a Distribution Agent session.
- A session deadlock freezes the activities of Distribution Agent.

In this situation, Distribution Agent coordinates all the sessions to commit together as soon as their commands are executed. A deadlock among the sessions occurs if the following conditions are true:

- Blocking occurs between the Distribution Agent sessions and a session that is not a Distribution Agent session.
- Distribution Agent is waiting for all the sessions to complete executing their commands before Distribution Agent coordinates all the sessions to commit together.

For example, you configure the *SubscriptionStreams* parameter to 8. Session 10 through session 17 are Distribution Agent sessions. Session 18 is not a Distribution Agent session. Session 10 is blocked by session 18, and session 18 is blocked by session 11. 
Additionally, session 10 and session 11 must be committed together. However, Distribution Agent cannot commit session 10 and session 11 together because of blocking. Therefore, Distribution Agent cannot coordinate these eight sessions to commit together until session 10 and session 11 complete executing their commands.

This example results in a state in which no sessions are executing their commands. When the time that is specified in the **QueryTimeout** property is reached, Distribution Agent cancels all the sessions.

> [!Note]
> By default, the value of the **QueryTimeout** property is 5 minutes.

You may notice the following trends from the Distribution Agent performance counters during this query time-out period: 

- The value of the **Dist: Delivered Cmds/sec** performance counter is always 0.
- The value of the **Dist: Delivered Trans/sec** performance counter is always 0.
- The **Dist: Delivery Latency** performance counter reports an increase in value until the thread deadlock is resolved.

The "Replication Distribution Agent" topic in SQL Server Books Online contains the following description for the *SubscriptionStreams* parameter:
"If one of the connections fails to execute or commit, all connections will abort the current batch, and the agent will use a single stream to retry the failed batches."

Distribution Agent uses one session to retry the batch that could not be applied. After Distribution Agent successfully applies the batch, Distribution Agent resumes using multiple sessions without restarting.

#### CommitBatchSize
- Increase the value of the **-CommitBatchSize** parameter for the Distribution Agent.  
  
Committing a set of transactions has a fixed overhead; by committing a larger number of transactions less frequently, the overhead is spread across a larger volume of data.  Increasing CommitBatchSize (up to 200) can improve performance as more transactions are committed to the subscriber. However, the benefit of increasing this parameter drops off as the cost of applying changes is gated by other factors, such as the maximum I/O of the disk that contains the log. Additionally, there is a trade-off to be considered: any failure that causes the Distribution Agent to start over must roll back and reapply a larger number of transactions. For unreliable networks, a lower value can result in fewer failures and a smaller number of transactions to roll back and reapply if a failure occurs.  
  

## See more
  
[Work with Replication Agent Profiles](../../../relational-databases/replication/agents/work-with-replication-agent-profiles.md)  
[View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
[Replication Agent Executables Concepts](../../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)  
  
