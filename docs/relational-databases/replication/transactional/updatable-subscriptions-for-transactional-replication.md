---
title: Updatable Subscriptions (Transactional)
description: Learn how updatable subscriptions for SQL Server transactional replication propagate Subscriber changes to the Publisher, and how to switch between update modes.
author: "MashaMSFT"
ms.author: "mathoma"
ms.date: 09/25/2024
ms.service: sql
ms.subservice: replication
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "transactional replication, updatable subscriptions"
  - "updatable subscriptions, about updatable subscriptions"
  - "queued updating subscriptions [SQL Server replication]"
  - "immediate updating subscriptions"
  - "subscriptions [SQL Server replication], updatable"
  - "updatable subscriptions"
---
# Updatable subscriptions - For transactional replication
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

    
> [!NOTE]  
>  This feature remains supported in versions of [!INCLUDE[ssNoVersion_md](../../../includes/ssnoversion-md.md)] from 2012 through 2016. [!INCLUDE[ssNoteDepFutureAvoid](../../../includes/ssnotedepfutureavoid-md.md)]  
  
 Transactional replication supports updates at Subscribers through updatable subscriptions and peer-to-peer replication. The following are the two types of updatable subscriptions:  
  
-   Immediate updating. The Publisher and Subscriber must be connected to update data at the Subscriber.  
  
-   Queued updating The Publisher and Subscriber don't have to be connected to update data at the Subscriber. You can update data while the Subscriber or Publisher is offline.  
  
 When you update data at a Subscriber, the update first goes to the Publisher and then to other Subscribers. If you use immediate updating, the changes go immediately by using the two-phase commit protocol. If you use queued updating, the changes go into a queue. The queued transactions go to the Publisher asynchronously when network connectivity is available. Because the updates go to the Publisher asynchronously, the same data might be updated by the Publisher or by another Subscriber and conflicts can occur when applying the updates. The system detects and resolves conflicts according to a conflict resolution policy that you set when creating the publication.  
  
 If you create a transactional publication with updatable subscriptions in the New Publication Wizard, both immediate updating and queued updating are enabled. If you create a publication with stored procedures, you can enable one or both options. When you create a subscription to the publication, you specify which update mode to use. You can then switch between update modes if necessary. For more information, see the following section "Switching between Update Modes".  
  
 To enable updatable subscriptions for transactional publications, see [Enable Updating Subscriptions for Transactional Publications](../../../relational-databases/replication/publish/enable-updating-subscriptions-for-transactional-publications.md).  
  
 To create updatable subscriptions for transactional publications, see [Create an Updatable Subscription to a Transactional Publication (Management Studio)](../../../relational-databases/replication/publish/create-an-updatable-subscription-to-a-transactional-publication.md). 
  
## Switching between update modes  
 When you use updatable subscriptions, you can specify one update mode for a subscription and switch to the other mode if the application requires it. For example, you can specify that a subscription use immediate updating, but switch to queued updating if a system failure results in the loss of network connectivity.  
  
> [!NOTE]  
>  Replication doesn't switch automatically between update modes. Set the update mode through SQL Server Management Studio or call [sp_setreplfailovermode &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-setreplfailovermode-transact-sql.md) in your application to switch between modes.  
  
 If you switch from immediate updating to queued updating, you can't switch back to immediate updating until the Subscriber and Publisher are connected and the Queue Reader Agent applies all pending messages in the queue to the Publisher.  
  
 **To switch between update modes**  
  
 To switch between updating modes, enable the publication and subscription for both update modes, and then switch between them if necessary. For more information, see  
[Switch Between Update Modes for an Updatable Transactional Subscription](../../../relational-databases/replication/administration/switch-between-update-modes-for-an-updatable-transactional-subscription.md).  
  
### Considerations for using updatable subscriptions  
  
-   After you enable a publication for updating subscriptions or queued updating subscriptions, you can't disable the option for the publication (although subscriptions don't need to use it). To disable the option, delete the publication and create a new one.  
  
-   Republishing data isn't supported.  
  
-   Replication adds the **msrepl_tran_version** column to published tables for tracking purposes. Because of this extra column, include a column list in all **INSERT** statements.  
  
-   To make schema changes on a table in a publication that supports updating subscriptions, stop all activity on the table at the Publisher and Subscribers, and propagate pending data changes to all nodes before making any schema changes. This process ensures that outstanding transactions don't conflict with the pending schema change. After the schema changes propagate to all nodes, activity can resume on the published tables. For more information, see [Quiesce a Replication Topology &#40;Replication Transact-SQL Programming&#41;](../../../relational-databases/replication/administration/quiesce-a-replication-topology-replication-transact-sql-programming.md).  
  
-   To switch between update modes, the Queue Reader Agent must run at least once after the subscription is initialized (by default, the Queue Reader Agent runs continuously).  
  
