---
title: "Detect and Resolve Merge Replication Conflicts | Microsoft Docs"
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
  - "merge replication conflict resolution [SQL Server replication], about conflict resolution"
  - "default conflict resolver"
  - "conflict resolution [SQL Server replication]"
  - "viewing merge replication conflicts"
  - "resolving merge replication conflicts"
  - "articles [SQL Server replication], conflict resolution"
  - "merge replication conflict resolution [SQL Server replication]"
  - "conflict resolution [SQL Server replication], merge replication"
ms.assetid: 0d033c76-e8c9-4e35-ab95-4d335abb18c1
caps.latest.revision: 37
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Advanced Merge Replication - Resolve Merge Replication Conflicts
  When a Publisher and a Subscriber are connected and synchronization occurs, the Merge Agent detects if there are any conflicts. If conflicts are detected, the Merge Agent uses a conflict resolver to determine which data will be accepted and propagated to other sites.  
  
> [!NOTE]  
>  Although a Subscriber synchronizes with the Publisher, conflicts typically occur between updates made at different Subscribers rather than updates made at a Subscriber and at the Publisher.  
  
 Merge replication offers a variety of methods to detect and resolve conflicts. For most applications, the default method is appropriate:  
  
-   If a conflict occurs between a Publisher and a Subscriber, the Publisher change is kept and the Subscriber change is discarded.  
  
-   If a conflict occurs between two Subscribers using client subscriptions (the default type for pull subscriptions), the change from the first Subscriber to synchronize with the Publisher is kept, and the change from the second Subscriber is discarded. For information about specifying client and server subscriptions, see [Specify a Merge Subscription Type and Conflict Resolution Priority &#40;SQL Server Management Studio&#41;](../../../relational-databases/replication/specify-a-merge-subscription-type-and-conflict-resolution-priority.md).  
  
-   If a conflict occurs between two Subscribers using server subscriptions (the default type for push subscriptions), the change from the Subscriber with the highest priority value is kept, and the change from the second Subscriber is discarded. If the priority values are equal, the change from the first Subscriber to synchronize with the Publisher is kept.  
  
 For more information about conflict detection and resolution for merge replication, see [Advanced Merge Replication Conflict Detection and Resolution](../../../relational-databases/replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md).  
  
## See Also  
 [Article Options for Merge Replication](../../../relational-databases/replication/merge/article-options-for-merge-replication.md)   
 [Subscribe to Publications](../../../relational-databases/replication/subscribe-to-publications.md)  
  
  