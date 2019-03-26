---
title: "Lesson 3: Synchronizing the Subscription to the Merge Publication | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: 49008384-2c55-4080-a890-9bceb40e4d6d
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 3: Synchronizing the Subscription to the Merge Publication
  In this lesson, you will start the Merge Agent to initialize the subscription using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. You also use this procedure to synchronize with the Publisher. This lesson requires that you have completed the previous lesson, [Lesson 2: Creating a Subscription to the Merge Publication](lesson-2-creating-a-subscription-to-the-merge-publication.md).  
  
### To start synchronization and initialize the subscription  
  
1.  Connect to the Subscriber in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, and then expand the **Replication** folder.  
  
2.  In the **Local Subscriptions** folder, right-click the subscription in the **SalesOrdersReplica** database, and then click **View Synchronization Status**.  
  
3.  Click **Start** to initialize the subscription.  
  
## Next Steps  
 You have successfully run the Merge Agent to start synchronization and initialize the subscription. You can also insert, update, or delete data in the **SalesOrderHeader** or **SalesOrderDetail** tables at the Publisher or Subscriber, repeat this procedure when network connectivity is available to synchronize data between the Publisher and the Subscriber, and then query the **SalesOrderHeader** or **SalesOrderDetail** tables at the other server to view the replicated changes.  
  
 This completes the Replicating Data with Mobile Clients tutorial. For a similar tutorial that uses transactional replication, see [Tutorial: Replicating Data Between Continuously Connected Servers](tutorial-replicating-data-between-continuously-connected-servers.md).  
  
## See Also  
 [Initialize a Subscription with a Snapshot](initialize-a-subscription-with-a-snapshot.md)   
 [Synchronize Data](synchronize-data.md)   
 [Synchronize a Pull Subscription](synchronize-a-pull-subscription.md)  
  
  
