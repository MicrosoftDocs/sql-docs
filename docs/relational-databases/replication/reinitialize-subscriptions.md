---
description: "Reinitialize Subscriptions"
title: "Reinitialize Subscriptions | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "initializing subscriptions [SQL Server replication], reinitializing"
  - "subscriptions [SQL Server replication], reinitializing"
  - "reinitializing subscriptions"
ms.assetid: fb13712b-e7ad-4f1f-b605-4554bad0cb60
author: "MashaMSFT"
ms.author: "mathoma"
monikerRange: "=azuresqldb-mi-current||>=sql-server-2016"
---
# Reinitialize Subscriptions
[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]
  Reinitializing a subscription involves applying a new snapshot of one or more articles to one or more Subscribers: transactional and snapshot replication allow individual articles to be reinitialized; merge replication requires all articles to be reinitialized. Nodes in a peer-to-peer transactional replication topology cannot be reinitialized. If you need to ensure a node has a new copy of the data, restore a backup at the node. Reinitialization occurs for one of two reasons:  
  
-   You explicitly mark a subscription for reinitialization.  
  
-   You perform an action, such as a property change, that requires a reinitialization. For more information about actions that require reinitialization, see [Change Publication and Article Properties](../../relational-databases/replication/publish/change-publication-and-article-properties.md).  
  
 In both cases, the most recent snapshot is applied to the Subscriber the next time the Distribution Agent or the Merge Agent runs. For snapshot and transactional replication, when reinitialization occurs, any changes made at the Subscriber, but not yet synchronized with the Publisher, are overwritten by the application of the new snapshot.  
  
 For merge replication, you can choose to have all the data changes uploaded from the Subscriber before the snapshot is applied. Any pending schema changes from the Publisher are applied at the Subscriber, and then any updates that have been made at the Subscriber since the last synchronization are propagated to the Publisher before the snapshot is reapplied. This behavior is controlled by the **upload_first** and **automatic_reinitialization_policy** properties; for more information, see [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md). If you mark a subscription for reinitialization using SQL Server Management Studio or Replication Monitor, you are given an option in the **Reinitialize Subscription(s)** dialog box to upload changes first.  
  
> [!IMPORTANT]  
>  If you add, drop, or change a parameterized filter in a merge publication, pending changes at the Subscriber cannot be uploaded to the Publisher during reinitialization. If you want to upload pending changes, synchronize all subscriptions before changing the filter.  
  
 If, you specified that no initial snapshot was to be applied to the Subscriber when you created the subscription, and you then mark the subscription for reinitialization, a snapshot is not applied. For more information, see [Initialize a Transactional Subscription Without a Snapshot](../../relational-databases/replication/initialize-a-transactional-subscription-without-a-snapshot.md).  
  
 **To reinitialize a subscription**  
  
 To reinitialize all articles in a subscription, use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], stored procedures or Replication Management Objects (RMO). To reinitialize individual articles in snapshot and transactional publications, you must use stored procedures. For more information, see [Reinitialize a Subscription](../../relational-databases/replication/reinitialize-a-subscription.md).  
  
## See Also  
 [Initialize a Subscription](../../relational-databases/replication/initialize-a-subscription.md)   
 [Subscription Expiration and Deactivation](../../relational-databases/replication/subscription-expiration-and-deactivation.md)  
  
  
