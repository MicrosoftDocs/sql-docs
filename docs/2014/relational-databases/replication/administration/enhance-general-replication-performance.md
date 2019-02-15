---
title: "Enhance General Replication Performance | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "publications [SQL Server replication], design and performance"
  - "designing databases [SQL Server], replication performance"
  - "Snapshot Agent, performance"
  - "snapshots [SQL Server replication], performance considerations"
  - "merge replication performance [SQL Server replication]"
  - "snapshot replication [SQL Server], performance"
  - "subscriptions [SQL Server replication], performance considerations"
  - "agents [SQL Server replication], performance"
  - "performance [SQL Server replication], general considerations"
  - "transactional replication, performance"
ms.assetid: 895b1ad7-ffb9-4a5c-bda6-e1dfbd56d9bf
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Enhance General Replication Performance
  You can enhance the general performance for all types of replication in your application and on your network by using the guidelines described in this topic.  
  
## Server and Network  
  
-   Set the minimum and maximum amount of memory allocated to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)].  
  
     By default, the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] changes its memory requirements dynamically based on available system resources. To avoid low memory availability during replication activities, use the **min server memory** option to set the minimum available memory. To avoid having the operating system page to disc for memory, you can also set a maximum amount of memory with the **max server memory** option. For more information, see [Server Memory Server Configuration Options](../../../database-engine/configure-windows/server-memory-server-configuration-options.md).  
  
-   Ensure proper allocation of database data files and log files. Use a separate disk drive for the transaction log for all databases involved in replication.  
  
     You can decrease the time it takes to write transactions by storing the log files on a disk drive different than the one used to store the database. You can mirror that drive, using a Redundant Array of Inexpensive Disks (RAID)-1, if you require fault tolerance. Use RAID 0 or 0+1 (depending on your need for fault tolerance) for other database files. This is a good practice regardless of whether or not replication is being used.  
  
-   Consider adding memory to servers used in replication, particularly the Distributor.  
  
-   Use multiprocessor computers.  
  
     Replication agents can take advantage of additional processors on the server. If you are running at high CPU usage, consider installing a faster CPU or multiple CPUs.  
  
-   Use a fast network.  
  
     The network can be a significant performance bottleneck, particularly for transactional replication. The propagation of changes to Subscribers can be significantly enhanced by using a fast network of 100 megabits per second (Mbps) or faster. If the network is slow, specify appropriate network settings and agent parameters.  
  
## Database Design  
  
-   Follow best practices for database design.  
  
     A replicated database generally benefits from the same performance optimizations as a non-replicated database. However, indexes should be used with caution at the Subscriber: the primary key column at the Subscriber should be indexed, but additional indexes can affect insert, update, and delete performance.  
  
-   Consider setting the READ_COMMITTED_SNAPSHOT database option.  
  
     To help reduce contention between user activity and replication agent activity, set this option for the publication and subscription databases:  
  
    ```  
    ALTER DATABASE AdventureWorks  
    SET READ_COMMITTED_SNAPSHOT ON  
    ```  
  
     For more information, see [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql).  
  
-   Be cautious with application logic in triggers.  
  
     Business logic in user-defined triggers at the Subscriber can slow down the replication of changes to the Subscriber:  
  
    -   For transactional replication, it can be more efficient to include this logic in custom stored procedures used to apply the replicated commands. For more information, see [Specify How Changes Are Propagated for Transactional Articles](../transactional/transactional-articles-specify-how-changes-are-propagated.md).  
  
    -   For merge replication, it can be more efficient to use business logic handlers. For more information, see [Execute Business Logic During Merge Synchronization](../merge/execute-business-logic-during-merge-synchronization.md).  
  
     If you use triggers to maintain referential integrity in tables published for merge replication, specify the processing order of tables to reduce the number of retries required for the Merge Agent. For more information, see [Specify Merge Replication properties](../publish/specify-merge-replication-properties.md).  
  
-   Limit the use of Large Object (LOB) data types.  
  
     LOBs require more storage space and processing than other column data types. Do not include these columns in articles unless necessary for your application. The data types `text`, `ntext`, and `image` are deprecated. If you do include LOBs, we recommend that you use the data types `varchar(max)`, `nvarchar(max)`, `varbinary(max)`, respectively.  
  
     For transactional replication, consider using the Distribution Agent profile called **Distribution Profile for OLEDB streaming**. For more information, see [Replication Agent Profiles](../agents/replication-agent-profiles.md).  
  
## Publication Design  
  
-   Publish only the data required.  
  
     Because replication is easy to set up, there is a tendency to publish more data than is actually required. This can consume additional resources within the distribution databases and snapshot files, and can lower the throughput for required data. Avoid publishing unnecessary tables and consider updating publications less frequently.  
  
-   Minimize conflicts through publication design and application behavior.  
  
     The following types of replication allow data to be changed at Subscribers: merge replication, transactional replication with updatable subscriptions, and peer-to-peer transactional replication. Merge replication and transactional replication with updatable subscriptions support data conflicts if a given row is updated at more than one node between synchronizations. Peer-to-peer replication does not support data conflicts; data changes must be partitioned. Regardless of the type of replication used, we recommend that you partition changes whenever possible, because this reduces the processing required for conflict detection and resolution.  
  
     Changes can be partitioned by publishing subsets of data to each Subscriber or by having an application direct changes for a given row to a given node:  
  
    -   Merge replication supports publishing subsets of data using parameterized filters with a single publication. For more information, see [Parameterized Row Filters](../merge/parameterized-filters-parameterized-row-filters.md).  
  
    -   Transactional replication supports publishing subsets of data using static filters with multiple publications. For more information, see [Filter Published Data](../publish/filter-published-data.md).  
  
