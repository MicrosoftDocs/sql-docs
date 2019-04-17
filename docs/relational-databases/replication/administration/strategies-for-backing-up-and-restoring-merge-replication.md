---
title: "Strategies for Backing Up and Restoring Merge Replication | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "recovery [SQL Server replication], merge replication"
  - "backups [SQL Server replication], merge replication"
  - "restoring [SQL Server replication], merge replication"
  - "merge replication [SQL Server replication], backup and restore"
ms.assetid: b8ae31c6-d76f-4dd7-8f46-17d023ca3eca
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
---
# Strategies for Backing Up and Restoring Merge Replication
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  For merge replication, back up the following databases regularly:  
  
-   The publication database at the Publisher  
  
-   The distribution database at the Distributor  
  
-   The subscription database at each Subscriber  
  
-   The **master** and **msdb** system databases at the Publisher, Distributor and all Subscribers. These databases should be backed up at the same time as each other and the relevant replication database. For example, back up the **master** and **msdb** databases at the Publisher at the same time you back up the publication database. If the publication database is restored, ensure that the **master** and **msdb** database are consistent with the publication database in terms of replication configuration and settings.  
  
 If you perform regular log backups, any replication-related changes should be captured in the log backups. If you don't perform log backups, a backup should be performed whenever a setting relevant to replication is changed. For more information, see [Common Actions Requiring an Updated Backup](../../../relational-databases/replication/administration/common-actions-requiring-an-updated-backup.md).  
  
 Choose one of the approaches detailed below for backing up and restoring the publication database, and then follow the recommendations listed for the distribution database and subscription databases.  
  
## Backing Up and Restoring the Publication Database  
 There are two approaches to restoring a merge publication database. After restoring the publication database from a backup, you should either:  
  
-   Synchronize the publication database with a subscription database.  
  
-   Reinitialize all subscriptions to the publications in the publication database.  
  
 Using either of these methods ensures that after a restore is performed, the Publisher and all Subscribers are synchronized.  
  
> [!NOTE]  
>  If any tables contain identity columns, you must ensure the correct identity ranges are assigned after a restore. For more information, see [Replicate Identity Columns](../../../relational-databases/replication/publish/replicate-identity-columns.md).  
  
### Synchronizing the Publication Database  
 Synchronizing a publication database with a subscription database allows you to upload from one or more subscription databases those changes made previously in the publication database, but not represented in the restored backup. The data that can be uploaded depends on the way in which a publication is filtered:  
  
-   If the publication is not filtered, you should be able to bring the publication database up-to-date by synchronizing with the most up-to-date Subscriber.  
  
-   If the publication is filtered, you might not be able to bring the publication database up-to-date. Consider a table that is partitioned such that each subscription receives customer data only for a single region: North, East, South, and West. If there is at least one Subscriber for each partition of data, synchronizing with a Subscriber for each partition should bring the publication database up-to-date. However, if data in the West partition, for example, was not replicated to any Subscribers, this data at the Publisher cannot be brought up-to-date.  
  
> [!IMPORTANT]  
>  Synchronizing a publication database with a subscription database can result in published tables being restored to a point in time that is more recent than the point in time of other non-published tables restored from the backup.  
  
 If you synchronize with a Subscriber that is running a version of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] prior to [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], the subscription cannot be anonymous; it must be a client subscription or server subscription (referred to as local subscriptions and global subscriptions in previous releases).  
  
 To synchronize a subscription, see [Synchronize a Push Subscription](../../../relational-databases/replication/synchronize-a-push-subscription.md) and [Synchronize a Pull Subscription](../../../relational-databases/replication/synchronize-a-pull-subscription.md).  
  
### Reinitializing all Subscriptions  
 Reinitializing all subscriptions ensures all Subscribers are in a state consistent with the restored publication database. This approach should be used if you want to return an entire topology to the previous state represented by a given publication database backup. For example, you might want to reinitialize all subscriptions if you are restoring a publication database to an earlier point in time as a mechanism to recover from an erroneously performed batch operation.  
  
 If you choose this option, generate a new snapshot for delivery to reinitialized Subscribers immediately after restoring your publication database.  
  
 To reinitialize a subscription, see [Reinitialize a Subscription](../../../relational-databases/replication/reinitialize-a-subscription.md).  
  
 To create and apply a snapshot, see [Create and Apply the Initial Snapshot](../../../relational-databases/replication/create-and-apply-the-initial-snapshot.md) and [Create a Snapshot for a Merge Publication with Parameterized Filters](../../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md).  
  
## Backing Up and Restoring the Distribution Database  
 With merge replication, the distribution database should be backed up regularly, and can be restored without any special considerations as long as the backup used is no older than the shortest retention period of all publications that use the Distributor. For example, if there are three publications with retention periods of 10, 20, and 30 days, respectively, the backup used to restore the database should not be more than 10 days old. The distribution database has a limited role in merge replication: it does not store any data used in change tracking and it does not provide temporary storage of merge replication changes to be forwarded to subscription databases (as it does in transactional replication).  
  
## Backing Up and Restoring a Subscription Database  
 To ensure successful recovery of a subscription database, Subscribers should synchronize with the Publisher before the subscription database is backed up; they should also synchronize after the subscription database is restored:  
  
-   Synchronizing with the Publisher before a subscription database backup helps ensure that if a Subscriber is restored from backup, the subscription is still within the publication retention period. For example, assume a publication with a retention period of 10 days. The last synchronization was 8 days ago, and now the backup is performed. If the backup is restored 4 days later, the last synchronization will have occurred 12 days ago, which is past the retention period. In this case, you would have to reinitialize the Subscriber. If the Subscriber had synchronized before the backup, the subscription database would be within the retention period.  
  
     The backup should be no older than the shortest retention period of all publications to which the Subscriber subscribes. For example, if a Subscriber subscribes to three publications with retention periods of 10, 20, and 30 days, respectively, the backup used to restore the database should not be more than 10 days old.  
  
-   Synchronizing the subscription database with each of its publications following a restore ensures that the Subscriber is up to date with all changes at the Publisher.  
  
 To set the publication retention period, see [Set the Expiration Period for Subscriptions](../../../relational-databases/replication/publish/set-the-expiration-period-for-subscriptions.md).  
  
 To synchronize a subscription, see [Synchronize a Push Subscription](../../../relational-databases/replication/synchronize-a-push-subscription.md) and [Synchronize a Pull Subscription](../../../relational-databases/replication/synchronize-a-pull-subscription.md).  
  
## Backing Up and Restoring a Republishing Database  
 When a database subscribes to data from a Publisher and in turn publishes that same data to other subscription databases, it is referred to as a republishing database. When restoring a republishing database, follow the guidelines described in "Backing Up and Restoring a Publication Database" and "Backing Up and Restoring a Subscription Database" in this topic.  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [Back Up and Restore Replicated Databases](../../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md)  
  
  
