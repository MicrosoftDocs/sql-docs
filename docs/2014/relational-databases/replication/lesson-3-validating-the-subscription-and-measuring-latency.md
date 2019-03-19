---
title: "Lesson 3: Validating the Subscription and Measuring Latency | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: replication
ms.topic: conceptual
helpviewer_keywords: 
  - "replication [SQL Server], tutorials"
ms.assetid: 147f7b93-1804-4e0b-9e17-57a51d035b2a
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Lesson 3: Validating the Subscription and Measuring Latency
  In this lesson, you will use tracer tokens to verify that changes are being replicated to the Subscriber and to determine latency, the time it takes for a change made at the Publisher to appear to the Subscriber. This lesson requires that you have completed the previous lesson, [Lesson 2: Creating a Subscription to the Transactional Publication](lesson-2-creating-a-subscription-to-the-transactional-publication.md).  
  
### To insert a tracer token and view information on the token  
  
1.  Connect to the Publisher in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the server node, right-click the **Replication** folder, and then click **Launch Replication Monitor**.  
  
     Replication Monitor launches.  
  
2.  Expand a Publisher group in the left pane, expand the Publisher instance, and then click the **AdvWorksProductTrans** publication.  
  
3.  Click the **Tracer Tokens** tab.  
  
4.  Click **Insert Tracer**.  
  
5.  View elapsed time for the tracer token in the following columns: **Publisher to Distributor**, **Distributor to Subscriber**, **Total Latency**. A value of **Pending** indicates that the token has not reached a given point.  
  
## Next Steps  
 In this lesson, you successfully used tracer tokens to validate that data changes are being replicated from the Publisher to the Subscriber. You can also insert, update, or delete data in the **Product** table at the Publisher and query the **Product** table at the Subscriber to view these changes after they are replicated.  
  
 This completes the Replicating Data Between Continuously Connected Servers tutorial. For a similar tutorial that uses merge replication, see [Tutorial: Replicating Data with Mobile Clients](tutorial-replicating-data-with-mobile-clients.md).  
  
## See Also  
 [Measure Latency and Validate Connections for Transactional Replication](monitor/measure-latency-and-validate-connections-for-transactional-replication.md)  
  
  