-   If the Subscriber database is partitioned horizontally and there are rows in the partition that exist at the Subscriber but not at the Publisher, the Subscriber can't update the preexisting rows. Attempting to update these rows returns an error. Delete the rows from the table and then add them at the Publisher.  

-   Transactional replication with queued updatable subscribers can experience slow performance when unique filtered indexes are used. If a conflict occurs on an article that has unique filtered indexes, conflict resolution leads to extra deletes and inserts on the subscriber for the rows that aren't covered by the unique filtered index.
  
### Updates at the Subscriber  
  
-   Updates at the Subscriber propagate to the Publisher even if a subscription is expired or inactive. Ensure that you drop or reinitialize any such subscriptions.  
  
-   If you use **TIMESTAMP** or **IDENTITY** columns and replicate them as their base data types, don't update values in these columns at the Subscriber.  
  
-   Subscribers can't update or insert **text**, **ntext**, or **image** values because replication change-tracking triggers can't read from the inserted or deleted tables. Similarly, Subscribers can't update or insert **text** or **image** values by using **WRITETEXT** or **UPDATETEXT** because the Publisher overwrites the data. Instead, you could partition the **text** and **image** columns into a separate table and modify both tables within a transaction.  
  
     To update large objects at a Subscriber, use the data types **varchar(max)**, **nvarchar(max)**, and **varbinary(max)** instead of **text**, **ntext**, and **image** data types, respectively.  
  
-   Updates to unique keys (including primary keys) that generate duplicates, such as an update of the form `UPDATE <column> SET <column> =<column>+1`, aren't allowed and are rejected because of a uniqueness violation. Set updates made at the Subscriber propagate by replication as individual **UPDATE** statements for each row affected.  
  
-   If the Subscriber database is partitioned horizontally and the partition contains rows that exist at the Subscriber but not at the Publisher, the Subscriber can't update the pre-existing rows. Attempting to update these rows returns an error. Delete and reinsert these rows.  
  
### User-defined Triggers  
  
-   If the application requires triggers at the Subscriber, define the triggers with the `NOT FOR REPLICATION` option at the Publisher and Subscriber. This option ensures that triggers fire only for the original data change, but not when replication propagates the change.  
  
     Ensure that the user-defined trigger doesn't fire when the replication trigger updates the table. Call the procedure **sp_check_for_sync_trigger** in the body of the user-defined trigger. For more information, see [sp_check_for_sync_trigger &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-check-for-sync-trigger-transact-sql.md).  
  
### Immediate Updating  
  
-   For immediate updating subscriptions, changes at the Subscriber propagate to the Publisher and apply by using Microsoft Distributed Transaction Coordinator (MS DTC). Ensure that MS DTC is installed and configured at the Publisher and Subscriber. For more information, see the Windows documentation.  
  
-   The triggers that immediate updating subscriptions use require a connection to the Publisher to replicate changes.  
  
-   If the publication allows immediate updating subscriptions and an article in the publication has a column filter, you can't filter out non-nullable columns without defaults.  
  
### Queued updating  
  
-   You can't publish tables included in a merge publication as part of a transactional publication that allows queued updating subscriptions.  
  
-   Don't update primary key columns when using queued updating because the primary key serves as a record locator for all queries. When the conflict resolution policy is set to Subscriber Wins, be cautious about updating primary keys. If both the Publisher and the Subscriber update the primary key, the result is two rows with different primary keys.  
  
-   For columns of data type **SQL_VARIANT**: when data is inserted or updated at the Subscriber, the Queue Reader Agent maps it in the following way when it copies data from the Subscriber to the queue:  
  
    -   **BIGINT**, **DECIMAL**, **NUMERIC**, **MONEY**, and **SMALLMONEY** map to **NUMERIC**.  
  
    -   **BINARY** and **VARBINARY** map to **VARBINARY** data.  
  
### Conflict detection and resolution  
  
-   For the Subscriber Wins conflict policy: conflict resolution doesn't support updates to primary key columns.  
  
-   Replication doesn't resolve conflicts due to foreign key constraint failures:  
  
    -   If you don't expect conflicts and data is well partitioned (Subscribers don't update the same rows), use foreign key constraints on the Publisher and Subscribers.  
  
    -   If you expect conflicts: don't use foreign key constraints at the Publisher or Subscriber if you use "Subscriber wins" conflict resolution. Don't use foreign key constraints at the Subscriber if you use "Publisher wins" conflict resolution.  
  
## Related content

- [Peer-to-Peer - Transactional Replication](peer-to-peer-transactional-replication.md)
- [Transactional Replication](transactional-replication.md)
- [Publish Data and Database Objects](../publish/publish-data-and-database-objects.md)
- [Subscribe to Publications](../subscribe-to-publications.md)