-   Use row filters judiciously.  
  
     When a transactional publication includes one or more articles that use row filters, the Log Reader Agent must apply the filter to each row affected by an update to the table as it scans the transaction log. The throughput of the Log Reader Agent is therefore affected.  
  
     Similarly, merge replication must evaluate changed or deleted rows to determine which Subscribers should receive those rows. When row filters are employed to reduce the data required at a Subscriber, this processing is more complex and can be slower than when you publish all rows in a table. Consider carefully the tradeoff between reduced storage requirements at each Subscriber and the need for achieving maximum throughput. For more information on filtering, see [Filter Published Data](../publish/filter-published-data.md).  
  
## Subscription Considerations  
  
-   Use pull subscriptions when there are a large number of Subscribers.  
  
     The Distribution Agent and Merge Agent run at the Distributor for push subscriptions, and at Subscribers for pull subscriptions. Using pull subscriptions can improve performance by moving agent processing from the Distributor to Subscribers. For more information, see [Subscribe to Publications](../subscribe-to-publications.md).  
  
-   Consider reinitialization of the subscription if Subscribers are too far behind.  
  
     When large amounts of changes need to be sent to Subscribers, reinitializing them with a new snapshot might be faster than using replication to move the individual changes. For more information, see [Reinitialize Subscriptions](../reinitialize-subscriptions.md).  
  
     For transactional replication, Replication Monitor displays on the **Undistributed Commands** tab information about: the number of transactions in the distribution database that have not yet been distributed to a Subscriber; and the estimated time for distributing these transactions. For more information, see [ View Information and Perform Tasks using Replication Monitor](../monitor/view-information-and-perform-tasks-replication-monitor.md).  
  
## Snapshot Considerations  
  
-   Run the Snapshot Agent only when necessary and at off-peak times.  
  
     The Snapshot Agent bulk copies data from the published table on the Publisher to a file in the snapshot folder on the Distributor. Generating a snapshot can be a resource intensive process and is best scheduled during off-peak times.  
  
-   Use a native mode snapshot unless a character mode snapshot is required.  
  
     Use the default native mode snapshot for all Subscribers except non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Subscribers and Subscribers running [!INCLUDE[ssEW](../../../includes/ssew-md.md)], which require a character mode snapshot.  
  
-   Use a single snapshot folder for a publication.  
  
     When specifying the publication properties related to snapshot location, you can choose to generate snapshot files to the default snapshot folder, to an alternate snapshot folder, or to both. Generating snapshot files in both locations requires additional disk space and more processing when the Snapshot Agent runs.  
  
-   Place the snapshot folder on a drive local to the Distributor that is not used to store database or log files.  
  
     The Snapshot Agent performs a sequential write of data to the snapshot folder. Placing the snapshot folder on a separate drive from any database or log files reduces contention among the disks and helps the snapshot process complete faster.  
  
-   When you create the subscription database at the Subscriber, consider specifying a recovery model of simple or bulk-logged. This allows minimal logging of the bulk inserts performed during the application of the snapshot at the Subscriber. After the snapshot has been applied to the subscription database, you can change to a different recovery model if necessary (replicated databases can use any of the recovery models). For more information about selecting a recovery model, see [Restore and Recovery Overview &#40;SQL Server&#41;](../../backup-restore/restore-and-recovery-overview-sql-server.md).  
  
-   Consider using the alternate snapshot folder and compressed snapshots on removable media for low-bandwidth networks.  
  
     Compressing snapshot files in the alternate snapshot folder can reduce snapshot disk storage requirements and make it easier to transfer snapshot files on removable media.  
  
     Compressed snapshots can, in some cases, improve the performance of transferring snapshot files across the network. However, compressing the snapshot requires additional processing by the Snapshot Agent when generating the snapshot files, and by the Distribution Agent or Merge Agent when applying the snapshot files. This may slow down snapshot generation and increase the time it takes to apply a snapshot in some cases. Additionally, compressed snapshots cannot be resumed if a network failure occurs; therefore they are not suitable for unreliable networks. Consider these tradeoffs carefully when using compressed snapshots across a network. For more information, see [Alternate Snapshot Folder Locations](../alternate-snapshot-folder-locations.md) and [Compressed Snapshots](../compressed-snapshots.md).  
  
-   Consider initializing a subscription manually.  
  
     In some scenarios, such as those involving large initial datasets, it is preferable to initialize a subscription using a method other than a snapshot. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../initialize-a-transactional-subscription-without-a-snapshot.md).  
  
## Agent Parameters  
  
-   Reduce the verbose levels of replication agents except during initial testing, monitoring, or debugging.  
  
     Reduce the **-HistoryVerboseLevel** parameter and the **-OutputVerboseLevel** parameter of the Distribution Agents or Merge Agents. This reduces the number of new rows inserted to track agent history and output. Instead, previous history messages with the same status are updated to the new history information. Increase the verbose levels for testing, monitoring, and debugging so that you have as much information about agent activity as possible.  
  
-   Use the **-MaxBCPThreads** parameter of the Snapshot Agent, Merge Agent, and Distribution Agent (the number of threads specified should not exceed the number of processors on the computer). This parameter specifies the number of bulk copy operations that can be performed in parallel when the snapshot is created and applied.  
  
-   Use the **-UseInprocLoader** parameter of the Distribution Agent and the Merge Agent (this parameter cannot be used if published tables include XML columns). This parameter causes the agent to use the BULK INSERT command when the snapshot is applied.  
  
 Agent parameters can be specified in agent profiles and on the command line. For more information, see:  
  
-   [Work with Replication Agent Profiles](../agents/work-with-replication-agent-profiles.md)  
  
-   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
-   [Replication Agent Executables Concepts](../concepts/replication-agent-executables-concepts.md).  
  
  
