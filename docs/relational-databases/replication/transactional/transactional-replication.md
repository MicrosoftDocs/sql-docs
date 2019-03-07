---
title: "Transactional Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "transactional replication, about transactional replication"
  - "transactional replication"
ms.assetid: 3ca82fb9-81e6-4c3c-94b3-b15f852b18bd
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Transactional Replication
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Transactional replication typically starts with a snapshot of the publication database objects and data. As soon as the initial snapshot is taken, subsequent data changes and schema modifications made at the Publisher are usually delivered to the Subscriber as they occur (in near real time). The data changes are applied to the Subscriber in the same order and within the same transaction boundaries as they occurred at the Publisher; therefore, within a publication, transactional consistency is guaranteed.  
  
 Transactional replication is typically used in server-to-server environments and is appropriate in each of the following cases:  
  
-   You want incremental changes to be propagated to Subscribers as they occur.  
  
-   The application requires low latency between the time changes are made at the Publisher and the changes arrive at the Subscriber.  
  
-   The application requires access to intermediate data states. For example, if a row changes five times, transactional replication allows an application to respond to each change (such as firing a trigger), not simply the net data change to the row.  
  
-   The Publisher has a very high volume of insert, update, and delete activity.  
  
-   The Publisher or Subscriber is a non-[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, such as Oracle.  
  
 By default, Subscribers to transactional publications should be treated as read-only, because changes are not propagated back to the Publisher. However, transactional replication does offer options that allow updates at the Subscriber.  
  
##  <a name="HowWorks"></a> How Transactional Replication Works  
 Transactional replication is implemented by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Snapshot Agent, Log Reader Agent, and Distribution Agent. The Snapshot Agent prepares snapshot files containing schema and data of published tables and database objects, stores the files in the snapshot folder, and records synchronization jobs in the distribution database on the Distributor.  
  
 The Log Reader Agent monitors the transaction log of each database configured for transactional replication and copies the transactions marked for replication from the transaction log into the distribution database, which acts as a reliable store-and-forward queue. The Distribution Agent copies the initial snapshot files from the snapshot folder and the transactions held in the distribution database tables to Subscribers.  
  
 Incremental changes made at the Publisher flow to Subscribers according to the schedule of the Distribution Agent, which can run continuously for minimal latency, or at scheduled intervals. Because changes to the data must be made at the Publisher (when transactional replication is used without immediate updating or queued updating options), update conflicts are avoided. Ultimately, all Subscribers will achieve the same values as the Publisher. If immediate updating or queued updating options are used with transactional replication, updates can be made at the Subscriber, and with queued updating, conflicts might occur.  
  
 The following illustration shows the principal components of transactional replication.  
  
 ![Transactional replication components and data flow](../../../relational-databases/replication/transactional/media/trnsact.gif "Transactional replication components and data flow")  
  
##  <a name="Dataset"></a> Initial Dataset  
 Before a new transactional replication Subscriber can receive incremental changes from a Publisher, the Subscriber must contain tables with the same schema and data as the tables at the Publisher. The initial dataset is typically a snapshot that is created by the Snapshot Agent and distributed and applied by the Distribution Agent. The initial dataset can also be supplied through a backup or other means, such as [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Integration Services.  
  
 When snapshots are distributed and applied to Subscribers, only those Subscribers waiting for initial snapshots are affected. Other Subscribers to that publication (those that have already been initialized) are unaffected.  
  
## Concurrent Snapshot Processing  
 Snapshot replication places shared locks on all tables published as part of replication for the duration of snapshot generation. This can prevent updates from being made on the publishing tables. Concurrent snapshot processing, the default with transactional replication, does not hold the share locks in place during the entire snapshot generation, which allows users to continue working uninterrupted while replication creates initial snapshot files.  
  
##  <a name="SnapshotAgent"></a> Snapshot Agent  
 The procedures by which the Snapshot Agent implements the initial snapshot in transactional replication are the same procedures used in snapshot replication (except as outlined above with regard to concurrent snapshot processing).  
  
 After the snapshot files have been generated, you can view them in the snapshot folder using [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Explorer.  
  
##  <a name="LogReaderAgent"></a> Modifying Data and the Log Reader Agent  
 The Log Reader Agent runs at the Distributor; it typically runs continuously, but can also run according to a schedule you establish. When executing, the Log Reader Agent first reads the publication transaction log (the same database log used for transaction tracking and recovery during regular [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Database Engine operations) and identifies any INSERT, UPDATE, and DELETE statements, or other modifications made to the data in transactions that have been marked for replication. Next, the agent copies those transactions in batches to the distribution database at the Distributor. The Log Reader Agent uses the internal stored procedure **sp_replcmds** to get the next set of commands marked for replication from the log. The distribution database then becomes the store-and-forward queue from which changes are sent to Subscribers. Only committed transactions are sent to the distribution database.  
  
 After the entire batch of transactions has been written successfully to the distribution database, it is committed. Following the commit of each batch of commands to the Distributor, the Log Reader Agent calls **sp_repldone** to mark where replication was last completed. Finally, the agent marks the rows in the transaction log that are ready to be purged. Rows still waiting to be replicated are not purged.  
  
 Transaction commands are stored in the distribution database until they are propagated to all Subscribers or until the maximum distribution retention period has been reached. Subscribers receive transactions in the same order in which they were applied at the Publisher.  
  
##  <a name="DistributionAgent"></a> Distribution Agent  
 The Distribution Agent runs at the Distributor for push subscriptions and at the Subscriber for pull subscriptions. The agent moves transactions from the distribution database to the Subscriber. If a subscription is marked for validation, the Distribution Agent also checks whether data at the Publisher and Subscriber match.  

## Publication types 
Transactional replication offers four publication types:  
  
|Publication Type|Description|  
|----------------------|-----------------|  
|Standard transactional publication|Appropriate for topologies in which all data at the Subscriber is read-only (transactional replication does not enforce this at the Subscriber).<br /><br /> Standard transactional publications are created by default when using Transact-SQL or Replication Management Objects (RMO). When using the New Publication Wizard, they are created by selecting **Transactional publication** on the **Publication Type** page.<br /><br /> For more information about creating publications, see [Publish Data and Database Objects](../../../relational-databases/replication/publish/publish-data-and-database-objects.md).|  
|Transactional publication with updatable subscriptions|The characteristics of this publication type are:<br /><br /> -Each location has identical data, with one Publisher and one Subscriber. <br /> -It is possible to update rows at the Subscriber<br /> -This topology is best suited for server environments requiring high availability and read scalability.<br /><br />For more information, see [Updatable Subscriptions](../../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md).|  
|Peer-to-peer topology|The characteristics of this publication type are:<br /> - Each location has identical data and acts as both a Publisher and Subscriber.<br /> - The same row can be changed only at one location at a time.<br /> - Supports [conflict detection](../../../relational-databases/replication/transactional/peer-to-peer-conflict-detection-in-peer-to-peer-replication.md)  <br />- This topology is best suited for server environments requiring high availability and read scalability.<br /><br />For more information, see [Peer-to-Peer Transactional Replication](../../../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md).|  
|Bidirectional transactional replication|The characteristics of this publication type are:<br />Bidirectional replication is similar to Peer-to-Peer replication, however, it does not provide conflict resolution. Additionally, bidirectional replication is limited to 2 servers. <br /><br /> For more information, see [Bidirectional Transactional Replication](../../../relational-databases/replication/transactional/bidirectional-transactional-replication.md) |  
  
  
