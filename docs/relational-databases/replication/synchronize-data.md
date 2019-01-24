---
title: "Synchronize Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "synchronization [SQL Server replication], about synchronization"
  - "merge replication synchronization [SQL Server replication]"
  - "scripts [SQL Server replication], synchronization and"
  - "synchronization [SQL Server replication]"
  - "snapshot replication [SQL Server], synchronization"
  - "transactional replication, synchronization"
  - "subscriptions [SQL Server replication], synchronizing"
  - "on demand script execution"
  - "replication [SQL Server], synchronization"
  - "scripts [SQL Server replication]"
ms.assetid: 724802f7-7d69-46d3-a330-bd8aa7f53114
author: "MashaMSFT"
ms.author: "mathoma"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Synchronize Data
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  Synchronizing data refers to the process of data and schema changes being propagated between the Publisher and Subscribers after the initial snapshot has been applied at the Subscriber. Synchronization can occur:  
  
-   Continuously, which is typical for transactional replication.  
  
-   On demand, which is typical for merge replication.  
  
-   On a schedule, which is typical for snapshot replication.  
  
 When a subscription is synchronized, different processes occur based on the type of replication you are using:  
  
-   Snapshot replication. Synchronization means that the Distribution Agent reapplies the snapshot at the Subscriber so that schema and data at the subscription database is consistent with the publication database.  
  
     If modifications to data or schema have been made at the Publisher, a new snapshot must be generated in order to propagate modifications to the Subscriber.  
  
-   Transactional replication. Synchronization means that the Distribution Agent transfers updates, inserts, deletes, and any other changes from the distribution database to the Subscriber.  
  
-   Merge replication. Synchronization means that the Merge Agent uploads changes from the Subscriber to the Publisher and then downloads changes from the Publisher to the Subscriber. Conflicts, if any, are detected and resolved. Data is converged, and the Publisher and all Subscribers eventually end up with the same data values. If conflicts were detected and resolved, work that was committed by some of the users is changed to resolve the conflict according to policies you define.  
  
 Snapshot publications completely refresh the schema at the Subscriber every time synchronization occurs, so all schema changes are applied to the Subscriber. Transactional replication and merge replication also support the most common schema changes. For more information, see [Make Schema Changes on Publication Databases](../../relational-databases/replication/publish/make-schema-changes-on-publication-databases.md).  
  
 To synchronize a push subscription, see [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md).  
  
 To synchronize a pull subscription, see [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md).  
  
 To set synchronization schedules, see [Specify Synchronization Schedules](../../relational-databases/replication/specify-synchronization-schedules.md).  
  
 **To view and resolve synchronization conflicts**  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: [View and Resolve Data Conflicts for Merge Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/view-and-resolve-data-conflicts-for-merge-publications.md)  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]: [View Data Conflicts for Transactional Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/view-data-conflicts-for-transactional-publications-sql-server-management-studio.md)  
  
## Executing Code During Synchronization  
 Replication supports two methods of executing code during synchronization  
  
-   On demand script execution is supported for transactional replication and merge replication. Using on demand script execution you can specify a SQL script to run during synchronization. The script is copied to the Subscriber and executed using **sqlcmd** at the beginning of the synchronization process. The script does not have access to the replicated changes as they are applied to the Subscriber. For more information, see [Execute Scripts During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/execute-scripts-during-synchronization-replication-transact-sql-programming.md).  
  
-   Business logic handlers are supported for merge replication. Using the business logic handler framework you can write a managed code assembly that is called during the merge synchronization process. The assembly includes business logic that can respond to a number of conditions during synchronization: data changes, conflicts, and errors. For more information, see [Execute Business Logic During Merge Synchronization](../../relational-databases/replication/merge/execute-business-logic-during-merge-synchronization.md).  
  
## See Also  
 [Detect and Resolve Merge Replication Conflicts](../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)  
  
  
