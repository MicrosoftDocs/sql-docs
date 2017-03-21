---
title: "Synchronize Subscriptions (Replication) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "replication"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "synchronization [SQL Server replication], subscriptions"
  - "subscriptions [SQL Server replication], synchronizing"
  - "replication [SQL Server], synchronization"
ms.assetid: cbe13120-8dd9-4309-88dd-07a801c68f5f
caps.latest.revision: 35
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Synchronize Subscriptions (Replication)
  Subscriptions are synchronized by replication agents. The Distribution Agent synchronizes subscriptions to transactional and snapshot publications, and the Merge Agent synchronizes subscriptions to merge publications. You can use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], replication stored procedures, and Replication Management Objects (RMO) to synchronize subscriptions and to control synchronization behavior. The following topics describe how synchronize subscriptions and specify synchronization options.  
  
## In This Section  
  
-   [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)  
  
-   [Create a Snapshot for a Merge Publication with Parameterized Filters](../../relational-databases/replication/create-a-snapshot-for-a-merge-publication-with-parameterized-filters.md)  
  
-   [Enable Initialization with a Backup for Transactional Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/enable-initialization-with-backup-for-transactional-publications.md)  
  
-   [Initialize a Transactional Subscription from a Backup &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/initialize-a-transactional-subscription-from-a-backup.md)  
  
-   [Initialize a Subscription Manually](../../relational-databases/replication/initialize-a-subscription-manually.md)  
  
-   [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md)  
  
-   [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md)  
  
-   [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md)  
  
-   [Execute Scripts Before and After a Snapshot Is Applied &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/execute-scripts-before-and-after-a-snapshot-is-applied.md)  
  
-   [Execute Scripts During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/execute-scripts-during-synchronization-replication-transact-sql-programming.md)  
  
-   [View and Resolve Data Conflicts for Merge Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/view-and-resolve-data-conflicts-for-merge-publications.md)  
  
-   [View Data Conflicts for Transactional Publications &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/view-data-conflicts-for-transactional-publications-sql-server-management-studio.md)  
  
-   [Synchronize a Subscription Using Windows Synchronization Manager &#40;Windows Synchronization Manager&#41;](../../relational-databases/replication/synchronize-a-subscription-using-windows-synchronization-manager.md)  
  
-   [Implement a Business Logic Handler for a Merge Article](../../relational-databases/replication/implement-a-business-logic-handler-for-a-merge-article.md)  
  
-   [Debug a Business Logic Handler &#40;Replication Programming&#41;](../../relational-databases/replication/debug-a-business-logic-handler-replication-programming.md)  
  
-   [Control the Behavior of Triggers and Constraints During Synchronization &#40;Replication Transact-SQL Programming&#41;](../../relational-databases/replication/control-behavior-of-triggers-and-constraints-in-synchronization.md)  
  
-   [Implement a Custom Conflict Resolver for a Merge Article](../../relational-databases/replication/implement-a-custom-conflict-resolver-for-a-merge-article.md)  
  
## See Also  
 [Synchronize Data](../../relational-databases/replication/synchronize-data.md)  
  
  